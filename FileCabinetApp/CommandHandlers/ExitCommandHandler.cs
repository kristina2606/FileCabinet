using System;

namespace FileCabinetApp.CommandHandlers
{
    /// <summary>
    /// Contain code for handling exit requests.
    /// </summary>
    public class ExitCommandHandler : CommandHandlerBase
    {
        private readonly Action<bool> onExited;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExitCommandHandler"/> class.
        /// </summary>
        /// <param name="isRunning">Application settings(on/off).</param>
        public ExitCommandHandler(Action<bool> isRunning)
        {
            this.onExited = isRunning;
        }

        /// <summary>
        /// Handling for exit requests.
        /// </summary>
        /// <param name="appCommand">>Configuratiion the application command and options.</param>
        public override void Handle(AppCommandRequest appCommand)
        {
            if (appCommand.Command.Equals("exit", StringComparison.InvariantCultureIgnoreCase))
            {
                this.onExited(false);
            }
            else if (appCommand.Command != null)
            {
                base.Handle(appCommand);
            }
        }
    }
}
