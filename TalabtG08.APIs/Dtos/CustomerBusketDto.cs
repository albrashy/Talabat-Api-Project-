using System.ComponentModel.DataAnnotations;
using TalabatG08.Core.Entites;

namespace TalabtG08.APIs.Dtos
{
    public class CustomerBusketDto
    {
        //[Required]
        //public string Id { get; set; }
        //public List<BusketItemDto> Items { get; set; }
        [Required]
        public string Id { get; set; } //basket1
        public List<BusketItemDto> Items { get; set; }
        public string? PaymentIntendId { get; set; }

        public string? ClientSecret { get; set; }

        public int? DeliveryMethodId { get; set; }
    }
}
