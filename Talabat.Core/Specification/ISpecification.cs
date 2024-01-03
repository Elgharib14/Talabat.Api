using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entityes;

namespace Talabat.Core.Specification
{
    public interface ISpecification<T> where T : BaseEntity
    {
        // this to where
        public Expression<Func<T,bool>> Criteria { get; set; }
        // this to include
        public List<Expression<Func<T,object>>> Includs { get; set; }
        // this to OrderBy
        public Expression<Func<T, object>> OrderBy { get; set; }
        // this to OrderByDescending
        public Expression<Func<T, object>> OrderByDescending { get; set; }
        public int Skip { get; set; }
        public int Take { get; set; }
        public bool IsPaginationEnabled { get; set; }
    }
}
