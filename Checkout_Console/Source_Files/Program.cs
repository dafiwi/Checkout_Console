using CheckoutConsole.Models;

namespace CheckoutConsole
{
    public class Program
    {
        public static List<InvoiceItem> invoiceItems = new List<InvoiceItem>();
        public static bool customerSelected = false;
        public static Customer? chosenCustomer = null;
        public static double invoiceTotal = 0.0;
        public static Address? shippingAddress = null;
        public static PaymentMethod? chosenPaymentMethod = null;

        public static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\nWelcome to our Console Checkout System\n");
            Console.ForegroundColor = ConsoleColor.White;

            #region Buy Articles loop

            Console.WriteLine("Do you want to buy an article? y/n");
            string? shoppingChoice = Console.ReadLine();

            do
            {
                try
                {
                    if (shoppingChoice == "y")
                    {
                        InvoiceItem invoiceItem = ShoppingController.AddArticle();
                        invoiceItems.Add(invoiceItem);
                    }
                    else if (shoppingChoice == "n")
                    {
                        if (invoiceItems.Count > 0)
                        {
                            Console.WriteLine("\nContinue to checkout...");
                        }
                        else
                        {
                            Console.WriteLine("\nThanks for visiting us and see you soon.");
                        }
                        ShoppingController.QuitProgram();
                    }
                    else
                    {
                        ShoppingController.WrongEntry();
                    }
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception.Message);
                }

                if (invoiceItems.Count > 0)
                {
                    Console.WriteLine("\nDo you want to buy another article? y/n");
                    shoppingChoice = Console.ReadLine();
                }
            } while (shoppingChoice == "y");
            #endregion

            #region Customer Choice
            if (invoiceItems.Count > 0)
            {
                Console.WriteLine("\nAre you a registered Customer? y/n");
                string? selectCustomer = Console.ReadLine();
                try
                {
                    if (selectCustomer == "y")
                    {
                        List<Customer> customers = ShoppingController.ListCustomers();
                        chosenCustomer = ShoppingController.ChooseCustomer(customers);
                        customerSelected = true;
                    }
                    else if (shoppingChoice == "n")
                    {
                        Console.WriteLine("Continue to checkout as a guest...");
                    }
                    else
                    {
                        ShoppingController.WrongEntry();
                    }
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception.Message);
                }
            }
            #endregion

            #region Show Shopping basket and Invoice total 

            if (invoiceItems.Count > 0)
            {
                Console.WriteLine("\nHere are the contents of your shopping basket: ");
                try
                {
                    invoiceTotal = ShoppingController.ShowShoppingBasket();
                    Console.WriteLine("The invoice total is: " + String.Format("{0:.##}", invoiceTotal));
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception.Message);
                }
            }
            #endregion

            #region Checkout overview and options

            if (invoiceItems.Count > 0)
            {
                Console.WriteLine("\nDo you want to change your order? ");
                string? changeOrderDecision = Console.ReadLine();

                while (changeOrderDecision == "y")
                {
                    try
                    {
                        if (changeOrderDecision == "y")
                        {
                            Console.WriteLine("\nDo you want to add (a), overwrite (o) or remove (r) an invoice item? " +
                                              "Press n to do nothing.");
                            string? changeOrderChoice = Console.ReadLine();

                            if (changeOrderChoice == "a")
                            {
                                InvoiceItem invoiceItem = ShoppingController.AddArticle();
                                invoiceItems.Add(invoiceItem);
                            }
                            else if (changeOrderChoice == "o")
                            {
                                InvoiceItem invoiceItem = ShoppingController.OverwriteInvoiceItem();
                            }
                            else if (changeOrderChoice == "r")
                            {
                                ShoppingController.RemoveInvoiceItem();                                
                            }
                            else if(changeOrderChoice == "n")
                            {
                                Console.WriteLine("\nYou will be forwarded to the checkout now.");
                            }
                        }
                        else
                        {
                            ShoppingController.WrongEntry();
                        }
                    }
                    catch (Exception exception)
                    {
                        Console.WriteLine(exception.Message);
                    }

                    Console.WriteLine("\nDo you want to change your order again? ");
                    changeOrderDecision = Console.ReadLine();
                }
            }
            #endregion            

            #region Checkout confirmation

            Console.WriteLine("\nThose are the invoice items in your shopping basket: \n");
            ShoppingController.ShowShoppingBasket();
            Console.WriteLine("\nPlease press any key to confirm your selection.");
            Console.ReadKey();
            #endregion

            #region Payment choice

            Console.WriteLine("\n\nPlease choose your prefered payment method: ");            

            if (customerSelected == true)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\nDear {chosenCustomer.CustomerName}, your payment method {chosenCustomer.PaymentMethod.PaymentMethodName} with the identifier {chosenCustomer.PaymentMethod.PaymentMethodIdentifier} is already in our database.");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine($"\nYou will use this payment method to pay the invoice total of {String.Format("{0:.##}", invoiceTotal)}.");                
            }
            else
            {
                Console.WriteLine("\nPlease enter your payment method here: ");
                try
                {
                    chosenPaymentMethod = ShoppingController.ChoosePaymentMethod();
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception.Message);
                }
            }
            #endregion            

            #region Address selection

            string? firstName = null;
            string lastName; 
            string street, houseNumber, postCode, city, country;

            if (customerSelected == true)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\n\nDear {chosenCustomer.CustomerName}, your address \n{chosenCustomer.Address}\nis already in our database.");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("\nWe will send your order to this address.");
                shippingAddress = chosenCustomer.Address;
            }
            else
            {
                Console.WriteLine("\n\nPlease enter your address here: ");
                try
                {
                    Console.WriteLine("\nWhat is your first name? ");
                    firstName = Console.ReadLine();
                    Console.WriteLine("\nWhat is your last name? ");
                    lastName = Console.ReadLine();
                    Console.WriteLine("\nWhat is your street name? ");
                    street = Console.ReadLine();
                    Console.WriteLine("\nWhat is your house number? ");
                    houseNumber = Console.ReadLine();
                    Console.WriteLine("\nWhat is your post code? ");
                    postCode = Console.ReadLine();
                    Console.WriteLine("\nWhat is your city name? ");
                    city = Console.ReadLine();
                    Console.WriteLine("\nIn which country do you live? ");
                    country = Console.ReadLine();
                    shippingAddress = new Address(street, houseNumber, postCode, city, country);

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"\nDear {firstName} {lastName},");
                    Console.WriteLine($"we will send your order to this address: \n{shippingAddress.Street} {shippingAddress.HouseNumber}\n{shippingAddress.PostCode} {shippingAddress.City}\n{shippingAddress.Country}");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception.Message);
                }
            }
            #endregion

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\nThanks for shopping and see you soon. ");
            Console.ForegroundColor = ConsoleColor.White;

            ShoppingController.QuitProgram();
        }
    }
}
