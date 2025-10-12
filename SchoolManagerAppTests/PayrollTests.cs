using SchoolManager;

namespace SchoolManagerAppTests
{
    public class PayrollTests
    {
        private Address Address() => new Address(1234, "Street", "City", "State", "12345", "Canada");
        
        [Fact]
        public async Task PayAsync_ShouldIncreaseBalanceByIncome()
        {
            var principal = new Principal("Alice", Address(), "12345678", income: 10); // small income for fast math
            var payroll = (IPayroll)principal;

            var start = payroll.GetBalance(); // expected 0

            // Act
            await payroll.PayAsync(); //Pay a first time
            await payroll.PayAsync(); //Pay a second time
            await payroll.PayAsync(); //Pay a third time

            // Assert
            Assert.Equal(start + 3 * principal.Income, payroll.GetBalance()); // expected 30
        }
    }
}  