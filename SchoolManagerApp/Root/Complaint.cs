namespace SchoolManager
{
       public class Complaint : EventArgs
    {
        public DateTime ComplaintTime { get; set; }
        public string ComplaintRaised { get; set; } = string.Empty;
    }
}