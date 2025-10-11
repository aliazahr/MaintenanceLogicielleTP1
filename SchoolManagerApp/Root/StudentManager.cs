public static double AverageGrade(List<Student> students)
{
    double avg = 0;
    foreach (Student student in students)
    {
        avg += student.Grade;
    }

    return avg / students.Count;
}
