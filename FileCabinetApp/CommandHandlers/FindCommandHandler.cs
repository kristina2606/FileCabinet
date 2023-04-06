using System;
using System.Collections.ObjectModel;
using System.Globalization;

namespace FileCabinetApp.CommandHandlers
{
    /// <summary>
    /// Contain code for handling find requests.
    /// </summary>
    public class FindCommandHandler : ServiceCommandHandlerBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FindCommandHandler"/> class.
        /// </summary>
        /// <param name="service">Interface instance IFileCabinetServise.</param>
        public FindCommandHandler(IFileCabinetService service)
            : base(service)
        {
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
                        OutputToTheConsoleDataFromTheList(this.service.FindByFirstName(searchParameter));
                        break;
                    case "lastname":
                        OutputToTheConsoleDataFromTheList(this.service.FindByLastName(searchParameter));
                        break;
                    case "dateofbirth":
                        if (DateTime.TryParseExact(searchParameter, "yyyy-MMM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dateOfBirth))
                        {
                            OutputToTheConsoleDataFromTheList(this.service.FindByDateOfBirth(dateOfBirth));
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

        private static void OutputToTheConsoleDataFromTheList(ReadOnlyCollection<FileCabinetRecord> list)
        {
            foreach (var record in list)
            {
                var id = record.Id;
                var firstName = record.FirstName;
                var lastName = record.LastName;
                var dateOfBirth = record.DateOfBirth.ToString("yyyy-MMM-dd", CultureInfo.InvariantCulture);
                var gender = record.Gender;
                var height = record.Height;
                var weight = record.Weight;

                Console.WriteLine($"#{id}, {firstName}, {lastName}, {dateOfBirth}, {gender}, {height}, {weight}");
            }
        }
    }
}
