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
            throw new NotImplementedException();
        }

        public async Task<ItemDetailsDTO> Read(int ItemId)
        {
            throw new NotImplementedException();
        }

        public async Task<HttpStatusCode> Update(ItemUpdateDTO Item)
        {
            throw new NotImplementedException();
        }

        public async Task<HttpStatusCode> Delete(int ItemId)
        {
            throw new NotImplementedException();
        }
    }
}