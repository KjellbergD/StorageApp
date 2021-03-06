using System.Net;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using System.Linq;
using StorageApp.Shared;

namespace StorageApp.Models
{
    public interface IItemRepository
    {
        Task<(int affectedRows, int id)> Create(ItemCreateDTO Item);
        Task<ItemDetailsDTO> Read(int ItemId);
        Task<HttpStatusCode> Update(ItemUpdateDTO Item);
        Task<HttpStatusCode> Delete(int ItemId);
    }
}