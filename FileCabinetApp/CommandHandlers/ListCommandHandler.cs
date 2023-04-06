using System;
using System.Collections.ObjectModel;
using System.Globalization;

namespace FileCabinetApp.CommandHandlers
{
    /// <summary>
    /// Contain code for handling list requests.
    /// </summary>
    public class ListCommandHandler : ServiceCommandHandlerBase
    {
        private readonly IRecordPrinter printer;

        /// <summary>
        /// Initializes a new instance of the <see cref="ListCommandHandler"/> class.
        /// </summary>
        /// <param name="service">Interface instance IFileCabinetServise.</param>
        /// <param name="printer">Screen print style.</param>
        public ListCommandHandler(IFileCabinetService service, IRecordPrinter printer)
            : base(service)
        {
            this.printer = printer;
        }

        /// <summary>
        /// Handling for list requests.
        /// </summary>
        /// <param name="appCommand">>Configuratiion the application command and options.</param>
        public override void Handle(AppCommandRequest appCommand)
        {
            if (appCommand.Command.Equals("list", StringComparison.InvariantCultureIgnoreCase))
            {
                ReadOnlyCollection<FileCabinetRecord> list = this.service.GetRecords();

                this.printer.Print(list);
            }
            else if (appCommand.Command != null)
            {
                base.Handle(appCommand);
            }
        }
    }
}
