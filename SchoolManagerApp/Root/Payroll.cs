namespace SchoolManager
{
    public interface IPayroll
    {
        Task PayAsync(); // Only async method
        int GetBalance(); // Pour avoir la balance avant le paiement
        void ResetBalance(int balance); // Pour undo action
    }
}
