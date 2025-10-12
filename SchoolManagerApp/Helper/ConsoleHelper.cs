using System;
using System.Text.RegularExpressions;

namespace SchoolManager
{
    public class UserConsole
    {
        static public string AskQuestion(string question)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(question))
                {
                    throw new ArgumentException("The question cannot be null or empty.");
                }

                while (true) {
                    System.Console.Write(question);
                    string? input = System.Console.ReadLine();

                    if (!string.IsNullOrWhiteSpace(input))
                    {
                        return input;
                    }

                    System.Console.WriteLine("Input cannot be empty. Please enter a valid response.");
                }
            }
            catch (ArgumentException)
            {
                throw; // Relance argument exceptions
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("\nFailed to read user input", ex);

            }
        }

        static public string AskQuestionName(string question)
        {
            if (string.IsNullOrWhiteSpace(question))
            {
                throw new ArgumentException("Question cannot be null or whitespace.");
            }

            const int minInputLength = 1;
            const int maxInputLength = 100;
            string pattern = @"^[a-zA-ZÀ-ÖØ-öø-ÿ]([a-zA-ZÀ-ÖØ-öø-ÿ\s-]*[a-zA-ZÀ-ÖØ-öø-ÿ])?$"; // Doit commencer et finir avec une lettre, mais peut contenir des accents, espaces, et tirets

            while (true)
            {
                try
                {
                    System.Console.Write(question);
                    string? input = System.Console.ReadLine();

                    if (!string.IsNullOrWhiteSpace(input))
                    {
                        if (Regex.IsMatch(input, pattern))
                        {
                            if (input.Length >= minInputLength && input.Length <= maxInputLength)
                            {
                                return input;
                            }
                            System.Console.WriteLine($"Invalid input. Please enter a valid name between the length of {minInputLength} character and {maxInputLength} caracters.");
                        }
                        else
                        {
                            System.Console.WriteLine("Invalid input. Please enter a valid name.");
                        }
                    }
                    else
                    {
                        System.Console.WriteLine("Input cannot be empty. Please enter a name.");
                    }
                }
                catch (RegexMatchTimeoutException)
                {
                    System.Console.WriteLine("\nName validation timed out. Please enter a simpler name.");
                }
                catch (Exception ex)
                {
                    throw new InvalidOperationException("\nFailed to read user input", ex);
                }
            }
        }

        static public SchoolManager.Address AskQuestionAddress(string question)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(question))
                {
                    throw new ArgumentException("Question cannot be null or whitespace.");
                }

                System.Console.WriteLine($"\n{question}");

                // Street number input
                System.Console.Write("Enter street number: ");
                int streetNumber;

                while (true)
                {
                    try
                    {
                        string? input = System.Console.ReadLine();
                        if (int.TryParse(input, out streetNumber) && streetNumber > 0)
                        {
                            break;
                        }
                        System.Console.Write("Invalid input. Please enter a valid street number: ");
                    }
                    catch (OverflowException)
                    {
                        System.Console.Write("Number too large. Please enter a valid street number: ");
                    }
                }

                // Street name input
                string streetName = AskQuestionName("Enter street name: ");

                // City input
                string city = AskQuestionName("Enter city: ");

                // Province input
                string province = AskQuestionName("Enter province: ");

                // Postal code input
                string pattern = @"^[a-zA-Z0-9\s]+$"; // Accepte les lettres, les chiffres et les espaces
                System.Console.Write("Enter postal code: ");
                string? postalCode = System.Console.ReadLine();
                while (string.IsNullOrWhiteSpace(postalCode) || !Regex.IsMatch(postalCode, pattern))
                {
                    System.Console.Write("Invalid input. Please enter a postal code: ");
                    postalCode = System.Console.ReadLine();
                }

                // Country input
                string country = AskQuestionName("Enter country: ");

                return new SchoolManager.Address(streetNumber, streetName, city, province, postalCode, country);
            }
            catch (ArgumentException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("\nFailed to read address input", ex);
            }
        }

        static public int AskQuestionMenu(string question, int minInput, int maxInput)
        {
            if (string.IsNullOrWhiteSpace(question))
            {
                throw new ArgumentException("\nQuestion cannot be null or whitespace.");
            }

            while (true)
            {
                try
                {
                    System.Console.Write(question);
                    string? input = System.Console.ReadLine();

                    if (!string.IsNullOrWhiteSpace(input))
                    {
                        if (int.TryParse(input, out int result))
                        {
                            if (result >= minInput && result <= maxInput)
                            {
                                return result;
                            }

                            System.Console.WriteLine($"\nInvalid input. Please enter a valid option between {minInput} and {maxInput}.");
                        }
                        else
                        {
                            System.Console.WriteLine("\nInvalid input. Please enter a valid option.");
                        }
                    }
                    else
                    {
                        System.Console.WriteLine("\nInput cannot be empty. Please enter a valid option.");
                    }
                }
                catch (FormatException)
                {
                    System.Console.WriteLine("\nInvalid input format. Please enter a valid option.");
                }
                catch (OverflowException)
                {
                    System.Console.WriteLine("\nInput number is too large. Please enter a valid option.");
                }
                catch (Exception ex)
                {
                    throw new InvalidOperationException("\nFailed to read menu selection", ex);
                }
            }
        }

        static public double AskQuestionGrade(string question)
        {
            if (string.IsNullOrWhiteSpace(question))
            {
                throw new ArgumentException("Question cannot be null or whitespace.");
            }

            const int minGrade = 0;
            const int maxGrade = 100;

            while (true)
            {
                try
                {
                    System.Console.Write(question);
                    string? input = System.Console.ReadLine();

                    if (!string.IsNullOrWhiteSpace(input))
                    {
                        if (double.TryParse(input, out double result))
                        {
                            if (result >= minGrade && result <= maxGrade)
                            {
                                return result;
                            }

                            System.Console.WriteLine($"Invalid input. Please enter a grade between {minGrade} and {maxGrade}.");
                        }
                        else
                        {
                            System.Console.WriteLine("Invalid input. Please enter a valid grade.");
                        }
                    }
                    else
                    {
                        System.Console.WriteLine("Input cannot be empty. Please enter a valid grade.");
                    }
                }
                catch (FormatException)
                {
                    System.Console.WriteLine("\nInvalid input format. Please enter a valid grade.");
                }
                catch (OverflowException)
                {
                    System.Console.WriteLine("\nInput number is too large. Please enter a valid grade.");
                }
                catch (Exception ex)
                {
                    throw new InvalidOperationException("\nFailed to read grade input", ex);
                }
            }
        }

        static public string AskQuestionPhoneNumber(string question)
        {
            if (string.IsNullOrWhiteSpace(question))
            {
                throw new ArgumentException("Question cannot be null or whitespace.");
            }

            var phoneRegex = new Regex(@"^\+?[1-9]\d{6,14}$");

            while (true)
            {
                    System.Console.Write(question);
                    string? input = System.Console.ReadLine();

                    if (!string.IsNullOrWhiteSpace(input))
                    {
                        string cleanInput = Regex.Replace(input, @"(?!^\+)\D", ""); // Enlève les espaces, tirets et parenthèses

                        if (phoneRegex.IsMatch(cleanInput))
                            return cleanInput;

                        System.Console.WriteLine("Input cannot be empty. Please enter a valid phone number.");
                    }
            }
        }
    }
}
