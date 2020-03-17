using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShoppingCartSystem.Abstraction.Model;
namespace ShoppingCartSystem.Abstraction
{
    /// <summary>
    /// Interface to manage users.
    /// </summary>
    public interface IUserManagement
    {
        /// <summary>
        /// Login to the applciation.
        /// </summary>
        /// <param name="username">input username as input.</param>
        /// <param name="password">input password as input.</param>
        /// <returns>Login Details object.</returns>
        LoginDetails Login(string username, string password);

        /// <summary>
        /// User registration.
        /// </summary>
        /// <param name="user">Input user information as Users object.</param>
        /// <returns>Return users object.</returns>
        Users UserRegistration(Users user);

        /// <summary>
        /// Delete user.
        /// </summary>
        /// <param name="username">input username as input.</param>
        /// <returns>Returns boolean value.</returns>
        bool DeleteUser(string username);

        /// <summary>
        /// Get User information.
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        Users GetUserInfo(string username);
    }
}
