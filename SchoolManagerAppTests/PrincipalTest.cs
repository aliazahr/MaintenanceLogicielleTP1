using SchoolManager;

namespace SchoolManagerAppTests;

public class PrincipalTest
{
    [Fact]
    public void Constructor_WithValidData_ShouldCreatePrincipal()
    {
        // Arrange
        var address = new Address(123, "Boul Rosemont", "Montreal", "QC", "H1A 1A1", "Canada");

        // Act
        var principal = new Principal("John Doe", address, "5141234567");

        // Assert
        Assert.Equal("John Doe", principal.Name);
        Assert.Equal(address, principal.Address);
        Assert.Equal("5141234567", principal.PhoneNumber);
        Assert.Equal(0, principal.TotalEarnings);
        Assert.Equal(50000, principal.Income); // Default income
    }

    [Fact]
    public void Constructor_WithEmptyName_ShouldThrowArgumentException()
    {
        // Arrange
        var address = new Address(123, "Boul Rosemont", "Montreal", "QC", "H1A 1A1", "Canada");

        // Act & Assert
        Assert.Throws<ArgumentException>(() => new Principal("", address, "5141234567"));
    }

    [Fact]
    public void Constructor_WithNullAddress_ShouldThrowArgumentNullException()
    {
        // Arrange, Act, Assert
        Assert.Throws<ArgumentNullException>(() => new Principal("John Doe", null!, "5141234567"));
    }

    [Fact]
    public void Constructor_WithoutIncome_ShouldApplyDefaultIncome()
    {
        // Arrange
        var address = new Address(123, "Boul Rosemont", "Montreal", "QC", "H1A 1A1", "Canada");

        // Act
        var principal = new Principal("John Doe", address, "5141234567");

        // Assert
        Assert.Equal(50000, principal.Income); // Default income
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(-1000)]
    [InlineData(int.MinValue)]
    public void Constructor_WithNegativeIncome_ShouldThrowArgumentOutOfRangeException(int income)
    {
        // Arrange
        var address = new Address(123, "Boul Rosemont", "Montreal", "QC", "H1A 1A1", "Canada");

        // Act & Assert
        Assert.Throws<ArgumentOutOfRangeException>(() => new Principal("John Doe", address, "5141234567", income));
    }

    [Fact]
    public void ToString_WithValidData_ShouldIncludeAllData()
    {
        // Arrange
        var address = new Address(123, "Boul Rosemont", "Montreal", "QC", "H1A 1A1", "Canada");
        var principal = new Principal("John Doe", address, "5141234567");

        // Act
        var result = principal.ToString();

        // Assert
        Assert.Contains("John Doe", result); // Name
        Assert.Contains("123 Boul Rosemont", result); // Address
        Assert.Contains("5141234567", result); // Phone Number
        Assert.Contains("50000", result); // Income
        Assert.Contains("0", result); // Total Earnings
    }
}