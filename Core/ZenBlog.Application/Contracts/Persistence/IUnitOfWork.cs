namespace ZenBlog.Application.Contracts.Persistence
{
    public interface IUnitOfWork
    {
        Task SaveChangesAsync();
    }
}
