using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entityes;

namespace Talabat.Core.Specification
{
    public class productwithfiltrationforcont : BaseSpecification<Product>
    {
        public productwithfiltrationforcont(ProductSpecParams specParams) : base(
            p =>
            (string.IsNullOrEmpty(specParams.Search) || p.Name.ToLower().Contains(specParams.Search)) &&
            (!specParams.BrandId.HasValue || p.ProductBrandId == specParams.BrandId.Value) &&
            (!specParams.TypeId.HasValue || p.ProductTypeId == specParams.TypeId.Value)
            )
        {
            
        }
    }
}
