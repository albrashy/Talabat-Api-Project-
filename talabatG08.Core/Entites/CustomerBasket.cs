using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TalabatG08.Core.Entites
{
    public class CustomerBasket
    {
     
        public string Id { get; set; } //basket1
        public List<BasketItem> Items { get; set; }
        public string PaymentIntendId { get; set; } 

        public string ClientSecret { get; set; }    

        public int? DeliveryMethodId { get; set; }  

        public CustomerBasket(string id)
        {
            Id = id;
        }
    }
}
