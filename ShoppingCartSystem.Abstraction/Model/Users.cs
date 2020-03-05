
namespace ShoppingCartSystem.Abstraction.Model
{

    // Role enum helps us to restrict during registration  user and authorization handling means what will going to authorize to which role.
    public enum Role
    {
        Admin = 1,
        User = 2
    }

    /// <summary>
    /// Register (userId, name, password, email, username, phoneNo) and validate user details
    /// </summary>
    public class Users
    {
        //userId, name, password, email, username, phoneNo

        /*private int _userId; 
        private string _name;
        private string _password;
        private string _username;
        private string _phoneNumber;
        private Role _userRole;*/ //guest, admin, member

        public int UserId { get; set; }

        public string Name { get; set; }

        public string Password { get; set; }

        public string UserName { get; set; }

        public string PhoneNumber { get; set; }

        public Role UserRole { get; set; }




    }
}
