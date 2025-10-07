using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SchoolManager
{
    public class Program
    {
        static public List<Student> Students = new List<Student>();
        static public List<Teacher> Teachers = new List<Teacher>();
        static public Principal Principal = new Principal();
        static public Receptionist Receptionist = new Receptionist();

        enum SchoolMemberType
        {
            typePrincipal = 1,
            typeTeacher,
            typeStudent,
            typeReceptionist
        }

        public static SchoolMember AcceptAttributes()
        {
            SchoolMember member = new SchoolMember();
            member.Name = Util.Console.AskQuestionName("Enter name: ");
            member.Address = Util.Console.AskQuestionAddress("Enter address: ");
            member.Phone = Util.Console.AskQuestionPhoneNumber("Enter phone number: ");

            return member;
        }

        private static int AcceptChoices()
        {
            const int minInput = 1;
            const int maxInput = 5;
            Console.WriteLine("\n====== Menu ======");
            return Util.Console.AskQuestionMenu("1. Add\n2. Display\n3. Pay\n4. Raise Complaint\n5. Student Performance\nPlease enter the member type: ", minInput, maxInput);
        }

        private static int AcceptMemberType()
        {
            const int minInput = 1;
            const int maxInput = 4;
            int x = Util.Console.AskQuestionMenu("\n1. Principal\n2. Teacher\n3. Student\n4. Receptionist\nPlease enter the member type: ", minInput, maxInput);

            return Enum.IsDefined(typeof(SchoolMemberType), x) ? x : -1;
        }

        public static void AddPrincpal()
        {
            SchoolMember member = AcceptAttributes();
            Principal.Name = member.Name;
            Principal.Address = member.Address;
            Principal.Phone = member.Phone;
        }

        private static void AddStudent()
        {
            SchoolMember member = AcceptAttributes();
            Student newStudent = new Student(member.Name, member.Address, member.Phone);
            newStudent.Grade = Util.Console.AskQuestionGrade("Enter grade: ");

            Students.Add(newStudent);
            Console.WriteLine($"\n=== Student '{newStudent.Name}' has been successfully added! ===");
        }

        private static void AddTeacher()
        {
            SchoolMember member = AcceptAttributes();
            Teacher newTeacher = new Teacher(member.Name, member.Address, member.Phone);
            newTeacher.Subject = Util.Console.AskQuestionName("Enter subject: ");

            Teachers.Add(newTeacher);
            Console.WriteLine($"\n=== Teacher '{newTeacher.Name}' has been successfully added! ===");
        }

        public static void Add()
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
                default:
                    Console.WriteLine("\nInvalid input. Terminating operation.");
                    break;
            }
        }

        private static void display()
        {
            int memberType = AcceptMemberType();

            switch (memberType)
            {
                case 1:
                    Console.WriteLine("\nThe Principal's details are:");
                    Principal.display();
                    break;
                case 2:
                    Console.WriteLine("\nThe teachers are:");
                    foreach (Teacher teacher in Teachers)
                        teacher.display();
                    break;
                case 3:
                    Console.WriteLine("\nThe students are:");
                    foreach (Student student in Students)
                        student.display();
                    break;
                case 4:
                    Console.WriteLine("\nThe Receptionist's details are:");
                    Receptionist.Display();
                    break;
                default:
                    Console.WriteLine("\nInvalid input. Terminating operation.");
                    break;
            }
        }

        public static void Pay()
        {
            Console.WriteLine("\nPlease note that the students cannot be paid.");
            int memberType = AcceptMemberType();

            Console.WriteLine("\nPayments in progress...");

            switch (memberType)
            {
                case 1:
                    Principal.Pay();
                    break;
                case 2:
                    List<Task> payments = new List<Task>();

                    foreach (Teacher teacher in Teachers)
                    {
                        Task payment = new Task(teacher.Pay);
                        payments.Add(payment);
                        payment.Start();
                    }

                    Task.WaitAll(payments.ToArray());

                    break;
                case 4:
                    Receptionist.Pay();
                    break;
                default:
                    Console.WriteLine("\nInvalid input. Terminating operation.");
                    break;
            }

            Console.WriteLine("Payments completed.\n");
        }

        public static void RaiseComplaint()
        {
            Receptionist.HandleComplaint();
        }

        private static void handleComplaintRaised(object sender, Complaint complaint)
        {
            Console.WriteLine("\nThis is a confirmation that we received your complaint. The details are as follows:");
            Console.WriteLine($"---------\nComplaint Time: {complaint.ComplaintTime.ToLongDateString()}, {complaint.ComplaintTime.ToLongTimeString()}");
            Console.WriteLine($"Complaint Raised: {complaint.ComplaintRaised}\n---------");
        }

        private static async Task showPerformance()
        {
            double average = await Task.Run(() => Student.averageGrade(Students));
            Console.WriteLine($"The student average performance is: {average}");
        }

        private static void addData()
        {
            // Adresse d'exemple
            Address receptionistAddress = new Address(123, "Boulevard Rosemont", "Montreal", "QC", "A1A 1A1", "Canada");
            Address principalAddress = new Address(456, "Rue Gauchetiere", "Montreal", "QC", "B2B 2B2", "Canada");
            
            Receptionist = new Receptionist("Receptionist", receptionistAddress, 123);
            Receptionist.ComplaintRaised += handleComplaintRaised;

            Principal = new Principal("Principal", principalAddress, 123);

            for (int i = 0; i < 10; i++)
            {
                // Create sample addresses for each student and teacher
                Address studentAddress = new Address(i + 100, $"Street{i}", $"City{i}", "QC", $"A{i}B {i}C{i}", "Canada");
                Address teacherAddress = new Address(i + 200, $"Avenue{i}", $"City{i}", "QC", $"D{i}E {i}F{i}", "Canada");

                Students.Add(new Student(i.ToString(), studentAddress, i, i));
                Teachers.Add(new Teacher(i.ToString(), teacherAddress, i));
            }
        }

        public static async Task Main(string[] args)
        {
            // Just for manual testing purposes.
            addData();

            Console.WriteLine("-------------- Welcome ---------------\n");

            //Console.WriteLine("Please enter the Princpals information.");
            //AddPrincpal();

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
                        display();
                        break;
                    case 3:
                        Pay();
                        break;
                    case 4:
                        RaiseComplaint();
                        break;
                    case 5:
                        await showPerformance();
                        break;
                    default:
                        flag = false;
                        break;
                }
            }

            Console.WriteLine("\n-------------- Bye --------------");
        }
    }
}
