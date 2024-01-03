using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entityes;
using Talabat.Core.Specification;

namespace Talabat.Core.Repository
{
    public interface IGenaricRepo<T> where T : BaseEntity
    {
        public Task<IReadOnlyList<T>> GetAll();   
        public Task<T> GetbyId(int id);

        public Task<IReadOnlyList<T>> GetWithSpecAll(ISpecification<T> spec);
        public Task<T> GetbyIdWithSpec(ISpecification<T> spec);

        Task<int> GetCountWithSpec(ISpecification<T> spec);

        Task Add(T entity);

        void update (T entity); 
        void Delete(T entity);

    }
}
