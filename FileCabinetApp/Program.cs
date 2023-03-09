using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace FileCabinetApp
{
    /// <summary>
    /// Works with user input when working with records.
    /// </summary>
    public static class Program
    {
        private const string DeveloperName = "Krystsina Labada";
        private const string HintMessage = "Enter your command, or enter 'help' to get help.";
        private const int CommandHelpIndex = 0;
        private const int DescriptionHelpIndex = 1;
        private const int ExplanationHelpIndex = 2;

        private static bool isRunning = true;
        private static IFileCabinetService fileCabinetService = new FileCabinetService(new DefaultValidator());
        private static IUserInputValidation inputValidation = new UserInputValidationDafault();
        private static string validationRules = "Using default validation rules.";

        private static Tuple<string, Action<string>>[] commands = new Tuple<string, Action<string>>[]
        {
            new Tuple<string, Action<string>>("help", PrintHelp),
            new Tuple<string, Action<string>>("exit", Exit),
            new Tuple<string, Action<string>>("stat", Stat),
            new Tuple<string, Action<string>>("create", Create),
            new Tuple<string, Action<string>>("list", List),
            new Tuple<string, Action<string>>("edit", Edit),
            new Tuple<string, Action<string>>("find", Find),
        };

        private static string[][] helpMessages = new string[][]
        {
            new string[] { "help", "prints the help screen", "The 'help' command prints the help screen." },
            new string[] { "exit", "exits the application", "The 'exit' command exits the application." },
            new string[] { "stat", "displays statistics on records", "The 'stat' displays statistics on records." },
            new string[] { "create", "creat new record", "The 'create' creat new record." },
            new string[] { "list", "returns a list of records.", "The 'list' returns a list of records." },
            new string[] { "edit", "editing a record by id.", "The 'edit' editing a record by id." },
            new string[] { "find", "finds all existing records by parameter.", "The 'find' finds all existing records by parameter." },
        };

        /// <summary>
        /// Receives a command from the user.
        /// </summary>
        /// <param name="args">Arguments of the appropriate type.</param>
        public static void Main(string[] args)
        {
            Console.WriteLine($"File Cabinet Application, developed by {Program.DeveloperName}");

            if (args.Length == 1)
            {
                var comand = args[0].Split('=');

                if (comand[0] == "--validation-rules" && comand[1].ToLowerInvariant() == "custom")
                {
                    fileCabinetService = new FileCabinetService(new CustomValidator());
                    inputValidation = new UserInputValidationCustom();
                    validationRules = "Using custom validation rules.";
                }
            }
            else if (args.Length == 2 && args[0] == "-v" && args[1].ToLowerInvariant() == "custom")
            {
                fileCabinetService = new FileCabinetService(new CustomValidator());
                inputValidation = new UserInputValidationCustom();
                validationRules = "Using custom validation rules.";
            }

            Console.WriteLine(Program.validationRules);
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
            int recordsCount = Program.fileCabinetService.GetStat();
            Console.WriteLine($"{recordsCount} record(s).");
        }

        private static void Create(string parameters)
        {
            Console.Write("First name: ");
            var firstName = ReadInput(StringConverter, inputValidation.FirstNameValidator);

            Console.Write("Last name: ");
            var lastName = ReadInput(StringConverter, inputValidation.LastNameValidator);

            Console.Write("Date of birth: ");
            var dateOfBirth = ReadInput(DateConverter, inputValidation.DateOfBirthValidator);

            Console.Write("Gender (man - 'm' or woman - 'f'): ");
            var gender = ReadInput(CharConverter, inputValidation.GenderValidator);

            Console.Write("Height: ");
            var height = ReadInput(ShortConverter, inputValidation.HeightValidator);

            Console.Write("Weight: ");
            var weight = ReadInput(DecimalConverter, inputValidation.WeightValidator);

            FileCabinetRecordNewData fileCabinetRecordNewData = new FileCabinetRecordNewData(firstName, lastName, dateOfBirth, gender, height, weight);
            int recordId = Program.fileCabinetService.CreateRecord(fileCabinetRecordNewData);

            Console.WriteLine($"Record #{recordId} is created.");
        }

        private static void List(string parameters)
        {
            ReadOnlyCollection<FileCabinetRecord> list = Program.fileCabinetService.GetRecords();

            OutputToTheConsoleDataFromTheList(list);
        }

        private static void Edit(string parameters)
        {
            Console.Write("Enter the record number for editing: ");
            var inputId = Console.ReadLine();
            int id;
            while (!int.TryParse(inputId, out id))
            {
                Console.WriteLine("You introduced an incorrect ID. Repeat the input.");
                Console.Write("Enter the record number for editing: ");
                inputId = Console.ReadLine();
            }

            if (Program.fileCabinetService.IsExist(id))
            {
                Console.Write("First name: ");
                var firstName = ReadInput(StringConverter, inputValidation.FirstNameValidator);

                Console.Write("Last name: ");
                var lastName = ReadInput(StringConverter, inputValidation.LastNameValidator);

                Console.Write("Date of birth: ");
                var dateOfBirth = ReadInput(DateConverter, inputValidation.DateOfBirthValidator);

                Console.Write("Gender (man - 'm' or woman - 'f'): ");
                var gender = ReadInput(CharConverter, inputValidation.GenderValidator);

                Console.Write("Height: ");
                var height = ReadInput(ShortConverter, inputValidation.HeightValidator);

                Console.Write("Weight: ");
                var weight = ReadInput(DecimalConverter, inputValidation.WeightValidator);

                FileCabinetRecordNewData fileCabinetRecordNewData = new FileCabinetRecordNewData(firstName, lastName, dateOfBirth, gender, height, weight);
                Program.fileCabinetService.EditRecord(id, fileCabinetRecordNewData);

                Console.WriteLine($"Record #{id} is updated.");
            }
            else
            {
                Console.WriteLine($"#{id} record is not found.");
            }
        }

        private static void Find(string parameters)
        {
            Console.Write("Enter search parameter: ");
            var searchOptions = Console.ReadLine().ToLowerInvariant().Split(' ');

            if (searchOptions.Length != 2)
            {
                Console.WriteLine("You have entered an invalid search parameter. Two are needed.");
                return;
            }

            var searchParameter = searchOptions[1].Trim('"');
            switch (searchOptions[0])
            {
                case "firstname":
                    OutputToTheConsoleDataFromTheList(Program.fileCabinetService.FindByFirstName(searchParameter));
                    break;
                case "lastname":
                    OutputToTheConsoleDataFromTheList(Program.fileCabinetService.FindByLastName(searchParameter));
                    break;
                case "dateofbirth":
                    if (DateTime.TryParseExact(searchParameter, "yyyy-MMM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dateOfBirth))
                    {
                        OutputToTheConsoleDataFromTheList(Program.fileCabinetService.FindByDateOfBirth(dateOfBirth));
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Error. You introduced the date in the wrong format. (correct format 2000-Jan-01)");
                        return;
                    }

                default:
                    Console.WriteLine("You entered an invalid search parameter.");
                    break;
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

        private static void OutputToTheConsoleDataFromTheList(ReadOnlyCollection<FileCabinetRecord> list)
        {
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

        private static T ReadInput<T>(Func<string, Tuple<bool, string, T>> converter, Func<T, Tuple<bool, string>> validator)
        {
            do
            {
                T value;

                var input = Console.ReadLine();
                var conversionResult = converter(input);

                if (!conversionResult.Item1)
                {
                    Console.WriteLine($"Conversion failed: {conversionResult.Item2}. Please, correct your input.");
                    continue;
                }

                value = conversionResult.Item3;

                var validationResult = validator(value);
                if (!validationResult.Item1)
                {
                    Console.WriteLine($"Validation failed: {validationResult.Item2}. Please, correct your input.");
                    continue;
                }

                return value;
            }
            while (true);
        }

        private static Tuple<bool, string, string> StringConverter(string name)
        {
            bool a = IsStringCorrect(name);

            return new Tuple<bool, string, string>(a, name, name);
        }

        private static Tuple<bool, string, DateTime> DateConverter(string date)
        {
            bool a = DateTime.TryParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var day);

            return new Tuple<bool, string, DateTime>(a, date, day);
        }

        private static Tuple<bool, string, char> CharConverter(string inputGender)
        {
            bool a = char.TryParse(inputGender, out var gender);

            return new Tuple<bool, string, char>(a, inputGender, gender);
        }

        private static Tuple<bool, string, short> ShortConverter(string inputHeight)
        {
            bool a = short.TryParse(inputHeight, CultureInfo.InvariantCulture, out var height);

            return new Tuple<bool, string, short>(a, inputHeight, height);
        }

        private static Tuple<bool, string, decimal> DecimalConverter(string inputWeight)
        {
            bool a = short.TryParse(inputWeight, CultureInfo.InvariantCulture, out var weight);

            return new Tuple<bool, string, decimal>(a, inputWeight, weight);
        }
    }
}