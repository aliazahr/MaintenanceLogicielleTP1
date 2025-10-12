using System.Diagnostics.CodeAnalysis;

namespace SchoolManager
{
    public class SchoolMember
    {
        // Fields start as null but will be required to be set in constructor
        private string _name = null!;
        private Address _address = null!;
        private string _phoneNumber = null!;

        [SetsRequiredMembers] // To indicate that this constructor sets all required members and can use constructor instead of object initializer
        public SchoolMember(string name, Address address, string phoneNumber)
        {
            Name = name;
            Address = address;
            PhoneNumber = phoneNumber;
        }

        public required string Name
        {
            get => _name;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Name cannot be empty.");
                _name = value;
            }
        }

        public required Address Address
        {
            get => _address;
            set
            {
                if (value == null)
                    throw new ArgumentNullException("Address cannot be null.");
                _address = value;
            }
        }

        public required string PhoneNumber
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