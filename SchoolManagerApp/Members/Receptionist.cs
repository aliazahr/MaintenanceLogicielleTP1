using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManager
{
    public class Receptionist : SchoolMember, IPayroll
    {
        private const int DefaultIncome = 10000;
        private const int InitialEarningsBalance = 0;

        private int _income;
        private int _totalEarnings;

        public event EventHandler<Complaint>? ComplaintRaised;

        public Receptionist(string name, Address address, string phoneNumber, int income = DefaultIncome)
            : base(name, address, phoneNumber)
        {
            Income = income;
            TotalEarnings = InitialEarningsBalance;
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
                    throw new InvalidOperationException("Receptionist income cannot be negative.");
                }

                if (_totalEarnings < 0)
                {
                    throw new InvalidOperationException("Receptionist total earnings cannot be negative before payment.");
                }

                int oldTotalEarnings = _totalEarnings;
                _totalEarnings = await Util.NetworkDelay.PayEntityAsync(_totalEarnings, _income);

                if (_totalEarnings < oldTotalEarnings)
                {
                    throw new InvalidOperationException("Total earnings decreased after payment.");
                }

                Console.WriteLine($"\nPaid Receptionist: {Name}. Total earnings: {_totalEarnings}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nPayment failed for receptionist {Name}. Error: {ex.Message}");
            }
        }

        public void HandleComplaint(string complaintText)
        {
            Complaint complaint = new Complaint();
            complaint.ComplaintTime = DateTime.Now;
            complaint.ComplaintRaised = complaintText;

            ComplaintRaised?.Invoke(this, complaint);
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

        public override string ToString()
        {
            return $"Receptionist: {Name}, Address: {Address}, Phone: {PhoneNumber}, Income: {Income}, Total Earnings: {TotalEarnings}";
        }
    }
}
