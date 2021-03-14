using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace StorageApp.Shared
{
    public class UserAuthDTO : UserDTO
    {
        public string Password { get; set; }
    }
}