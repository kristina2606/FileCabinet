using System;

namespace FileCabinetApp.CommandHandlers.Commands
{
    /// <summary>
    /// Represents a command handler for handling exit requests.
    /// </summary>
    public class ExitCommandHandler : CommandHandlerBase
    {
        private readonly Action<bool> exit;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExitCommandHandler"/> class.
        /// </summary>
        /// <param name="exitAction">The exit action to be invoked.</param>
        public ExitCommandHandler(Action<bool> exitAction)
        {
            this.exit = exitAction;
        }

        /// <summary>
        /// Handles 'exit' requests.
        /// </summary>
        /// <param name="appCommand">The application command and options.</param>
        public override void Handle(AppCommandRequest appCommand)
        {
            if (!appCommand.Command.Equals("exit", StringComparison.OrdinalIgnoreCase))
            {
                base.Handle(appCommand);
                return;
            }

            this.exit.Invoke(true);
        }
    }
}
