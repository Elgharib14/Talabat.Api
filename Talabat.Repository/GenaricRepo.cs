using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entityes;
using Talabat.Core.Repository;
using Talabat.Core.Specification;
using Talabat.Repository.Context;

namespace Talabat.Repository
{
    public class GenaricRepo<T> : IGenaricRepo<T> where T : BaseEntity
    {
        private readonly AppDbcontext context;

        public GenaricRepo(AppDbcontext context)
        {
            this.context = context;
        }

        public async Task Add(T entity)
        {
            await context.Set<T>().AddAsync(entity);
        }

        public void Delete(T entity)
        {
            context.Set<T>().Remove(entity);
        }

        public async Task<IReadOnlyList<T>> GetAll()
        {
            return await context.Set<T>().ToListAsync();
        }

        public async Task<T> GetbyId(int id)
        {
            //return await context.Set<T>().Where(x=> x.Id == id).FirstOrDefaultAsync();    
            return await context.Set<T>().FindAsync(id);
        }

        public async Task<T> GetbyIdWithSpec(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).FirstOrDefaultAsync();
        }

        public async Task<int> GetCountWithSpec(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).CountAsync();
        }

        public async Task<IReadOnlyList<T>> GetWithSpecAll(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).ToListAsync();
        }

        public void update(T entity)
        {
            context.Set<T>().Update(entity);
        }

        private IQueryable<T> ApplySpecification(ISpecification<T> spec)
        {
            return SpecificationsEvalutor<T>.GetQuery(context.Set<T>(), spec);
        }


    }
}
