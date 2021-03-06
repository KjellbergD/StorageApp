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
            throw new NotImplementedException();
        }

        public async Task<ContainerDetailsDTO> Read(int ContainerId)
        {
            throw new NotImplementedException();
        }

        public async Task<HttpStatusCode> Update(ContainerUpdateDTO Container)
        {
            throw new NotImplementedException();
        }

        public async Task<HttpStatusCode> Delete(int ContainerId)
        {
            throw new NotImplementedException();
        }
    }
}