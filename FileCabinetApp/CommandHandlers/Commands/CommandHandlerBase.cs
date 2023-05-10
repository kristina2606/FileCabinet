namespace FileCabinetApp.CommandHandlers.Commands
{
    /// <summary>
    /// Represents the base class for command handlers that process requests and redirect them to the next handler.
    /// </summary>
    public abstract class CommandHandlerBase : ICommandHandler
    {
        private ICommandHandler nextHandler;

        /// <summary>
        /// Processes the request and redirects it to the next handler in the chain.
        /// </summary>
        /// <param name="appCommand">The application command and options to be processed.</param>
        public virtual void Handle(AppCommandRequest appCommand)
        {
            this.nextHandler?.Handle(appCommand);
        }

        /// <summary>
        /// Sets the next handler in the chain.
        /// </summary>
        /// <param name="commandHandler">The next handler in the chain.</param>
        public void SetNext(ICommandHandler commandHandler)
        {
            this.nextHandler = commandHandler;
        }
    }
}
