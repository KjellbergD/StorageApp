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
                Password = User.Password
            };
            Console.WriteLine(3);

            _context.User.Add(entity);

            Console.WriteLine(4);
            var affectedRows = await _context.SaveChangesAsync();
            Console.WriteLine(5);
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
                            ContainerIds = u.UserContainers.Select(uc => uc.ContainerId).ToList()
                        };

            return await User.FirstOrDefaultAsync();
        }

        public async Task<UserAuthDTO> ReadUserLogin(string username)
        {
            return await _context.User.Where(u => u.UserName == username)
                                      .Select(u => new UserAuthDTO 
                                      {   
                                          Id = u.Id,
                                          UserName = u.UserName, 
                                          Password = u.Password
                                      })
                                      .FirstOrDefaultAsync();    
        }

        public async Task<HttpStatusCode> Update(UserUpdateDTO User)
        {
            var entity = await _context.User.FindAsync(User.Id);

            if (entity == null) return NotFound;

            if(User.UserName != null) entity.UserName = User.UserName;
            if(User.NewContainer != null) entity.UserContainers.Add(await _context.UserContainer.FindAsync(entity.Id, User.NewContainer));

            await _context.SaveChangesAsync();

            return OK;
        }

        public async Task<HttpStatusCode> Delete(int UserId)
        {
            var entity = await _context.User.FindAsync(UserId);

            if (entity == null) return NotFound;

            var toRemove =  from uc in _context.UserContainer
                            where uc.UserId == UserId
                            select uc;
            
            _context.UserContainer.RemoveRange(toRemove);

            _context.User.Remove(entity);
            
            await _context.SaveChangesAsync();

            return OK;
        }
    }
}