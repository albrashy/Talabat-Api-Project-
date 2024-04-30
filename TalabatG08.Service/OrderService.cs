using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;
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
using Order = TalabatG08.Core.Entites.Order_Aggregate.Order;

namespace TalabatG08.Service
{
    public class OrderService : IOrderService
    {
        private readonly IBasketRepository basketRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPaymentService _paymentService;

        public OrderService(IBasketRepository basketRepository ,IUnitOfWork unitOfWork,IPaymentService paymentService)
        {
            this.basketRepository = basketRepository;
            this._unitOfWork = unitOfWork;
            this._paymentService = paymentService;
        }
        public async Task<Order?> CreateOrderAsync(string BuyerEmail, string BasketId, int DeliveryMethodId, Address ShippingAddress)
        {
            //1.Get Basket From Basket Repo
            var busket = await basketRepository.GetBasketAsync(BasketId);

            //2.Get Selected Items at Basket From Product Repo

            var OrderItems = new List<OrderItem>();

            if(busket?.Items.Count > 0)
            {
               foreach(var item in busket.Items)
               {
                    var product =await _unitOfWork.Repository<Product>().GetByIdAsync(item.Id);
                    var ProductItemOrder = new ProductItemOrder(product.Id,product.Name,product.PictureUrl);
                    var orderitem = new OrderItem(ProductItemOrder, product.Price, item.Quantity);
                    OrderItems.Add(orderitem);
               }
            
            }

            //3.Calculate SubTotal
            var subTotal = OrderItems.Sum(OI=>OI.Quantity * OI.Price);  
            //4.Get Delivery Method From DeliveryMethod Repo
            var DeliveryMethod = await _unitOfWork.Repository<DeliveryMethod>().GetByIdAsync(DeliveryMethodId);
            //5.Create Order

            //var spec =new orders
            var spec = new OrderWithPaymentIntendSpec(busket.PaymentIntendId);

            var ExOrder =await _unitOfWork.Repository<Order>().GetByEntityWithSpecAsync(spec);

            if(ExOrder is not  null)
            {
                 _unitOfWork.Repository<Order>().Delete(ExOrder);
                await _paymentService.CreateOUpdatePaymentIntend(BasketId);

            }

            var Order = new Order(BuyerEmail, ShippingAddress, DeliveryMethod, OrderItems, subTotal,busket.PaymentIntendId);

            //6.Add Order Locally
            await _unitOfWork.Repository<Order>().Add(Order);

            //7.Save Order To Database[ToDo]
            var result =await _unitOfWork.CompleteAsync();
             if(result <=0) { return null; }
           


            return Order;


        }

        public async Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsService()
        {
            var DeliveryMethods = await _unitOfWork.Repository<DeliveryMethod>().GetAllAsync();
            return DeliveryMethods; 
        }

        public Task<Order> GetOrderByIdForSpecificUserAsync(string BuyerEmail, int OrdersId)
        {
            var spec = new OrderSpecifications(OrdersId, BuyerEmail);
            var orderForSpecUser =  _unitOfWork.Repository<Order>().GetByEntityWithSpecAsync(spec);
            return orderForSpecUser;
        }

        public async Task<IReadOnlyList<Order>> GetOrdersForSpecificUserAsync(string BuyerEmail)
        {
            var orderSpec = new OrderSpecifications(BuyerEmail);
            var orders =await _unitOfWork.Repository<Order>().GetAllWithSpecAsync(orderSpec);

            return orders;  
                

        }

       

    }
}
