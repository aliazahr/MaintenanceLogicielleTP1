namespace SchoolManager
{
    // Interface pour les actions annulables
    public interface IUndoableAction
    {
        void Execute(); // Pour exécuter l'action
        void Undo(); // Pour annuler l'action
        string Description { get; } // Description de l'action
    }

    // Pour l'ajout des étudiants
    public class AddStudentAction : IUndoableAction
    {
        private List<Student> students; // Référence à la liste des étudiants
        private Student student; // Étudiant ajouté

        public AddStudentAction(List<Student> students, Student student)
        {
            this.students = students;
            this.student = student;
        }

        // Ajout de l'étudiant à la liste
        public void Execute()
        {
            students.Add(student);
        }

        // Suppression de l'étudiant de la liste
        public void Undo()
        {
            students.Remove(student);
        }

        public string Description => $"Add Student: {student.Name}";
    }


    // Pour l'ajout des enseignants
    public class AddTeacherAction : IUndoableAction
    {
        private List<Teacher> teachers;
        private Teacher teacher;

        public AddTeacherAction(List<Teacher> teachers, Teacher teacher)
        {
            this.teachers = teachers;
            this.teacher = teacher;
        }

        public void Execute()
        {
            teachers.Add(teacher);
        }

        public void Undo()
        {
            teachers.Remove(teacher);
        }

        public string Description => $"Add Teacher: {teacher.Name}";
    }

    // Pour l'ajout des complaintes
    public class ComplaintAction : IUndoableAction
    {
        private string complaint;

        public ComplaintAction(string complaint)
        {
            this.complaint = complaint;
        }

        public void Execute()
        {
            // Complainte déjà soumise
        }

        public void Undo()
        {
            Console.WriteLine($"Complaint removed: {complaint}");
        }

        public string Description => $"Raise Complaint: {complaint}";
    }


    // Paiement des professeurs
    public class PayTeachersAction : IUndoableAction
    {
        private List<Teacher> teachers;
        private Dictionary<Teacher, int> previousBalances; // balance avant paiement

        public PayTeachersAction(List<Teacher> teachers, Dictionary<Teacher, int> previousBalances)
        {
            this.teachers = teachers;
            this.previousBalances = previousBalances;
        }

        public void Execute()
        {
            // Paiement déjà effectué
        }

        public void Undo()
        {
            // Rétablir les soldes précédents pour chaque enseignant
            foreach (var teacher in teachers)
            {
                if (previousBalances.ContainsKey(teacher))
                {
                    teacher.ResetBalance(previousBalances[teacher]);
                    Console.WriteLine($"Payment reversed for Teacher: {teacher.Name}. Balance reset to {previousBalances[teacher]}.");
                }
            }

            Console.WriteLine($"All {teachers.Count} teacher payments have been reversed.");
        }
        public string Description => $"Payment to all {teachers.Count} teachers";
    }

    
    // Pour les paiements du Principal et Receptionist
    public class PaymentAction : IUndoableAction
    {
        private readonly IPayroll member; // Membre payé
        private readonly int previousBalance; // Solde avant paiement
        private readonly string description; // Description de l'action

        public PaymentAction(IPayroll member, int previousBalance, string description)
        {
            this.member = member;
            this.previousBalance = previousBalance;
            this.description = description;
        }
        public void Execute()
        {
            // Paiement déjà effectué
        }

        public void Undo()
        {
            // Rétablir le balance précédent
            member.ResetBalance(previousBalance);
            Console.WriteLine($"Payment reversed: {description}. Balance reset to {previousBalance}.");
        }

        public string Description => description;
    }
}
