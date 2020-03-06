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
         static int userFilterKey;
         static LoginDetails loginInfo;
        static void Main(string[] args)
        {
            // Flow of application :
            // 1. Product Information which is visible to every one.
            // 2. Buy/ exit app
            // 3. Login ( Enter username and password ) else click as new user
            // 4. Registered then show products name price and description 
            // 5. If new user then do registration as per option fill form accodingly and validate it


           
            AskForLogin();

            #region List of Available Products
            ToDoBeforeLogin();
            #endregion

            #region Add Product
            int productId = AddNewProducts();
            #endregion
            
            #region Delete Product
            new ProductManagementService().DeleteProduct(productId);
            #endregion

            #region Update Product Detail
            new ProductManagementService().UpdateProductInfo(
                new Products()
                {
                    ProductId = productId,
                    Price = 40,
                    Quantity = 111,
                    Description="Hello World",
                    Name = "item updated"
                }
                );
            #endregion
            ToDoBeforeLogin();
           


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

        public static void ToDoBeforeLogin()
        {
            ProductManagementService.ProductDetails();
        }

        public static void AskForLogin()
        {
            Console.WriteLine("Are you a User or Admin?");
            Console.WriteLine("Press 1 for Admin?");
            Console.WriteLine("Press 2 for User?");
             
            while (!InputKeyValidation())
            {
                Console.WriteLine("Wrong");
            }

            if (userFilterKey == 1 || userFilterKey == 2)
            {
                Console.WriteLine();
                Console.WriteLine("Enter username :");
                string username = Console.ReadLine();
                Console.WriteLine("Enter password :");
                string password = Console.ReadLine();
                loginInfo=new UserManagementService().Login(username, password);
                if (loginInfo.LoginStaus)
                {
                    Console.WriteLine("Welcome Mr. {0}",loginInfo.User.Name);
                }
                else
                {
                    Console.WriteLine(loginInfo.Message);
                }

                Console.WriteLine(loginInfo.Message);
            }
            else if (userFilterKey == 3)
            {

            }

        }

        public static void ToDoAfterSuccessfulLoginByUser()
        {

        }

        public static void ToDoAfterSuccessfulLoginByAdmin()
        {

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
                PhoneNumber = phonenumber
            };
        }

        public static  int AddNewProducts()
        {
            Console.WriteLine("Enter Product Name :");
            string name = Console.ReadLine();

            Console.WriteLine("Enter Description of product :");
            string description = Console.ReadLine();
            
            int price;
            int quantity;

            price=ValidateInputPrice(out price);

            quantity = ValidateInputPrice( out quantity);

            var newProduct = new Products()
            { 
                Name = name,
                Description = description,
                Quantity = quantity,
                Price = price,
            };
            return new ProductManagementService().AddNewProduct(newProduct);
        }

        public static  int ValidateInputPrice( out int input)
        {
            string userInput;
            do
            {
                Console.Write(string.Format("Enter input : #{0}: ", 1));
                userInput = Console.ReadLine();
            } while (!int.TryParse(userInput,out input));
            return input;


        }


        public static string InsertUserName()
        {
            Console.WriteLine("Enter Username :");
            string username = Console.ReadLine();
            if (!UserManagementService.IsUserExists(username))
                return username;
            else
            {
                Console.WriteLine("User already exists,Please add different username.");
            }
            return InsertUserName();
        }

        public static bool InputKeyValidation()
        {
            Console.WriteLine();
            try
            {
                int number = 0;
                char input = Console.ReadKey().KeyChar;
                userFilterKey = int.Parse(input + "");
                if(userFilterKey == 1 || userFilterKey == 2 || userFilterKey==3)
                return true;
                else
                {
                    Console.WriteLine();
                    Console.WriteLine("Please check your input and Try Again...");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine();
                Console.WriteLine("Please check your input and Try Again...");
                return false;
            }

        }
    }
}



/*
 
 */
