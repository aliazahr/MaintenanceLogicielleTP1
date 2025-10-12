using System;
using System.Diagnostics.CodeAnalysis;

namespace SchoolManager
{
    public class Principal : SchoolEmployee
    {
        private const int DefaultIncome = 50000;
        private int _income;
        public override int Income => _income;

        [SetsRequiredMembers] // To indicate that this constructor sets all required members and can use constructor instead of object initializer
        public Principal(string name, Address address, string phoneNumber, int? income = null)
            : base(name, address, phoneNumber, income ?? DefaultIncome)
        {
            if (income < 0)
                throw new ArgumentOutOfRangeException(nameof(income), "Income cannot be negative.");
            _income = income ?? DefaultIncome;
        }

        public override string ToString()
        {
            return $"Principal: {Name}, Address: {Address}, Phone: {PhoneNumber}, Income: {Income}, Total Earnings: {TotalEarnings}";
        }
    }
}
