namespace CheckoutConsole.Models
{
    public class Address
    {
        public string Street { get; set; }
        public string HouseNumber { get; set; }
        public string PostCode { get; set; }
        public string City { get; set; }
        public string Country { get; set; }

        public Address(string street, string houseNumber, string postCode, string city, string country)
        {
            this.Street = street;
            this.HouseNumber = houseNumber;
            this.PostCode = postCode;
            this.City = city;
            this.Country = country;
        }

        public override string ToString()
        {
            return $"\n{Street} {HouseNumber}\n{PostCode} {City}\n{Country}\n";
        }
    }
}
