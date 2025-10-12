using SchoolManager;

namespace SchoolManagerAppTests;

public class StudentTest
{
    [Fact]
    public void Constructor_WithValidData_ShouldCreateStudent()
    {
        // Arrange
        var address = new Address(123, "Boul Rosemont", "Montreal", "QC", "H1A 1A1", "Canada");

        // Act
        var student = new Student("Jane Doe", address, "5149876543");

        // Assert
        Assert.Equal("Jane Doe", student.Name);
        Assert.Equal(address, student.Address);
        Assert.Equal("5149876543", student.PhoneNumber);
    }

    [Fact]
    public void Constructor_WithEmptyName_ShouldThrowArgumentException()
    {
        // Arrange
        var address = new Address(123, "Boul Rosemont", "Montreal", "QC", "H1A 1A1", "Canada");

        // Act & Assert
        Assert.Throws<ArgumentOutOfRangeException>(() => new Student("", address, "5149876543"));
    }

    [Fact]
    public void Constructor_WithNullAddress_ShouldThrowArgumentNullException()
    {
        // Arrange, Act, Assert
        Assert.Throws<ArgumentNullException>(() => new Student("Jane Doe", null!, "5149876543"));
    }

    [Fact]
    public void Grade_SetValidGrade_ShouldUpdateGrade()
    {
        // Arrange
        var address = new Address(123, "Boul Rosemont", "Montreal", "QC", "H1A 1A1", "Canada");
        var student = new Student("Jane Doe", address, "5149876543");

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
        var address = new Address(123, "Boul Rosemont", "Montreal", "QC", "H1A 1A1", "Canada");
        var student = new Student("Jane Doe", address, "5149876543");

        // Act & Assert
        Assert.Throws<ArgumentOutOfRangeException>(() => student.Grade = invalidGrade);
    }

    [Fact]
    public void ToString_WithValidData_ShouldIncludeAllDetails()
    {
        // Arrange
        var address = new Address(123, "Boul Rosemont", "Montreal", "QC", "H1A 1A1", "Canada");
        var student = new Student("Jane Doe", address, "5149876543", 90.0);

        // Act
        var result = student.ToString();

        // Assert
        Assert.Contains("Jane Doe", result); // Name
        Assert.Contains("123 Boul Rosemont", result); // Address
        Assert.Contains("5149876543", result); // Phone Number
        Assert.Contains("90", result); // Grade
    }
}