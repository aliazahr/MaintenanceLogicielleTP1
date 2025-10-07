namespace SchoolManager
{
    public class Address
    {
        public int streetNumber { get; set; }
        public string streetName { get; set; }
        public string city { get; set; }
        public string province { get; set; }
        public string postalCode { get; set; }
        public string country { get; set; }

        public Address(int streetNumber, string streetName, string city, string province, string postalCode, string country)
        {
            this.streetNumber = streetNumber;
            this.streetName = streetName;
            this.city = city;
            this.province = province;
            this.postalCode = postalCode;
            this.country = country;
        }

        public override string ToString()
        {
            return $"{streetNumber} {streetName}, {city}, {province}, {postalCode}, {country}";
        }
    }
}