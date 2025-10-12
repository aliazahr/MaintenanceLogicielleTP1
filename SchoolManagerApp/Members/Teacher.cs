using System;

namespace SchoolManager
{
    public class Teacher : SchoolEmployee
    {
        private const int DefaultIncome = 25000;
        private string _subject;
        private int _income;

        public Teacher(string name, Address address, string phoneNumber, int income = DefaultIncome) 
            : base(name, address, phoneNumber, income)
        {
            if (income < 0)
                throw new ArgumentOutOfRangeException(nameof(income), "Income cannot be negative.");
            _income = income;
        }

        public string Subject
        {
            get => _subject;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Subject cannot be empty.", nameof(value));
                _subject = value;
            }
        }   

        public override int Income
        {
            get => _income;
        }

        public override string ToString()
        {
            return $"Teacher: {Name}, Address: {Address}, Phone: {PhoneNumber}, Subject: {Subject}, Income: {Income}, Total Earnings: {TotalEarnings}";
        }
    }
}
