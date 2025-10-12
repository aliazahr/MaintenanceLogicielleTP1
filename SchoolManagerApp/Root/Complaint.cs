namespace SchoolManager
{
    public class ComplaintEventArgs : EventArgs
    {
        public DateTime ComplaintTime { get; }
        public string ComplaintText { get; set; } = string.Empty;

        public ComplaintEventArgs(string complaintText)
        {
            ComplaintTime = DateTime.Now;
            ComplaintText = complaintText;
        }

        public override string ToString()
        {
            return $"Complaint raised at {ComplaintTime}: {ComplaintText}";
        }
    }
}