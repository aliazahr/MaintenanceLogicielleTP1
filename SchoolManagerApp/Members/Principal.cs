using System;

namespace SchoolManager
{
    public class Principal : SchoolMember, IPayroll
    {
        private const int DefaultIncome = 50000;
        private const int InitialEarningsBalance = 0;

        private int _income;
        private int _totalEarnings;

        public Principal(string name, Address address, string phoneNumber, int income = DefaultIncome)
            : base(name, address, phoneNumber)
        {
            _income = income;
            _totalEarnings = InitialEarningsBalance;
        }

        public void ResetTotalEarnings(int totalEarnings)
        {
            _totalEarnings = totalEarnings;
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
            catch (Exception ex)
            {
                Console.WriteLine($"\nPayment failed for principal {Name}. Error: {ex.Message}");
            }
        }

        public int TotalEarnings
        {
            get => _totalEarnings;
            set { _totalEarnings = value; }
        }   

        public int Balance
        {
            get => _balance;
            set { _balance = value; }
        }

        public override string ToString()
        {
            return $"Principal: {Name}, Address: {Address}, Phone: {PhoneNumber}, Income: {Income}, Balance: {Balance}";
        }
    }
}
