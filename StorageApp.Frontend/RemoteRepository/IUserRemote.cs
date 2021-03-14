using System.Net;
using System.Threading.Tasks;
using StorageApp.Shared;

namespace StorageApp.Frontend
{
    public interface IUserRemote
    {
        Task<HttpStatusCode> CreateUser(UserCreateDTO user);
        Task<HttpStatusCode> LoginUser(UserLoginDTO user);
    }
}