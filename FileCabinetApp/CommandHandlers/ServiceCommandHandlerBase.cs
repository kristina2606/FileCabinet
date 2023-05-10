using FileCabinetApp.CommandHandlers.Commands;
using FileCabinetApp.FileCabinetService;

namespace FileCabinetApp.CommandHandlers
{
    /// <summary>
    /// Base class for command handlers that operate on an instance of IFileCabinetService.
    /// </summary>
    public abstract class ServiceCommandHandlerBase : CommandHandlerBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceCommandHandlerBase"/> class.
        /// </summary>
        /// <param name="service">The file cabinet service.</param>
        protected ServiceCommandHandlerBase(IFileCabinetService service)
        {
            this.Service = service;
        }

        /// <summary>
        /// Gets the instance of IFileCabinetServise.
        /// </summary>
        /// <value>The instance IFileCabinetServise.</value>
        protected IFileCabinetService Service { get; }
    }
}
