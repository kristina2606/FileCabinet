using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace FileCabinetApp
{
    public static class Program
    {
        private const string DeveloperName = "Krystsina Labada";
        private const string HintMessage = "Enter your command, or enter 'help' to get help.";
        private const int CommandHelpIndex = 0;
        private const int DescriptionHelpIndex = 1;
        private const int ExplanationHelpIndex = 2;

        private static bool isRunning = true;
        private static FileCabinetService fileCabinetService = new FileCabinetService();

        private static Tuple<string, Action<string>>[] commands = new Tuple<string, Action<string>>[]
        {
            new Tuple<string, Action<string>>("help", PrintHelp),
            new Tuple<string, Action<string>>("exit", Exit),
            new Tuple<string, Action<string>>("stat", Stat),
            new Tuple<string, Action<string>>("create", Create),
            new Tuple<string, Action<string>>("list", List),
        };

        private static string[][] helpMessages = new string[][]
        {
            new string[] { "help", "prints the help screen", "The 'help' command prints the help screen." },
            new string[] { "exit", "exits the application", "The 'exit' command exits the application." },
            new string[] { "stat", "displays statistics on records", "The 'stat' displays statistics on records." },
            new string[] { "create", "creat data", "The 'create' creat data." },
            new string[] { "list", "returns a list of records added to the service.", "The 'list' returns a list of records added to the service.." },
        };

        public static void Main(string[] args)
        {
            Console.WriteLine($"File Cabinet Application, developed by {Program.DeveloperName}");
            Console.WriteLine(Program.HintMessage);
            Console.WriteLine();

            do
            {
                Console.Write("> ");
                var line = Console.ReadLine();
                var inputs = line != null ? line.Split(' ', 2) : new string[] { string.Empty, string.Empty };
                const int commandIndex = 0;
                var command = inputs[commandIndex];

                if (string.IsNullOrEmpty(command))
                {
                    Console.WriteLine(Program.HintMessage);
                    continue;
                }

                var index = Array.FindIndex(commands, 0, commands.Length, i => i.Item1.Equals(command, StringComparison.InvariantCultureIgnoreCase));
                if (index >= 0)
                {
                    const int parametersIndex = 1;
                    var parameters = inputs.Length > 1 ? inputs[parametersIndex] : string.Empty;
                    commands[index].Item2(parameters);
                }
                else
                {
                    PrintMissedCommandInfo(command);
                }
            }
            while (isRunning);
        }

        private static void PrintMissedCommandInfo(string command)
        {
            Console.WriteLine($"There is no '{command}' command.");
            Console.WriteLine();
        }

        private static void PrintHelp(string parameters)
        {
            if (!string.IsNullOrEmpty(parameters))
            {
                var index = Array.FindIndex(helpMessages, 0, helpMessages.Length, i => string.Equals(i[Program.CommandHelpIndex], parameters, StringComparison.InvariantCultureIgnoreCase));
                if (index >= 0)
                {
                    Console.WriteLine(helpMessages[index][Program.ExplanationHelpIndex]);
                }
                else
                {
                    Console.WriteLine($"There is no explanation for '{parameters}' command.");
                }
            }
            else
            {
                Console.WriteLine("Available commands:");

                foreach (var helpMessage in helpMessages)
                {
                    Console.WriteLine("\t{0}\t- {1}", helpMessage[Program.CommandHelpIndex], helpMessage[Program.DescriptionHelpIndex]);
                }
            }

            Console.WriteLine();
        }

        private static void Exit(string parameters)
        {
            Console.WriteLine("Exiting an application...");
            isRunning = false;
        }

        private static void Stat(string parameters)
        {
            var recordsCount = Program.fileCabinetService.GetStat();
            Console.WriteLine($"{recordsCount} record(s).");
        }

        private static void Create(string parameters)
        {
            var culture = CultureInfo.InvariantCulture;

            Console.Write("First name: ");
            var name = Console.ReadLine();
            while (!IsStringCorrect(name))
            {
                Console.WriteLine("Your first name contains not only letters. Repeat the input.");
                Console.Write("First name: ");
                name = Console.ReadLine();
            }

            Console.Write("Last name: ");
            var lastName = Console.ReadLine();
            while (!IsStringCorrect(lastName))
            {
                Console.WriteLine("Your last name contains not only letters. Repeat the input.");
                Console.Write("Last name: ");
                lastName = Console.ReadLine();
            }

            Console.Write("Date of birth: ");
            var date = Console.ReadLine();
            DateTime dateOfBitrh;
            while (!DateTime.TryParseExact(date, "dd/MM/yyyy", culture, DateTimeStyles.None, out dateOfBitrh))
            {
                Console.WriteLine("You introduced the date in the wrong format. Repeat the input of the date in the format 'dd/MM/yyyy'.");
                Console.Write("Date of birth: ");
                date = Console.ReadLine();
            }

            Console.Write("Gender (man - 'm' or woman - 'f'): ");
            var gender_ = Console.ReadLine();
            while (!IsStringCorrect(gender_))
            {
                Console.WriteLine("The gender contains not only letters. Repeat the input.");
                Console.Write("Gender (man - 'm' or woman - 'f'): ");
                gender_ = Console.ReadLine();
            }

            if (!char.TryParse(gender_, out char gender))
            {
                throw new ArgumentException("gender is entered in the wrong format.");
            }

            Console.Write("Height: ");
            var height_ = Console.ReadLine();
            while (!IsNumberCorrect(height_))
            {
                Console.WriteLine("Height contains not only numbers. Repeat the input.");
                Console.Write("Height: ");
                height_ = Console.ReadLine();
            }

            if (!short.TryParse(height_, culture, out short height))
            {
                throw new ArgumentException("height is entered in the wrong format.");
            }

            Console.Write("Weight: ");
            var weight_ = Console.ReadLine();
            while (!IsNumberCorrect(weight_))
            {
                Console.WriteLine("Weight contains not only numbers. Repeat the input.");
                Console.Write("Height: ");
                weight_ = Console.ReadLine();
            }

            if (!decimal.TryParse(weight_, culture, out decimal weight))
            {
                throw new ArgumentException("weight is entered in the wrong format.");
            }

            var recordId = Program.fileCabinetService.CreateRecord(name, lastName, dateOfBitrh, gender, height, weight);

            Console.WriteLine($"Record #{recordId} is created.");
        }

        private static void List(string parameters)
        {
            var list = Program.fileCabinetService.GetRecords();

            foreach (var record in list)
            {
                var id = record.Id;
                var firstName = record.FirstName;
                var lastName = record.LastName;
                var dateOfBirth = record.DateOfBirth.ToString("yyyy-MMM-dd", CultureInfo.InvariantCulture);
                var gender = record.Gender;
                var height = record.Height;
                var weight = record.Weight;

                Console.WriteLine($"#{id}, {firstName}, {lastName}, {dateOfBirth}, {gender}, {height}, {weight}");
            }
        }

        private static bool IsStringCorrect(string name)
        {
            var result = true;
            foreach (var letter in name)
            {
                if (!char.IsLetter(letter))
                {
                    result = false;
                }
            }

            return result;
        }

        private static bool IsNumberCorrect(string number)
        {
            var result = true;
            foreach (var digit in number)
            {
                if (!char.IsDigit(digit))
                {
                    result = false;
                }
            }

            return result;
        }
    }
}