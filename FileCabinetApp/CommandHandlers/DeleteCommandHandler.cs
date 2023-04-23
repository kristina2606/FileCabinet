using System;
using System.Collections.Generic;
using System.Linq;

namespace FileCabinetApp.CommandHandlers
{
    public class DeleteCommandHandler : ServiceCommandHandlerBase
    {
        private readonly StringComparison stringComparison = StringComparison.InvariantCultureIgnoreCase;
        private readonly IUserInputValidation validationRules;

        public DeleteCommandHandler(IFileCabinetService service, IUserInputValidation inputValidation)
            : base(service)
        {
            this.validationRules = inputValidation;
        }

        public override void Handle(AppCommandRequest appCommand)
        {
            if (appCommand.Command.Equals("delete", this.stringComparison))
            {
                var parametrs = appCommand.Parameters.Replace("where ", string.Empty, this.stringComparison).Replace("'", string.Empty, this.stringComparison).Split('=');

                if (parametrs.Length != 2)
                {
                    Console.WriteLine("You introduced an incorrect parametrs.");
                    return;
                }

                string parametrForDelete = parametrs[0].ToLowerInvariant();
                string valueForDelete = parametrs[1];
                List<int> recordsForDelete = new List<int>();

                try
                {
                    switch (parametrForDelete)
                    {
                        case "id":
                            recordsForDelete.Add(Converter.IntConverter(valueForDelete).Item3);
                            break;
                        case "firstname":
                            var firstName = Convert(Converter.StringConverter, this.validationRules.ValidateFirstName, valueForDelete);
                            recordsForDelete = this.Service.FindByFirstName(firstName).Select(x => x.Id).ToList();
                            break;
                        case "lastname":
                            var lastName = Convert(Converter.StringConverter, this.validationRules.ValidateLastName, valueForDelete);
                            recordsForDelete = this.Service.FindByLastName(lastName).Select(x => x.Id).ToList();
                            break;
                        case "dateofbirth":
                            var dateOfBirth = Convert(Converter.DateConverter, this.validationRules.ValidateDateOfBirth, valueForDelete);
                            recordsForDelete = this.Service.FindByDateOfBirth(dateOfBirth).Select(x => x.Id).ToList();
                            break;
                    }

                    int[] deletedRecords = this.Service.Delete(recordsForDelete);

                    Console.WriteLine($"Records #{string.Join(", #", deletedRecords)} are deleted.");
                }
                catch
                {
                    Console.WriteLine($"Record with {valueForDelete} parametr doesn't exists.");
                }
            }
            else
            {
                base.Handle(appCommand);
            }

        }

        private static T Convert<T>(Func<string, Tuple<bool, string, T>> converter, Func<T, Tuple<bool, string>> validator, string value)
        {
            var conversionResult = converter(value);

            if (conversionResult.Item1 && validator(conversionResult.Item3).Item1)
            {
                return conversionResult.Item3;
            }

            throw new ArgumentException($"Validation failed: {conversionResult.Item2}.");
        }

    }
}
