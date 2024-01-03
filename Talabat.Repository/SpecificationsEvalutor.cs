using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entityes;
using Talabat.Core.Specification;

namespace Talabat.Repository
{
    public static class SpecificationsEvalutor<T> where T : BaseEntity
    {
        // this i did it to give me the staticquery ( context.set<product> )
        //and give me the spec => the condition  
        public static IQueryable<T> GetQuery(IQueryable<T> Inputquery , ISpecification<T> spec)
        {
            var query = Inputquery;
            //this to where(Criteria)
            if (spec.Criteria is not null)
            {
                query = query.Where(spec.Criteria);
            }


            // this to orderbyAsc
            if(spec.OrderBy is not null)
            {
                query = query.OrderBy(spec.OrderBy);
            }

            // this to orderbyDesc
            if (spec.OrderByDescending is not null)
            {
                query = query.OrderByDescending(spec.OrderByDescending);
            }


            if (spec.IsPaginationEnabled)
            {
                query = query.Skip(spec.Skip).Take(spec.Take);
            }

            //this  to Include
            query = spec.Includs.Aggregate(query,(CurrentQuery,includExpression)=>CurrentQuery.Include(includExpression));
            return query;
        }
    }
}
