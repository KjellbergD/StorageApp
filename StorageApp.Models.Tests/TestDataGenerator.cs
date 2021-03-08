using System.Collections.Generic;
using System;
using StorageApp.Entities;

namespace StorageApp.Models.Test
{
    public static class TestDataGenerator
    {
        public static void GenerateTestData(this StorageContext context)
        {
            var newUser = new User { UserName = "Dk", FullName = "DanielKjellberg", UserContainers = new List<UserContainer>() };

            var newContainer = new Container { Name = "Fryser", UserContainers = new List<UserContainer>(), Items = new List<Item>() };

            var newUserContainer = new UserContainer { User = newUser, UserId = 1, ContainerId = 1, Container = newContainer };

            var newItem = new Item { Name = "kylling", Container = newContainer, ContainerId = 1, Note = "2stk" };
            var newItem2 = new Item { Name = "bær", Note = "4poser" };

            newUser.UserContainers.Add(newUserContainer);
            newContainer.UserContainers.Add(newUserContainer);
            newContainer.Items.Add(newItem);

            context.UserContainer.Add(newUserContainer);
            context.User.Add(newUser);
            context.Container.Add(newContainer);
            context.Item.AddRange(newItem, newItem2);
            context.SaveChanges();
        }
    }
}