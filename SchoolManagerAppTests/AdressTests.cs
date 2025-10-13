using SchoolManager;

namespace SchoolManagerAppTests;

public class AdressTests
{
    [Fact]
    public void Constructor_WithValidData_ShouldCreateAdress()
    {
        // Arrange
        int number = 123;
        string street = "Boul Rosemont";
        string city = "Montreal";
        string province = "QC";
        string postalCode = "H1A 1A1";
        string country = "Canada";

        // Act
        var address = new Address(number, street, city, province, postalCode, country);

        // Assert
        Assert.Equal(number, address.StreetNumber);
        Assert.Equal(street, address.StreetName);
        Assert.Equal(city, address.City);
        Assert.Equal(province, address.Province);
        Assert.Equal(postalCode, address.PostalCode);
        Assert.Equal(country, address.Country);
    }

        
    // Nulls -> ArgumentNullException
    [Theory]
    [InlineData(null)]
    public void StreetName_Null_Throws(string? value)
        => Assert.Throws<ArgumentNullException>(() =>
            new Address(1, value!, "City", "Province", "Code", "Country"));

    [Theory]
    [InlineData(null)]
    public void City_Null_Throws(string? value)
        => Assert.Throws<ArgumentNullException>(() =>
            new Address(1, "Street", value!, "Province", "Code", "Country"));

    [Theory]
    [InlineData(null)]
    public void Province_Null_Throws(string? value)
        => Assert.Throws<ArgumentNullException>(() =>
            new Address(1, "Street", "City", value!, "Code", "Country"));

    [Theory]
    [InlineData(null)]
    public void PostalCode_Null_Throws(string? value)
        => Assert.Throws<ArgumentNullException>(() =>
            new Address(1, "Street", "City", "Province", value!, "Country"));

    [Theory]
    [InlineData(null)]
    public void Country_Null_Throws(string? value)
        => Assert.Throws<ArgumentNullException>(() =>
            new Address(1, "Street", "City", "Province", "Code", value!));

    // Empty/whitespace -> ArgumentException
    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public void StreetName_EmptyOrWhitespace_Throws(string value)
        => Assert.Throws<ArgumentException>(() =>
            new Address(1, value, "City", "Province", "Code", "Country"));

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public void City_EmptyOrWhitespace_Throws(string value)
        => Assert.Throws<ArgumentException>(() =>
            new Address(1, "Street", value, "Province", "Code", "Country"));

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public void Province_EmptyOrWhitespace_Throws(string value)
        => Assert.Throws<ArgumentException>(() =>
            new Address(1, "Street", "City", value, "Code", "Country"));

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public void PostalCode_EmptyOrWhitespace_Throws(string value)
        => Assert.Throws<ArgumentException>(() =>
            new Address(1, "Street", "City", "Province", value, "Country"));

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public void Country_EmptyOrWhitespace_Throws(string value)
        => Assert.Throws<ArgumentException>(() =>
            new Address(1, "Street", "City", "Province", "Code", value));

    // Street number guard (if you added it)
    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void StreetNumber_NonPositive_Throws(int n)
        => Assert.Throws<ArgumentOutOfRangeException>(() =>
            new Address(n, "Street", "City", "Province", "Code", "Country"));
}
