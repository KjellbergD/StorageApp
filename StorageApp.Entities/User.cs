using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace StorageApp.Entities
{
    public class User
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public string FullName { get; set; }

        public ICollection<UserContainer> UserContainers { get; set; }
    }
}