using System;
using FileCabinetApp.FileCabinetService;

namespace FileCabinetApp.CommandHandlers.Commands
{
    /// <summary>
    /// Represents a command handler for handling stat requests.
    /// </summary>
    public class StatCommandHandler : ServiceCommandHandlerBase
    {
        private const string CommandName = "stat";

        /// <summary>
        /// Initializes a new instance of the <see cref="StatCommandHandler"/> class.
        /// </summary>
        /// <param name="service">The file cabinet service.</param>
        public StatCommandHandler(IFileCabinetService service)
            : base(service)
        {
        }

        /// <summary>
        /// Handles 'stat' requests.
        /// </summary>
        /// <param name="appCommand">The application command and options configuration.</param>
        public override void Handle(AppCommandRequest appCommand)
        {
            if (!appCommand.Command.Equals(CommandName, StringComparison.OrdinalIgnoreCase))
            {
                base.Handle(appCommand);
                return;
            }

            var (activeRecords, deletedRecords) = this.Service.GetStat();
            Console.WriteLine($"{activeRecords + deletedRecords} record(s), {deletedRecords} of them deleted.");
        }
    }
}
