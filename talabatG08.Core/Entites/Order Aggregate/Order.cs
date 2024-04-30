using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TalabatG08.Core.Entites.Order_Aggregate
{
    public class Order: BaseEntity
    {

        public Order()
        {
            
        }

        public Order(string buyerEmail, Address shippingAddress, DeliveryMethod deliveryMethod, ICollection<OrderItem> items, decimal subtotal,string paymentIntendId)
        {
            BuyerEmail = buyerEmail;
            ShippingAddress = shippingAddress;
            DeliveryMethod = deliveryMethod;
            Items = items;
            Subtotal = subtotal;
            this.PaymentIntendId = paymentIntendId; 
        }

        public string BuyerEmail { get; set; }
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;
        public OrderStatus Status { get; set; } = OrderStatus.Pending;

        public Address ShippingAddress { get; set; }  
        
        public DeliveryMethod DeliveryMethod { get; set; }
        public ICollection<OrderItem> Items { get; set;} = new HashSet<OrderItem>();   

        public decimal Subtotal { get; set; } 
            
        public decimal GetTotal()=> Subtotal + DeliveryMethod.Cost;

        public string PaymentIntendId { get; set; } 
        
    }
}
