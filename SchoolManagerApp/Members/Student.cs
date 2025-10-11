using System;
using System.Collections.Generic;

namespace SchoolManager
{
    public class Student : SchoolMember
    {
        private double _grade;

        public Student(string name, Address address, string phoneNumber, double grade)
            : base(name, address, phoneNumber)
        {
            Grade = grade;
        }

        public static double AverageGrade(List<Student> students)
        {
            double avg = 0;
            foreach (Student student in students)
            {
                avg += student.Grade;
            }

            return avg / students.Count;
        }

        public void Display()
        {
            Console.WriteLine("Name: {0}, Address: {1}, Phone: {2}, Grade: {3}", Name, Address, Phone, Grade);
        }

        public double Grade
        {
            get => _grade;
            set { grade = value; }
        }
    }
}
