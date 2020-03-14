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
        public static List<Users> users = new List<Users>()
         {
             new Users()
             {
               UserId =1,
               Name="Administrator",
               Password="admin",
               UserName="admin",
               PhoneNumber= "8909910092",
               UserRole=Role.Admin
             }
         };

        public LoginDetails Login(string username, string password)
        {
            //bool isUser=IsUserExists(username);
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

            throw new NotImplementedException();
        }


        public Users UserRegistration(Users user)
        {
            var newUser = new Users()
            {
                UserId = InsertUserId(),
                Name = user.Name,
                Password = user.Password,
                UserName = user.UserName,
                PhoneNumber = user.PhoneNumber,
                UserRole = user.UserRole
            };

            users.Add(newUser);
            return newUser;
        }


        public int InsertUserId()
        {
            var lastAddedUser = users.Last();
            return lastAddedUser.UserId + 1;
        }


        public static Users GetUserInfo(int userId)
        {
            return users.Where(x => x.UserId == userId).First(); 
        }

        public static Users GetUserInfo(string username)
        {

            return users.Where(x => x.UserName == username).FirstOrDefault(); ;
        }
        public static bool IsUserExists(int userId)
        {
            return users.Select(x => x.UserId == userId).First();
        }

        public static bool IsUserExists(string username)
        {
            return users.Select(x => x.UserName == username).First();
        }



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


        public static Dictionary<string, int> ActionOfAdminUser()
        {
            string[] actions = { "AddProduct", "View", "UpdateDetails", "Delete", "CheckAllOrders" };
            Dictionary<string, int> adminActions = new Dictionary<string, int>();
            for (int i = 1; i <= actions.Length; i++)
            {
                adminActions.Add(actions[i], i);
            }

            return adminActions;
        }

        public static void ActionOfGuestUser()
        {
            string[] actions = { "ViewProducts", "ViewProductsInCart", "AddToCart", "DeleteToCart", "ApplyOrder", "CheckOrderStatus" };
            Dictionary<string, int> userActions = new Dictionary<string, int>();
            for (int i = 1; i <= actions.Length; i++)
            {
                userActions.Add(actions[i], i);
            }
        }

        public static bool IsPhoneNumber(string number)
        {
            return Regex.Match(number, @"^(\+[0-9]{9,14})$").Success;
        }

        public static bool IsPasswordCriteriaMatch(string password)
        {
            return Regex.Match(password, @"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{6,}$").Success;
        }

        public static bool IsValidName(string nameCheck)
        {
            var name = nameCheck.Trim();
            if (name.Length <= 0)
                return false;
            if (!Regex.IsMatch(name, @"^[\p{L}\p{M}' \.\-]+$"))
            {
                return false;
            }
            return true;
//            return Regex.Match(name, @"^{1,}$").Success;
        }
    }
}
