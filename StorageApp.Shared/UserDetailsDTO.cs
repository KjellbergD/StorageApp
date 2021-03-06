using System.Collections.Generic;

namespace StorageApp.Shared
{
    public class UserDetailsDTO
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public string FullName { get; set; }

        public ICollection<ContainerDTO> Containers { get; set; }
    }
}