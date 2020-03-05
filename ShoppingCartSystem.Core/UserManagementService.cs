using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ShoppingCartSystem.Abstraction.Model;
using ShoppingCartSystem.Abstraction;


namespace ShoppingCartSystem.Core
{
    public class UserManagementService : IUserManagement
    {
         public static List<Users> users= new List<Users>()
         {
             new Users()
             {
               UserId =1, 
               Name="Administrator", 
               Password="admin",
               UserName="admin", 
               PhoneNumber= "8909910092", 
               UserRole=Role.Admin
             }
         };

        public bool UserLogin(string username, string password)
        {
            throw new NotImplementedException();
        }


        public Users UserRegistration()
        {
            Console.WriteLine("Enter Name :");
            string name=Console.ReadLine();

            Console.WriteLine("Enter Password :");
            string password = Console.ReadLine();

            Console.WriteLine("Enter Username :");
            string username = Console.ReadLine();

            Console.WriteLine("Enter Phone Number :");
            string phonenumber = Console.ReadLine();

            var newUser = new Users()
            {
                UserId = InsertUserId(),
                Name = name,
                Password = password,
                UserName = username,
                PhoneNumber = phonenumber,
                UserRole = Role.User
            };

            users.Add(newUser);
            return newUser;
        }


        public int InsertUserId()
        {
            var lastAddedUser=users.Last();
            return lastAddedUser.UserId+1;
        }
    }
}
