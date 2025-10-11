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

        public void ResetTotalEarnings()
        {
            _totalEarnings = InitialEarningsBalance;
        }

        public async Task PayAsync()
        {
            try
            {
                if (_income < 0)
                {
                    throw new InvalidOperationException("Principal income cannot be negative.");
                }

                if (_totalEarnings < 0)
                {
                    throw new InvalidOperationException("Principal total earnings cannot be negative before payment.");
                }

                int oldEarningBalance = _totalEarnings;
                _totalEarnings = await Util.NetworkDelay.PayEntityAsync(_totalEarnings, _income);

                if (_totalEarnings < oldEarningBalance)
                {
                    throw new InvalidOperationException("Total earnings decreased after payment.");
                }

                Console.WriteLine($"\nPaid Principal: {Name}. Total earnings: {_totalEarnings}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nPayment failed for principal {Name}. Error: {ex.Message}");
            }
        }

        public int TotalEarnings
        {
            get => _totalEarnings;
            private set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException(nameof(value), "Total earnings cannot be negative.");
                _totalEarnings = value;
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
            return $"Principal: {Name}, Address: {Address}, Phone: {PhoneNumber}, Income: {Income}, Total Earnings: {TotalEarnings}";
        }
    }
}
