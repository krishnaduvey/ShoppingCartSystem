using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ShoppingCartSystem.Abstraction.Model;
using ShoppingCartSystem.Abstraction;
using System.Text.RegularExpressions;

namespace ShoppingCartSystem.Core
{
    public class UserManagementService : IUserManagement
    {

        /// <summary>
        /// static object to hold users information.
        /// </summary>
        private static readonly List<Users> users = new List<Users>()
         {
             new Users()
             {
               UserId =1,
               Name="Administrator",
               Password="admin",
               UserName="admin",
               Email="krishankant.duvey@soti.net",
               PhoneNumber= "9716941237",
               UserRole=Role.Admin
             }
         };

        /// <summary>
        /// Login to application.
        /// </summary>
        /// <param name="username">Username of a user.</param>
        /// <param name="password">Password of user</param>
        /// <returns> Returns a LoginDetails type object.</returns>
        public LoginDetails Login(string username, string password)
        {
            Users userDetails = GetUserInfo(username);
            if (userDetails == null)
                return new LoginDetails()
                {
                    LoginStaus = false,
                    Message = "Username does not exist.",
                    User = userDetails
                };

            if (username == userDetails.UserName && userDetails.Password == password)
            {
                return new LoginDetails()
                {
                    LoginStaus = true,
                    Message = "Login successful",
                    User = userDetails
                };
            }
            else
            {
                return new LoginDetails()
                {
                    LoginStaus = false,
                    Message = "Login password does not matched.",
                    User = userDetails
                };
            }
        }


        public List<Users> GetUsers() {
            return users;
        }

        /// <summary>
        /// Register a user.
        /// </summary>
        /// <param name="user">User registration information in from of Users type object.</param>
        /// <returns>Returns Users type object.</returns>
        public Users UserRegistration(Users user)
        {
            var newUser = new Users()
            {
                UserId = InsertUserId(),
                Name = user.Name,
                Password = user.Password,
                UserName = user.UserName,
                PhoneNumber = user.PhoneNumber,
                Email=user.Email,
                UserRole = user.UserRole
            };
            try
            {
                users.Add(newUser);
                return newUser;
            }
            catch (Exception )
            {
                return null;
            }

        }

        /// <summary>
        /// Create new userId for each new user.
        /// </summary>
        /// <returns>Returns userId of int type.</returns>
        public int InsertUserId()
        {
            var lastAddedUser = users.Last();
            return lastAddedUser.UserId + 1;
        }

        /// <summary>
        /// Get a user information on the basis of userId.
        /// </summary>
        /// <param name="userId">User registraction information in from of Users type object.</param>
        /// <returns>Returns Users type object which contains user information.</returns>
        public static Users GetUserInfo(int userId)
        {
            return users.Where(x => x.UserId == userId).First(); 
        }

        /// <summary>
        /// Get UserInfo on the basis of username.
        /// </summary>
        /// <param name="username">input username as string type.</param>
        /// <returns>returns a username as string type.</returns>
        public Users GetUserInfo(string username)
        {
            return users.Where(x => x.UserName == username).FirstOrDefault(); ;
        }

        /// <summary>
        /// Check existence of user via user id.
        /// </summary>
        /// <param name="userId">userId as input parameter.</param>
        /// <returns>Returns boolean value of existence of user.</returns>
        public static bool IsUserExists(int userId)
        {
            var isUserExist = users.Any(x => x.UserId == userId);
            return isUserExist;
        }

        /// <summary>
        /// Check existence of user via user username.
        /// </summary>
        /// <param name="username">username as input parameter.</param>
        /// <returns>Returns boolean value of existence of user.</returns>
        public static bool IsUserExists(string username)
        {
            var isUserExist = users.Any(x => x.UserName == username);
            return isUserExist;
        }

        /// <summary>
        /// Delete a existing user.
        /// </summary>
        /// <param name="username"> Input parameter should be a string type.</param>
        /// <returns>Returns a boolean of check user deleted or not.</returns>
        public bool DeleteUser(string username)
        {
            try
            {
                if (username == "admin")
                {
                    Console.WriteLine("Admin user cann't be deleted.");
                    return false;
                }
                var result = users.Remove(users.Single(x => x.UserName == username));
                return result;
            }
            catch (ArgumentNullException argumentNull)
            {
                Console.WriteLine(argumentNull);
                return false;
            }
            catch (InvalidOperationException invalidOperation)
            {
                Console.WriteLine(invalidOperation);
                return false;
            }
        }

        /// <summary>
        /// Validate phone number formate to allow indian standard phone numer.
        /// </summary>
        /// <param name="number"> string type phone number as input parameter.</param>
        /// <returns>Returns boolean value of phone number check.</returns>
        public static bool IsPhoneNumber(string number)
        {
            if (number.Length == 10 && IsContainsDigitOnly(number))
                return true;

                return Regex.Match(number, @"^\s*(?:\+?(\d{1,3}))?[- (]*(\d{3})[- )]*(\d{3})[- ]*(\d{4})(?: *[x/#]{1}(\d+))?\s*$").Success;
        }

        /// <summary>
        /// To check string contains digit only, its a util method
        /// </summary>
        /// <param name="num">String form of a number as input parameter.</param>
        /// <returns>Returns bool value.</returns>
        private static bool IsContainsDigitOnly(string num) {
            foreach (char c in num)
            {
                if (c < '0' || c > '9')
                    return false;
            }

            return true;
        }
        public static bool IsPasswordCriteriaMatch(string password)
        {
            if (password.Length > 6)
                return true;
            else return false;
        }

        public static bool IsValidName(string nameCheck)
        {
            var name = nameCheck.Trim();
            if (name.Length <= 0)
                return false;
            return true;
        }

        /// <summary>
        /// Email format check.
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public static bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

    }
}
