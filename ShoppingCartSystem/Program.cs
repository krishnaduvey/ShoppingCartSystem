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
        private static bool isAdded = false;
        private static Dictionary<string, int> userAction = new Dictionary<string, int>();


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
                bool isUserWantToLogin=false;
                do
                {
                    isUserWantToLogin = DoYouWantToLoginToTheApplication(userType);
                    if (!isUserWantToLogin)
                    {
                        Environment.Exit(0);
                    }
                    else
                    {
                        if (DoYouHaveAnAccount())
                            isUserLoggedIn = LoginToApplication();
                        else
                        {
                            int userId = CreateNewUser();
                            isUserLoggedIn = LoginToApplication();
                        }

                    }
                } while (!isUserWantToLogin);


            } while (!loginInfo.LoginStaus);
            //} while (!isUserLoggedIn);

            if (isUserLoggedIn) {
                ShowProducts();
                Console.WriteLine();

                ActionsToBePerform();
                Console.WriteLine();
            }
            while (isUserLoggedIn)
            {                
                isUserLoggedIn=PerformAction();               
            }

            goto Start;

        }

        public static int EnterNumber()
        {
            string input = Console.ReadLine();
            int num = -1;
            if (!int.TryParse(input, out num))
            {
                Console.WriteLine("Please input valide number...\n");
                EnterNumber();
            }
            else
            {
                return num;
            }
            return 0;
        }


        public static void ShowAllOrders()
        {           
            new OrderManagementService().ViewAllOrders(loginInfo.User.UserId);
        }

        public static void ShowAllUsers() {
            Console.WriteLine("-------------------------------------");
            Console.WriteLine("|\t\tRegistered User :");
            Console.WriteLine("-------------------------------------");
            var users=UserManagementService.users;
            foreach (var user in users) {
                Console.WriteLine("|\t\tName :{0}\t|",user.Name);
            }
        }

        public static Users AddNewUser()
        {
            Console.WriteLine("-------------------------------------");
            Console.WriteLine("|\t\tUser Registration : ");
            Console.WriteLine("-------------------------------------");

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

            int price, quantity;

            Console.WriteLine("-------------------------------------");
            Console.WriteLine("|\t\tAdd Product : |");
            Console.WriteLine("-------------------------------------");

            Console.WriteLine("Enter Product Name :");
            string name = Console.ReadLine();

            Console.WriteLine("Enter Description of product :");
            string description = Console.ReadLine();

            Console.WriteLine("Enter Price of product :");
            price = ValidateInputPrice(out price);
            Console.WriteLine("Enter Quantity of product :");
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

        public static void DisplayAllProducts()
        {
            Console.WriteLine("-------------------------------------");
            Console.WriteLine("|\t\tProducts : |");
            Console.WriteLine("-------------------------------------");
            new ProductManagementService().GetAllAvailableProducts();
        }

        public static void RemoveProduct()
        {
           
            bool isEvent=new ProductManagementService().DeleteProduct(EnterNumber());
            if (isEvent)
            {
                Console.WriteLine("Product Successfully removed.");
            }
        }

        public static void ModifyProduct()
        {

            Console.WriteLine("-------------------------------------");
            Console.WriteLine("|\t\tUpdate Product : ");
            Console.WriteLine("-------------------------------------");

            Console.WriteLine("Enter Product Id :");
            Console.WriteLine("Not implemented yet : it will update dummy values to the product that you input");
          
            new ProductManagementService().UpdateProductInfo(
                               new Products()
                               {
                                   ProductId = EnterNumber(),
                                   Price = 40,
                                   Quantity = 111,
                                   Description = "Hello World",
                                   Name = "item updated"
                               }
                              );
        }

        public static int provideValidQuantity(int productId) {
            int quantity = 0;
            Console.WriteLine("Enter Quantity of product :");
            quantity = ValidateInputPrice(out quantity);
            int leftQuantityOfProduct = ProductManagementService.GetProductQuantity(productId);
            if (leftQuantityOfProduct > quantity)
                return quantity;
            else
            {
                Console.WriteLine();
                Console.WriteLine("This much quanity nto availale.", quantity);
                Console.WriteLine("Only few {0} products left in stock. Try again with valid quantity.",leftQuantityOfProduct);
                Console.WriteLine();
            }
            return provideValidQuantity(productId);
        }

        public static void AddProductToCart()
        {

            Console.WriteLine("-------------------------------------");
            Console.WriteLine("|\t\tAdd Product To Cart : ");
            Console.WriteLine("-------------------------------------");

            Console.WriteLine("Enter Product ID :");
            string productIdInString = Console.ReadLine();
            int productId = int.Parse(productIdInString);


            

            var productToAdd = new Products()
            {
                ProductId= productId,
                Quantity= provideValidQuantity(productId)
            };
            new OrderManagementService().AddProductToCart(new Products(),0);
        }

        public static void ViewProductInCart() {
            new OrderManagementService().ViewProductsInCart(loginInfo.User.UserId);
        }

        public static void RemoveProductFromCart() {
            Console.WriteLine("-------------------------------------");
            Console.WriteLine("|\t\tRemove Product from Cart : ");
            Console.WriteLine("-------------------------------------");
        }

        public static void ApplyOrder() {
            Console.WriteLine("-------------------------------------");
            Console.WriteLine("|\t\tApply Order : ");
            Console.WriteLine("-------------------------------------");
            new OrderManagementService().ApplyOrder(loginInfo.User.UserId);
        }

        public static void CancelOrder() {
            Console.WriteLine("-------------------------------------");
            Console.WriteLine("|\t\tCancel Order : ");
            Console.WriteLine("-------------------------------------");
            int orderId = 0;
            new OrderManagementService().CancelOrder(orderId,loginInfo.User.UserId);
        }


        public static void ModifyProductInCart() {
            Console.WriteLine("Not implemented yet.");
            //new OrderManagementService().UpdateCart();
        }


        public static bool PerformAction() 
        {

            bool isUserWantToLogout = false;
            if (loginInfo.User.UserRole.ToString() == "Admin")
            {
                Console.WriteLine("Input action number :");
                isUserWantToLogout=AdminActions(EnterNumber());
            }
            else
            {
                Console.WriteLine("Input action number :");
                isUserWantToLogout=UserActions(EnterNumber());
            }
            isUserWantToLogout=DoYouWantToContinue();
            return isUserWantToLogout;
       }


        public static void RemoveUser() { 
        
        }


        public static bool UserActions(int value) {            
            switch (value)
            {
                case 1:
                    DisplayAllProducts();
                    break;
                case 2:
                    AddProductToCart();
                    break;
                case 3:
                    ViewProductInCart();
                    break;
                case 4:
                    RemoveProductFromCart();
                    break;
                case 5:
                    ApplyOrder();
                    break;
                case 6:
                    CancelOrder();
                    break;
                case 7:
                    ShowAllOrders();
                    break;
                case 8:
                    ModifyProductInCart();
                    break;
                case 9:
                    Console.WriteLine(" Thank you Mr. {0}", loginInfo.User.Name);
                    return isUserLoggedIn = false;
                    
                default:
                    Console.WriteLine("Please provide valid input. and Try Again.. ");
                    ActionsToBePerform();
                    PerformAction();
                    break;
            }
            return true;
        }

        public static bool AdminActions(int input)
        {
            switch (input)
            {
                case 1:
                    DisplayAllProducts();                   
                    break;
                case 2:
                    AddNewProducts();
                    break;
                case 3:
                    ModifyProduct();
                    break;
                case 4:
                    RemoveProduct();
                    break;
                case 5:
                    ShowAllOrders();
                    break;
                case 6:
                    AddNewUser();
                    break;
                case 7:
                    RemoveUser();
                    break;
                case 8:
                    ShowAllUsers();
                    break;
                case 9:
                    Console.WriteLine(" Thank you Mr. {0}", loginInfo.User.Name);
                    return isUserLoggedIn = false;
                default:
                    Console.WriteLine("Please provide valid input. and Try Again.. ");
                    PerformAction();
                    break;
            }
            return true;
        }
        public static void ActionsToBePerform()
        {
            Console.WriteLine("-------------------------------------");
            Console.WriteLine("Actions List : ");
            Console.WriteLine("-------------------------------------");

            var user = UserManagementService.GetUserInfo(loginInfo.User.UserId);
            var role = user.UserRole.GetValueOrDefault();
            if (role.ToString() == "Admin")
            {
                Console.WriteLine("Check below list of actions that \"" + role.ToString() + "\" can perform.");
                Console.WriteLine("Enter number to perform operation :");
                Console.WriteLine("[1] To View Products");// productservice
                Console.WriteLine("[2] To Add New Product");// productservice
                Console.WriteLine("[3] To Update Product");// productservice
                Console.WriteLine("[4] To Delete Product");// productservice
                Console.WriteLine("[5] To Check all Orders");// orderservice
                Console.WriteLine("[6] To Add new User");// orderservice
                Console.WriteLine("[7] To Remove user.");// userservice
                Console.WriteLine("[8] To All Users information.");//userservice   
                Console.WriteLine("[9] To Logout");
            }
            else if (role.ToString() == "User")
            {
                Console.WriteLine("Check below list of actions that \"" + role.ToString() + "\" can perform.");
                Console.WriteLine("Enter number to perform operation :");
                Console.WriteLine("[1] To show all Products");//productservice
                Console.WriteLine("[2] To Add Product To Cart");//orderservice
                Console.WriteLine("[3] To View Product in Cart");//orderservice
                Console.WriteLine("[4] To Delete Product from Cart");//orderservice
                Console.WriteLine("[5] To Buy Order");//orderservice
                Console.WriteLine("[6] To Cancel Order");//orderservice
                Console.WriteLine("[9] To Logout");
            }
        }

        public static void ShowProducts()
        {
            new ProductManagementService().GetAllAvailableProducts();
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
            Users user = AddNewUser();
            return user.UserId;
        }

        public static bool DoYouWantToContinue()
        {
            try
            {
                Console.WriteLine();
                Console.WriteLine("Do you want to continue with us :");
                Console.WriteLine("Please input Y/N : Y to Continue and N to Logout");
                char input = Console.ReadKey().KeyChar;
                Console.WriteLine();
                var inputChar = Char.ToUpper(input);
                if (inputChar == 'Y')
                {
                    return true;
                }
                else if (inputChar == 'N')
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Please provide valid input... and try again");
            }
            return DoYouWantToContinue();

        }

        public static bool DoYouWantToLoginToTheApplication(Role? userType)
        {
            try
            {
                Console.WriteLine();
                Console.WriteLine("Hi {0},", userType.ToString());
                Console.WriteLine("Do you want to Login Or Register with us :");
                Console.WriteLine("Please input Y/N : Y to Continue and N to Exit");
                char input = Console.ReadKey().KeyChar;
                Console.WriteLine();
                var inputChar = Char.ToUpper(input);
                if (inputChar == 'Y')
                    return true;
                else if (inputChar == 'N')
                {
                    Environment.Exit(0);                    
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
            Console.WriteLine("Login to Application :");
            Console.WriteLine("Enter username :");
            string username = Console.ReadLine();
            Console.WriteLine("Enter password :");
            string password = Console.ReadLine();
            loginInfo = new UserManagementService().Login(username, password);
            if (loginInfo.LoginStaus)
            {
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("-------------------------------------");
                Console.WriteLine("Welcome Mr. {0}", loginInfo.User.Name);
                Console.WriteLine("-------------------------------------");
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

      

        public static Role addRoleToUser()
        {
            Console.WriteLine("Enter 1 for Admin and 2 For User role");

            int roleValue = 0;
            int.TryParse(Console.ReadLine(), out roleValue);
            Console.WriteLine();

            if ((roleValue - 1) < 0 || (roleValue - 1) > 1)
            {
                Console.WriteLine("Try again with valid number.");
                addRoleToUser();
            }

            if ((roleValue - 1) == 0 && (userType.ToString() == "User"))
            {
                Console.WriteLine("You dont have permission to create admin user. Please select valid role:");
                addRoleToUser();
            }

            Role role = (Role)(roleValue - 1);
            return role;
        }






        public static int ValidateInputPrice(out int input)
        {
            string userInput;
            do
            {
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


      


    }
}



/*
 
 */
