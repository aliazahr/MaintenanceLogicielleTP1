using System;
using System.Threading.Tasks;

namespace SchoolManager
{
    public abstract class SchoolEmployee : SchoolMember, IPayroll
    {
        protected abstract int Income { get; }
        private const int InitialEarnings = 0;

        private int _totalEarnings;
    
        protected SchoolEmployee(string name, Address address, string phoneNumber, int income)
            : base(name, address, phoneNumber)
        {
            TotalEarnings = InitialEarnings;
        }

        public async Task PayAsync()
        {
            try
            {
                if (Income < 0)
                    throw new InvalidOperationException($"{GetType().Name} income cannot be negative.");

                if (_totalEarnings < 0)
                    throw new InvalidOperationException($"{GetType().Name} total earnings cannot be negative before payment.");

                int oldEarnings = _totalEarnings;
                _totalEarnings = await Util.NetworkDelay.PayEntityAsync(_totalEarnings, Income);

                if (_totalEarnings < oldEarnings)
                    throw new InvalidOperationException("Total earnings decreased after payment.");

                Console.WriteLine($"\nPaid {GetType().Name}: {Name}. Total earnings: {_totalEarnings}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nPayment failed for {GetType().Name} {Name}. Error: {ex.Message}");
            }
        }

        public void ResetBalance(int previousBalance)
        {
            _totalEarnings = previousBalance;
        }

        public int GetBalance() => TotalEarnings;

        public int TotalEarnings
        {
            get => _totalEarnings;
            protected set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException(nameof(value), "Total earnings cannot be negative.");
                _totalEarnings = value;
            }
        }
    }
}
