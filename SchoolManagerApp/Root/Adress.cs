namespace SchoolManager
{
    public class Address
    {
        public int StreetNumber { get; set; }
        public string StreetName { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }

        public Address(int streetNumber, string streetName, string city, string province, string postalCode, string country)
        {
            this.StreetNumber = streetNumber;
            this.StreetName = streetName;
            this.City = city;
            this.Province = province;
            this.PostalCode = postalCode;
            this.Country = country;
        }

        public override string ToString()
        {
            return $"{StreetNumber} {StreetName}, {City}, {Province}, {PostalCode}, {Country}";
        }
    }
}