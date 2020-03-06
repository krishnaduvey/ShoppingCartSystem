using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ShoppingCartSystem.Abstraction.Model;
using ShoppingCartSystem.Abstraction;


namespace ShoppingCartSystem.Core
{
    public class UserManagementService : IUserManagement
    {
         public static List<Users> users= new List<Users>()
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

            if(userDetails==null)
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
                    LoginStaus=true,
                    Message= "Login successful",
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


        public Users UserRegistration(Users user, int userId)
        {
           

            var newUser = new Users()
            {
                UserId = InsertUserId(),
                Name = user.Name,
                Password = user.Password,
                UserName = user.UserName,
                PhoneNumber = user.PhoneNumber,
                UserRole = Role.User
            };

            users.Add(newUser);
            return newUser;
        }


        public int InsertUserId()
        {
            var lastAddedUser=users.Last();
            return lastAddedUser.UserId+1;
        }


        public static Users GetUserInfo(int userId)
        {
            return users.Where(x => x.UserId == userId).First(); ;
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



        public bool DeleteUser(int userId)
        {
            try
            {
                if (userId == 1)
                {
                    Console.WriteLine("Admin user cann't be deleted.");
                    return false;
                }
                var result = users.Remove(users.Single(x => x.UserId == userId));
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


    }
}
