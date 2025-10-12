using System;
using System.Threading.Tasks;

namespace SchoolManager
{
    public abstract class SchoolEmployee : SchoolMember, IPayroll
    {
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
                if (_income < 0)
                    throw new InvalidOperationException($"{GetType().Name} income cannot be negative.");

                if (_totalEarnings < 0)
                    throw new InvalidOperationException($"{GetType().Name} total earnings cannot be negative before payment.");

                int oldEarnings = _totalEarnings;
                _totalEarnings = await Util.NetworkDelay.PayEntityAsync(_totalEarnings, _income);

                if (_totalEarnings < oldEarnings)
                    throw new InvalidOperationException("Total earnings decreased after payment.");

                Console.WriteLine($"\nPaid {GetType().Name}: {Name}. Total earnings: {_totalEarnings}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nPayment failed for {GetType().Name} {Name}. Error: {ex.Message}");
            }
        }

        public void ResetEarnings()
        {
            _totalEarnings = InitialEarnings;
        }

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
