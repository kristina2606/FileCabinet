using System;
using System.Collections.Generic;

namespace FileCabinetApp.CommandHandlers
{
    /// <summary>
    /// Contain code for handling miss command requests.
    /// </summary>
    public class MissCommandHandler : CommandHandlerBase
    {
        private readonly string[] allCommands = { "help", "exit", "stat", "create", "list", "find", "export", "import", "purge", "insert", "delete", "update" };

        /// <summary>
        /// Handling for miss command requests.
        /// </summary>
        /// <param name="appCommand">>Configuratiion the application command and options.</param>
        public override void Handle(AppCommandRequest appCommand)
        {
            if (string.IsNullOrEmpty(appCommand.Command))
            {
                base.Handle(appCommand);
                return;
            }

            Console.WriteLine($"'{appCommand.Command}' is not a command. See 'help'.");
            Console.WriteLine();

            var similarCommands = new List<string>();
            int bestDistance = 2;

            foreach (var command in this.allCommands)
            {
                if (bestDistance >= LevenshteinDistance(command, appCommand.Command))
                {
                    similarCommands.Add(command);
                }
            }

            if (similarCommands.Count > 0)
            {
                Console.WriteLine("The most similar commands are");
                foreach (var command in similarCommands)
                {
                    Console.WriteLine(command);
                }

                Console.WriteLine();
            }
        }

        private static int LevenshteinDistance(string command, string inputCommand)
        {
            int[,] matrix = new int[command.Length + 1, inputCommand.Length + 1];

            for (var i = 0; i <= command.Length; i++)
            {
                matrix[i, 0] = i;
            }

            for (var j = 0; j <= inputCommand.Length; j++)
            {
                matrix[0, j] = j;
            }

            for (var i = 1; i <= command.Length; i++)
            {
                for (var j = 1; j <= inputCommand.Length; j++)
                {
                    var equal = command[i - 1] == inputCommand[j - 1] ? 0 : 1;

                    var delete = matrix[i, j - 1] + 1;
                    var insert = matrix[i - 1, j] + 1;
                    var change = matrix[i - 1, j - 1] + equal;

                    matrix[i, j] = Math.Min(Math.Min(delete, insert), change);
                }
            }

            return matrix[command.Length, inputCommand.Length];
        }
    }
}
