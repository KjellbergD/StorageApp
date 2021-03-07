using System.Collections.Generic;

namespace StorageApp.Shared
{
    public class ContainerUpdateDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int? ItemId { get; set; }

        public int? NewUser { get; set; }
    }
}