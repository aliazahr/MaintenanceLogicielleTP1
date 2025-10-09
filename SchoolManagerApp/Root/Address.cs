namespace SchoolManager
{
    public class Address
    {
        public int StreetNumber { get; set; }
        public string StreetName { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Province { get; set; } = string.Empty;
        public string PostalCode { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;

        public Address(int streetNumber, string streetName, string city, string province, string postalCode, string country)
        {
            this.StreetNumber = streetNumber;
            this.StreetName = streetName ?? throw new ArgumentNullException(nameof(streetName));
            this.City = city ?? throw new ArgumentNullException(nameof(city));
            this.Province = province ?? throw new ArgumentNullException(nameof(province));
            this.PostalCode = postalCode ?? throw new ArgumentNullException(nameof(postalCode));
            this.Country = country ?? throw new ArgumentNullException(nameof(country));
        }

        public override string ToString()
        {
            return $"{StreetNumber} {StreetName}, {City}, {Province}, {PostalCode}, {Country}";
        }
    }
}