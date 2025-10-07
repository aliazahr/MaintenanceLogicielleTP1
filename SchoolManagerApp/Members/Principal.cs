using System;

namespace SchoolManager
{
    public class Principal : SchoolMember, IPayroll
    {
        private int income;
        private int balance;

        public Principal(int income = 50000)
        {
            this.income = income;
            balance = 0;
        }

        public Principal(string name, Address? address, int phoneNum, int income = 50000)
        {
            Name = name;
            Address = address!;
            Phone = phoneNum;
            this.income = income;
            balance = 0;
        }

        public void display()
        {
            Console.WriteLine("Name: {0}, Address: {1}, Phone: {2}", Name, Address, Phone);
        }

        public async Task PayAsync()
        {
            balance = await Util.NetworkDelay.PayEntityAsync(balance, income);
            Console.WriteLine($"\nPaid Principal: {Name}. Total balance: {balance}");
        }
    }
}
