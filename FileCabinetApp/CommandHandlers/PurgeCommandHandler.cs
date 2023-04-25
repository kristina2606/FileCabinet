using System;

namespace FileCabinetApp.CommandHandlers
{
    /// <summary>
    /// Contain code for handling purge requests.
    /// </summary>
    public class PurgeCommandHandler : ServiceCommandHandlerBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PurgeCommandHandler"/> class.
        /// </summary>
        /// <param name="service">Interface instance IFileCabinetServise.</param>
        public PurgeCommandHandler(IFileCabinetService service)
            : base(service)
        {
        }

        /// <summary>
        /// Handling for purge requests.
        /// </summary>
        /// <param name="appCommand">>Configuratiion the application command and options.</param>
        public override void Handle(AppCommandRequest appCommand)
        {
            if (!appCommand.Command.Equals("purge", StringComparison.InvariantCultureIgnoreCase))
            {
                base.Handle(appCommand);
            }

            var (activeRecords, deletedRecords) = this.Service.GetStat();

            var purgedRecordsCount = this.Service.Purge();

            Console.WriteLine($"Data file processing is completed: {purgedRecordsCount} of {activeRecords + deletedRecords} records were purged.");
        }
    }
}