using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCartSystem.Abstraction.Model
{
    class LoginDetails
    {
        private int _loginSessionId;
        private bool _loginStatus;
        private string _message;

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
