using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShoppingCartSystem.Abstraction.Model;
namespace ShoppingCartSystem.Abstraction
{
    interface IUserManagement
    {
        bool UserLogin(string username, string password);
        int UserRegistration(Users user);

    }
}
