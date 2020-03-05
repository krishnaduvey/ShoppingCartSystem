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
        {
            set { this._loginSessionId = value; }
        }
        public bool LoginStaus
        {
            set { this._loginStatus = value; }
        }
        public string Message
        {
            get
            {
                if (!_loginStatus)
                    return "Login Failed.";
                else return "Login Successfully Done.";
            }
        }


    }
}
