namespace CheckoutConsole.Models
{
    public class Customer
    {
        //[Key]
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public Address Address { get; set; }

        public Customer(int customerId, string customerName, PaymentMethod paymentMethod, Address address)
        {
            this.CustomerId = customerId;
            this.CustomerName = customerName;
            this.PaymentMethod = paymentMethod;
            this.Address = address;
        }
    }
}
