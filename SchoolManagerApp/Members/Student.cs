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

        public double Grade
        {
            get => _grade;
            set { grade = value; }
        }

        public override string ToString()
        {
            return $"Student: {Name}, Address: {Address}, Phone: {PhoneNumber}, Grade: {Grade}";
        }
    }
}
