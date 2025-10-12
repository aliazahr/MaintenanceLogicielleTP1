using System;

namespace SchoolManager
{
    public class Teacher : SchoolEmployee, IPayroll
    {
        private string _subject;
        private int _income;

        public Teacher(string name, Address address, string phoneNumber, string subject = "", int income = 25000) 
            : base(name, address, phoneNumber, income)
        {
            Subject = subject;
            Income = income;
        }
        public string Subject
        {
            get => _subject;
            set { _subject = value; }
        }   

        public int Income
        {
            get => _income;
            private set { _income = value; }
        }

        public override string ToString()
        {
            return $"Teacher: {Name}, Address: {Address}, Phone: {PhoneNumber}, Subject: {Subject}, Income: {Income}, Total Earnings: {TotalEarnings}";
        }
    }
}
