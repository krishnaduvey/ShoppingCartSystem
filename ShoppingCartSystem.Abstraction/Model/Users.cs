using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCartSystem.Abstraction.Model
{

    // Role enum helps us to restrict during registration  user and authorization handling means what will going to authorize to which role.
    public enum Role
    {
        Admin = 1,
        Member = 2,
        Guest = 3
    }


    /// <summary>
    /// Register (userId, name, password, email, username, phoneNo) and validate user details
    /// </summary>
    class Users
    {
        //userId, name, password, email, username, phoneNo

        private int _userId; 
        private string _name;
        private string _password;
        private string _username;
        private string _phoneNumber;
        private Role _userRole; //guest, admin.

        public int UserId
        {
            get
            {
                return this._userId;
            }
            set { this._userId = value; }
        }

        public string Name
        {
            get
            {
                return this._name;
            }
            set { this._name = value; }
        }

        public string Password
        {
            get
            {
                return this._password;
            }
            set { this._password = value; }
        }

        public string UserName
        {
            get
            {
                return this._username;
            }
            set { this._username = value; }
        }

        public string PhoneNumber
        {
            get
            { return this._phoneNumber; }
            set { this._phoneNumber = value; }
        }

        public Role UserRole
        {
            get { return this._userRole;}
            set { this._userRole = value; }
        }




    }
}
