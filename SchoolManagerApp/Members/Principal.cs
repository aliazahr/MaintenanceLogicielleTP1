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
            try
            {
                if (income < 0)
                {
                    throw new InvalidOperationException("Principal income cannot be negative.");
                }

                if (balance < 0)
                {
                    throw new InvalidOperationException("Principal balance cannot be negative before payment.");
                }

                int oldBalance = balance;
                balance = await Util.NetworkDelay.PayEntityAsync(balance, income);

                if (balance < oldBalance)
                {
                    throw new InvalidOperationException("Balance decreased after payment.");
                }

                Console.WriteLine($"\nPaid Principal: {Name}. Total balance: {balance}");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Payment failed for principal {Name}. Error: {ex.Message}");
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Payment failed for principal {Name}. Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected payment error occurred while paying Principal: {Name}. Error: {ex.Message}");
                throw new InvalidOperationException("Unexpected error during principal payment.", ex);
            }
        }
    }
}
