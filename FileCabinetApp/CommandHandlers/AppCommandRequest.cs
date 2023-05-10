namespace FileCabinetApp.CommandHandlers
{
    /// <summary>
    /// Represents a request for an application command with customizable options.
    /// </summary>
    public class AppCommandRequest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AppCommandRequest"/> class.
        /// </summary>
        /// <param name="command">The customizable command.</param>
        /// <param name="parameters">The customizable options.</param>
        public AppCommandRequest(string command, string parameters)
        {
            this.Command = command;
            this.Parameters = parameters;
        }

        /// <summary>
        /// Gets the customizable command.
        /// </summary>
        /// <value>The customizable command.</value>
        public string Command { get; }

        /// <summary>
        /// Gets the customizable options.
        /// </summary>
        /// <value>The customizable options.</value>
        public string Parameters { get; }
    }
}
