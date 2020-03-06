using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCartSystem.Abstraction.Model
{
    class LoginDetails
    { 
        private bool _loginStatus;
        public int LoginSessionId
        { get; set; }
        public bool LoginStaus
        { get; set; }
        public string Message
        {
            get
            {
                if (!_loginStatus)
                    return "User login failed.";
                else return "Login successful";
            }
        }


    }
}
