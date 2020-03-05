using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShoppingCartSystem.Core;
using ShoppingCartSystem.Abstraction.Model;
using ShoppingCartSystem.Abstraction;

namespace ShoppingCartSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            // Flow of application :
            // 1. Product Information which is visible to every one.
            // 2. Buy/ exit app
            // 3. Login ( Enter username and password ) else click as new user
            // 4. Registered then show products name price and description 
            // 5. If new user then do registration as per option fill form accodingly and validate it

            IUserManagement user= new UserManagementService();
            user.UserRegistration();
            user.UserRegistration();
           

            int userAdded=UserManagementService.users.Count();
            new UserManagementService().UserRegistration();
            userAdded = UserManagementService.users.Count();
            /*  switch (@enum)
              {
  //                new UserManagementService().AddUser();
          }*/

        }
    }
}
