using System;
using System.Collections.Generic;
using System.Linq;

namespace FileCabinetApp.CommandHandlers
{
    /// <summary>
    /// Contain code for handling delete requests.
    /// </summary>
    public class DeleteCommandHandler : ServiceCommandHandlerBase
    {
        private readonly StringComparison stringComparison = StringComparison.InvariantCultureIgnoreCase;
        private readonly IUserInputValidation validationRules;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteCommandHandler"/> class.
        /// </summary>
        /// <param name="service">Interface instance IFileCabinetServise.</param>
        /// <param name="inputValidation">Interface instance IUserInputValidation.</param>
        public DeleteCommandHandler(IFileCabinetService service, IUserInputValidation inputValidation)
            : base(service)
        {
            this.validationRules = inputValidation;
        }

        /// <summary>
        /// Handling for delete requests.
        /// </summary>
        /// <param name="appCommand">>Configuratiion the application command and options.</param>
        public override void Handle(AppCommandRequest appCommand)
        {
            if (!appCommand.Command.Equals("delete", this.stringComparison))
            {
                base.Handle(appCommand);
            }

            var parametrs = appCommand.Parameters.Replace("where ", string.Empty, this.stringComparison).Replace("'", string.Empty, this.stringComparison).Split('=');

            if (parametrs.Length != 2)
            {
                Console.WriteLine("You introduced an incorrect parametrs.");
                return;
            }

            string parametrForDelete = parametrs[0].ToLowerInvariant();
            string valueForDelete = parametrs[1];
            List<int> recordsForDelete = new List<int>();
            List<int> deletedRecords = new List<int>();

            try
            {
                switch (parametrForDelete)
                {
                    case "id":
                        recordsForDelete.Add(Converter.IntConverter(valueForDelete).Item3);
                        break;
                    case "firstname":
                        var firstName = UserInputHelpers.Convert(Converter.StringConverter, this.validationRules.ValidateFirstName, valueForDelete);
                        recordsForDelete = this.Service.FindByFirstName(firstName).Select(x => x.Id).ToList();
                        break;
                    case "lastname":
                        var lastName = UserInputHelpers.Convert(Converter.StringConverter, this.validationRules.ValidateLastName, valueForDelete);
                        recordsForDelete = this.Service.FindByLastName(lastName).Select(x => x.Id).ToList();
                        break;
                    case "dateofbirth":
                        var dateOfBirth = UserInputHelpers.Convert(Converter.DateConverter, this.validationRules.ValidateDateOfBirth, valueForDelete);
                        recordsForDelete = this.Service.FindByDateOfBirth(dateOfBirth).Select(x => x.Id).ToList();
                        break;
                }

                foreach (var record in recordsForDelete)
                {
                    this.Service.Remove(record);

                    deletedRecords.Add(record);
                }

                Console.WriteLine($"Records #{string.Join(", #", deletedRecords)} are deleted.");
            }
            catch
            {
                Console.WriteLine($"Record with {valueForDelete} parametr doesn't exists.");
            }
        }
    }
}
