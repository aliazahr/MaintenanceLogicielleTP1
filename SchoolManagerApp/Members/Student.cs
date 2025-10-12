using System;
using System.Collections.Generic;

namespace SchoolManager
{
    public class Student : SchoolMember
    {
        private double? _grade;

        public Student(string name, Address address, string phoneNumber, double? grade = null)
            : base(name, address, phoneNumber)
        {
            Grade = grade;
        }

        public double? Grade
        {
            get => _grade;
            set
            {
                if (value.HasValue)
                {
                    if (value < 0 || value > 100)
                        throw new ArgumentOutOfRangeException(nameof(value), "Grade must be between 0 and 100.");
                }
                _grade = value;
            }
        }

        public override string ToString()
        {
            return $"Student: {Name}, Address: {Address}, Phone: {PhoneNumber}, Grade: {Grade}";
        }
    }
}
