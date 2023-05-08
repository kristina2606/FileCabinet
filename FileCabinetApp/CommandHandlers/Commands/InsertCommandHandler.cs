using System;
using FileCabinetApp.CommandHandlers.Helpers;
using FileCabinetApp.FileCabinetService;
using FileCabinetApp.Helpers;
using FileCabinetApp.Models;
using FileCabinetApp.UserInputValidator;

namespace FileCabinetApp.CommandHandlers.Commands
{
    /// <summary>
    /// Contain code for handling insert requests.
    /// </summary>
    public class InsertCommandHandler : ServiceCommandHandlerBase
    {
        private const int NumberFieldsInRecord = 7;
        private readonly StringComparison stringComparison = StringComparison.InvariantCultureIgnoreCase;
        private readonly IUserInputValidation validationRules;

        /// <summary>
        /// Initializes a new instance of the <see cref="InsertCommandHandler"/> class.
        /// </summary>
        /// <param name="service">Interface instance IFileCabinetServise.</param>
        /// <param name="inputValidation">Interface instance IUserInputValidation.</param>
        public InsertCommandHandler(IFileCabinetService service, IUserInputValidation inputValidation)
            : base(service)
        {
            validationRules = inputValidation;
        }

        /// <summary>
        /// Handling for insert requests.
        /// </summary>
        /// <param name="appCommand">>Configuratiion the application command and options.</param>
        public override void Handle(AppCommandRequest appCommand)
        {
            if (!appCommand.Command.Equals("insert", stringComparison))
            {
                base.Handle(appCommand);
                return;
            }

            var parametrs = appCommand.Parameters.Split(QueryConstants.Values);

            if (parametrs.Length != 2)
            {
                Console.WriteLine("You introduced an incorrect data.");
                return;
            }

            string[] fields = parametrs[0].Replace("(", string.Empty, stringComparison)
                                          .Replace(")", string.Empty, stringComparison)
                                          .Split(',');

            string[] values = parametrs[1].Replace("(", string.Empty, stringComparison)
                                          .Replace(")", string.Empty, stringComparison)
                                          .Replace("'", string.Empty, stringComparison)
                                          .Split(',');

            if (fields.Length != NumberFieldsInRecord || values.Length != NumberFieldsInRecord)
            {
                Console.WriteLine("Incorrect number of fields or values.");
                return;
            }

            var record = new FileCabinetRecord();
            try
            {
                for (var i = 0; i < fields.Length; i++)
                {
                    var fieldForInsert = fields[i].Trim().ToLowerInvariant();
                    var valueForInsert = values[i].Trim();

                    switch (fieldForInsert)
                    {
                        case "id":
                            record.Id = Converter.IntConverter(valueForInsert).Item3;
                            break;
                        case "firstname":
                            record.FirstName = UserInputHelpers.Convert(Converter.StringConverter, validationRules.ValidateFirstName, valueForInsert);
                            break;
                        case "lastname":
                            record.LastName = UserInputHelpers.Convert(Converter.StringConverter, validationRules.ValidateLastName, valueForInsert);
                            break;
                        case "dateofbirth":
                            record.DateOfBirth = UserInputHelpers.Convert(Converter.DateConverter, validationRules.ValidateDateOfBirth, valueForInsert);
                            break;
                        case "gender":
                            record.Gender = UserInputHelpers.Convert(Converter.CharConverter, validationRules.ValidateGender, valueForInsert);
                            break;
                        case "height":
                            record.Height = UserInputHelpers.Convert(Converter.ShortConverter, validationRules.ValidateHeight, valueForInsert);
                            break;
                        case "weight":
                            record.Weight = UserInputHelpers.Convert(Converter.DecimalConverter, validationRules.ValidateWeight, valueForInsert);
                            break;
                    }
                }

                Service.Insert(record);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}