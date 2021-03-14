using System.Net;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using System.Linq;
using StorageApp.Shared;

namespace StorageApp.Models
{
    public interface IUserRepository
    {
        Task<(int affectedRows, int id)> Create(UserCreateDTO User);
        Task<UserDetailsDTO> Read(int UserId);
        Task<UserAuthDTO> ReadUserLogin(string username);
        Task<HttpStatusCode> Update(UserUpdateDTO User);
        Task<HttpStatusCode> Delete(int UserId);
    }
}