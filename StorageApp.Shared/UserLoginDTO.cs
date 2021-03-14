using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace StorageApp.Shared
{
    public class UserLoginDTO
    {
        public string UserName { get; set; }

        public string Password { get; set; }
    }
}