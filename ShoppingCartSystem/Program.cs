using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShoppingCartSystem.Core;
using ShoppingCartSystem.Abstraction.Model;

namespace ShoppingCartSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            new UserManagementService().AddUser();
            new UserManagementService().AddUser();
            int userAdded=UserManagementService.users.Count();
            new UserManagementService().AddUser();
            userAdded = UserManagementService.users.Count();
            /*  switch (@enum)
              {
  //                new UserManagementService().AddUser();
          }*/

        }
    }
}
