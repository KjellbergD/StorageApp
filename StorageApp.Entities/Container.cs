using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace StorageApp.Entities
{
    public class Container
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<UserContainer> UserContainers { get; set; }

        public ICollection<Item> Items { get; set; }
    }
}