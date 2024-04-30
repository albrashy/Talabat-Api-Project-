using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TalabatG08.Core.Entites
{
    public class Product :BaseEntity
    {
        public string Name { get; set; }    
        public string Description { get; set; }

        public decimal Price { get; set; }

        public string PictureUrl { get; set; }  

        public int ProductTypeId { get; set; }

        public ProductType productType { get; set; }

        public int ProductBrandId { get; set; }
        public ProductBrand productBrand { get; set; }

       

        




        // "Name": "White Chocolate Mocha",
        //"Description": "Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Proin pharetra nonummy pede. Mauris et orci.",
        //"Price": 300,
        //"PictureUrl": "images/products/sb-core2.png",
        //"ProductTypeId": 3,
        //"ProductBrandId": 2

    }
}
