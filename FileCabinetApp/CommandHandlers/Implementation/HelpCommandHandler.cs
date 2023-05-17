using System;

namespace FileCabinetApp.CommandHandlers.Commands
{
    /// <summary>
    /// Represents a command handler for handling help requests.
    /// </summary>
    public class HelpCommandHandler : CommandHandlerBase
    {
        private const string CommandName = "help";
        private const int CommandHelpIndex = 0;
        private const int DescriptionHelpIndex = 1;
        private const int ExplanationHelpIndex = 2;

        private readonly string[][] helpMessages = new string[][]
{
            new string[] { "help", "prints the help screen", "The 'help' command prints the help screen." },
            new string[] { "exit", "exits the application", "The 'exit' command exits the application." },
            new string[] { "stat", "displays statistics on records", "The 'stat' displays statistics on records." },
            new string[] { "create", "creates new record", "The 'create' creates new record." },
            new string[] { "export", "exports service data to a .csv or .xml file.", "The 'export' exports service data to a .csv or .xml file." },
            new string[] { "import", "imports data from a .csv or .xml file.", "The 'import' imports data from a .csv or .xml file." },
            new string[] { "purge", "defragments the data file.", "The 'purge' command defragments the data file." },
            new string[] { "insert", "adds a record using the passed data.", "The 'insert' adds a record using the passed data." },
            new string[] { "delete", "deletes record based on the spetified criteria.", "The 'delete' deletes record  based on the spetified criteria." },
            new string[] { "update", "updates record fields using the specified search criteria.", "The 'update' updates record fields using the specified search criteria." },
            new string[] { "select", "accepts a list of fields to display and search criteria.", "The 'select' accepts a list of fields to display and search criteria." },
};

        /// <summary>
        /// Handles 'help' requests.
        /// </summary>
        /// <param name="appCommand">The application command and options.</param>
        public override void Handle(AppCommandRequest appCommand)
        {
            if (!appCommand.Command.Equals(CommandName, StringComparison.OrdinalIgnoreCase))
            {
                base.Handle(appCommand);
                return;
            }

            if (!string.IsNullOrEmpty(appCommand.Parameters))
            {
                var index = Array.FindIndex(this.helpMessages, 0, this.helpMessages.Length, i => string.Equals(i[CommandHelpIndex], appCommand.Parameters, StringComparison.OrdinalIgnoreCase));
                if (index >= 0)
                {
                    Console.WriteLine(this.helpMessages[index][ExplanationHelpIndex]);
                }
                else
                {
                    Console.WriteLine($"There is no explanation for the '{appCommand.Parameters}' command.");
                }
            }
            else
            {
                Console.WriteLine("Available commands:");

                foreach (string[] helpMessage in this.helpMessages)
                {
                    Console.WriteLine($"\t{helpMessage[CommandHelpIndex]}\t- {helpMessage[DescriptionHelpIndex]}");
                }
            }

            Console.WriteLine();
        }
    }
}
