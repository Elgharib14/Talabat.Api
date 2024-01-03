using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entityes;
using Talabat.Core.Entityes.OrderAggregate;
using Talabat.Core.Repository;

namespace Talabat.Core
{
    public interface IUnitOfWork : IAsyncDisposable 
    {

        IGenaricRepo<T> Repository<T>() where T : BaseEntity;
        Task<int> Complete();



    }
}
