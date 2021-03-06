using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace StorageApp.Entities
{
    public interface IStorageContext
    {
        DbSet<User> User { get; }
        DbSet<Container> Container { get; set; }
        DbSet<UserContainer> UserContainer { get; set; }
        DbSet<Item> Item { get; set; }
        
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}