namespace FileCabinetApp.CommandHandlers.Commands
{
    /// <summary>
    /// Represents a command handler in the chain of responsibility pattern.
    /// </summary>
    public interface ICommandHandler
    {
        /// <summary>
        /// Sets the next handler in the chain.
        /// </summary>
        /// <param name="nextHandler">The next handler in the chain.</param>
        void SetNext(ICommandHandler nextHandler);

        /// <summary>
        /// Handles the command by processing it or redirecting it to the next handler in the chain.
        /// </summary>
        /// <param name="appCommand">The application command and options.</param>
        void Handle(AppCommandRequest appCommand);
    }
}
