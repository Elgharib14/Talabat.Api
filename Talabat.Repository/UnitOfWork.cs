using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core;
using Talabat.Core.Entityes;
using Talabat.Core.Entityes.OrderAggregate;
using Talabat.Core.Repository;
using Talabat.Repository.Context;

namespace Talabat.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbcontext dbcontext;

        //private Dictionary<string, GenaricRepo<BaseEntity>> repository;
        private Hashtable repository;


        public UnitOfWork(AppDbcontext dbcontext )
        {
            this.dbcontext = dbcontext;
            repository = new Hashtable();
        }

        public IGenaricRepo<T> Repository<T>() where T : BaseEntity
        {
           var type = typeof( T ).Name;  
            if(!repository.ContainsKey(type))
            {
                var repo = new GenaricRepo<T>(dbcontext) ;
                repository.Add(type, repo);
            }

            return repository[type] as IGenaricRepo<T>;
        }

        public async Task<int> Complete()
        => await dbcontext.SaveChangesAsync();
        

        public async ValueTask DisposeAsync()
        => await dbcontext.DisposeAsync();
        
    }
}
