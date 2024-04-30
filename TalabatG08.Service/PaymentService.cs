using Microsoft.Extensions.Configuration;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TalabatG08.Core;
using TalabatG08.Core.Entites;
using TalabatG08.Core.Entites.Order_Aggregate;
using TalabatG08.Core.Repositories;
using TalabatG08.Core.Services;
using TalabatG08.Core.Specifications.OrderSpec;
using Product = TalabatG08.Core.Entites.Product;

namespace TalabatG08.Service
{
    public class PaymentService : IPaymentService
    {
        private readonly IConfiguration _configuration;
        private readonly IBasketRepository _basketRepository;
        private readonly IUnitOfWork unitOfWork;

        public PaymentService(IConfiguration configuration,IBasketRepository basketRepository,IUnitOfWork unitOfWork)
        {
            this._configuration = configuration;
            this._basketRepository = basketRepository;
            this.unitOfWork = unitOfWork;
        }


        public async Task<CustomerBasket?> CreateOUpdatePaymentIntend(string BasketId)
        {
            //Secret Key
            StripeConfiguration.ApiKey = _configuration["StripeKeys:Secretkey"];
            //GetBusket
            var Busket =await _basketRepository.GetBasketAsync(BasketId);

            if (Busket is null) return null;

            var ShippingPrice = 0M;

            if(Busket.DeliveryMethodId.HasValue)
            {
                var DeliveryMethod = await unitOfWork.Repository<DeliveryMethod>().GetByIdAsync(Busket.DeliveryMethodId.Value);
                ShippingPrice = DeliveryMethod.Cost;
            }
           

            if(Busket.Items.Count>0)
            {
                foreach (var item in Busket.Items)
                {
                    var product = await unitOfWork.Repository<Product>().GetByIdAsync(item.Id);

                    if (item.Price != product.Price)
                    {
                        item.Price = product.Price;
                    }

                }
            }

            // Total = SubTotal + DM.Cost
            var SubTotal = Busket.Items.Sum(item => item.Price * item.Quantity);

            //Create Payment Intent
            var Service = new PaymentIntentService();
            PaymentIntent paymentIntent;
            if(string.IsNullOrEmpty(Busket.PaymentIntendId)) //Create
            {
                var options = new PaymentIntentCreateOptions()
                {
                    Amount = (long)((SubTotal + ShippingPrice) * 100),
                    Currency = "usd",
                    PaymentMethodTypes = new List<string> { "card" }
                };
                paymentIntent =await Service.CreateAsync(options);
                Busket.PaymentIntendId = paymentIntent.Id;
                Busket.ClientSecret = paymentIntent.ClientSecret;   
            }
            else //Update
            {
                var options = new PaymentIntentUpdateOptions()
                {
                    Amount = (long)((SubTotal + ShippingPrice) * 100)
                };
                paymentIntent = await Service.UpdateAsync(Busket.PaymentIntendId, options);
                Busket.PaymentIntendId = paymentIntent.Id;
                Busket.ClientSecret = paymentIntent.ClientSecret;

            }
            await _basketRepository.UpadteBasketAsync(Busket);
            return Busket;


        }

        public async Task<Order> UpdatePaymentIntendToSucceedOrFailed(string PaymentIntendId, bool flag)
        {
            var spec = new OrderWithPaymentIntendSpec(PaymentIntendId);
            var Order = await unitOfWork.Repository<Order>().GetByEntityWithSpecAsync(spec);

            if(flag)
            {
                Order.Status = OrderStatus.PaymentRecived;
            }
            else
            {
                Order.Status = OrderStatus.PaymentFailed;
            }

            unitOfWork.Repository<Order>().Update(Order);
            await unitOfWork.CompleteAsync();

            return Order;

        }
    }
}
