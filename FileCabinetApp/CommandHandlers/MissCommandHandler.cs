using System;

namespace FileCabinetApp.CommandHandlers
{
    /// <summary>
    /// Contain code for handling miss command requests.
    /// </summary>
    public class MissCommandHandler : CommandHandlerBase
    {
        /// <summary>
        /// Handling for miss command requests.
        /// </summary>
        /// <param name="appCommand">>Configuratiion the application command and options.</param>
        public override void Handle(AppCommandRequest appCommand)
        {
            if (!string.IsNullOrEmpty(appCommand.Command))
            {
                Console.WriteLine($"There is no '{appCommand.Command}' command.");
                Console.WriteLine();
            }
            else if (appCommand.Command != null)
            {
                base.Handle(appCommand);
            }
        }
    }
}
