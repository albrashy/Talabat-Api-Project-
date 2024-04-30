using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TalabatG08.Core.Entites;

namespace TalabatG08.Core.Specifications
{
    public class ProductForFilterationForCountSpecifications : BaseSpecification<Product>
    {
        public ProductForFilterationForCountSpecifications(ProductSpecParams productSpec) : base(p =>
         (string.IsNullOrEmpty(productSpec.Search) || p.Name.ToLower().Contains(productSpec.Search)) &&
         (!productSpec.BrandId.HasValue || p.ProductBrandId == productSpec.BrandId) &&
         (!productSpec.TypeId.HasValue || p.ProductTypeId == productSpec.TypeId))
                            
        {


        }
    }
}
