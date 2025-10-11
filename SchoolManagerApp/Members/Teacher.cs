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
            Address = address  ?? throw new ArgumentNullException(nameof(address));
            Phone = phoneNum;
            Subject = subject;
            this.income = income;
            balance = 0;
        }

        public int GetBalance()
        {
            return balance;
        }

        public void ResetBalance(int balance)
        {
            this.balance = balance;
        }

        public void display()
        {
            Console.WriteLine("Name: {0}, Address: {1}, Phone: {2}, Subject: {3}", Name, Address, Phone, Subject);
        }

        public async Task PayAsync()
        {
            try
            {
                if (income < 0)
                {
                    throw new InvalidOperationException("Teacher income cannot be negative.");
                }

                if (balance < 0)
                {
                    throw new InvalidOperationException("Teacher balance cannot be negative before payment.");
                }

                int oldBalance = balance;
                balance = await Util.NetworkDelay.PayEntityAsync(balance, income);

                if (balance < oldBalance)
                {
                    throw new InvalidOperationException("Balance decreased after payment.");
                }

                Console.WriteLine($"\nPaid Teacher: {Name}. Total balance: {balance}");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"\nPayment failed for teacher {Name}. Error: {ex.Message}");
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"\nPayment failed for teacher {Name}. Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nUnexpected payment error occurred while paying Teacher: {Name}. Error: {ex.Message}");
                throw new InvalidOperationException("Unexpected error during teacher payment.", ex);
            }
        }
    }
}
