using System;

namespace FileCabinetApp.CommandHandlers
{
    /// <summary>
    /// Contain code for handling stat requests.
    /// </summary>
    public class StatCommandHandler : ServiceCommandHandlerBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StatCommandHandler"/> class.
        /// </summary>
        /// <param name="service">Interface instance IFileCabinetServise.</param>
        public StatCommandHandler(IFileCabinetService service)
            : base(service)
        {
        }

        /// <summary>
        /// Handling for stat requests.
        /// </summary>
        /// <param name="appCommand">>Configuratiion the application command and options.</param>
        public override void Handle(AppCommandRequest appCommand)
        {
            if (appCommand.Command.Equals("stat", StringComparison.InvariantCultureIgnoreCase))
            {
                var (activeRecords, deletedRecords) = this.service.GetStat();
                Console.WriteLine($"{activeRecords + deletedRecords} record(s), {deletedRecords} of them deleted.");
            }
            else if (appCommand.Command != null)
            {
                base.Handle(appCommand);
            }
        }
    }
}
