﻿using System;

namespace FileCabinetApp.CommandHandlers
{
    /// <summary>
    /// Contain code for handling help requests.
    /// </summary>
    public class HelpCommandHandler : CommandHandlerBase
    {
        private const int CommandHelpIndex = 0;
        private const int DescriptionHelpIndex = 1;
        private const int ExplanationHelpIndex = 2;

        private readonly string[][] helpMessages = new string[][]
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
            new string[] { "remove", "remove record by id.", "The 'remove' remove record by id." },
            new string[] { "purge", "The command defragments the data file.", "The 'purge' command defragments the data file." },
            new string[] { "insert", "The command adds a record using the passed data.", "The 'insert' adds a record using the passed data." },
};

        /// <summary>
        /// Handling for help requests.
        /// </summary>
        /// <param name="appCommand">>Configuratiion the application command and options.</param>
        public override void Handle(AppCommandRequest appCommand)
        {
            if (appCommand.Command.Equals("help", StringComparison.InvariantCultureIgnoreCase))
            {
                if (!string.IsNullOrEmpty(appCommand.Parameters))
                {
                    var index = Array.FindIndex(this.helpMessages, 0, this.helpMessages.Length, i => string.Equals(i[CommandHelpIndex], appCommand.Parameters, StringComparison.InvariantCultureIgnoreCase));
                    if (index >= 0)
                    {
                        Console.WriteLine(this.helpMessages[index][ExplanationHelpIndex]);
                    }
                    else
                    {
                        Console.WriteLine($"There is no explanation for '{appCommand.Parameters}' command.");
                    }
                }
                else
                {
                    Console.WriteLine("Available commands:");

                    foreach (var helpMessage in this.helpMessages)
                    {
                        Console.WriteLine("\t{0}\t- {1}", helpMessage[CommandHelpIndex], helpMessage[DescriptionHelpIndex]);
                    }
                }

                Console.WriteLine();
            }
            else
            {
                base.Handle(appCommand);
            }
        }
    }
}
