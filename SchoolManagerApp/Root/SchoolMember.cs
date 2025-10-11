namespace SchoolManager
{
    public class SchoolMember
    {
        public string Name;
        public Address Address;
        private int phone;

        public SchoolMember(string name = "", Address? address = null, int phone = 0)
        {
            Name = name;
            Address = address ?? throw new ArgumentNullException(nameof(address));
            this.phone = phone;
        }

        public int Phone
        {
            get { return phone; }
            set { phone = value; }
        }
    }
}
