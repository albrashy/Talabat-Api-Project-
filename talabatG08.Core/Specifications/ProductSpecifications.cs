using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TalabatG08.Core.Entites;

namespace TalabatG08.Core.Specifications
{
    public class ProductSpecifications:BaseSpecification<Product>
    {
        //where(p.productbrand == productbrand && p.productType == productType)
        //where(true && p.productType == productType)
        //where(p.productbrand == productbrand && true)
        //where(true && true)

        public ProductSpecifications(ProductSpecParams productSpec) :base(p=>
        (string.IsNullOrEmpty(productSpec.Search) || p.Name.ToLower().Contains(productSpec.Search) )&&
        (!productSpec.BrandId.HasValue ||p.ProductBrandId == productSpec.BrandId ) &&
        (!productSpec.TypeId.HasValue || p.ProductTypeId==productSpec.TypeId)
        )
        {
            Includes.Add(p => p.productBrand);
            Includes.Add(p => p.productType);
            if(!string.IsNullOrEmpty(productSpec.Sort))
            {
               switch (productSpec.Sort)
               {
                    case "PriceAsc":
                        AddOrderby(p=>p.Price);
                        break;
                    case "PriceDesc":
                        AddOrderbyDesend(p => p.Price);
                        break;
                    default:
                         AddOrderby(p => p.Name);
                         break;

               }
            }

            Applypagination(productSpec.PageSize* (productSpec.PageIndex-1) , productSpec.PageSize);

        }

        public ProductSpecifications(int id):base(p=>p.Id==id)
        {
            Includes.Add(p => p.productBrand);
            Includes.Add(p => p.productType);
        }

    }
}
