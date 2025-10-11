namespace SchoolManager
{
    public class SchoolMember
    {
        private string _name;
        private Address? _address;
        private string _phone;

        public SchoolMember(string name = "", Address? address = null, string phone = "")
        {
            Name = name;
            Address = address;
            Phone = phone;
        }

        public string Name
        {
            get => _name;
            set => _name = value;
        }

        public Address? Address
        {
            get => _address;
            set => _address = value;
        }

        public string Phone
        {
            get => _phone;
            set => _phone = value;
        }
    }
} 