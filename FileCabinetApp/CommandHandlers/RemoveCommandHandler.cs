using System;

namespace FileCabinetApp.CommandHandlers
{
    /// <summary>
    /// Contain code for handling remove requests.
    /// </summary>
    public class RemoveCommandHandler : ServiceCommandHandlerBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RemoveCommandHandler"/> class.
        /// </summary>
        /// <param name="service">Interface instance IFileCabinetServise.</param>
        public RemoveCommandHandler(IFileCabinetService service)
            : base(service)
        {
        }

        /// <summary>
        /// Handling for remove requests.
        /// </summary>
        /// <param name="appCommand">>Configuratiion the application command and options.</param>
        public override void Handle(AppCommandRequest appCommand)
        {
            if (appCommand.Command.Equals("remove", StringComparison.InvariantCultureIgnoreCase))
            {
                if (!int.TryParse(appCommand.Parameters, out int id))
                {
                    Console.WriteLine("You introduced an incorrect ID.");
                    return;
                }

                try
                {
                    this.service.Remove(id);

                    Console.WriteLine($"Record #{id} is removed.");
                }
                catch
                {
                    Console.WriteLine($"Record #{id} doesn't exists.");
                }
            }
            else
            {
                base.Handle(appCommand);
            }
        }
    }
}
