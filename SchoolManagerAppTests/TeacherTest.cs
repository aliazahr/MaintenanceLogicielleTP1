using SchoolManager;

namespace SchoolManagerAppTests;

public class TeacherTest
{
    private readonly string _defaultName = "Jane Doe";
    private readonly Address _defaultAddress = new Address(123, "Boul Rosemont", "Montreal", "QC", "H1A 1A1", "Canada");
    private readonly string _defaultPhoneNumber = "5147654321";
    private readonly int _defaultTotalEarnings = 0;
    private readonly int _defaultIncome = 25000;
    private readonly string _defaultSubject = "Math";

    [Fact]
    public void Constructor_WithValidData_ShouldCreateTeacher()
    {
        // Act
        var teacher = new Teacher(_defaultName, _defaultAddress, _defaultPhoneNumber, _defaultSubject);

        // Assert
        Assert.Equal(_defaultName, teacher.Name);
        Assert.Equal(_defaultAddress, teacher.Address);
        Assert.Equal(_defaultPhoneNumber, teacher.PhoneNumber);
        Assert.Equal(_defaultTotalEarnings, teacher.TotalEarnings);
        Assert.Equal(_defaultIncome, teacher.Income);
    }

    [Fact]
    public void Constructor_WithEmptyName_ShouldThrowArgumentException()
    {
        // Act & Assert
        Assert.Throws<ArgumentException>(() => new Teacher("", _defaultAddress, _defaultPhoneNumber, _defaultSubject));
    }

    [Fact]
    public void Constructor_WithNullAddress_ShouldThrowArgumentNullException()
    {
        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => new Teacher(_defaultName, null!, _defaultPhoneNumber, _defaultSubject));
    }

    [Fact]
    public void Constructor_WithoutIncome_ShouldApplyDefaultIncome()
    {
        // Act
        var teacher = new Teacher(_defaultName, _defaultAddress, _defaultPhoneNumber, _defaultSubject);

        // Assert
        Assert.Equal(_defaultIncome, teacher.Income); // Default income = 25000
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(-1000)]
    [InlineData(int.MinValue)]
    public void Constructor_WithNegativeIncome_ShouldThrowArgumentOutOfRangeException(int income)
    {
        // Act & Assert
        Assert.Throws<ArgumentOutOfRangeException>(() => new Teacher(_defaultName, _defaultAddress, _defaultPhoneNumber, _defaultSubject, income));
    }

    [Fact]
    public void Subject_SetValidSubject_ShouldStoreSubject()
    {
        // Arrange
        var teacher = new Teacher(_defaultName, _defaultAddress, _defaultPhoneNumber, _defaultSubject);

        // Act
        teacher.Subject = "Math";

        // Assert
        Assert.Equal("Math", teacher.Subject);
    }

    [Fact]
    public void Subject_SetEmptyValue_ShouldThrowArgumentException()
    {
        // Arrange
        var teacher = new Teacher(_defaultName, _defaultAddress, _defaultPhoneNumber, _defaultSubject);

        // Act & Assert
        Assert.Throws<ArgumentException>(() => teacher.Subject = "");
    }

    [Fact]
    public void Subject_SetNullValue_ShouldThrowArgumentException()
    {
        // Arrange
        var teacher = new Teacher(_defaultName, _defaultAddress, _defaultPhoneNumber, _defaultSubject);

        // Act & Assert
        Assert.Throws<ArgumentException>(() => teacher.Subject = null!);
    }

    [Fact]
    public void ToString_WithValidData_ShouldIncludeAllData()
    {
        // Arrange
        var teacher = new Teacher(_defaultName, _defaultAddress, _defaultPhoneNumber, _defaultSubject);

        // Act
        var result = teacher.ToString();

        // Assert
        Assert.Contains(_defaultName, result);
        Assert.Contains(_defaultAddress.ToString(), result);
        Assert.Contains(_defaultPhoneNumber, result);
        Assert.Contains(_defaultSubject, result);
        Assert.Contains(_defaultIncome.ToString(), result);
        Assert.Contains(_defaultTotalEarnings.ToString(), result);
    }
}