using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManager
{
    public class Receptionist : SchoolEmployee, IPayroll
    {
        private const int DefaultIncome = 10000;

        public event EventHandler<Complaint>? ComplaintRaised;

        public Receptionist(string name, Address address, string phoneNumber, int income = DefaultIncome)
            : base(name, address, phoneNumber)
        {
            Income = income;
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

        public override string ToString()
        {
            return $"Receptionist: {Name}, Address: {Address}, Phone: {PhoneNumber}, Income: {Income}, Total Earnings: {TotalEarnings}";
        }
    }
}
