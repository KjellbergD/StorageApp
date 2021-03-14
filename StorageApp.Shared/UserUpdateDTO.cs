using System.Collections.Generic;

namespace StorageApp.Shared
{
    public class UserUpdateDTO
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public int? NewContainer { get; set; }
    }
}