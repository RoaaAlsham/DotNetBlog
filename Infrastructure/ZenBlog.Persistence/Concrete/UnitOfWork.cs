

using ZenBlog.Application.Contracts.Persistence;
using ZenBlog.Persistence.Context;

namespace ZenBlog.Persistence.Concrete
{
    internal class UnitOfWork(AppDbContext _context) : IUnitOfWork
    {
        public async Task<bool> SaveChangesAsync()
        {
         
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
