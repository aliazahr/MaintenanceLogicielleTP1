namespace SchoolManager
{
    public class SchoolMember
    {
        private string _name;
        private Address _address;
        private string _phoneNumber;

        public SchoolMember(string name, Address address, string phoneNumber)
        {
            Name = name;
            Address = address;
            PhoneNumber = phoneNumber;
        }

        public string Name
        {
            get => _name;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Name cannot be empty.");
                _name = value;
            }
        }

        public Address Address
        {
            get => _address;
            set
            {
                if (value == null)
                    throw new ArgumentNullException("Address cannot be null.");
                _address = value;
            }
        }

        public string PhoneNumber
        {
            get => _phoneNumber;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Phone cannot be empty.");
                if (value.Length < 8)
                    throw new ArgumentOutOfRangeException("Phone number seems to be short");

                _phoneNumber = value;
            }
        }
    }
}