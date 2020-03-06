using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShoppingCartSystem.Core;
using ShoppingCartSystem.Abstraction.Model;
using ShoppingCartSystem.Abstraction;

namespace ShoppingCartSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            // Flow of application :
            // 1. Product Information which is visible to every one.
            // 2. Buy/ exit app
            // 3. Login ( Enter username and password ) else click as new user
            // 4. Registered then show products name price and description 
            // 5. If new user then do registration as per option fill form accodingly and validate it


            /*  switch (@enum)
              {
  //                new UserManagementService().AddUser();
          }*/

            ProductManagementService.ProductDetails();

            Console.WriteLine("Are you a User or Admin?");
            Console.WriteLine("Press 1 for Admin?");
            Console.WriteLine("Press 2 for User?");
            int enteredUser=int.Parse(Console.ReadKey());

            Console.WriteLine("Are you a New User? :Press 1");
            Console.WriteLine("Are you already Registered ? :Press 2");
            Console.WriteLine("Press 1");

            Console.ReadKey();


/*            string x = "";
            switch (x)
            {
                case "": break;
                
            }
*/
        }



        public void AddUser(int userId)
        {
            Console.WriteLine("Enter Name :");
            string name = Console.ReadLine();

            Console.WriteLine("Enter Password :");
            string password = Console.ReadLine();

            
            string username = InsertUserName();

            Console.WriteLine("Enter Phone Number :");
            string phonenumber = Console.ReadLine();

            var newUser = new Users()
            {
                Name = name,
                Password = password,
                UserName = username,
                PhoneNumber =phonenumber
            };
        }

        public static string InsertUserName()
        {
            Console.WriteLine("Enter Username :");
            string username = Console.ReadLine();
            if(!UserManagementService.IsUserNameExists(username))
            return username;
            else {
                Console.WriteLine("User already exists,Please add different username.");
            }

            return InsertUserName();
        }
    }
}



/*
 
 */