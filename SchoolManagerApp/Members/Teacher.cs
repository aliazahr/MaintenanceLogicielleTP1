using System;

namespace SchoolManager
{
    public class Teacher : SchoolEmployee, IPayroll
    {
        private const int DefaultIncome = 25000;
        private string _subject;
        private int _income;

        public Teacher(string name, Address address, string phoneNumber, string subject, int income = 25000) 
            : base(name, address, phoneNumber, income)
        {
            Subject = subject;
            Income = income;
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

        public int Income
        {
            get => _income;
            private set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException(nameof(value), "Income cannot be negative.");
                _income = value;
            }
        }

        public override string ToString()
        {
            return $"Teacher: {Name}, Address: {Address}, Phone: {PhoneNumber}, Subject: {Subject}, Income: {Income}, Total Earnings: {TotalEarnings}";
        }
    }
}
