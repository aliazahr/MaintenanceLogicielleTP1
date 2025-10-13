using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManager
{
    public class Receptionist : SchoolEmployee
    {
        private const int DefaultIncome = 10000;
        private int _income;
        public override int Income => _income;

        public event EventHandler<ComplaintEventArgs>? ComplaintRaised;

        [SetsRequiredMembers] // To indicate that this constructor sets all required members and can use constructor instead of object initializer
        public Receptionist(string name, Address address, string phoneNumber, int? income = null)
            : base(name, address, phoneNumber, income ?? DefaultIncome)
        {
            if (income < 0)
                throw new ArgumentOutOfRangeException(nameof(income), "Income cannot be negative.");
            _income = income ?? DefaultIncome;
        }

        public void HandleComplaint(string complaintText)
        {
            if (string.IsNullOrWhiteSpace(complaintText))
                throw new ArgumentException("Complaint cannot be empty.", nameof(complaintText));

            ComplaintEventArgs complaint = new ComplaintEventArgs(complaintText);
            ComplaintRaised?.Invoke(this, complaint);

            Console.WriteLine($"Complaint handled by {Name} at {complaint.ComplaintTime}: {complaint.ComplaintText}");
        }
        
        public override string ToString()
        {
            return $"Receptionist: {Name}, Address: {Address}, Phone: {PhoneNumber}, Income: {Income}, Total Earnings: {TotalEarnings}";
        }
    }
}
