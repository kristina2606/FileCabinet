using System;
using System.IO;
using FileCabinetApp.CommandHandlers;
using FileCabinetApp.CommandHandlers.Commands;
using FileCabinetApp.FileCabinetService;
using FileCabinetApp.FileCabinetService.ServiceComponents;
using FileCabinetApp.Helpers;
using FileCabinetApp.RecordValidator;
using FileCabinetApp.UserInputValidator;

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
        private const string CustomValidationRules = "Using custom validation rules.";
        private const string FileNameFormatTxt = "log.txt";

        private static bool isRunning = true;
        private static string validationRules = DefaultValidationRules;

        /// <summary>
        /// Receives a command from the user.
        /// </summary>
        /// <param name="args">Arguments of the appropriate type.</param>
        public static void Main(string[] args)
        {
            Console.WriteLine($"File Cabinet Application, developed by {DeveloperName}");

            ParseArguments(args, out IFileCabinetService fileCabinetService, out IUserInputValidation inputValidation, out StreamWriter streamWriter, out FileStream fileStream);

            Console.WriteLine(validationRules);
            Console.WriteLine(HintMessage);
            Console.WriteLine();

            var commandHandler = CreateCommandHandlers(fileCabinetService, inputValidation);

            try
            {
                do
                {
                    Console.Write("> ");
                    var line = Console.ReadLine();
                    var inputs = line != null ? line.Split(' ', 2) : new string[] { string.Empty, string.Empty };
                    const int commandIndex = 0;
                    var command = inputs[commandIndex];

                    if (string.IsNullOrEmpty(command))
                    {
                        Console.WriteLine(HintMessage);
                        continue;
                    }

                    const int parametersIndex = 1;
                    var parameters = inputs.Length > 1 ? inputs[parametersIndex] : string.Empty;

                    commandHandler.Handle(new AppCommandRequest(command, parameters));
                }
                while (isRunning);
            }
            finally
            {
                streamWriter?.Dispose();
                fileStream?.Dispose();
            }
        }

        private static void ParseArguments(string[] args, out IFileCabinetService fileCabinetService, out IUserInputValidation inputValidation, out StreamWriter streamWriter, out FileStream fileStream)
        {
            IRecordValidator validatorBuilder = new ValidatorBuilder().CreateDefault();

            streamWriter = null;
            fileStream = null;
            inputValidation = new UserInputValidationDafault();
            fileCabinetService = new FileCabinetMemoryService(validatorBuilder);

            for (var i = 0; i < args.Length; i++)
            {
                var comand = args[i].Split('=');
                if ((comand[0] == "--validation-rules" && comand[1].ToLowerInvariant() == "custom") || (args[i] == "-v" && args[i + 1].ToLowerInvariant() == "custom"))
                {
                    validatorBuilder = new ValidatorBuilder().CreateCustom();
                    inputValidation = new UserInputValidationCustom();
                    validationRules = CustomValidationRules;
                }

                if ((comand[0] == "--storage" && comand[1].ToLowerInvariant() == "file") || (args[i] == "-s" && args[i + 1].ToLowerInvariant() == "file"))
                {
                    fileStream = new FileStream(FileNameFormatDatabasePath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None);

                    fileCabinetService = new FileCabinetFilesystemService(fileStream, validatorBuilder);
                }

                if (args[i] == "-" && args[i + 1].ToLowerInvariant() == "use-stopwatch")
                {
                    fileCabinetService = new ServiceMeter(fileCabinetService);
                }

                if (args[i] == "-" && args[i + 1].ToLowerInvariant() == "use-logger")
                {
                    streamWriter = new StreamWriter(FileNameFormatTxt, true);
                    fileCabinetService = new ServiceLogger(fileCabinetService, streamWriter);
                }
            }
        }

        private static void Exit(bool exit)
        {
            Console.WriteLine("Exiting an application...");
            isRunning = !exit;
        }

        private static ICommandHandler CreateCommandHandlers(IFileCabinetService fileCabinetService, IUserInputValidation inputValidation)
        {
            var helpHandler = new HelpCommandHandler();
            var createHandler = new CreateCommandHandler(fileCabinetService, inputValidation);
            var insertHandler = new InsertCommandHandler(fileCabinetService, inputValidation);
            var updateHandler = new UpdateCommandHandler(fileCabinetService, inputValidation);
            var statHandler = new StatCommandHandler(fileCabinetService);
            var selectHandler = new SelectCommandHandler(fileCabinetService, inputValidation);
            var exportHandler = new ExportCommandHandler(fileCabinetService);
            var importHandler = new ImportCommandHandler(fileCabinetService);
            var deleteHandler = new DeleteCommandHandler(fileCabinetService, inputValidation);
            var purgeHandler = new PurgeCommandHandler(fileCabinetService);
            var exitHandler = new ExitCommandHandler(Exit);
            var missHandler = new MissCommandHandler();

            helpHandler.SetNext(createHandler);
            createHandler.SetNext(insertHandler);
            insertHandler.SetNext(updateHandler);
            updateHandler.SetNext(statHandler);
            statHandler.SetNext(selectHandler);
            selectHandler.SetNext(exportHandler);
            exportHandler.SetNext(importHandler);
            importHandler.SetNext(deleteHandler);
            deleteHandler.SetNext(purgeHandler);
            purgeHandler.SetNext(exitHandler);
            exitHandler.SetNext(missHandler);

            return helpHandler;
        }
    }
}