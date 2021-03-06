using System.Diagnostics.SymbolStore;
using System.Runtime.Serialization;
using System.Net.Sockets;
using System.Collections.Generic;
using System;
using StorageApp.Entities;
using StorageApp.Shared;
using static System.Net.HttpStatusCode;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Net;
using System.Linq;

namespace StorageApp.Models
{
    public class UserRepository : IUserRepository
    {
        private readonly IStorageContext _context;

        public UserRepository(IStorageContext context)
        {
            _context = context;
        }

        public async Task<(int affectedRows, int id)> Create(UserCreateDTO User)
        {
            var entity = new User
            {
                UserName = User.UserName,
                FullName = User.FullName
            };

            _context.User.Add(entity);

            var affectedRows = await _context.SaveChangesAsync();

            return (affectedRows, entity.Id);
        }

        public async Task<UserDetailsDTO> Read(int UserId)
        {
            var User =  from h in _context.User
                        where h.Id == UserId
                        select new UserDetailsDTO
                        {
                            
                        };

            return await User.FirstOrDefaultAsync();
        }

        public async Task<HttpStatusCode> Update(UserUpdateDTO User)
        {
            throw new NotImplementedException();
        }

        public async Task<HttpStatusCode> Delete(int UserId)
        {
            throw new NotImplementedException();
        }
    }
}