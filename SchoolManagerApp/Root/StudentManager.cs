namespace SchoolManager
{
    public class StudentManager 
    {
        public static double CalculateAverageGrade(IEnumerable<Student> students)
        {
            if (students == null || !students.Any())
                throw new ArgumentException("Student list cannot be null or empty.");

            double total = 0;
            int count = 0;

            foreach (var student in students)
            {
                if (student.Grade.HasValue)
                {
                    total += student.Grade.Value;
                    count++;
                }
            }

            if (count == 0)
                throw new InvalidOperationException("No students with grades available to calculate average.");

            return total / count;
        }
    }
}