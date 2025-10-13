namespace SchoolManager
{
    public class Address
    {
        private int _streetNumber;
        private string _streetName = string.Empty;
        private string _city       = string.Empty;
        private string _province   = string.Empty;
        private string _postalCode = string.Empty;
        private string _country    = string.Empty;

        public Address(int streetNumber, string streetName, string city, string province, string postalCode, string country)
        {
            StreetNumber = streetNumber;
            StreetName = streetName ?? throw new ArgumentNullException(nameof(streetName));
            City = city ?? throw new ArgumentNullException(nameof(city));
            Province = province ?? throw new ArgumentNullException(nameof(province));
            PostalCode = postalCode ?? throw new ArgumentNullException(nameof(postalCode));
            Country = country ?? throw new ArgumentNullException(nameof(country));
        }

        public int StreetNumber
        {
            get => _streetNumber;
            set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException(nameof(StreetNumber), "Street number must be positive.");
                _streetNumber = value;
            }
        }

        public string StreetName
        {
            get => _streetName;
            set
            {
                if (value is null) throw new ArgumentNullException(nameof(StreetName), "Value cannot be null.");
                if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("Value cannot be empty or whitespace.", nameof(StreetName));
                _streetName = value.Trim();
            }
        }

        public string City
        {
            get => _city;
            set
            {
                if (value is null) throw new ArgumentNullException(nameof(City), "Value cannot be null.");
                if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("Value cannot be empty or whitespace.", nameof(City));
                _city = value.Trim();
            }
        }

        public string Province
        {
            get => _province;
            set
            {
                if (value is null) throw new ArgumentNullException(nameof(Province), "Value cannot be null.");
                if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("Value cannot be empty or whitespace.", nameof(Province));
                _province = value.Trim();
            }
        }

        public string PostalCode
        {
            get => _postalCode;
            set
            {
                if (value is null) throw new ArgumentNullException(nameof(PostalCode), "Value cannot be null.");
                if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("Value cannot be empty or whitespace.", nameof(PostalCode));
                _postalCode = value.Trim();
            }
        }

        public string Country
        {
            get => _country;
            set
            {
                if (value is null) throw new ArgumentNullException(nameof(Country), "Value cannot be null.");
                if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("Value cannot be empty or whitespace.", nameof(Country));
                _country = value.Trim();
            }
        }

        public override string ToString()
        {
            return $"{_streetNumber} {_streetName}, {_city}, {_province}, {_postalCode}, {_country}";
        }
    }
}