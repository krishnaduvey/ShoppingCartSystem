﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShoppingCartSystem.Abstraction.Model;
namespace ShoppingCartSystem.Abstraction
{
    public interface IUserManagement
    {
        bool UserLogin(string username, string password);
        Users UserRegistration();


    }
}
