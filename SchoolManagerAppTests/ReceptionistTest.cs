using SchoolManager;

namespace SchoolManagerAppTests;

public class ReceptionistTest
{
    private readonly string _defaultName = "Jane Doe";
    private readonly Address _defaultAddress = new Address(123, "Boul Rosemont", "Montreal", "QC", "H1A 1A1", "Canada");
    private readonly string _defaultPhoneNumber = "5147654321";
    private readonly int _defaultTotalEarnings = 0;
    private readonly int _defaultIncome = 10000;

    [Fact]
    public void Constructor_WithValidData_ShouldCreateReceptionist()
    {
        // Act
        var receptionist = new Receptionist(_defaultName, _defaultAddress, _defaultPhoneNumber);

        // Assert
        Assert.Equal(_defaultName, receptionist.Name);
        Assert.Equal(_defaultAddress, receptionist.Address);
        Assert.Equal(_defaultPhoneNumber, receptionist.PhoneNumber);
        Assert.Equal(_defaultTotalEarnings, receptionist.TotalEarnings);
        Assert.Equal(_defaultIncome, receptionist.Income); // Default income
    }

    [Fact]
    public void Constructor_WithEmptyName_ShouldThrowArgumentException()
    {
        // Act & Assert
        Assert.Throws<ArgumentException>(() => new Receptionist("", _defaultAddress, _defaultPhoneNumber));
    }

    [Fact]
    public void Constructor_WithNullAddress_ShouldThrowArgumentNullException()
    {
        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => new Receptionist(_defaultName, null!, _defaultPhoneNumber));
    }

    [Fact]
    public void Constructor_WithoutIncome_ShouldApplyDefaultIncome()
    {
        // Act
        var receptionist = new Receptionist(_defaultName, _defaultAddress, _defaultPhoneNumber);

        // Assert
        Assert.Equal(_defaultIncome, receptionist.Income); // Default income
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(-1000)]
    [InlineData(int.MinValue)]
    public void Constructor_WithNegativeIncome_ShouldThrowArgumentOutOfRangeException(int income)
    {
        // Act & Assert
        Assert.Throws<ArgumentOutOfRangeException>(() => new Receptionist(_defaultName, _defaultAddress, _defaultPhoneNumber, income));
    }

    [Fact]
    public void ToString_WithValidData_ShouldIncludeAllData()
    {
        // Arrange
        var receptionist = new Receptionist(_defaultName, _defaultAddress, _defaultPhoneNumber, _defaultIncome + 2000); // Income = 12000

        // Act
        var result = receptionist.ToString();

        // Assert
        Assert.Contains(_defaultName, result);
        Assert.Contains(_defaultAddress.ToString(), result);
        Assert.Contains(_defaultPhoneNumber, result);
        Assert.Contains(receptionist.Income.ToString(), result); // Income = 12000
        Assert.Contains(receptionist.TotalEarnings.ToString(), result); // Total Earnings = 0
    }
}