using SchoolManager;

namespace SchoolManagerAppTests;

public class StudentTest
{
    private readonly string _defaultName = "Jane Doe";
    private readonly string _defaultPhoneNumber = "5149876543";

    private readonly Address _defaultAddress = new Address(123, "Boul Rosemont", "Montreal", "QC", "H1A 1A1", "Canada");

    [Fact]
    public void Constructor_WithValidData_ShouldCreateStudent()
    {
        // Act
        var student = new Student(_defaultName, _defaultAddress, _defaultPhoneNumber);

        // Assert
        Assert.Equal(_defaultName, student.Name);
        Assert.Equal(_defaultAddress, student.Address);
        Assert.Equal(_defaultPhoneNumber, student.PhoneNumber);
    }

    [Fact]
    public void Constructor_WithEmptyName_ShouldThrowArgumentException()
    {
        // Act & Assert
        Assert.Throws<ArgumentException>(() => new Student("", _defaultAddress, _defaultPhoneNumber));
    }

    [Fact]
    public void Constructor_WithNullAddress_ShouldThrowArgumentNullException()
    {
        // Arrange, Act, Assert
        Assert.Throws<ArgumentNullException>(() => new Student(_defaultName, null!, _defaultPhoneNumber));
    }

    [Fact]
    public void Grade_SetValidGrade_ShouldUpdateGrade()
    {
        // Arrange
        var student = new Student(_defaultName, _defaultAddress, _defaultPhoneNumber);

        // Act
        student.Grade = 85.5;

        // Assert
        Assert.Equal(85.5, student.Grade);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(101)]
    public void Grade_SetInvalidGrade_ShouldThrowArgumentOutOfRangeException(double invalidGrade)
    {
        // Arrange
        var student = new Student(_defaultName, _defaultAddress, _defaultPhoneNumber);

        // Act & Assert
        Assert.Throws<ArgumentOutOfRangeException>(() => student.Grade = invalidGrade);
    }

    [Fact]
    public void ToString_WithValidData_ShouldIncludeAllDetails()
    {
        // Arrange
        double grade = 92.3;
        var student = new Student(_defaultName, _defaultAddress, _defaultPhoneNumber, grade);

        // Act
        var result = student.ToString();

        // Assert
        Assert.Contains(_defaultName, result); // Name
        Assert.Contains(_defaultAddress.ToString(), result); // Address
        Assert.Contains(_defaultPhoneNumber, result); // Phone Number
        Assert.Contains(grade.ToString(), result); // Grade
    }
}