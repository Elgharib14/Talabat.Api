using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entityes;

namespace Talabat.Core.Specification
{
    public class BaseSpecification<T> : ISpecification<T> where T : BaseEntity
    {
        public Expression<Func<T, bool>> Criteria { get; set; }
        public List<Expression<Func<T, object>>> Includs { get ; set ; } =  new List<Expression<Func<T, object>>>();
        public Expression<Func<T, object>> OrderBy { get ; set; }
        public Expression<Func<T, object>> OrderByDescending { get; set ; }
        public int Skip { get ; set; }
        public int Take { get ; set; }
        public bool IsPaginationEnabled { get; set; }

        public BaseSpecification()
        {
           
        }
        public BaseSpecification(Expression<Func<T, bool>> CriteriaExpression)
        {
            Criteria = CriteriaExpression;
           
        }

        public void AddOrderBy(Expression<Func<T, object>> OrderByExpiration)
        {
            OrderBy = OrderByExpiration;
        }

        public void AddOrderByDesc(Expression<Func<T, object>> OrderByDesExpiration)
        {
            OrderByDescending = OrderByDesExpiration;
        }

        public void ApplyPagination( int skip , int take)
        {
            IsPaginationEnabled = true;
            Skip = skip;
            Take = take;
        }

    }
}
