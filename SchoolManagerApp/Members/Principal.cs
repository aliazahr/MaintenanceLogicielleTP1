using System;

namespace SchoolManager
{
    public class Principal : SchoolEmployee
    {
        private const int DefaultIncome = 50000;
        private int _income;
        protected override int Income => _income;

        public Principal(string name, Address address, string phoneNumber, int income = DefaultIncome)
            : base(name, address, phoneNumber, income)
        {
            if (income < 0)
                throw new ArgumentOutOfRangeException(nameof(income), "Income cannot be negative.");
            _income = income;
        }

        public override string ToString()
        {
            return $"Principal: {Name}, Address: {Address}, Phone: {PhoneNumber}, Income: {Income}, Total Earnings: {TotalEarnings}";
        }
    }
}
