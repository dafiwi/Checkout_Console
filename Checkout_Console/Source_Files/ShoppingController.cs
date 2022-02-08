using CheckoutConsole.Models;

namespace CheckoutConsole
{
    static class ShoppingController
    {
        public static List<Article> ListArticles()
        {
            // Mockup list:

            List<Article> Articles = new List<Article>();

            Console.WriteLine();
            Articles.Add(new Article(1, "Sweater", "L", "green", 59.95));
            Articles.Add(new Article(2, "T-Shirt", "M", "red", 22.99));
            Articles.Add(new Article(3, "Hat", "M", "orange", 35.00));
            Articles.Add(new Article(4, "Pants", "XL", "blue", 64.99));

            return Articles;
        }

        public static InvoiceItem AddArticle()
        {
            List<Article> articles = ListArticles();
            Article? chosenArticle = null;
            List<InvoiceItem> invoiceItems = Program.invoiceItems;
            InvoiceItem? chosenInvoiceItem = null;

            ShowArticles();

            Console.WriteLine("Please enter the Id of your desired article: ");
            string? articleChoice = Console.ReadLine();

            Console.WriteLine("How many copys of this article do you want?");
            string? countChoice = Console.ReadLine();
            int numberOfArticles = Convert.ToInt32(countChoice);                        

            if (Int32.TryParse(articleChoice, out int articleId))
            {
                chosenArticle = articles.First(article => article.ArticleId == articleId);

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\nThe following article was added to your shopping basket: ");
                Console.WriteLine($"ArticleId: {chosenArticle.ArticleId}   Article Name: {chosenArticle.ArticleName}   Number of articles: {numberOfArticles}");
                Console.ForegroundColor = ConsoleColor.White;

                int invoiceItemsIndex = 0;

                if (invoiceItems == null)
                {
                    chosenInvoiceItem = new InvoiceItem(1, chosenArticle, numberOfArticles);
                }
                else
                {
                    chosenInvoiceItem = new InvoiceItem(invoiceItemsIndex = invoiceItems.Count + 1, chosenArticle, numberOfArticles);
                }                                
            }
            else
            {
                Console.WriteLine($"Int32.TryParse could not parse '{articleChoice}' to an integer.");
            }

            return chosenInvoiceItem;
        }

        public static InvoiceItem ChooseInvoiceItem()
        {
            string? invoiceItemPositionChoice;
            InvoiceItem? chosenInvoiceItem = null;

            Console.WriteLine("\nPlease enter the position of the invoice item you want to overwrite: ");
            invoiceItemPositionChoice = Console.ReadLine();

            chosenInvoiceItem = Program.invoiceItems.First(invoiceItem => invoiceItem.ItemPosition == Convert.ToInt32(invoiceItemPositionChoice));

            return chosenInvoiceItem;
        }

        public static InvoiceItem OverwriteInvoiceItem()
        {
            InvoiceItem chosenInvoiceItem = ChooseInvoiceItem();

            Console.WriteLine("How many copys of this article do you want?");
            string? countChoice = Console.ReadLine();

            if (Int32.TryParse(countChoice, out int numberOfArticles))
            {
                chosenInvoiceItem.NumberOfArticles = numberOfArticles;
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"You have changed the number of articles of the invoice item with the position {chosenInvoiceItem.ItemPosition} ({chosenInvoiceItem.Article.ArticleName}) to {chosenInvoiceItem.NumberOfArticles}.");
                Console.ForegroundColor = ConsoleColor.White;
                Program.invoiceItems.Remove(chosenInvoiceItem);
                Program.invoiceItems.Add(chosenInvoiceItem);
            }
            else
            {
                Console.WriteLine($"Int32.TryParse could not parse '{countChoice}' to an integer.");
            }

            return chosenInvoiceItem;
        }

        public static void RemoveInvoiceItem()
        {
            InvoiceItem chosenInvoiceItem = ChooseInvoiceItem();

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"The invoice item {chosenInvoiceItem.Article.ArticleName} at item position {chosenInvoiceItem.ItemPosition} was deleted. ");
            Console.ForegroundColor = ConsoleColor.White;
            Program.invoiceItems.Remove(chosenInvoiceItem);
        }

        public static void ShowArticles()
        {
            List<Article> Articles = ListArticles();

            foreach (Article article in Articles)
            {
                Console.WriteLine("ArticleId: " + article.ArticleId);
                Console.WriteLine("Article name: " + article.ArticleName);
                Console.WriteLine("Article size: " + article.ArticleSize);
                Console.WriteLine("Article color: " + article.ArticleColor);
                Console.WriteLine("Article price: " + article.ArticlePrice + "\n");
            }
        }

        public static List<Customer> ListCustomers()
        {
            // Mockup list:

            List<Customer> Customers = new List<Customer>();

            Customers.Add(new Customer(1, "Donnie McKenzie", new PaymentMethod(1, "bank transfer", "0123456789"), new Address("Elm Street", "111", "59ES1", "Alderson", "Canada")));
            Customers.Add(new Customer(2, "John Smith", new PaymentMethod(2, "PayPal", "john.smith@yahoo.com"), new Address("Main Street", "4711", "68004", "Chicago", "USA")));
            Customers.Add(new Customer(3, "Alfons Schmitz", new PaymentMethod(3, "Apple Pay", "a.schmitz@mail.de"), new Address("Waldweg", "1", "37825", "Buxtehude", "Deutschland")));

            return Customers;
        }

        public static Customer ChooseCustomer(List<Customer> Customers)
        {
            Console.WriteLine("\nPlease enter your Customer Id: ");
            string? customerChoice = Console.ReadLine();
            Customer? chosenCustomer = null;

            if (Int32.TryParse(customerChoice, out int customerId))
            {
                if (customerId > Customers.Count || customerId < 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Error: There is no Customer with an Id of {customerId} in our database. ");
                    Console.ForegroundColor = ConsoleColor.White;
                    QuitProgram();
                }   
                else
                {
                    chosenCustomer = Customers.First(customer => customer.CustomerId == customerId);

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"You registered as our Customer {chosenCustomer.CustomerName}.");
                    Console.ForegroundColor = ConsoleColor.White;
                }                
            }
            else
            {
                chosenCustomer = null;
                Console.WriteLine($"Int32.TryParse could not parse '{customerChoice}' to an integer.");
            }            

            return chosenCustomer;
        }

        public static double ShowShoppingBasket()
        {
            List<InvoiceItem> InvoiceItems = Program.invoiceItems;
            double invoiceTotal = 0.0;

            Console.ForegroundColor = ConsoleColor.Green;

            foreach (InvoiceItem invoiceItem in InvoiceItems.OrderBy(invoiceItem => invoiceItem.ItemPosition))
            {
                Console.WriteLine("\nItem position: " + invoiceItem.ItemPosition);
                Console.WriteLine("Article name: " + invoiceItem.Article.ArticleName);
                Console.WriteLine("Price per article: " + invoiceItem.Article.ArticlePrice);
                Console.WriteLine("Number of articles: " + invoiceItem.NumberOfArticles);
                Console.WriteLine("Invoice item price: " + String.Format("{0:.##}", invoiceItem.Article.ArticlePrice * invoiceItem.NumberOfArticles) + "\n");

                invoiceTotal += invoiceItem.Article.ArticlePrice * invoiceItem.NumberOfArticles;
            }

            Console.ForegroundColor = ConsoleColor.White;

            return invoiceTotal;
        }

        public static List<PaymentMethod> ListPaymentMethods()
        {
            // Mockup list:

            List<PaymentMethod> PaymentMethods = new List<PaymentMethod>();

            PaymentMethods.Add(new PaymentMethod(1, "Bank transfer", "Bank account number"));
            PaymentMethods.Add(new PaymentMethod(2, "PayPal", "Email address"));
            PaymentMethods.Add(new PaymentMethod(3, "Apple Pay", "Account name"));

            return PaymentMethods;
        }

        public static List<PaymentMethod> ShowPaymentMethods()
        {
            List<PaymentMethod> PaymentMethods = ListPaymentMethods();
            
            foreach (PaymentMethod paymentMethod in PaymentMethods)
            {
                Console.WriteLine("\nPayment method Id: " + paymentMethod.PaymentMethodId);
                Console.WriteLine("Payment method name: " + paymentMethod.PaymentMethodName);
                Console.WriteLine("Payment method identifier: " + paymentMethod.PaymentMethodIdentifier);
            }

            return PaymentMethods;
        }

        public static PaymentMethod ChoosePaymentMethod()
        {
            List<PaymentMethod> PaymentMethods = ShowPaymentMethods();

            Console.WriteLine("\nPlease enter your Payment method Id: ");
            string? paymentMethodChoice = Console.ReadLine();
            PaymentMethod? chosenPaymentMethod = null;

            if (Int32.TryParse(paymentMethodChoice, out int paymentMethodId))
            {
                if (paymentMethodId > PaymentMethods.Count || paymentMethodId < 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Error: There is no payment method with an Id of {paymentMethodId} in our database. ");
                    Console.ForegroundColor = ConsoleColor.White;
                    QuitProgram();
                }
                else
                {
                    chosenPaymentMethod = PaymentMethods.First(paymentMethod => paymentMethod.PaymentMethodId == paymentMethodId);

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"\nYou chose {chosenPaymentMethod.PaymentMethodName} as your payment method.");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("\nPlease enter your payment method identifier: ");
                    chosenPaymentMethod.PaymentMethodIdentifier = Console.ReadLine();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"Your payment method identifier {chosenPaymentMethod.PaymentMethodIdentifier} was saved. ");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine($"\nYou will use this payment method to pay the invoice total of {String.Format("{0:.##}", Program.invoiceTotal)}.");
                }
            }
            else
            {
                Console.WriteLine($"Int32.TryParse could not parse '{paymentMethodChoice}' to an integer.");
            }

            return chosenPaymentMethod;
        }

        public static void WrongEntry()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Error: Please enter only the letters which are allowed. ");
            Console.ForegroundColor = ConsoleColor.White;
            QuitProgram();
        }

        public static void QuitProgram()
        {
            Console.WriteLine("\nPress any key to quit the program.");
            Console.ReadKey();
            Environment.Exit(0);
        }
    }
}