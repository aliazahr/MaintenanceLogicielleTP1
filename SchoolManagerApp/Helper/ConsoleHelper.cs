using System.Text.RegularExpressions;

namespace Util
{
    public class Console
    {
        static public string AskQuestion(string question)
        {
            System.Console.Write(question);
            return System.Console.ReadLine() ?? string.Empty;
        }

        static public string AskQuestionName(string question)
        {
            const int minInputLength = 1;
            const int maxInputLength = 100;
            string pattern = @"^[a-zA-ZÀ-ÖØ-öø-ÿ]([a-zA-ZÀ-ÖØ-öø-ÿ\s-]*[a-zA-ZÀ-ÖØ-öø-ÿ])?$"; // Doit commencer et finir avec une lettre, mais peut contenir des accents, espaces, et tirets

            while (true)
            {
                System.Console.Write(question);
                string? input = System.Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(input) && Regex.IsMatch(input, pattern))
                {
                    if (input.Length >= minInputLength && input.Length <= maxInputLength)
                    {
                        return input;
                    }

                    System.Console.WriteLine($"Invalid input. Please enter a valid name between the length of {minInputLength} character and {maxInputLength} caracters.");
                }

                System.Console.WriteLine("Invalid input. Please enter a valid name.");
            }
        }

        static public SchoolManager.Address AskQuestionAddress(string question)
        {
            System.Console.WriteLine($"\n{question}");

            // Street number input
            System.Console.Write("Enter street number: ");
            int streetNumber;
            while (!int.TryParse(System.Console.ReadLine(), out streetNumber) || streetNumber <= 0)
            {
                System.Console.Write("Invalid input. Please enter a valid street number: ");
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

        static public int AskQuestionMenu(string question, int minInput, int maxInput)
        {
            while (true)
            {
                System.Console.Write(question);
                string? input = System.Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(input) && int.TryParse(input, out int result))
                {
                    if (result >= minInput && result <= maxInput)
                    {
                        return result;
                    }

                    System.Console.WriteLine($"Invalid input. Please enter a valid option between {minInput} and {maxInput}.");
                }
                else
                {
                    System.Console.WriteLine("Invalid input. Please enter a valid option.");
                }
            }
        }

        static public double AskQuestionGrade(string question)
        {
            const int minGrade = 0;
            const int maxGrade = 100;

            while (true)
            {
                System.Console.Write(question);
                string? input = System.Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(input) && double.TryParse(input, out double result))
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
        }

        static public int AskQuestionPhoneNumber(string question)
        {
            const int minPhone = 1000000000; // 10 digits minimum
            const int maxPhone = int.MaxValue; // Value maximum pour un integer

            while (true)
            {
                System.Console.Write(question);
                string? input = System.Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(input) && int.TryParse(input, out int result))
                {
                    if (result >= minPhone && result <= maxPhone)
                    {
                        return result;
                    }

                    System.Console.WriteLine($"Invalid input. Please enter a valid phone number (at least 10 digits).");
                }
                else
                {
                    System.Console.WriteLine("Invalid input. Please enter a valid phone number.");
                }
            }
        }
    }
}
