using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace StorageApp.Entities
{
    public class UserContainer
    {
        public int UserId { get; set; }

        public User User { get; set; }

        public int ContainerId { get; set; }

        public Container Container { get; set; }
    }
}