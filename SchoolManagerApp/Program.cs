using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualBasic;

namespace SchoolManager
{
    public class Program
    {
        static public List<Student> Students = new List<Student>();
        static public List<Teacher> Teachers = new List<Teacher>();
        static public Principal? Principal;
        static public Receptionist? Receptionist;

        enum SchoolMemberType
        {
            typePrincipal = 1,
            typeTeacher,
            typeStudent,
            typeReceptionist,
            cancelOperation = 5
        }

        public static SchoolMember AcceptAttributes()
        {
            try
            {
                string memberName = UserConsole.AskQuestionName("Enter name: ");
                Address memberAddress = UserConsole.AskQuestionAddress("--- Enter address ---");
                string memberPhoneNumber = UserConsole.AskQuestionPhoneNumber("\nEnter phone number: ");

                SchoolMember member = new SchoolMember(memberName, memberAddress, memberPhoneNumber);   
                
                return member;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("\nFailed to collect member attributes", ex);
            }
        }

        private static int AcceptChoices()
        {
            const int minInput = 1;
            const int maxInput = 7;
            Console.WriteLine("\n====== Menu ======");
            return UserConsole.AskQuestionMenu("1. Add\n2. Display\n3. Pay\n4. Raise Complaint\n5. Student Performance\n6. Undo last action\n7. Exit\nPlease enter your desired option: ", minInput, maxInput);
        }

        private static int AcceptMemberType()
        {
            const int minInput = 1;
            const int maxInput = 5;
            int x = UserConsole.AskQuestionMenu("\n1. Principal\n2. Teacher\n3. Student\n4. Receptionist\n5. Cancel\nPlease enter the member type: ", minInput, maxInput);

            return Enum.IsDefined(typeof(SchoolMemberType), x) ? x : -1;
        }

        public static void UndoLastAction()
        {
            Console.WriteLine("\n--- Undo Last Action ---");
            UndoManager.UndoLastAction();
        }

        public static void AddPrincpal()
        {
            try
            {
                SchoolMember member = AcceptAttributes();
                if (member == null)
                {
                    throw new InvalidOperationException("Failed to create principal with provided attributes");
                }

                Principal.Name = member.Name;
                Principal.Address = member.Address;
                Principal.PhoneNumber = member.PhoneNumber;
                Console.WriteLine($"\n--- Principal '{Principal.Name}' has been successfully added! ---");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nError while adding the principal: {ex.Message}");
            }
        }

        private static void AddStudent()
        {
            try
            {
                SchoolMember member = AcceptAttributes();
                if (member == null)
                {
                    Console.WriteLine("Failed to collect student information. Operation cancelled.");
                    return;
                }

                Student newStudent = new Student(member.Name, member.Address, member.PhoneNumber);
                newStudent.Grade = UserConsole.AskQuestionGrade("Enter grade: ");

                // Enregistrer l'action d'ajout pour pouvoir l'annuler plus tard
                var action = new AddStudentAction(Students, newStudent);

                // Exécuter l'action et l'enregistrer dans le UndoManager
                action.Execute();

                // Pousser l'action dans la stack
                UndoManager.RecordAction(action);

                Console.WriteLine($"\n=== Student '{newStudent.Name}' has been successfully added! ===");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nError while adding the student: {ex.Message}");
            }
        }

        private static void AddTeacher()
        {
            try
            {
                SchoolMember member = AcceptAttributes();
                if (member == null)
                {
                    Console.WriteLine("Failed to collect teacher information. Operation cancelled.");
                    return;
                }

                Teacher newTeacher = new Teacher(member.Name, member.Address, member.PhoneNumber);
                newTeacher.Subject = UserConsole.AskQuestionName("Enter subject: ");

                var action = new AddTeacherAction(Teachers, newTeacher);
                action.Execute();
                UndoManager.RecordAction(action);

                Console.WriteLine($"\n=== Teacher '{newTeacher.Name}' has been successfully added! ===");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nError while adding the teacher: {ex.Message}");
            }
        }

        public static void Add()
        {
            try
            {
                Console.WriteLine("\n--- Add School Member ---");
                Console.WriteLine("\nPlease note that the Principal/Receptionist details cannot be added or modified now.");
                int memberType = AcceptMemberType();

                while (memberType == 1 || memberType == 4)
                {
                    Console.WriteLine("\nPrincipals and receptionists cannot be added. Please select a different member type.");
                    memberType = AcceptMemberType();
                }

                switch (memberType)
                {
                    case 2:
                        AddTeacher();
                        break;
                    case 3:
                        AddStudent();
                        break;
                    case 5:
                        Console.WriteLine("\nOperation cancelled.");
                        break;
                    default:
                        Console.WriteLine("\nInvalid input. Terminating operation.");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nError in Add operation: {ex.Message}");
            }
        }

        private static void Display()
        {
            Console.WriteLine("\n--- Display School Members ---");
            int memberType = AcceptMemberType();

            switch (memberType)
            {
                case 1:
                    Console.WriteLine("\nThe Principal's details are:");
                    Console.WriteLine(Principal.ToString());
                    break;
                case 2:
                    Console.WriteLine("\nThe teachers are:");
                    foreach (Teacher teacher in Teachers)
                        Console.WriteLine(teacher.ToString());
                    break;
                case 3:
                    Console.WriteLine("\nThe students are:");
                    foreach (Student student in Students)
                        Console.WriteLine(student.ToString());
                    break;
                case 4:
                    Console.WriteLine("\nThe Receptionist's details are:");
                    Console.WriteLine(Receptionist.ToString());
                    break;
                case 5:
                    Console.WriteLine("\nOperation cancelled.");
                    break;
                default:
                    Console.WriteLine("\nInvalid input. Terminating operation.");
                    break;
            }
        }

        public static async Task Pay()
        {
            try
            {
                Console.WriteLine("\n--- Pay School Members ---");
                Console.WriteLine("\nPlease note that the students cannot be paid.");
                int memberType = AcceptMemberType();

                while (memberType == 3)
                {
                    Console.WriteLine("\nStudents cannot be paid. Please select a different member type.");
                    memberType = AcceptMemberType();
                }

                // Mise dans un if case au lieu du switch case pour éviter l'impression de "Payments in progress..." et de "Payments completed"
                if (memberType == 5)
                {
                    Console.WriteLine("\nOperation cancelled.");
                    return;
                }

                Console.WriteLine("\nPayments in progress...");

                switch (memberType)
                {
                    case 1:
                        if (Principal == null)
                        {
                            Console.WriteLine("\nNo principal available to pay.");
                            return;
                        }

                        int previousBalancePrincipal = Principal.GetBalance();
                        await Principal.PayAsync();
                        UndoManager.RecordAction(new PaymentAction(Principal, previousBalancePrincipal, $"Payment to Principal: {Principal.Name}"));

                        break;
                    case 2:
                        if (Teachers.Count == 0 || Teachers == null)
                        {
                            Console.WriteLine("\nNo teachers available to pay.");
                            return;
                        }

                        // Sauvegarder les balances précédents des enseignants avant le paiement
                        var teacherBalances = new Dictionary<Teacher, int>();
                        foreach (var teacher in Teachers)
                        {
                            teacherBalances[teacher] = teacher.GetBalance();
                        }

                        // Paiement des enseignants
                        var teacherTasks = Teachers.Select(teacher =>
                        {
                            if (teacher == null)
                            {
                                throw new InvalidOperationException("\nNull teacher found, skipping payment.");
                            }
                            return teacher.PayAsync();
                        }).ToArray();

                        await Task.WhenAll(teacherTasks);

                        // Enregistrer l'action de paiement des enseignants pour pouvoir l'annuler plus tard
                        UndoManager.RecordAction(new PayTeachersAction(Teachers, teacherBalances));

                        break;
                    case 4:
                        if (Receptionist == null)
                        {
                            Console.WriteLine("\nNo receptionist available to pay.");
                            return;
                        }

                        int previousBalanceReceptionist = Receptionist.GetBalance();
                        await Receptionist.PayAsync();
                        UndoManager.RecordAction(new PaymentAction(Receptionist, previousBalanceReceptionist, $"Payment to Receptionist: {Receptionist.Name}"));

                        break;
                    default:
                        Console.WriteLine("\nInvalid input. Terminating operation.");
                        break;
                }

                Console.WriteLine("\nPayments completed.\n");
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"\nPayment operation failed: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nUnexpected error during payment operation: {ex.Message}");
            }
        }

        public static void RaiseComplaint()
        {
            Console.WriteLine("\n--- Raise Complaint ---");

            Console.WriteLine("1. Submit Complaint\n2. Cancel");
            int choice = UserConsole.AskQuestionMenu("Please enter your choice: ", 1, 2);

            switch (choice)
            {
                case 1:
                    string complaintText = UserConsole.AskQuestion("Enter your complaint: ");
                    var action = new ComplaintAction(complaintText);
                    action.Execute();
                    UndoManager.RecordAction(action);

                    Receptionist.HandleComplaint(complaintText);
                    break;
                case 2:
                    Console.WriteLine("\nOperation cancelled.");
                    break;
                default:
                    Console.WriteLine("\nInvalid input. Terminating operation.");
                    break;
            }
        }

        private static void HandleComplaintRaised(object? sender, ComplaintEventArgs complaint)
        {
            Console.WriteLine("\nThis is a confirmation that we received your complaint. The details are as follows:");
            Console.WriteLine($"---------\nComplaint Time: {complaint.ComplaintTime.ToLongDateString()}, {complaint.ComplaintTime.ToLongTimeString()}");
            Console.WriteLine($"Complaint Raised: {complaint.ComplaintText}\n---------");
        }

        private static async Task ShowPerformance()
        {
            double average = await Task.Run(() => StudentManager.CalculateAverageGrade(Students));
            Console.WriteLine($"The student average performance is: {average}");
        }

        private static void AddData()
        {
            // Adresse d'exemple
            Address receptionistAddress = new Address(123, "Boulevard Rosemont", "Montreal", "QC", "A1A 1A1", "Canada");
            Address principalAddress = new Address(456, "Rue Gauchetiere", "Montreal", "QC", "B2B 2B2", "Canada");

            Principal = new Principal("Principal", principalAddress, "514-123-4567");

            Receptionist = new Receptionist("Receptionist", receptionistAddress, "514-123-4567");
            Receptionist.ComplaintRaised += HandleComplaintRaised;

            Principal = new Principal("Principal", principalAddress, "514-123-4567");

            for (int i = 0; i < 10; i++)
            {
                // Create sample addresses for each student and teacher
                Address studentAddress = new Address(i + 100, $"Street{i}", $"City{i}", "QC", $"A{i}B {i}C{i}", "Canada");
                Address teacherAddress = new Address(i + 200, $"Avenue{i}", $"City{i}", "QC", $"D{i}E {i}F{i}", "Canada");

                string studentPhone = $"514-000-000{i}";
                string teacherPhone = $"514-111-111{i}";
                Students.Add(new Student(i.ToString(), studentAddress, studentPhone, i * 10));
                Teachers.Add(new Teacher(i.ToString(), teacherAddress, teacherPhone));
            }
        }

        public static async Task Main(string[] args)
        {
            // Just for manual testing purposes.
            AddData();
            SetupConfig();

            Console.WriteLine("-------------- Welcome ---------------\n");

            bool flag = true;
            while (flag)
            {

                int choice = AcceptChoices();
                switch (choice)
                {
                    case 1:
                        Add();
                        break;
                    case 2:
                        Display();
                        break;
                    case 3:
                        await Pay();
                        break;
                    case 4:
                        RaiseComplaint();
                        break;
                    case 5:
                        await ShowPerformance();
                        break;
                    case 6:
                        UndoLastAction();
                        break;
                    case 7:
                        Console.WriteLine("\n-------------- Bye --------------");
                        Environment.Exit(0);
                        break;
                    default:
                        flag = false;
                        break;
                }
            }
        }

        private static void SetupConfig()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false)
                .AddEnvironmentVariables(prefix: "APP_")
                .Build();

            var networkDelaySettings = config.GetRequiredSection("NetworkDelay").Get<NetworkDelaySettings>();
            config.GetSection("NetworkDelay").Bind(networkDelaySettings);

            Console.WriteLine($"Min = {networkDelaySettings.MinMs}");
            Console.WriteLine($"Max = {networkDelaySettings.MaxMs}");
        }
    }
}
