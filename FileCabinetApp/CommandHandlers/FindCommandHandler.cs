using System;
using System.Collections.Generic;
using System.Globalization;

namespace FileCabinetApp.CommandHandlers
{
    /// <summary>
    /// Contain code for handling find requests.
    /// </summary>
    public class FindCommandHandler : ServiceCommandHandlerBase
    {
        private readonly Action<IEnumerable<FileCabinetRecord>> printer;

        /// <summary>
        /// Initializes a new instance of the <see cref="FindCommandHandler"/> class.
        /// </summary>
        /// <param name="service">Interface instance IFileCabinetServise.</param>
        /// <param name="printer">Screen print style.</param>
        public FindCommandHandler(IFileCabinetService service, Action<IEnumerable<FileCabinetRecord>> printer)
            : base(service)
        {
            this.printer = printer;
        }

        /// <summary>
        /// Handling for create requests.
        /// </summary>
        /// <param name="appCommand">>Configuratiion the application command and options.</param>
        public override void Handle(AppCommandRequest appCommand)
        {
            if (appCommand.Command.Equals("find", StringComparison.InvariantCultureIgnoreCase))
            {
                var searchParametrs = appCommand.Parameters.ToLowerInvariant().Split(' ');

                if (searchParametrs.Length != 2)
                {
                    Console.WriteLine("You have entered an invalid search parameter. Two are needed.");
                    return;
                }

                var searchСategory = searchParametrs[0];
                var searchParameter = searchParametrs[1].Trim('"');

                switch (searchСategory)
                {
                    case "firstname":
                        this.printer(this.service.FindByFirstName(searchParameter));
                        break;
                    case "lastname":

                        this.printer(this.service.FindByLastName(searchParameter));
                        break;
                    case "dateofbirth":
                        if (DateTime.TryParseExact(searchParameter, "yyyy-MMM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dateOfBirth))
                        {
                            this.printer(this.service.FindByDateOfBirth(dateOfBirth));
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Error. You introduced the date in the wrong format. (correct format 2000-Jan-01)");
                            return;
                        }

                    default:
                        Console.WriteLine("You entered an invalid search parameter.");
                        break;
                }
            }
            else if (appCommand.Command != null)
            {
                base.Handle(appCommand);
            }
        }
    }
}
