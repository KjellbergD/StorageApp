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
            var User =  from u in _context.User
                        where u.Id == UserId
                        select new UserDetailsDTO
                        {
                            Id = u.Id,
                            UserName = u.UserName,
                            FullName = u.FullName,
                            ContainerIds = u.UserContainers.Select(uc => uc.ContainerId).ToList()
                        };

            return await User.FirstOrDefaultAsync();
        }

        public async Task<HttpStatusCode> Update(UserUpdateDTO User)
        {
            var entity = await _context.User.FindAsync(User.Id);

            if (entity == null) return NotFound;

            entity.UserName = User.UserName;
            entity.FullName = User.FullName;

            await _context.SaveChangesAsync();

            return NoContent;
        }

        public async Task<HttpStatusCode> Delete(int UserId)
        {
            var entity = await _context.User.FindAsync(UserId);

            if (entity == null) return NotFound;

            _context.User.Remove(entity);
            
            await _context.SaveChangesAsync();

            return NoContent;
        }
    }
}