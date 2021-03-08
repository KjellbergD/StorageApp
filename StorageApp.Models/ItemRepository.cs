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
    public class ItemRepository : IItemRepository
    {
        private readonly IStorageContext _context;

        public ItemRepository(IStorageContext context)
        {
            _context = context;
        }

        public async Task<(int affectedRows, int id)> Create(ItemCreateDTO Item)
        {
            var Containing = _context.Container.Find(Item.ContainerId);

            var entity = new Item
            {
                Name = Item.Name,
                Note = Item.Note,
                ContainerId = Item.ContainerId,
                Container = Containing
            };

            Containing.Items.Add(entity);

            _context.Item.Add(entity);

            var affectedRows = await _context.SaveChangesAsync();

            return (affectedRows, entity.Id);
        }

        public async Task<ItemDetailsDTO> Read(int ItemId)
        {
            var Item =  from i in _context.Item
                        where i.Id == ItemId
                        select new ItemDetailsDTO
                        {
                            Id = i.Id,
                            Name = i.Name,
                            Note = i.Note
                        };

            return await Item.FirstOrDefaultAsync();
        }

        public async Task<HttpStatusCode> Update(ItemUpdateDTO Item)
        {
            var entity = await _context.Item.FindAsync(Item.Id);

            if (entity == null) return NotFound;

            if(Item.Name != null) entity.Name = Item.Name;
            if(Item.Note != null) entity.Note = Item.Note;

            await _context.SaveChangesAsync();

            return NoContent;
        }

        public async Task<HttpStatusCode> Delete(int ItemId)
        {
            var entity = await _context.Item.FindAsync(ItemId);

            if (entity == null) return NotFound;

            entity.Container.Items.Remove(entity);

            _context.Item.Remove(entity);
            
            await _context.SaveChangesAsync();

            return NoContent;
        }
    }
}