using System.Net;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using System.Linq;
using StorageApp.Shared;

namespace StorageApp.Models
{
    public interface IContainerRepository
    {
        Task<(int affectedRows, int id)> Create(ContainerCreateDTO Container);
        Task<ContainerDetailsDTO> Read(int ContainerId);
        Task<HttpStatusCode> Update(ContainerUpdateDTO Container);
        Task<HttpStatusCode> Delete(int ContainerId);
    }
}