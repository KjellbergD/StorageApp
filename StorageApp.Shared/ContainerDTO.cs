using System.Collections.Generic;

namespace StorageApp.Shared
{
    public class ContainerDTO
    {
        public int Id { get; set; }
        
        public string Name { get; set; }

        public ICollection<ItemDTO> Items { get; set; }
    }
}