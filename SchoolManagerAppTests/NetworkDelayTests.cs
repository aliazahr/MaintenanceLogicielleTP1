using System.Diagnostics;
using System.Threading.Tasks;
using Xunit;
using SchoolManager;
using Util;


namespace SchoolManagerAppTests
{
    public class NetworkDelayTests
    {
        private static Address Address() => new Address(123, "Street", "City", "Province", "H1A1A1", "Canada");

        [Fact]
        public async Task PayAsync_CompletesWithinAllowedWindow()
        {
            var principal = new Principal("Alice", Address(), "12345678", income: 5);
            var payroll = (IPayroll)principal;

            Stopwatch stopwatch = Stopwatch.StartNew();
            await payroll.PayAsync(); 
            stopwatch.Stop();

            //Lower the constants to account for test environment variability
            var lower = NetworkDelay.MinDelay - 150; if (lower < 0) lower = 0;
            var upper = NetworkDelay.MaxDelay + 300; // Add 300ms buffer for test execution overhead

            Assert.InRange(stopwatch.ElapsedMilliseconds, lower, upper);
        }

    }
}