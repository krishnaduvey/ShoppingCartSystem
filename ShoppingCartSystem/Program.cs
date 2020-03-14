using System;
using System.Collections.Generic;
using ShoppingCartSystem.Core;
using ShoppingCartSystem.Abstraction.Model;

namespace ShoppingCartSystem
{
    class Program
    {
        private static LoginDetails loginInfo;
        private static bool isUserLoggedIn = false;
        private static Role? userType;


        public static void Main(string[] args)
        {
        Start:
            Console.WriteLine("-------------------------------------------------");
            Console.WriteLine("|\t\tShopping Cart System\t\t|");
            Console.WriteLine("-------------------------------------------------");
            Console.WriteLine();
            do
            {
                userType = AskForUserType();
            } while (userType == null);

            do
            {
                do
                {
                    if (userType.ToString() == "Admin")
                    {
                        isUserLoggedIn = LoginToApplication();
                    }
                    else
                    {
                        if (DoYouHaveAnAccount())
                        {
                            isUserLoggedIn = LoginToApplication();
                        }
                        else
                        {
                            Users user = AddNewUser();
                            isUserLoggedIn = LoginToApplication();
                        }
                    }

                } while (!isUserLoggedIn);


            } while (!loginInfo.LoginStaus);





            if (isUserLoggedIn)
            {
                ShowProducts();
                Console.WriteLine();

                ActionsToBePerform();
                Console.WriteLine();
            }
            do
            {
                isUserLoggedIn = PerformAction();
            } while (isUserLoggedIn);

            goto Start;

        }

        private static bool PerformAction()
        {

            bool isUserWantToLogout = false;
            if (loginInfo.User.UserRole.ToString() == "Admin")
            {
                Console.WriteLine("Input action number :");
                isUserWantToLogout = AdminActions(EnterDigit());
            }
            else
            {
                Console.WriteLine("Input action number :");
                isUserWantToLogout = UserActions(EnterDigit());
            }
            //isUserWantToLogout = DoYouWantToContinue();
            return isUserWantToLogout;
        }

        private static bool AdminActions(int value)
        {
            switch (value)
            {
                case 1:
                    Console.WriteLine();
                    Console.WriteLine("-------------------------------------------");
                    Console.WriteLine("|\t\tProduct Details\t\t   |");
                    Console.WriteLine("-------------------------------------------");
                    Console.WriteLine();
                    DisplayAllProducts();
                    break;
                case 2:
                    Console.WriteLine();
                    Console.WriteLine("---------------------------------------------");
                    Console.WriteLine("|\t\tAdd New Product\t\t |");
                    Console.WriteLine("---------------------------------------------");
                    Console.WriteLine();
                    AddNewProducts();
                    break;
                case 3:
                    Console.WriteLine();
                    Console.WriteLine("-------------------------------------------------");
                    Console.WriteLine("|\t\tUpdate Product\t\t|");
                    Console.WriteLine("-------------------------------------------------");
                    Console.WriteLine();
                    ModifyProduct();
                    break;
                case 4:
                    Console.WriteLine();
                    Console.WriteLine("-------------------------------------------------");
                    Console.WriteLine("|\t\tDelete Product\t\t|");
                    Console.WriteLine("-------------------------------------------------");
                    Console.WriteLine();
                    RemoveProduct();
                    break;
                case 5:
                    Console.WriteLine();
                    Console.WriteLine("-------------------------------------------------");
                    Console.WriteLine("|\t\tList Of Orders\t\t|");
                    Console.WriteLine("-------------------------------------------------");
                    Console.WriteLine();
                    ShowAllOrders();
                    break;
                case 6:
                    Console.WriteLine();
                    AddNewUser();
                    break;
                case 7:
                    Console.WriteLine();
                    Console.WriteLine("-------------------------------------------------");
                    Console.WriteLine("|\t\tDelete User\t\t|");
                    Console.WriteLine("-------------------------------------------------");
                    Console.WriteLine();
                    RemoveUser();
                    break;
                case 8:
                    Console.WriteLine();
                    Console.WriteLine("-------------------------------------------------");
                    Console.WriteLine("|\t\tList Of Users\t\t|");
                    Console.WriteLine("-------------------------------------------------");
                    Console.WriteLine();
                    ShowAllUsers();
                    break;
                case 9:
                    Console.WriteLine(" Thank you Mr. {0}", loginInfo.User.Name);
                    return isUserLoggedIn = false;
                default:
                    Console.WriteLine();
                    if (value == 0)
                    {
                        ActionsToBePerform();
                        PerformAction();
                    }
                    else
                    {
                        Console.WriteLine("Please provide valid input. and Try Again.. ");
                        ActionsToBePerform();
                        PerformAction();
                    }
                    break;

            }
            return true;
        }

        private static bool UserActions(int value)
        {
            switch (value)
            {
                case 1:
                    Console.WriteLine();
                    Console.WriteLine("-------------------------------------------------");
                    Console.WriteLine("|\t\tList Of Producrs\t\t|");
                    Console.WriteLine("-------------------------------------------------");
                    Console.WriteLine();
                    DisplayAllProducts();
                    break;
                case 2:
                    Console.WriteLine();
                    Console.WriteLine("-------------------------------------------------");
                    Console.WriteLine("|\t\tAdd Product To Cart\t\t|");
                    Console.WriteLine("-------------------------------------------------");
                    Console.WriteLine();
                    AddProductToCart();
                    break;
                case 3:
                    Console.WriteLine();
                    Console.WriteLine("-------------------------------------------------");
                    Console.WriteLine("|\t\tCart\t\t|");
                    Console.WriteLine("-------------------------------------------------");
                    Console.WriteLine();
                    ViewProductInCart();
                    break;
                case 4:
                    Console.WriteLine();
                    Console.WriteLine("-------------------------------------------------");
                    Console.WriteLine("|\t\tDelete Product from Cart\t|");
                    Console.WriteLine("-------------------------------------------------");
                    Console.WriteLine();
                    RemoveProductFromCart();
                    break;
                case 5:
                    Console.WriteLine();
                    Console.WriteLine("-------------------------------------------------");
                    Console.WriteLine("|\t\tApply Order\t|");
                    Console.WriteLine("-------------------------------------------------");
                    Console.WriteLine();
                    ApplyOrder();
                    break;
                case 6:
                    Console.WriteLine();
                    Console.WriteLine("-------------------------------------------------");
                    Console.WriteLine("|\t\tCancel Order\t|");
                    Console.WriteLine("-------------------------------------------------");
                    Console.WriteLine();
                    CancelOrder();
                    break;
                case 7:
                    Console.WriteLine();
                    Console.WriteLine("-------------------------------------------------");
                    Console.WriteLine("|\t\tList of Orders\t|");
                    Console.WriteLine("-------------------------------------------------");
                    Console.WriteLine();
                    ShowAllOrders();
                    break;
                case 8:
                    Console.WriteLine();
                    Console.WriteLine("-------------------------------------------------");
                    Console.WriteLine("|\t\tUpdate Cart\t|");
                    Console.WriteLine("-------------------------------------------------");
                    Console.WriteLine();
                    ModifyProductInCart();
                    break;
                case 9:
                    Console.WriteLine();
                    Console.WriteLine("\nThank you Mr. {0}", loginInfo.User.Name);
                    Console.WriteLine();
                    return isUserLoggedIn = false;
                    
                default:
                    Console.WriteLine();
                    if (value == 0)
                    {
                        ActionsToBePerform();
                        PerformAction();
                    }
                    else
                    {
                        Console.WriteLine("Please provide valid input. and Try Again.. ");
                        ActionsToBePerform();
                        PerformAction();
                    }
                    break;
            }
            return true;
        }

        private static bool LoginToApplication()
        {
            Console.WriteLine("---------------------------------");
            Console.WriteLine("|\t\tLogin\t\t|");
            Console.WriteLine("---------------------------------");
            Console.WriteLine();
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
                LoginToApplication();
            }
            return loginInfo.LoginStaus;
        }

        private static void DisplayAllProducts()
        {
            var products = new ProductManagementService().GetAllAvailableProducts();
            if (products.Count > 0)
            {
                foreach (var product in products)
                {
                    Console.WriteLine("Product ID : {0} | Name : {1} | Price : {2}", product.ProductId, product.Name, product.Price);
                }
            }
            else
                Console.WriteLine("No stock available.");
        }

        private static int AddNewProducts()
        {

            int quantity;
            decimal price;
            Console.WriteLine("Enter Product Name :");
            string name = Console.ReadLine();

            Console.WriteLine("Enter Description of product :");
            string description = Console.ReadLine();

            Console.WriteLine("Enter Price of product :");
            price = ValidatePrice(out price);
            Console.WriteLine("Enter Quantity of product :");
            quantity = ValidateQuantity(out quantity);
            var newProduct = new Products()
            {
                Name = name,
                Description = description,
                Quantity = quantity,
                Price = price,
            };
           int producId= new ProductManagementService().AddNewProduct(newProduct);
            if (producId > 0)
            {
                Console.WriteLine("New Product added.");
                return producId;
            }
            else {
                Console.WriteLine("Product does not added.");
                return producId;
            }
        }

        private static void ModifyProduct()
        {
            Console.WriteLine("Enter Product Id :");
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
            Console.WriteLine("Not implemented yet : it will update dummy values to the product that you input\n");
        }

        private static bool RemoveProduct()
        {
            Console.WriteLine("\nEnter ProductId :");
            bool isEvent = new ProductManagementService().DeleteProduct(EnterNumber());
            if (isEvent)
            {
                Console.WriteLine("Product Successfully removed.");
                return true;
            }
            return false;
        }

        private static void ShowAllOrders()
        {
            printOrderDetails(new OrderManagementService().ViewAllOrders(loginInfo.User.UserId));
        }

        private static Users AddNewUser()
        {
            Console.WriteLine("--------------------------------------------");
            Console.WriteLine("|\t\tUser Registration\t\t|");
            Console.WriteLine("--------------------------------------------");
            Console.WriteLine();

            string username = EnterUserName();
            string name = EnterName();
            string phonenumber = EnterPhoneNumber();
            string password = EnterPassword();
         
                var newUser = new Users()
                {
                    Name = name,
                    Password = password,
                    UserName = username,
                    PhoneNumber = phonenumber,
                    UserRole = addRoleToUser()

                };
                var user=new UserManagementService().UserRegistration(newUser);
            if (user!=null)
            {
                Console.WriteLine("New User created..");
                return user;
            }
            else
            {
                Console.WriteLine("User does not created.");
                return null;
            }

        }
        private static void RemoveUser()
        {
            Console.WriteLine("Enter Username :");
            bool isEvent = new UserManagementService().DeleteUser(Console.ReadLine());
            if (isEvent)
            {
                Console.WriteLine("User Successfully deleted.");
            }
            else
            {
                Console.WriteLine("User does not found");
            }

        }

        private static void ShowAllUsers()
        {
            var users = UserManagementService.users;
            foreach (var user in users)
            {
                Console.WriteLine("|\t\tName :{0}\t|", user.Name);
            }
        }

        private static bool AddProductToCart()
        {
            bool isProductAdded = false;
            try
            {
                Console.WriteLine();
                Console.WriteLine("Enter Product ID :");
                int productId = EnterNumber();
                var productToAdd = new Products()
                {
                    ProductId = productId,
                    Quantity = CheckValidProductQuantity(productId)
                };
                isProductAdded = new OrderManagementService().AddProductToCart(productToAdd, loginInfo.User.UserId);
                if (!isProductAdded)
                {
                    Console.WriteLine("\nNo stock available for this much number of quantity");
                    return isProductAdded;

                }
            }
            catch (Exception )
            {
                Console.WriteLine("\nProduct not available.");
                isProductAdded = false;
            }
            return isProductAdded;
        }

        private static bool RemoveProductFromCart()
        {
            Console.WriteLine("\nEnter ProductId :");
            bool isEvent = new OrderManagementService().RemoveProductFromCart(EnterNumber(), loginInfo.User.UserId);
            if (isEvent)
            {
                Console.WriteLine("Product Successfully removed from your cart.");
                return true;
            }
            else
            {
                Console.WriteLine("Product does not removed.");
                return false;
            }
        }

        private static void ApplyOrder()
        {
            bool orderApplied = new OrderManagementService().ApplyOrder(loginInfo.User.UserId);
            if (orderApplied)
                Console.WriteLine("Applied Successfully.");
            else
                Console.WriteLine("Does not apply.");

        }

        private static void CancelOrder()
        {
            Console.WriteLine("\nEnter Order Id :");
            bool isOrderCancel = new OrderManagementService().CancelOrder(EnterNumber(), loginInfo.User.UserId);
            if (isOrderCancel)
            {
                Console.WriteLine("Order cancelled.");
            }
            else
                Console.WriteLine("Doesn't cancel.");
        }

        private static bool ModifyProductInCart()
        {
            ViewProductInCart();
            Console.WriteLine();
            Console.WriteLine("\nEnter Product Id to modify :");
            int productId = EnterNumber();
            if (OrderManagementService.isProductInCart(productId, loginInfo.User.UserId))
            {
                Console.WriteLine("Not implemented yet.");
                var prod = new Products()
                {

                };
                new OrderManagementService().ModifyCartProduct(prod, loginInfo.User.UserId);
                return true;
            }
            return false;
        }

        private static void ViewProductInCart()
        {
            try
            {
                Console.WriteLine("Items in your Cart :");
                var listOfProduct = new OrderManagementService().ViewProductsInCart(loginInfo.User.UserId);
                if (listOfProduct != null)
                    printCartDetails(listOfProduct);
                else
                    Console.WriteLine("Cart is empty.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static int EnterNumber()
        {
            string input = Console.ReadLine();
            int num = -1;
            if (!int.TryParse(input, out num))
            {
                Console.WriteLine("Please input valid number...\n");

            }
            else
            {
                return num;
            }
            return EnterNumber(); ;
        }

        private static int EnterDigit()
        {
            string input = Console.ReadKey().KeyChar + "";
            int num = -1;
            if (!int.TryParse(input, out num))
            {
                Console.WriteLine();
                Console.WriteLine("\nPlease input valid number...\n");
            }
            else
            {
                return num;
            }
            return EnterDigit();
        }

        private static string EnterPassword()
        {
            bool isValidInput = false;
            Console.WriteLine("Enter Password :");
            string password = Console.ReadLine();

            isValidInput = UserManagementService.IsPasswordCriteriaMatch(password);
            
            if (isValidInput)
            {
                return password;
            }
            else
            {
                Console.WriteLine("Please provide valid password. Password should contains :\" Minimum six characters \"");
            }
            return EnterPassword();
        }

        private static string EnterName()
        {
            bool isValidInput = false;
            Console.WriteLine("Enter Name :");
            string name = Console.ReadLine();
            isValidInput = UserManagementService.IsValidName(name);
            if (isValidInput)
            {
                return name;
            }
            else
            {
                Console.WriteLine("Please provide valid name. Length should be one or more then.. Please Try again...");
            }

           return EnterName();
        }

        private static string EnterPhoneNumber() {
            bool isValidInput = false;            
            Console.WriteLine("Enter Phone Number :");
            string phone = Console.ReadLine();
            isValidInput = UserManagementService.IsPhoneNumber(phone);
            if (isValidInput)
            {
                return phone;
            }
            else
            {
                Console.WriteLine("Please provide valid number. Phone should contains 9 to 14 digits only. Please Try again...");
            }
            return EnterPhoneNumber();
        }


        private static int CheckValidProductQuantity(int productId)
        {
            int quantity = 0;
            Console.WriteLine("Enter Quantity :");
            quantity = ValidateQuantity(out quantity);
            int leftQuantityOfProduct = ProductManagementService.GetProductQuantity(productId);
            if (leftQuantityOfProduct > quantity)
                return quantity;
            else
            {
                Console.WriteLine();
                Console.WriteLine("This much quanity not available.", quantity);
                Console.WriteLine("Only few {0} products left in stock. Try again with valid quantity.", leftQuantityOfProduct);
                Console.WriteLine();
            }
            return CheckValidProductQuantity(productId);
        }

        private static void ActionsToBePerform()
        {
            Console.WriteLine("----------------------------------------");
            Console.WriteLine("|\t\tActions List\t\t|");
            Console.WriteLine("----------------------------------------");
            Console.WriteLine();

            var user = UserManagementService.GetUserInfo(loginInfo.User.UserId);
            var role = user.UserRole.GetValueOrDefault();
            if (role.ToString() == "Admin")
            {
                Console.WriteLine("Check below list of actions that \"" + role.ToString() + "\" can perform.");
                Console.WriteLine("Enter number to perform operation :");
                Console.WriteLine("[0] To Help");
                Console.WriteLine("[1] To View Products");
                Console.WriteLine("[2] To Add New Product");
                Console.WriteLine("[3] To Update Product");
                Console.WriteLine("[4] To Delete Product");
                Console.WriteLine("[5] To Check all Orders");
                Console.WriteLine("[6] To Add new User");
                Console.WriteLine("[7] To Remove user.");
                Console.WriteLine("[8] To All Users information.");
                Console.WriteLine("[9] To Logout");
            }
            else if (role.ToString() == "User")
            {
                Console.WriteLine("Check below list of actions that \"" + role.ToString() + "\" can perform.");
                Console.WriteLine("Enter number to perform operation :");
                Console.WriteLine("[0] To Help");
                Console.WriteLine("[1] To show all Products");
                Console.WriteLine("[2] To Add Product To Cart");
                Console.WriteLine("[3] To View Product in Cart");
                Console.WriteLine("[4] To Delete Product from Cart");
                Console.WriteLine("[5] To Buy Order");
                Console.WriteLine("[6] To Cancel Order");
                Console.WriteLine("[9] To Logout");
            }
        }

        private static void ShowProducts()
        {
            new ProductManagementService().GetAllAvailableProducts();
        }

        private static Role? GetUserType()
        {
            Console.WriteLine("Are you a User or Admin? Please Enter.. ");
            Console.WriteLine("[1] to Admin?");
            Console.WriteLine("[2] to Normal User?\n");
            int inputNumber = 0;
            try
            {
                inputNumber = EnterDigit();
                Console.WriteLine();
                if (inputNumber == 1)
                    return Role.Admin;
                else if (inputNumber == 2)
                    return Role.User;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Please provide valid input type?\n{0}", ex.Message);
                return GetUserType();
            }
            Console.WriteLine("Please Enter valid input type?\n");
            return GetUserType();
        }

        private static Role? AskForUserType()
        {
            Role? typeOfUser = new Role();
            typeOfUser = GetUserType();
            Console.WriteLine();
            return typeOfUser;

        }

        private static bool DoYouHaveAnAccount()
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

        private static bool DoYouWantToContinue()
        {
            try
            {
                Console.WriteLine();
                Console.WriteLine("Do you want to continue with us :");
                if (loginInfo != null)
                    Console.WriteLine("Please input Y/N : 'Y' to Continue and 'N' to Logout");
                else
                    Console.WriteLine("Please input Y/N : 'Y' to Continue and 'N' to Re-start application.");
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
                Console.WriteLine("Please provide valid input... and try again\n{0}", ex.Message);
            }
            return DoYouWantToContinue();
        }

        private static bool DoYouWantToLoginToTheApplication(Role? userType)
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

       
        /// <summary>
        /// Select role for user.
        /// </summary>
        /// <returns>returns the user</returns>
        private static Role addRoleToUser()
        {
            Console.WriteLine("Enter 1 for Admin and 2 For User role");

            int roleValue = 0;
            int.TryParse(Console.ReadKey().KeyChar.ToString(), out roleValue);
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

        /// <summary>
        /// Price input
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private static int ValidateQuantity(out int input)
        {
            string userInput;
            do
            {
                userInput = Console.ReadLine();
            } while (!int.TryParse(userInput, out input));
            return input;
        }

        private static decimal ValidatePrice(out decimal input)
        {
            string userInput;
            do
            {
                userInput = Console.ReadLine();
            } while (!decimal.TryParse(userInput, out input));
            return input;
        }






        /// <summary>
        /// Username input
        /// </summary>
        /// <returns> returns the unique username.</returns>
        private static string EnterUserName()
        {
            Console.WriteLine("Enter Username :");
            string username = Console.ReadLine();

            if (!UserManagementService.IsUserExists(username))
            {
                if (username.Length > 4)
                    return username;
                else
                    Console.WriteLine("Username length should be more than 4");
            }
            else
            Console.WriteLine("User already exists,Please add different username.");
                
            return EnterUserName();
        }


        /// <summary>
        /// Print orders details.
        /// </summary>
        /// <param name="orders"> order list.</param>
        private static void printOrderDetails(List<OrderDetail> orders)
        {
            if (orders.Count <= 0)
            {
                Console.WriteLine("No new Order.");
            }
            else
            {
                foreach (var order in orders)
                {
                    Console.WriteLine("Order Id : {0}\t|\tProduct Name : {1}\t|\tQuantity : {2}\t|\tOrderStatus : {3}", order.OrderId, ProductManagementService.GetProductName(order.Product.ProductId), order.Product.Quantity, order.Status);
                }

            }
        }

        /// <summary>
        /// Print Cart details.
        /// </summary>
        /// <param name="products">List of products in a cart.</param>
        private static void printCartDetails(List<Products> products)
        {
            foreach (var product in products)
            {
                Console.WriteLine("Item Name : {0}\t|\tQuantity : {1}\t|\tItem Price : {2}", ProductManagementService.GetProductName(product.ProductId),
                    product.Quantity, OrderManagementService.GetTotalPriceAccordingToQuantity(ProductManagementService.GetProductPrice(product.ProductId), product.Quantity));
                OrderManagementService.totalAmount += OrderManagementService.GetTotalPriceAccordingToQuantity(ProductManagementService.GetProductPrice(product.ProductId), product.Quantity);
            }
            Console.WriteLine("Total Amount : {0}", OrderManagementService.totalAmount);
        }
    }
}