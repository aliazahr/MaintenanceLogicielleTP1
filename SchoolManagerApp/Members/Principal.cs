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

        public Principal(string name, string address, int phoneNum, int income = 50000)
        {
            Name = name;
            Address = address;
            Phone = phoneNum;
            this.income = income;
            balance = 0;
        }

        public void display()
        {
            Console.WriteLine("Name: {0}, Address: {1}, Phone: {2}", Name, Address, Phone);
        }

        public void Pay()
        {
            Util.NetworkDelay.PayEntity("Principal", Name, ref balance, income);
        }
    }
}
