using SchoolManager;

namespace SchoolManagerAppTests;

public class PrincipalTest
{
    // Declared default values for reuse
    private readonly string _defaultName = "John Doe";
    private readonly string _defaultPhoneNumber = "5149876543";

    private readonly Address _defaultAddress = new Address(123, "Boul Rosemont", "Montreal", "QC", "H1A 1A1", "Canada");
    private readonly int _defaultIncome = 50000;
    private readonly int _defaultTotalEarnings = 0;
    
    [Fact]
    public void Constructor_WithValidData_ShouldCreatePrincipal()
    {
        // Act
        var principal = new Principal(_defaultName, _defaultAddress, _defaultPhoneNumber);

        // Assert
        Assert.Equal(_defaultName, principal.Name);
        Assert.Equal(_defaultAddress, principal.Address);
        Assert.Equal(_defaultPhoneNumber, principal.PhoneNumber);
        Assert.Equal(_defaultTotalEarnings, principal.TotalEarnings);
        Assert.Equal(_defaultIncome, principal.Income);
    }

    [Fact]
    public void Constructor_WithEmptyName_ShouldThrowArgumentException()
    {
        // Act & Assert
        Assert.Throws<ArgumentException>(() => new Principal("", _defaultAddress, _defaultPhoneNumber));
    }

    [Fact]
    public void Constructor_WithNullAddress_ShouldThrowArgumentNullException()
    {
        // Arrange, Act, Assert
        Assert.Throws<ArgumentNullException>(() => new Principal(_defaultName, null!, _defaultPhoneNumber));
    }

    [Fact]
    public void Constructor_WithoutIncome_ShouldApplyDefaultIncome()
    {
        // Act
        var principal = new Principal(_defaultName, _defaultAddress, _defaultPhoneNumber);

        // Assert
        Assert.Equal(_defaultIncome, principal.Income); // Default income = 50000
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(-1000)]
    [InlineData(int.MinValue)]
    public void Constructor_WithNegativeIncome_ShouldThrowArgumentOutOfRangeException(int income)
    {
        // Act & Assert
        Assert.Throws<ArgumentOutOfRangeException>(() => new Principal(_defaultName, _defaultAddress, _defaultPhoneNumber, income));
    }

    [Fact]
    public void ToString_WithValidData_ShouldIncludeAllData()
    {
        // Arrange
        var principal = new Principal(_defaultName, _defaultAddress, _defaultPhoneNumber);

        // Act
        var result = principal.ToString();

        // Assert
        Assert.Contains(_defaultName, result); // Name
        Assert.Contains(_defaultAddress.ToString(), result); // Address
        Assert.Contains(_defaultPhoneNumber, result); // Phone Number
        Assert.Contains(_defaultIncome.ToString(), result); // Income
        Assert.Contains(_defaultTotalEarnings.ToString(), result); // Total Earnings
    }
}