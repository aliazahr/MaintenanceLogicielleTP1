namespace SchoolManager
{
    public class ComplaintEventArgs : EventArgs
    {
        public DateTime ComplaintTime { get; set; }
        public string ComplaintRaised { get; set; } = string.Empty;

        public ComplaintEventArgs(string complaintRaised)
        {
            ComplaintTime = DateTime.Now;
            ComplaintRaised = complaintRaised;
        }

        public override string ToString()
        {
            return $"Complaint raised at {ComplaintTime}: {ComplaintRaised}";
        }
    }
}