namespace CheckoutConsole.Models
{
    public class PaymentMethod
    {
        //[Key]
        public int PaymentMethodId { get; set; }
        public string PaymentMethodName { get; set; }
        public string PaymentMethodIdentifier { get; set; }
        
        public PaymentMethod(int paymentMethodId, string paymentMethodName, string paymentMethodIdentifier)
        {
            this.PaymentMethodId = paymentMethodId;
            this.PaymentMethodName = paymentMethodName;
            this.PaymentMethodIdentifier = paymentMethodIdentifier;
        }

        public override string ToString()
        {
            return $"\nPayment method: {PaymentMethodName}\nIdentifier: {PaymentMethodIdentifier}\n";
        }
    }
}
