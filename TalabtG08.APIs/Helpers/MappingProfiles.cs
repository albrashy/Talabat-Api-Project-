using AutoMapper;
using System.Net.Sockets;
using TalabatG08.Core.Entites;
using TalabatG08.Core.Entites.Identity;
using TalabatG08.Core.Entites.Order_Aggregate;
using TalabtG08.APIs.Dtos;
using OrderAddress = TalabatG08.Core.Entites.Order_Aggregate.Address;
using IdentityAddress = TalabatG08.Core.Entites.Identity.Address;

namespace TalabtG08.APIs.Helpers
{
    public class MappingProfiles :Profile
    {
        public MappingProfiles()
        {

            CreateMap<Product, ProductToReturnDto>().ForMember(PD => PD.productBrand, P => P.MapFrom(p => p.productBrand.Name))
                                                    .ForMember(PD => PD.productType, P => P.MapFrom(p => p.productType.Name))
                                                    .ForMember(PD => PD.PictureUrl, o => o.MapFrom<ProductPictureUrlResolver>());

            CreateMap<IdentityAddress, AddressDto>().ReverseMap();

            CreateMap<CustomerBusketDto, CustomerBasket>().ReverseMap();

            CreateMap<BusketItemDto,BasketItem>().ReverseMap();
            CreateMap<AddressDto, OrderAddress>();

            CreateMap<Order, OrderToReturnDto>()
                .ForMember(OR => OR.DeliveryMethod, o => o.MapFrom(O => O.DeliveryMethod.ShortName))
                .ForMember(OR => OR.DeliveryMethod, o => o.MapFrom(O => O.DeliveryMethod.Cost));

            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(des => des.ProductId, o => o.MapFrom(sou => sou.Product.ProductId))
                .ForMember(des => des.ProductName, o => o.MapFrom(sou => sou.Product.ProductName))
                .ForMember(des => des.PictureUrl, o => o.MapFrom(sou => sou.Product.PictureUrl))
                .ForMember(des => des.PictureUrl, o => o.MapFrom<OrderItemPictureUrlResolver>());

          //  CreateMap<CustomerBasket, CustomerBusketDto>().ReverseMap();




        }


    }
}
