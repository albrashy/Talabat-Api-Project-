namespace TalabtG08.APIs.Dtos
{
    public class OrderDto
    {
        public string BasketId { get; set; }
        public int deliveryMethodId { get; set; }
        public AddressDto shipToAddress { get; set; } 
    }
}
