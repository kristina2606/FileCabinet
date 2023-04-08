using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using FileCabinetApp.CommandHandlers;

namespace FileCabinetApp
{
    /// <summary>
    /// Works with user input when working with records.
    /// </summary>
    public static class Program
    {
        private const string DeveloperName = "Krystsina Labada";
        private const string HintMessage = "Enter your command, or enter 'help' to get help.";
        private const string FileNameFormatDatabasePath = "cabinet-records.db";
        private const string DefaultValidationRules = "Using default validation rules.";
        private const string CustomValidationRules = "Using default validation rules.";

        private static bool isRunning = true;

        private static IRecordValidator defaultValidator = new ValidatorBuilder()
            .ValidateFirstName(2, 60)
            .ValidateLastName(2, 60)
            .ValidateDateOfBirth(0, 75)
            .ValidateGender('f', 'm', StringComparison.InvariantCulture)
            .ValidateHeight(0, 250)
            .ValidateWeight(0, 300)
            .Create();

        private static IRecordValidator customValidator = new ValidatorBuilder()
             .ValidateFirstName(2, 15)
             .ValidateLastName(2, 20)
             .ValidateDateOfBirth(18, 150)
             .ValidateGender('f', 'm', StringComparison.InvariantCultureIgnoreCase)
             .ValidateHeight(145, 250)
             .ValidateWeight(40, 300)
             .Create();

        private static IFileCabinetService fileCabinetService = new FileCabinetMemoryService(defaultValidator);
        private static IUserInputValidation inputValidation = new UserInputValidationDafault();
        private static string validationRules = DefaultValidationRules;

        private static Action<bool> onExited;

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
                    fileCabinetService = new FileCabinetMemoryService(customValidator);
                    inputValidation = new UserInputValidationCustom();
                    validationRules = CustomValidationRules;
                }

                if ((comand[0] == "--storage" && comand[1].ToLowerInvariant() == "file") || (args[i] == "-s" && args[i + 1].ToLowerInvariant() == "file"))
                {
                    if (validationRules == CustomValidationRules)
                    {
                        fileCabinetService = new FileCabinetFilesystemService(new FileStream(FileNameFormatDatabasePath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None), customValidator);
                    }
                    else
                    {
                        fileCabinetService = new FileCabinetFilesystemService(new FileStream(FileNameFormatDatabasePath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None), defaultValidator);
                    }
                }
            }

            Console.WriteLine(Program.validationRules);
            Console.WriteLine(Program.HintMessage);
            Console.WriteLine();

            onExited += Exit;
            var commandHandler = CreateCommandHandlers();

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

                const int parametersIndex = 1;
                var parameters = inputs.Length > 1 ? inputs[parametersIndex] : string.Empty;

                commandHandler.Handle(new AppCommandRequest(command, parameters));
            }
            while (isRunning);
        }

        private static void Exit(bool exit)
        {
            Console.WriteLine("Exiting an application...");
            isRunning = exit;
        }

        private static void DefaultRecordPrint(IEnumerable<FileCabinetRecord> records)
        {
            foreach (var record in records)
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

        private static ICommandHandler CreateCommandHandlers()
        {
            var helpHandler = new HelpCommandHandler();
            var createHandler = new CreateCommandHandler(Program.fileCabinetService, Program.inputValidation);
            var editHandler = new EditCommandHandler(Program.fileCabinetService, Program.inputValidation);
            var listHandler = new ListCommandHandler(Program.fileCabinetService, DefaultRecordPrint);
            var statHandler = new StatCommandHandler(Program.fileCabinetService);
            var findHandler = new FindCommandHandler(Program.fileCabinetService, DefaultRecordPrint);
            var exportHandler = new ExportCommandHandler(Program.fileCabinetService);
            var importHandler = new ImportCommandHandler(Program.fileCabinetService);
            var removeHandler = new RemoveCommandHandler(Program.fileCabinetService);
            var purgeHandler = new PurgeCommandHandler(Program.fileCabinetService);
            var exitHandler = new ExitCommandHandler(onExited);
            var missHandler = new MissCommandHandler();

            helpHandler.SetNext(createHandler);
            createHandler.SetNext(editHandler);
            editHandler.SetNext(listHandler);
            listHandler.SetNext(statHandler);
            statHandler.SetNext(findHandler);
            findHandler.SetNext(exportHandler);
            exportHandler.SetNext(importHandler);
            importHandler.SetNext(removeHandler);
            removeHandler.SetNext(purgeHandler);
            purgeHandler.SetNext(exitHandler);
            exitHandler.SetNext(missHandler);

            return helpHandler;
        }
    }
}