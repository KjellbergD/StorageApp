using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace StorageApp.Entities
{
    public class Item
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public Container Container { get; set; }

        public int? ContainerId { get; set; }

        public string Note { get; set; }
    }
}