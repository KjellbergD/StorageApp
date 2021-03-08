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
    public class ContainerRepository : IContainerRepository
    {
        private readonly IStorageContext _context;

        public ContainerRepository(IStorageContext context)
        {
            _context = context;
        }

        public async Task<(int affectedRows, int id)> Create(ContainerCreateDTO Container)
        {
            var entity = new Container
            {
                Name = Container.Name,
                UserContainers = new List<UserContainer>(),
                Items = new List<Item>()
            };

            var UserContainer = new UserContainer { UserId = Container.UserId, User = _context.User.Find(Container.UserId), ContainerId = entity.Id, Container = entity };

            entity.UserContainers.Add(UserContainer);

            _context.Container.Add(entity);

            var affectedRows = await _context.SaveChangesAsync();

            return (affectedRows, entity.Id);
        }

        public async Task<ContainerDetailsDTO> Read(int ContainerId)
        {
            var Container = from c in _context.Container
                            where c.Id == ContainerId
                            select new ContainerDetailsDTO
                            {
                                Id = c.Id,
                                Name = c.Name,
                                Items = c.Items.Select(i => new ItemDTO {
                                    Id = i.Id,
                                    Name = i.Name,
                                    Note = i.Note
                                }).ToList()
                            };

            return await Container.FirstOrDefaultAsync();
        }

        public async Task<HttpStatusCode> Update(ContainerUpdateDTO Container)
        {
            var entity = await _context.Container.FindAsync(Container.Id);

            if (entity == null) return NotFound;

            if(Container.Name != null) entity.Name = Container.Name;
            if(Container.ItemId != null) entity.Items.Add(await _context.Item.FindAsync(Container.ItemId));
            if(Container.NewUser != null) entity.UserContainers.Add(await _context.UserContainer.FindAsync(Container.NewUser, entity.Id)); //maybe add to User aswell

            await _context.SaveChangesAsync();

            return NoContent;
        }

        public async Task<HttpStatusCode> Delete(int ContainerId)
        {
            var entity = await _context.Container.FindAsync(ContainerId);

            if (entity == null) return NotFound;

            var toRemove =  from i in _context.Item
                            where i.ContainerId == ContainerId
                            select i;
            
            _context.Item.RemoveRange(toRemove);

            _context.Container.Remove(entity);
            
            await _context.SaveChangesAsync();

            return NoContent;
        }
    }
}