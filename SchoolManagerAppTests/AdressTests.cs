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


    
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void Null_StreetName_Throws(string? value)
    {
        Assert.Throws<ArgumentNullException>(() =>
            new Address(1, value!, "City", "Province", "Code", "Country"));
    }

    
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void Null_City_Throws(string? value)
    {
        Assert.Throws<ArgumentNullException>(() =>
            new Address(1, "Street", value!, "Province", "Code", "Country"));
    }

    
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void Null_Province_Throws(string? value)
    {
        Assert.Throws<ArgumentNullException>(() =>
            new Address(1, "Street", "City", value!, "Code", "Country"));
    }

    
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void Null_PostalCode_Throws(string? value)
    {
        Assert.Throws<ArgumentNullException>(() =>
            new Address(1, "Street", "City", "Province", value!, "Country"));
    }

   
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void Null_Country_Throws(string? value)
    {
        Assert.Throws<ArgumentNullException>(() =>
            new Address(1, "Street", "City", "Province", "Code", value!));
    }

}