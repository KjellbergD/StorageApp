using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace StorageApp.Shared
{
    public class UserDTO
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public string FullName { get; set; }
    }
}