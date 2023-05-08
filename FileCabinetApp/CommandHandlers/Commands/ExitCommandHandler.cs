using System;

namespace FileCabinetApp.CommandHandlers.Commands
{
    /// <summary>
    /// Contain code for handling exit requests.
    /// </summary>
    public class ExitCommandHandler : CommandHandlerBase
    {
        private readonly Action<bool> exit;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExitCommandHandler"/> class.
        /// </summary>
        /// <param name="isRunning">Application settings(on/off).</param>
        public ExitCommandHandler(Action<bool> isRunning)
        {
            this.exit = isRunning;
        }

        /// <summary>
        /// Handling for exit requests.
        /// </summary>
        /// <param name="appCommand">>Configuratiion the application command and options.</param>
        public override void Handle(AppCommandRequest appCommand)
        {
            if (!appCommand.Command.Equals("exit", StringComparison.InvariantCultureIgnoreCase))
            {
                base.Handle(appCommand);
                return;
            }

            this.exit(true);
        }
    }
}
