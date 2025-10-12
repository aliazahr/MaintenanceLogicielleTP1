using System.ComponentModel;
using System.Threading.Tasks;
using SchoolManager;

namespace SchoolManagerAppTests;

public class UndoActionsTests
{
    //Some helper methods to reduce redundancy
    private static Address Address() => new Address(123, "street", "city", "QC", "H1A1A1", "country");
    private static string Phone() => "5141234567";   
    private static void ClearUndo()
    {
        while (UndoManager.CanUndo())
        {
            UndoManager.UndoLastAction();
        }
    }

    [Fact]
    // Test undo on empty stack does not throw
    public void UndoOnEmptyStack_ShouldNotThrow()
    {
        // Arrange
        ClearUndo();
        Assert.False(UndoManager.CanUndo()); // precondition

        // Act
        var ex1 = Record.Exception(() => UndoManager.UndoLastAction());
        var ex2 = Record.Exception(() => UndoManager.UndoLastAction()); // call twice to make sure still safe

        // Assert
        Assert.Null(ex1);
        Assert.Null(ex2);
        Assert.False(UndoManager.CanUndo()); // still empty
    }

    [Fact]
    // Test adding a student and then undoing removes the student
    public void AddStudent_ThenUndo_RemovesStudent()
    {
        ClearUndo();
        List<Student> studentsList = new List<Student>();
        Student newStudent = new Student("NewStudent", Address(), Phone(), grade: 95);

        //Create and execute the action
        AddStudentAction action = new AddStudentAction(studentsList, newStudent);
        action.Execute(); // add
        UndoManager.RecordAction(action); // track for undo

        Assert.Contains(newStudent, studentsList); // precondition

        // Act
        UndoManager.UndoLastAction();

        // Assert
        Assert.DoesNotContain(newStudent, studentsList);
        Assert.False(UndoManager.CanUndo());
    }

    [Fact]
    public void AddTeacher_ThenUndo_RemovesTeacher()
    {
        ClearUndo();
        List<Teacher> teachersList = new List<Teacher>();
        Teacher newTeacher = new Teacher("NewTeacher", Address(), Phone(), subject: "Math");

        AddTeacherAction action = new AddTeacherAction(teachersList, newTeacher);
        action.Execute(); // add
        UndoManager.RecordAction(action); // track for undo

        Assert.Contains(newTeacher, teachersList); // precondition

        // Act
        UndoManager.UndoLastAction();

        // Assert
        Assert.DoesNotContain(newTeacher, teachersList);
        Assert.False(UndoManager.CanUndo());
    }

    [Fact]
    public async Task PayTeacher_ThenUndo_ShouldRevertPayment()
    {
        // Arrange
        ClearUndo();
        Teacher teacher1 = new Teacher("Teacher1", Address(), Phone(), subject: "Science", income: 1000);
        Teacher teacher2 = new Teacher("Teacher2", Address(), Phone(), subject: "Math", income: 1200);
        
        //  Precondition: both teachers have 0 balance
        int previousBalanceTeacher1 = teacher1.GetBalance();
        int previousBalanceTeacher2 = teacher2.GetBalance();
        await ((IPayroll)teacher1).PayAsync();
        await ((IPayroll)teacher2).PayAsync();

        //Act
        UndoManager.RecordAction(new PayTeachersAction(new List<Teacher> { teacher1, teacher2 }, new Dictionary<Teacher, int> { { teacher1, previousBalanceTeacher1 }, { teacher2, previousBalanceTeacher2 } }));
        UndoManager.UndoLastAction();

        // Assert: balances should be reverted to previous values
        Assert.Equal(previousBalanceTeacher1, teacher1.GetBalance());
        Assert.Equal(previousBalanceTeacher2, teacher2.GetBalance());
        Assert.False(UndoManager.CanUndo());
    }
}