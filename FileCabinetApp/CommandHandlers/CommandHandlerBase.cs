namespace FileCabinetApp.CommandHandlers
{
    /// <summary>
    /// Processes the request redirects the request to the next handler.
    /// </summary>
    public abstract class CommandHandlerBase : ICommandHandler
    {
        private ICommandHandler nextHandler;

        /// <summary>
        /// Processes the request redirects the request to the next handler.
        /// </summary>
        /// <param name="appCommand">Configuratiion the application command and options.</param>
        public virtual void Handle(AppCommandRequest appCommand)
        {
            if (this.nextHandler != null)
            {
                this.nextHandler.Handle(appCommand);
            }
        }

        /// <summary>
        /// Changing a field to store a link to the next handler in the chain.
        /// </summary>
        /// <param name="commandHandler">Next handler in the chain.</param>
        public void SetNext(ICommandHandler commandHandler)
        {
            this.nextHandler = commandHandler;
        }
    }
}
