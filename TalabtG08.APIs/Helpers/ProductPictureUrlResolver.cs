using AutoMapper;
using TalabatG08.Core.Entites;
using TalabtG08.APIs.Dtos;

namespace TalabtG08.APIs.Helpers
{
    public class ProductPictureUrlResolver : IValueResolver<Product, ProductToReturnDto, string>
    {
        private readonly IConfiguration configuration;

        public ProductPictureUrlResolver(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public string Resolve(Product source, ProductToReturnDto destination, string destMember, ResolutionContext context)
        {
            if(!string.IsNullOrEmpty(source.PictureUrl))
            {
                return $"{configuration["ApiBaseUrl"]}{source.PictureUrl}";
            }

            return string.Empty;
        }
    }
}
