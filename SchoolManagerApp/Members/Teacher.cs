using System;

namespace SchoolManager
{
    public class Teacher : SchoolMember, IPayroll
    {
        public string Subject;
        private int income;
        private int balance;

        public Teacher(string name, Address? address, int phoneNum, string subject = "", int income = 25000)
        {
            Name = name;
            Address = address!;
            Phone = phoneNum;
            Subject = subject;
            this.income = income;
            balance = 0;
        }

        public void display()
        {
            Console.WriteLine("Name: {0}, Address: {1}, Phone: {2}, Subject: {3}", Name, Address, Phone, Subject);
        }

        public async Task PayAsync()
        {
            balance = await Util.NetworkDelay.PayEntityAsync(balance, income);
            Console.WriteLine($"\nPaid Teacher: {Name}. Total balance: {balance}");
        }
    }
}
