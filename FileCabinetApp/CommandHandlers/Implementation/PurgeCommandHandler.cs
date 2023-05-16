using System;
using FileCabinetApp.FileCabinetService;

namespace FileCabinetApp.CommandHandlers.Commands
{
    /// <summary>
    /// Represents a command handler for handling purge requests.
    /// </summary>
    public class PurgeCommandHandler : ServiceCommandHandlerBase
    {
        private const string CommandName = "purge";

        /// <summary>
        /// Initializes a new instance of the <see cref="PurgeCommandHandler"/> class.
        /// </summary>
        /// <param name="service">The file cabinet service.</param>
        public PurgeCommandHandler(IFileCabinetService service)
            : base(service)
        {
        }

        /// <summary>
        /// Handles 'purge' requests.
        /// </summary>
        /// <param name="appCommand">The application command and options.</param>
        public override void Handle(AppCommandRequest appCommand)
        {
            if (!appCommand.Command.Equals(CommandName, StringComparison.OrdinalIgnoreCase))
            {
                base.Handle(appCommand);
                return;
            }

            var (activeRecords, deletedRecords) = this.Service.GetStat();
            var purgedRecordsCount = this.Service.Purge();

            Console.WriteLine($"Data file processing completed: {purgedRecordsCount} of {activeRecords + deletedRecords} records were purged.");
        }
    }
}