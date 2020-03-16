
namespace ShoppingCartSystem.Abstraction.Model
{

    // Role enum helps us to restrict during registration  user and authorization handling means what will going to authorize to which role.
    public enum Role
    {
        Admin,
        User
    }

    /// <summary>
    /// Register (userId, name, password, email, username, phoneNo) and validate user details
    /// </summary>
    public class Users
    {
        public int UserId { get; set; }

        public string Name { get; set; }

        public string Password { get; set; }

        public string UserName { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }
        public Role? UserRole { get; set; }
    }
}
