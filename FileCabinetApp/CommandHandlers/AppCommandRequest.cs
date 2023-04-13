namespace FileCabinetApp.CommandHandlers
{
    /// <summary>
    /// Configures the application command and options.
    /// </summary>
    public class AppCommandRequest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AppCommandRequest"/> class.
        /// </summary>
        /// <param name="command">customizable command.</param>
        /// <param name="parameters">customizable options.</param>
        public AppCommandRequest(string command, string parameters)
        {
            this.Command = command;
            this.Parameters = parameters;
        }

        /// <summary>
        /// Gets customizable command.
        /// </summary>
        /// <value>
        /// Customizable command.
        /// </value>
        public string Command { get; }

        /// <summary>
        /// Getscustomizable options.
        /// </summary>
        /// <value>
        /// Customizable options.
        /// </value>
        public string Parameters { get; }
    }
}
