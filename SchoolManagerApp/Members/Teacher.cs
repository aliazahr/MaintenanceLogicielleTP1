using System;

namespace SchoolManager
{
    public class Teacher : SchoolEmployee, IPayroll
    {
        private string _subject;
        private int _income;

        public Teacher(string name, Address? address, int phoneNum, string subject = "", int income = 25000)
        {
            Name = name;
            Address = address  ?? throw new ArgumentNullException(nameof(address));
            Phone = phoneNum;
            Subject = subject;
            this.income = income;
            balance = 0;
        }

        public void display()
        {
            Console.WriteLine("Name: {0}, Address: {1}, Phone: {2}, Subject: {3}", Name, Address, Phone, Subject);
        }
    }
}
