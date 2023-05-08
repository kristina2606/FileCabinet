using System;

namespace FileCabinetApp.CommandHandlers.Commands
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
            new string[] { "export", "exports service data to .csv or .xml file.", "The 'export' exports service data to .csv or .xml file." },
            new string[] { "import", "import data from .csv or .xml file.", "The 'import' import data from .csv or .xml file." },
            new string[] { "purge", "defragments the data file.", "The 'purge' command defragments the data file." },
            new string[] { "insert", "adds a record using the passed data.", "The 'insert' adds a record using the passed data." },
            new string[] { "delete", "delete record by predetermined criteria.", "The 'delete' delete record by predetermined criteria." },
            new string[] { "update", "update record fields using the specified search criteria.", "The 'update' update record fields using the specified search criteria." },
            new string[] { "select", "accept a list of fields to display and search criteria.", "The 'select' accept a list of fields to display and search criteria." },
};

        /// <summary>
        /// Handling for help requests.
        /// </summary>
        /// <param name="appCommand">>Configuratiion the application command and options.</param>
        public override void Handle(AppCommandRequest appCommand)
        {
            if (!appCommand.Command.Equals("help", StringComparison.InvariantCultureIgnoreCase))
            {
                base.Handle(appCommand);
                return;
            }

            if (!string.IsNullOrEmpty(appCommand.Parameters))
            {
                var index = Array.FindIndex(helpMessages, 0, helpMessages.Length, i => string.Equals(i[CommandHelpIndex], appCommand.Parameters, StringComparison.InvariantCultureIgnoreCase));
                if (index >= 0)
                {
                    Console.WriteLine(helpMessages[index][ExplanationHelpIndex]);
                }
                else
                {
                    Console.WriteLine($"There is no explanation for '{appCommand.Parameters}' command.");
                }
            }
            else
            {
                Console.WriteLine("Available commands:");

                foreach (var helpMessage in helpMessages)
                {
                    Console.WriteLine("\t{0}\t- {1}", helpMessage[CommandHelpIndex], helpMessage[DescriptionHelpIndex]);
                }
            }

            Console.WriteLine();
        }
    }
}
