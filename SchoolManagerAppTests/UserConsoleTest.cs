using SchoolManager;
using Xunit;
using System.Collections.Generic;
using System.Linq;

namespace SchoolManagerAppTests;


public class UserConsoleTest
{
    [Fact]
    public void AskQuestionMenu_LoopsUntilValidInput()
    {
        var simulatedInput = new StringReader("abcde\n99999\n\n3\n"); //Simulate invalid inputs followed by a valid input
        var capturedOutput = new StringWriter(); // Capture console output

        var originalIn = Console.In; // Save original input
        var originalOut = Console.Out;  // Save original output

        try
        {
            Console.SetIn(simulatedInput); // Console is reading from simulated input
            Console.SetOut(capturedOutput); // Console is writing to captured output

            // Act
            int result = UserConsole.AskQuestionMenu("Choose an option: ", minInput: 1, maxInput: 5); // Valid inputs are between 1 and 5

            // Assert
            Assert.Equal(3, result); // Loop should end when valid input 3 is provided

            var output = capturedOutput.ToString(); // Get all captured output and the error messages

            //Verify the first error was for non-numeric input
            Assert.Contains("Invalid input. Please enter a valid option.", output);
            //Verify the second error was for out-of-range input
            Assert.Contains("between 1 and 5", output);
            //Verify the third error was for empty input
            Assert.Contains("Input cannot be empty", output);
        }
        finally
        {
            Console.SetIn(originalIn);
            Console.SetOut(originalOut);
        }
    }

    [Fact]
    public void AskQuestionMenu_NullInput_ThrowsArgumentNullException()
    {
        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => UserConsole.AskQuestionMenu(null!, 1, 5)); // Null question should throw ArgumentNullException
    }
}