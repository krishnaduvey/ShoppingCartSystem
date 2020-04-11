using ShoppingCartSystem.Abstraction.Model;
using ShoppingCartSystem.Core;
using ShoppingCartSystem.Infra;
using System;
using System.Collections.Generic;

namespace ShoppingCartSystem
{
    class Program
    {
        private static readonly string appTitle = "SHOPPING CART SYSTEM";
        private static LoginDetails loginInfo;
        private static bool isUserLoggedIn = false;
        private static Role? userType;
        private static DatabaseConfig dbObject = new DatabaseConfig();
        /// <summary>
        ///     The perform database operations.
        /// </summary>


        public static void PerformDatabaseOperations()
        {
                dbObject.Database.CreateIfNotExists();           
        }

        /// <summary>
        /// Full control of application
        /// </summary>
        /// <param name="args">No need of args for this app</param>
        public static void Main()
        {
            PerformDatabaseOperations();
            Console.Write("Person saved !");
            Console.ReadLine();

        Start:
            Console.WriteLine("-----------------------------------------------------------------------");
            Console.WriteLine("|||||||\t\t\t" + appTitle + "\t\t\t|||||||");
            Console.WriteLine("-----------------------------------------------------------------------");
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
                        if (isUserLoggedIn)
                            userType = loginInfo.User.UserRole;
                    }
                    else
                    {
                        if (DoYouHaveAnAccount())
                        {
                            isUserLoggedIn = LoginToApplication();
                            if (isUserLoggedIn)
                                userType = loginInfo.User.UserRole;

                        }
                        else
                        {
                            _ = AddNewUser();
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

        /// <summary>
        /// Here every operation named action, this method divide the actions between admin and normal user.
        /// </summary>
        /// <returns>return bool value of operations perform.</returns>
        private static bool PerformAction()
        {
            bool isUserWantToLogout;
            if (loginInfo.User.UserRole.ToString() == "Admin")
            {
                Console.WriteLine("\n\nInput action number :");
                isUserWantToLogout = AdminActions(EnterDigit());
            }
            else
            {
                Console.WriteLine("\n\nInput action number :");
                isUserWantToLogout = UserActions(EnterDigit());
            }
            return isUserWantToLogout;
        }

        /// <summary>
        /// Actions that related to admin user should be listed and operated.
        /// </summary>
        /// <param name="value">value defines which action to be perform.</param>
        /// <returns></returns>
        private static bool AdminActions(int value)
        {
            switch (value)
            {
                case 1:
                    Console.WriteLine();
                    Console.WriteLine("-------------------------------------------");
                    Console.WriteLine("\t\tProduct Details\t\t");
                    Console.WriteLine("-------------------------------------------");
                    Console.WriteLine();
                    DisplayAllProducts();
                    break;
                case 2:
                    Console.WriteLine();
                    Console.WriteLine("---------------------------------------------");
                    Console.WriteLine("\t\tAdd New Product\t\t");
                    Console.WriteLine("---------------------------------------------");
                    Console.WriteLine();
                    AddNewProducts();
                    break;
                case 3:
                    Console.WriteLine();
                    Console.WriteLine("-------------------------------------------------");
                    Console.WriteLine("\t\tUpdate Product\t\t\t");
                    Console.WriteLine("-------------------------------------------------");
                    Console.WriteLine();
                    ModifyProduct();
                    break;
                case 4:
                    Console.WriteLine();
                    Console.WriteLine("-------------------------------------------------");
                    Console.WriteLine("\t\tDelete Product\t\t");
                    Console.WriteLine("-------------------------------------------------");
                    Console.WriteLine();
                    RemoveProduct();
                    break;
                case 5:
                    Console.WriteLine();
                    Console.WriteLine("-------------------------------------------------");
                    Console.WriteLine("\t\tList Of Orders\t\t");
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
                    Console.WriteLine("\t\tDelete User\t\t");
                    Console.WriteLine("-------------------------------------------------");
                    Console.WriteLine();
                    RemoveUser();
                    break;
                case 8:
                    Console.WriteLine();
                    Console.WriteLine("-------------------------------------------------");
                    Console.WriteLine("\t\tList Of Users\t\t");
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

        /// <summary>
        /// Actions that related to normal user actions should be listed and operated.
        /// </summary>
        /// <param name="value">value defines which action to be perform.</param>
        /// <returns>Returns bool value on successfully/failed operations.</returns>
        private static bool UserActions(int value)
        {
            switch (value)
            {
                case 1:
                    Console.WriteLine();
                    Console.WriteLine("-------------------------------------------------");
                    Console.WriteLine("\t\tList Of Producrs\t\t");
                    Console.WriteLine("-------------------------------------------------");
                    Console.WriteLine();
                    DisplayAllProducts();
                    break;
                case 2:
                    Console.WriteLine();
                    Console.WriteLine("-------------------------------------------------");
                    Console.WriteLine("\t\tAdd Product To Cart\t\t");
                    Console.WriteLine("-------------------------------------------------");
                    Console.WriteLine();
                    AddProductToCart();
                    break;
                case 3:
                    Console.WriteLine();
                    Console.WriteLine("-------------------------------------------------");
                    Console.WriteLine("\t\tCart\t\t");
                    Console.WriteLine("-------------------------------------------------");
                    Console.WriteLine();
                    ViewProductInCart();
                    break;
                case 4:
                    Console.WriteLine();
                    Console.WriteLine("-------------------------------------------------");
                    Console.WriteLine("\t\tDelete Product from Cart\t");
                    Console.WriteLine("-------------------------------------------------");
                    Console.WriteLine();
                    RemoveProductFromCart();
                    break;
                case 5:
                    Console.WriteLine();
                    Console.WriteLine("-------------------------------------------------");
                    Console.WriteLine("\t\tApply Order\t");
                    Console.WriteLine("-------------------------------------------------");
                    Console.WriteLine();
                    ApplyOrder();
                    break;
                case 6:
                    Console.WriteLine();
                    Console.WriteLine("-------------------------------------------------");
                    Console.WriteLine("\t\tCancel Order\t");
                    Console.WriteLine("-------------------------------------------------");
                    Console.WriteLine();
                    CancelOrder();
                    break;
                case 7:
                    Console.WriteLine();
                    Console.WriteLine("-------------------------------------------------");
                    Console.WriteLine("\t\tList of Orders\t");
                    Console.WriteLine("-------------------------------------------------");
                    Console.WriteLine();
                    ShowAllOrders();
                    break;
                case 8:
                    Console.WriteLine();
                    Console.WriteLine("-------------------------------------------------");
                    Console.WriteLine("\t\tUpdate Cart\t");
                    Console.WriteLine("-------------------------------------------------");
                    Console.WriteLine();
                    ModifyProductInCart();
                    break;
                case 9:
                    Console.WriteLine();
                    Console.WriteLine("\nThank you Mr. {0}", loginInfo.User.Name);
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

        /// <summary>
        /// Application Login
        /// </summary>
        /// <returns>Returns bool value on successful/failed login operation</returns>
        private static bool LoginToApplication()
        {
            Console.WriteLine("---------------------------------");
            Console.WriteLine("\t\tLogin\t\t");
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
                //LoginToApplication();
            }
            return loginInfo.LoginStaus;
        }

        /// <summary>
        /// Displaying list of products.
        /// </summary>
        private static void DisplayAllProducts()
        {
            var products = new ProductManagementService().GetAllAvailableProducts();
            if (products.Count > 0)
            {
                foreach (var product in products)
                {
                    if (loginInfo.User.UserRole.ToString() != "Admin")
                        Console.WriteLine("Product ID : {0} | Name : {1} | Price : {2}", product.ProductId, product.Name, product.Price);
                    else
                        Console.WriteLine("Product ID : {0} | Name : {1} | Price : {2} | Quantity : {3}", product.ProductId, product.Name, product.Price, product.Quantity);
                }
            }
            else
                Console.WriteLine("No stock available. So please add few products and then try again..");
        }


        /// <summary>
        /// Add a new product.
        /// </summary>
        /// <returns>Returns id of newly created product.</returns>
        private static int AddNewProducts()
        {
            var newProduct = CreateNewProduct();
            int producId = new ProductManagementService().AddNewProduct(newProduct);
            if (producId > 0)
            {
                Console.WriteLine("Product added successfully.");
                return producId;
            }
            else
            {
                Console.WriteLine("Product doesn't added. Please provide healthy information.");
                return producId;
            }
        }

        /// <summary>
        /// Create a object that contains products information.
        /// </summary>
        /// <returns>Return a object of Products type.</returns>
        public static Products CreateNewProduct()
        {
            int quantity;
            decimal price;
            string name = EnterProductName();
            string description = EnterProductDescription();
            price = EnterProductPrice();
            quantity = EnterProductQuantity();
            var newProduct = new Products()
            {
                Name = name,
                Description = description,
                Quantity = quantity,
                Price = price,
            };
            return newProduct;
        }

        /// <summary>
        /// Enter Product name.
        /// </summary>
        /// <returns>Returns product name of string type.</returns>
        private static string EnterProductName()
        {
            Console.WriteLine("Enter Product Name :");
            string name = Console.ReadLine();
            if (name.Length > 0)
                return name;
            else
            {
                Console.WriteLine("Enter valid product name.");
            }
            return EnterProductName();
        }

        /// <summary>
        /// Enter Description of product.
        /// </summary>
        /// <returns>Returns product description of string type.</returns>
        private static string EnterProductDescription()
        {
            Console.WriteLine("Enter Product description :");
            string description = Console.ReadLine();
            if (description.Length > 0)
                return description;
            else
            {
                Console.WriteLine("Enter valid product description.");
            }
            return EnterProductDescription();
        }

        /// <summary>
        /// Enter Product Price.
        /// </summary>
        /// <returns>Returns product price of decimal type.</returns>
        private static decimal EnterProductPrice()
        {
            decimal price;
            price = ValidatePrice(out _);
            return price;
        }

        /// <summary>
        /// Enter Product Quantity.
        /// </summary>
        /// <returns>Returns product Quantity of int type.</returns>
        private static int EnterProductQuantity()
        {
            int quantity;
            quantity = ValidateQuantity(out _);
            return quantity;
        }

        /// <summary>
        /// Update product information.
        /// </summary>
        private static void ModifyProduct()
        {
            Console.WriteLine("Enter Product Id :");
            int productId = EnterNumber();
            PrintUpdatableAttributeList();
            Console.WriteLine("Enter number :");
            int toSelect = EnterDigit();
            Console.WriteLine();
            Products prod = SelectAttributeToUpdate(toSelect, productId);
            if (prod != null)
                UpdateFullProductInfo(prod);
        }


        /// <summary>
        /// Update full product info
        /// </summary>
        /// <param name="productInfo">Object of Products type , which contains information.</param>
        private static void UpdateFullProductInfo(Products productInfo)
        {
            new ProductManagementService().UpdateProductInfo(
                   new Products()
                   {
                       ProductId = productInfo.ProductId,
                       Price = productInfo.Price,
                       Quantity = productInfo.Quantity,
                       Description = productInfo.Description,
                       Name = productInfo.Name
                   }
                  );
        }

        /// <summary>
        /// List of product attribute that need to update.
        /// </summary>
        private static void PrintUpdatableAttributeList()
        {
            Console.WriteLine("----------------------------------------");
            Console.WriteLine("\t\tSelect to modify\t\t");
            Console.WriteLine("----------------------------------------");
            Console.WriteLine();
            Console.WriteLine("[0] To Full Product");
            Console.WriteLine("[1] To Update Products Quantity");
            Console.WriteLine("[2] To Update Product Name");
            Console.WriteLine("[3] To Update Product Price");
            Console.WriteLine("[4] To Update Product Description");
        }

        /// <summary>
        /// Which attribute want to update ( Control flow)
        /// </summary>
        /// <param name="attribute">Name of attribute that need to update.</param>
        /// <param name="productId">Identify object.</param>
        /// <returns>Returen updated info in type of Products class</returns>
        private static Products SelectAttributeToUpdate(int attribute, int productId)
        {
            switch (attribute)
            {
                case 0:
                    var prodObj = new Products()
                    {
                        ProductId = productId,
                        Name = EnterProductName(),
                        Price = EnterProductPrice(),
                        Quantity = EnterProductQuantity(),
                        Description = EnterProductDescription()
                    };
                    return new ProductManagementService().UpdateProductInfo(prodObj);

                case 1:
                    var prodQuantityObj = new Products()
                    {
                        ProductId = productId,
                        Quantity = EnterProductQuantity(),
                    };
                    var updatedQuant = new ProductManagementService().UpdateProductQuantity(prodQuantityObj);
                    if (updatedQuant != null)
                    {
                        Console.WriteLine("Quantity updated successfully.");
                    }
                    else
                        Console.WriteLine("Quantity  does not updated.");
                    break;
                case 2:
                    var prodNameObj = new Products()
                    {
                        ProductId = productId,
                        Name = EnterProductName(),
                    };
                    var updatedName = new ProductManagementService().UpdateProductName(prodNameObj);
                    if (updatedName != null)
                        Console.WriteLine("Name updated successfully.");
                    else
                        Console.WriteLine("Name  does not updated.");
                    break;
                case 3:
                    var prodPriceObj = new Products()
                    {
                        ProductId = productId,
                        Price = EnterProductPrice(),
                    };
                    var price = new ProductManagementService().UpdateProductPrice(prodPriceObj);
                    if (price != null)
                    {
                        Console.WriteLine("Price updated successfully.");
                    }
                    else
                        Console.WriteLine("DescripPricetion  does not updated.");
                    break;
                case 4:
                    var prodDescriptionObj = new Products()
                    {
                        ProductId = productId,
                        Description = EnterProductDescription(),
                    };
                    var desc = new ProductManagementService().UpdateProductDescription(prodDescriptionObj);
                    if (desc != null)
                    {
                        Console.WriteLine("Description updated successfully.");
                    }
                    else
                        Console.WriteLine("Description  does not updated.");
                    break;
                default:
                    Console.WriteLine();
                    Console.WriteLine("Please provide valid input. and Try Again.. ");
                    PrintUpdatableAttributeList();
                    Console.WriteLine("Enter Number :");
                    int toSelect = EnterDigit();
                    Console.WriteLine();
                    SelectAttributeToUpdate(toSelect, productId);
                    break;
            }
            return null;
        }

        /// <summary>
        /// Remove Product
        /// </summary>
        /// <returns>Returns boolean value.</returns>
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


        /// <summary>
        /// Print List of orders
        /// </summary>
        private static void ShowAllOrders()
        {
            PrintOrderDetails(new OrderManagementService().ViewAllOrders(loginInfo.User.UserId));
        }


        /// <summary>
        /// Add new user.
        /// </summary>
        /// <returns></returns>
        private static Users AddNewUser()
        {
            Console.WriteLine("--------------------------------------------");
            Console.WriteLine("\t\tUser Registration\t\t");
            Console.WriteLine("--------------------------------------------");
            Console.WriteLine();

            string username = EnterUserName();
            string name = EnterName();
            string phonenumber = EnterPhoneNumber();
            string password = EnterPassword();
            string email = EnterEmail();
            var newUser = new Users()
            {
                Name = name,
                Password = password,
                UserName = username,
                PhoneNumber = phonenumber,
                Email = email,
                UserRole = AddRoleToUser()

            };
            var user = new UserManagementService().UserRegistration(newUser);
            if (user != null)
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

        /// <summary>
        /// Remove user.
        /// </summary>
        private static void RemoveUser()
        {
            Console.WriteLine("Enter Username :");
            bool isEvent = new UserManagementService().DeleteUser(Console.ReadLine());
            if (isEvent)
                Console.WriteLine("User Successfully deleted.");
            else
                Console.WriteLine("User does not found");

        }

        /// <summary>
        /// List of users
        /// </summary>
        /// <returns></returns>
        private static bool ShowAllUsers()
        {
            var users = new UserManagementService().GetUsers();
            foreach (var user in users)
            {
                Console.WriteLine("\t\tName :{0}\tEmail :{1}", user.Name, user.Email);
            }
            return true;
        }

        /// <summary>
        /// Add product to cart.
        /// </summary>
        /// <returns></returns>
        private static bool AddProductToCart()
        {
            bool isProductAdded;
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
            catch (Exception)
            {
                Console.WriteLine("\nProduct not available.");
                isProductAdded = false;
            }
            return isProductAdded;
        }

        /// <summary>
        /// Remove product from cart
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// To buy products
        /// </summary>
        private static void ApplyOrder()
        {
            bool orderApplied = new OrderManagementService().ApplyOrder(loginInfo.User.UserId);
            if (orderApplied)
                Console.WriteLine("Applied Successfully.");


        }

        /// <summary>
        /// Cancel order.
        /// </summary>
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

        /// <summary>
        /// Modify product which palced in cart.
        /// </summary>
        /// <returns></returns>
        private static bool ModifyProductInCart()
        {
            Console.WriteLine();
            Console.WriteLine("\nEnter Product Id to modify Quantity :");
            int productId = EnterNumber();
            if (OrderManagementService.IsProductInCart(productId, loginInfo.User.UserId))
            {
                Console.WriteLine("Not implemented yet.");
                int quantity = CheckValidProductQuantity(productId);
                var prod = new Products()
                {
                    ProductId = productId,
                    Quantity = quantity
                };
                new OrderManagementService().ModifyCartProduct(prod, loginInfo.User.UserId);
                return true;
            }
            else
            {
                Console.WriteLine("No such product in Cart");
            }
            return false;
        }

        /// <summary>
        /// List of products in cart
        /// </summary>
        private static void ViewProductInCart()
        {
            try
            {
                var listOfProduct = new OrderManagementService().ViewProductsInCart(loginInfo.User.UserId);
                if (listOfProduct != null)
                {
                    Console.WriteLine("Items in your Cart : {0}", listOfProduct.Count);
                    PrintCartDetails(listOfProduct);
                }
                else
                    Console.WriteLine("Cart is empty.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Enter any number, holds you here till you input a valid number.
        /// </summary>
        /// <returns></returns>
        private static int EnterNumber()
        {
            string input = Console.ReadLine();

            if (!int.TryParse(input, out int num))
            {
                Console.WriteLine();
                Console.WriteLine("Please input valid number...\n");
            }
            else
                return num;
            return EnterNumber();
        }

        /// <summary>
        /// Enter any digit, holds you here till you input a valid number.
        /// </summary>
        private static int EnterDigit()
        {
            string input = Console.ReadKey().KeyChar + "";
            if (!int.TryParse(input, out int num))
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

        /// <summary>
        /// Enter any password, holds you here till you input a valid password.
        /// </summary>
        private static string EnterPassword()
        {
            bool isValidInput;
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

        /// <summary>
        /// Enter any email, holds you here till you input a valid email.
        /// </summary>
        private static string EnterEmail()
        {
            bool isValidEmail;
            Console.WriteLine("Enter Email address :");
            string email = Console.ReadLine();

            isValidEmail = UserManagementService.IsValidEmail(email);

            if (isValidEmail)
            {
                return email;
            }
            else
            {
                Console.WriteLine("Please provide valid email address. Email be in format :\" abc@domain.xyz \"");
            }
            return EnterEmail();
        }


        /// <summary>
        /// Enter any Name, holds you here till you input a valid Name.
        /// </summary>
        private static string EnterName()
        {
            bool isValidInput;
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

        /// <summary>
        /// Enter any Phone number, holds you here till you input a valid Phone number.
        /// </summary>
        private static string EnterPhoneNumber()
        {
            bool isValidInput;
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

        /// <summary>
        /// Validate product quantity.
        /// </summary>
        /// <param name="productId"> input product id as input param.</param>
        /// <returns></returns>
        private static int CheckValidProductQuantity(int productId)
        {
            int quantity;
            quantity = ValidateQuantity(out _);
            int leftQuantityOfProduct = ProductManagementService.GetProductQuantity(productId);
            if (leftQuantityOfProduct >= quantity)
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

        /// <summary>
        /// List of actions
        /// </summary>
        private static void ActionsToBePerform()
        {
            Console.WriteLine("----------------------------------------");
            Console.WriteLine("\t\tActions List\t\t");
            Console.WriteLine("----------------------------------------");
            Console.WriteLine();

            var user = UserManagementService.GetUserInfo(loginInfo.User.UserId);
            var role = user.UserRole.GetValueOrDefault();
            if (role.ToString() == "Admin")
            {
                Console.WriteLine("Check below list of actions that \"" + role.ToString() + "\" can perform.");
                Console.WriteLine("Enter number to perform operation :");
                Console.WriteLine("[0] To get list of actions.");
                Console.WriteLine("[1] To display list of products.");
                Console.WriteLine("[2] To add new product.");
                Console.WriteLine("[3] To update product information.");
                Console.WriteLine("[4] To delete product.");
                Console.WriteLine("[5] To check all orders in which is applied by customer.");
                Console.WriteLine("[6] To Add new user.");
                Console.WriteLine("[7] To delete a user.");
                Console.WriteLine("[8] To get all users information.");
                Console.WriteLine("[9] To logout.");//user
            }
            else if (role.ToString() == "User")
            {
                Console.WriteLine("Check below list of actions that \"" + role.ToString() + "\" can perform.");
                Console.WriteLine("Enter number to perform operation :");
                Console.WriteLine("[0] To get list of actions.");
                Console.WriteLine("[1] To display list of products");
                Console.WriteLine("[2] To add product to cart");
                Console.WriteLine("[3] To get list of product in your cart.");
                Console.WriteLine("[4] To delete product from Cart");
                Console.WriteLine("[5] To buy a product.");
                Console.WriteLine("[6] To cancel already applied order.");
                Console.WriteLine("[7] To list of orders");
                Console.WriteLine("[8] To update your cart.");
                Console.WriteLine("[9] To Logout");
            }
        }

        /// <summary>
        /// Display all products
        /// </summary>
        private static void ShowProducts()
        {
            new ProductManagementService().GetAllAvailableProducts();
        }

        private static Role? GetUserType()
        {
            Console.WriteLine("Are you a User or Admin? Please Enter.. ");
            Console.WriteLine("[1] to Admin?");
            Console.WriteLine("[2] to Normal User?\n");
            int inputNumber;
            try
            {
                Console.WriteLine("Enter Here...");
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
            Role? typeOfUser;
            typeOfUser = GetUserType();
            Console.WriteLine();
            return typeOfUser;

        }

        /// <summary>
        /// To check user is new or existing one. 
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Hold user untill they are showing interest in app
        /// </summary>
        /// <returns></returns>
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
        private static Role AddRoleToUser()
        {
            Console.WriteLine("Enter 1 for Admin and 2 For User role");


            int.TryParse(Console.ReadKey().KeyChar.ToString(), out int roleValue);
            Console.WriteLine();

            if ((roleValue - 1) < 0 || (roleValue - 1) > 1)
            {
                Console.WriteLine("Try again with valid number.");
                AddRoleToUser();
            }

            if ((roleValue - 1) == 0 && (userType.ToString() == "User"))
            {
                Console.WriteLine("You dont have permission to create admin user. Please select valid role:");
                AddRoleToUser();
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
            try
            {
                Console.WriteLine("Enter Product Quantity :");
                userInput = Console.ReadLine();
                var isValidQuantity = int.TryParse(userInput, out input);
                if (isValidQuantity)
                    return input;
                else
                    Console.WriteLine("Please enter valid quantity.");
            }
            catch (Exception)
            {
                Console.WriteLine("Please enter valid quantity.");
            }

            return ValidateQuantity(out input);
        }

        private static decimal ValidatePrice(out decimal input)
        {
            try
            {
                string userInput;
                Console.WriteLine("Enter Product Price :");
                userInput = Console.ReadLine();
                var isValidPrice = decimal.TryParse(userInput, out input);
                if (isValidPrice)
                    return input;
                else
                    Console.WriteLine("Please enter valid amount.");
            }
            catch (Exception)
            {
                Console.WriteLine("Please enter valid amount.");
            }
            return ValidatePrice(out input);
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
        private static void PrintOrderDetails(List<OrderDetail> orders)
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
        private static void PrintCartDetails(List<Products> products)
        {
            foreach (var product in products)
            {
                Console.WriteLine("Product Id : {0}\t|\tItem Name : {1}\t|\tQuantity : {2}\t|\tItem Price : {3}", product.ProductId, ProductManagementService.GetProductName(product.ProductId),
                    product.Quantity, OrderManagementService.GetTotalPriceAccordingToQuantity(ProductManagementService.GetProductPrice(product.ProductId), product.Quantity));
                OrderManagementService.totalAmount += OrderManagementService.GetTotalPriceAccordingToQuantity(ProductManagementService.GetProductPrice(product.ProductId), product.Quantity);
            }
            Console.WriteLine("Total Amount : {0}", OrderManagementService.totalAmount);
        }
    }
}