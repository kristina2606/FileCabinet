using System;

namespace FileCabinetApp.CommandHandlers.Commands
{
    /// <summary>
    /// Represents a command handler for handling exit requests.
    /// </summary>
    public class ExitCommandHandler : CommandHandlerBase
    {
        private const string CommandName = "exit";

        private readonly Action<bool> exit;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExitCommandHandler"/> class.
        /// </summary>
        /// <param name="exit">The exit action to be invoked.</param>
        public ExitCommandHandler(Action<bool> exit)
        {
            this.exit = exit;
        }

        /// <summary>
        /// Handles 'exit' requests.
        /// </summary>
        /// <param name="appCommand">The application command and options.</param>
        public override void Handle(AppCommandRequest appCommand)
        {
            if (!appCommand.Command.Equals(CommandName, StringComparison.OrdinalIgnoreCase))
            {
                base.Handle(appCommand);
                return;
            }

            this.exit(true);
        }
    }
}
