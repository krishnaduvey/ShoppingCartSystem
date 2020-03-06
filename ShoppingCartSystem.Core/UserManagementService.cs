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


        public Users UserRegistration(Users user, int userId)
        {
           

            var newUser = new Users()
            {
                UserId = InsertUserId(),
                Name = user.Name,
                Password = user.Password,
                UserName = user.UserName,
                PhoneNumber = user.PhoneNumber,
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


        public static Users GetUserRole(int userId)
        {
            return users.Where(x => x.UserId == userId).First(); ;
        }

        public static bool IsUserExists(int userId)
        {
            return users.Select(x => x.UserId == userId).First();
        }

        public static bool IsUserNameExists(string username)
        {
            return users.Select(x => x.UserName == username).First();
        }

    }
}
