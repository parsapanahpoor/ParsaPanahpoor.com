using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataContext.UnitOfWork
{
    public  interface IUnitOfWork<TContext> : IDisposable where TContext : DbContext
    {
        void SaveChangesDB();
        Task<int> SaveChangesDBAsync();
    }


}
