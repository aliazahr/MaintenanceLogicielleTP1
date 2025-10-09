using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManager
{
    public class Complaint : EventArgs
    {
        public DateTime ComplaintTime { get; set; }
        public string ComplaintRaised { get; set; } = string.Empty;
    }

    public class Receptionist : SchoolMember, IPayroll
    {
        private int income;
        private int balance;
        public event EventHandler<Complaint>? ComplaintRaised;

        public Receptionist(int income = 10000) 
        {
            this.income = income;
            balance = 0;
        }

        public Receptionist(string name, Address? address, int phoneNum, int income = 10000)
        {
            Name = name;
            Address = address  ?? throw new ArgumentNullException(nameof(address));
            Phone = phoneNum;
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

        public void Display()
        {
            Console.WriteLine("Name: {0}, Address: {1}, Phone: {2}", Name, Address, Phone);
        }

        public async Task PayAsync()
        {
            try
            {
                if (income < 0)
                {
                    throw new InvalidOperationException("Receptionist income cannot be negative.");
                }

                if (balance < 0)
                {
                    throw new InvalidOperationException("Receptionist balance cannot be negative before payment.");
                }

                int oldBalance = balance;
                balance = await Util.NetworkDelay.PayEntityAsync(balance, income);

                if (balance < oldBalance)
                {
                    throw new InvalidOperationException("Balance decreased after payment.");
                }

                Console.WriteLine($"\nPaid Receptionist: {Name}. Total balance: {balance}");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"\nPayment failed for receptionist {Name}. Error: {ex.Message}");
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"\nPayment failed for receptionist {Name}. Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nUnexpected payment error occurred while paying Receptionist: {Name}. Error: {ex.Message}");
                throw new InvalidOperationException("Unexpected error during receptionist payment.", ex);
            }
        }

        public void HandleComplaint(string complaintText)
        {
            Complaint complaint = new Complaint();
            complaint.ComplaintTime = DateTime.Now;
            complaint.ComplaintRaised = complaintText;

            ComplaintRaised?.Invoke(this, complaint);
        }
    }
}
