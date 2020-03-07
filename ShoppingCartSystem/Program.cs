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
        private static int userFilterKey;
        private static LoginDetails loginInfo;
        private static bool isUserLoggedIn = false;
        private static Role? userType;



        public static void ActionListAsPerRole(Role? role)
        {
            if (role.ToString() == "Admin")
            {
                Console.WriteLine("Check below list of actions that \"Admin\" can perform.");
                Console.WriteLine("Enter number to perform operation :");
                Console.WriteLine("[1] To View Products");
                Console.WriteLine("[2] To Add New Product");
                Console.WriteLine("[3] To Update Product");
                Console.WriteLine("[4] To Delete Product");
                Console.WriteLine("[5] To Check all Orders");
                Console.WriteLine("[6] To Remove user.");
                Console.WriteLine("[7] To All Users information.");
            }
            else if (role.ToString() == "User")
            {
                Console.WriteLine("Check below list of actions that \"Admin\" can perform.");
                Console.WriteLine("Enter number to perform operation :");
                Console.WriteLine("[1] To View Products");
                Console.WriteLine("[2] To Add Product To Cart");
                Console.WriteLine("[3] To View Product in Cart");
                Console.WriteLine("[4] To Delete Product from Cart");
                Console.WriteLine("[5] To Check Product availablity on application");
                Console.WriteLine("[6] To Remove Product from cart");
                Console.WriteLine("[7] To Buy Order");
                Console.WriteLine("[8] To Cancel Order");
            }
        }

        static void Main(string[] args)
        {

        Start:
          /*  do
            {
           */
                do
                {
                    userType = AskForUserType();

                } while (userType == null);

                do
                {
                    bool isUserWantToLogin;
                    do
                    {
                        isUserWantToLogin = DoYouWantToLoginToTheApplication(userType);
                        if (!isUserWantToLogin)
                        {
                            ShowProducts();
                        }
                        else
                        {
                            if (DoYouHaveAnAccount())
                                isUserLoggedIn=LoginToApplication();
                            else {                                
                                int userId=CreateNewUser();
                                isUserLoggedIn = LoginToApplication();
                            }

                        }
                    } while (!isUserWantToLogin);


                } while (!loginInfo.LoginStaus);
            //} while (!isUserLoggedIn);


            while (isUserLoggedIn)
            {
                ShowProducts();
                ActionListAsPerRole(loginInfo.User.UserRole);
                Console.ReadLine();
            }

                goto Start;




            AskForLogin();

            #region List of Available Products
            //ToDoBeforeLogin();
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
                    Description = "Hello World",
                    Name = "item updated"
                }
                );

            #endregion
            //ToDoBeforeLogin();



            Console.WriteLine("Are you a New User? :Press 1");
            Console.WriteLine("Are you already Registered ? :Press 2");
            Console.WriteLine("Press 1");

            Console.ReadKey();

        }

        public static void ShowProducts()
        {
            new ProductManagementService().ShowAllProducts();
        }

        public static Role? GetUserType(char input)
        {
            int inputNumber = 0;
            Role userRole;
            try
            {
                inputNumber = Convert.ToInt32(input.ToString());
                if (inputNumber == 1)
                    return Role.Admin;
                else if (inputNumber == 2)
                    return Role.User;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Please provide valid input type?");
                return null;
            }
            Console.WriteLine("Please provide valid input type?");
            return null;
        }
        public static Role? AskForUserType()
        {
            Console.WriteLine("Are you a User or Admin?");
            Console.WriteLine("Press 1 for Admin?");
            Console.WriteLine("Press 2 for User?");
            Role? typeOfUser = new Role();
            typeOfUser = GetUserType(Console.ReadKey().KeyChar);
            Console.WriteLine();
            return typeOfUser;

        }


        public static bool DoYouHaveAnAccount()
        {
            try
            {
                Console.WriteLine();
                Console.WriteLine("Do you have a account:");
                Console.WriteLine("Please input => 'Y' to Continue to login page and 'N' to create your account");
                char input = Console.ReadKey().KeyChar;
                Console.WriteLine();
                var inputChar = Char.ToUpper(input);
                if (inputChar == 'Y')
                    return true;
                else if (inputChar == 'N')
                    return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.WriteLine();
            Console.WriteLine("Please provide valid input.");
            Console.WriteLine();
            return DoYouHaveAnAccount();
        }

        public static int CreateNewUser()
        {
            Users user=AddUser();
            return user.UserId;
        }

        public static bool DoYouWantToLoginToTheApplication(Role? userType)
        {
            try
            {
                Console.WriteLine();
                Console.WriteLine("Hi {0},", userType.ToString());
                Console.WriteLine("Do you want to Login Or Register with us :");
                Console.WriteLine("Please input Y/N : Y to Continue and N to exit application.");
                char input = Console.ReadKey().KeyChar;
                Console.WriteLine();
                var inputChar = Char.ToUpper(input);
                if (inputChar == 'Y')
                    return true;
                else if (inputChar == 'N')
                {
                    //Environment.Exit(0);
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return DoYouWantToLoginToTheApplication(null);
        }

        public static bool LoginToApplication()
        {
            Console.WriteLine();
            Console.WriteLine("Enter username :");
            string username = Console.ReadLine();
            Console.WriteLine("Enter password :");
            string password = Console.ReadLine();
            loginInfo = new UserManagementService().Login(username, password);
            if (loginInfo.LoginStaus)
            {
                Console.WriteLine("Welcome Mr. {0}", loginInfo.User.Name);
            }
            else
            {
                Console.WriteLine(loginInfo.Message);
            }
            return loginInfo.LoginStaus;
        }

        public static bool AskForLogin()
        {
            Console.WriteLine("");

            Console.WriteLine("Are you a User or Admin?");
            Console.WriteLine("Press 1 for Admin?");
            Console.WriteLine("Press 2 for User?");



            if (userFilterKey == 1 || userFilterKey == 2)
            {
            }
            else if (userFilterKey == 3)
            {

            }

            return false;
        }

        public static void ToDoAfterSuccessfulLoginByUser()
        {

        }

        public static void ToDoAfterSuccessfulLoginByAdmin()
        {

        }

        public static Role addRoleToUser() {
            Console.WriteLine("Enter 1 for Admin and 2 For User role");

            int roleValue = 0;
            int.TryParse(Console.ReadLine(), out roleValue);
            Console.WriteLine();

            if ((roleValue - 1) < 0 || (roleValue - 1) > 1) {
                Console.WriteLine("Try with valid input.");
                addRoleToUser();
            }

            if ((roleValue - 1) == 0 && (userType.ToString()=="User")) {
                Console.WriteLine("You dont have permission to create admin user. Please select valid role:");
                addRoleToUser();
            }

            Role role = (Role)(roleValue - 1);
            return role;
        }

        public static Users AddUser()
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
                PhoneNumber = phonenumber,
                UserRole = addRoleToUser()

            };
            return new UserManagementService().UserRegistration(newUser);
        }

        public static int AddNewProducts()
        {
            Console.WriteLine("Enter Product Name :");
            string name = Console.ReadLine();

            Console.WriteLine("Enter Description of product :");
            string description = Console.ReadLine();

            int price;
            int quantity;

            price = ValidateInputPrice(out price);

            quantity = ValidateInputPrice(out quantity);

            var newProduct = new Products()
            {
                Name = name,
                Description = description,
                Quantity = quantity,
                Price = price,
            };
            return new ProductManagementService().AddNewProduct(newProduct);
        }

        public static int ValidateInputPrice(out int input)
        {
            string userInput;
            do
            {
                Console.Write(string.Format("Enter input : #{0}: ", 1));
                userInput = Console.ReadLine();
            } while (!int.TryParse(userInput, out input));
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
                Console.WriteLine();
                userFilterKey = int.Parse(input + "");
                if (userFilterKey == 1 || userFilterKey == 2 || userFilterKey == 3)
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

        public static void ActionOfAdminUser()
        {
            string[] actions = { "Login", "Add", "View", "UpdateDetails", "Delete", "CheckAllOrders" };
        }

        public static void ActionOfGuestUser()
        {
            string[] actions = { "Login", "Registeration", "ViewProducts", "ViewProductsInCart", "AddToCart", "DeleteToCart", "ApplyOrder", "CheckOrderStatus" };
        }

    }
}



/*
 
 */
