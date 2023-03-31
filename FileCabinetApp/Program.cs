using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;

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
        private const string FileTypeCsv = "csv";
        private const string FileTypeXml = "xml";
        private const string FileNameFormatDatabasePath = "cabinet-records.db";

        private static bool isRunning = true;
        private static IFileCabinetService fileCabinetService = new FileCabinetMemoryService(new DefaultValidator());
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
            new Tuple<string, Action<string>>("export", Export),
            new Tuple<string, Action<string>>("import", Import),
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
            new string[] { "export", "exports service data to .csv or .xml file.", "The 'export' exports service data to .csv or .xml file." },
            new string[] { "import", "import data from .csv or .xml file.", "The 'import' import data from .csv or .xml file." },
        };

        /// <summary>
        /// Receives a command from the user.
        /// </summary>
        /// <param name="args">Arguments of the appropriate type.</param>
        public static void Main(string[] args)
        {
            Console.WriteLine($"File Cabinet Application, developed by {Program.DeveloperName}");

            for (var i = 0; i < args.Length; i++)
            {
                var comand = args[i].Split('=');
                if ((comand[0] == "--validation-rules" && comand[1].ToLowerInvariant() == "custom") || (args[i] == "-v" && args[i + 1].ToLowerInvariant() == "custom"))
                {
                    fileCabinetService = new FileCabinetMemoryService(new CustomValidator());
                    inputValidation = new UserInputValidationCustom();
                    validationRules = "Using custom validation rules.";
                }

                if ((comand[0] == "--storage" && comand[1].ToLowerInvariant() == "file") || (args[i] == "-s" && args[i + 1].ToLowerInvariant() == "file"))
                {
                    fileCabinetService = new FileCabinetFilesystemService(new FileStream(FileNameFormatDatabasePath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None));
                }
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
            var firstName = ReadInput(Converter.StringConverter, inputValidation.ValidateFirstName);

            Console.Write("Last name: ");
            var lastName = ReadInput(Converter.StringConverter, inputValidation.ValidateLastName);

            Console.Write("Date of birth: ");
            var dateOfBirth = ReadInput(Converter.DateConverter, inputValidation.ValidateDateOfBirth);

            Console.Write("Gender (man - 'm' or woman - 'f'): ");
            var gender = ReadInput(Converter.CharConverter, inputValidation.ValidateGender);

            Console.Write("Height: ");
            var height = ReadInput(Converter.ShortConverter, inputValidation.ValidateHeight);

            Console.Write("Weight: ");
            var weight = ReadInput(Converter.DecimalConverter, inputValidation.ValidateWeight);

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
                var firstName = ReadInput(Converter.StringConverter, inputValidation.ValidateFirstName);

                Console.Write("Last name: ");
                var lastName = ReadInput(Converter.StringConverter, inputValidation.ValidateLastName);

                Console.Write("Date of birth: ");
                var dateOfBirth = ReadInput(Converter.DateConverter, inputValidation.ValidateDateOfBirth);

                Console.Write("Gender (man - 'm' or woman - 'f'): ");
                var gender = ReadInput(Converter.CharConverter, inputValidation.ValidateGender);

                Console.Write("Height: ");
                var height = ReadInput(Converter.ShortConverter, inputValidation.ValidateHeight);

                Console.Write("Weight: ");
                var weight = ReadInput(Converter.DecimalConverter, inputValidation.ValidateWeight);

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

        private static void Export(string parameters)
        {
            var makeSnapshot = Program.fileCabinetService.MakeSnapshot();

            Console.Write("Enter export format (csv/xml): ");
            var format = Console.ReadLine().ToLowerInvariant();

            if (format != FileTypeCsv && format != FileTypeXml)
            {
                Console.WriteLine("You entered an invalid format.");
                return;
            }

            Console.Write("Enter the export path: ");
            var path = Console.ReadLine();

            var folder = Path.GetDirectoryName(path);
            if (!Directory.Exists(folder))
            {
                Console.WriteLine($"Export failed: can't open file {path}.");
            }
            else if (File.Exists(path))
            {
                Console.Write($"File is exist - rewrite {path}? [Y/n] ");
                var fileRewrite = Console.ReadLine().ToLowerInvariant();

                if (fileRewrite == "y" || string.IsNullOrEmpty(fileRewrite))
                {
                    ExportData(makeSnapshot, format, path);
                }
                else if (fileRewrite != "n")
                {
                    Console.WriteLine("You entered an invalid character.");
                }
            }
            else
            {
                ExportData(makeSnapshot, format, path);
            }

            Console.WriteLine($"All records are exported to file {path}.");
        }

        private static void ExportData(FileCabinetServiceSnapshot makeSnapshot, string format, string path)
        {
            using (StreamWriter sw = new StreamWriter(path))
            {
                switch (format)
                {
                    case FileTypeCsv:
                        makeSnapshot.SaveToCsv(sw);
                        break;
                    case FileTypeXml:
                        makeSnapshot.SaveToXml(sw);
                        break;
                }
            }
        }

        private static void Import(string parameters)
        {
            Console.Write("Enter export format (csv/xml): ");
            var format = Console.ReadLine().ToLowerInvariant();

            if (format != FileTypeCsv && format != FileTypeXml)
            {
                Console.WriteLine("You entered an invalid format.");
                return;
            }

            Console.Write("Enter the import path: ");
            var path = Console.ReadLine();

            if (!File.Exists(path))
            {
                Console.WriteLine($"Import error: {path} is not exist.");
                return;
            }

            using (FileStream fs = new FileStream(path, FileMode.Open))
            {
                FileCabinetServiceSnapshot fileCabinetServiceSnapshot = new FileCabinetServiceSnapshot();
                using (StreamReader sr = new StreamReader(fs))
                {
                    switch (format)
                    {
                        case FileTypeCsv:
                            fileCabinetServiceSnapshot.LoadFromCsv(sr);
                            break;
                        case FileTypeXml:
                            fileCabinetServiceSnapshot.LoadFromXml(sr);
                            break;
                    }
                }

                try
                {
                    Program.fileCabinetService.Restore(fileCabinetServiceSnapshot);
                }
                catch (ImportException dict)
                {
                    foreach (var exeption in dict.Dictionary)
                    {
                        Console.WriteLine($"Record with id = {exeption.Key} - {exeption.Value}.");
                    }
                }

                Console.WriteLine($"All records were imported from {path}.");
            }
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
    }
}