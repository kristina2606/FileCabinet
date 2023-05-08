namespace FileCabinetApp.CommandHandlers.Commands
{
    /// <summary>
    /// Processes the request redirects the request to the next handler.
    /// </summary>
    public interface ICommandHandler
    {
        /// <summary>
        /// Changing a field to store a link to the next handler in the chain.
        /// </summary>
        /// <param name="commandHandler">Next handler in the chain.</param>
        void SetNext(ICommandHandler commandHandler);

        /// <summary>
        /// Processes the request redirects the request to the next handler.
        /// </summary>
        /// <param name="appCommand">Configuratiion the application command and options.</param>
        void Handle(AppCommandRequest appCommand);
    }
}
