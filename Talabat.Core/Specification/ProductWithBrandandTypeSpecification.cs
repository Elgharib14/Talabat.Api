using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Talabat.Core.Entityes;

namespace Talabat.Core.Specification
{
    public class ProductWithBrandandTypeSpecification : BaseSpecification<Product>
    {
        //this used for getallProduct
        public ProductWithBrandandTypeSpecification(ProductSpecParams specParams) :base(
            p=>
            (string.IsNullOrEmpty(specParams.Search) ||  p.Name.ToLower().Contains(specParams.Search))&&
            (!specParams.BrandId.HasValue || p.ProductBrandId == specParams.BrandId.Value) &&
            (!specParams.TypeId.HasValue || p.ProductTypeId == specParams.TypeId.Value)
            )
        {
            Includs.Add(p => p.ProductBrand);
            Includs.Add(p => p.ProductType);

            // here to mack the Sorting default by Name
            AddOrderBy(p => p.Name);


            // here we chose the Sortin on price
            if(!string.IsNullOrEmpty(specParams.Sort))
            {
                switch (specParams.Sort)
                {
                    case "PriceAsc" :
                        AddOrderBy(p => p.Price);
                            break;
                    case "PriceDesc":
                        AddOrderByDesc(p => p.Price);
                        break;
                    default:
                        AddOrderBy(p => p.Name);
                        break;
                }
            }

            // the first param to chose weich 10product will Take 
            ApplyPagination(specParams.PageSize * (specParams.PageIndex-1) , specParams.PageSize);
        }

       

        //this used for getProductByID
        public ProductWithBrandandTypeSpecification(int id):base(p=>p.Id == id)
        {
            Includs.Add(p => p.ProductBrand);
            Includs.Add(p => p.ProductType);
        }


    }
}
