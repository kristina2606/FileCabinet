using FileCabinetApp.CommandHandlers.Commands;
using FileCabinetApp.FileCabinetService;

namespace FileCabinetApp.CommandHandlers
{
    /// <summary>
    /// Create interface instance IFileCabinetServise.
    /// </summary>
    public abstract class ServiceCommandHandlerBase : CommandHandlerBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceCommandHandlerBase"/> class.
        /// </summary>
        /// <param name="service">Interface instance IFileCabinetServise.</param>
        protected ServiceCommandHandlerBase(IFileCabinetService service)
        {
            this.Service = service;
        }

        /// <summary>
        /// Gets contain interface instance IFileCabinetServise.
        /// </summary>
        /// <value>
        /// Interface instance IFileCabinetServise.
        /// </value>
        protected IFileCabinetService Service { get; }
    }
}
