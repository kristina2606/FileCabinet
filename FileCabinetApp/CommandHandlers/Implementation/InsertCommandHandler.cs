using System;
using FileCabinetApp.Enums;
using FileCabinetApp.FileCabinetService;
using FileCabinetApp.Helpers;
using FileCabinetApp.Models;
using FileCabinetApp.UserInputValidator;
using FileCabinetApp.Сonstants;

namespace FileCabinetApp.CommandHandlers.Commands
{
    /// <summary>
    /// Represents a command handler for handling insert requests.
    /// </summary>
    public class InsertCommandHandler : ServiceCommandHandlerBase
    {
        private const string CommandName = "insert";
        private const int NumberFieldsInRecord = 7;
        private readonly StringComparison stringComparison = StringComparison.OrdinalIgnoreCase;
        private readonly IUserInputValidation validationRules;

        /// <summary>
        /// Initializes a new instance of the <see cref="InsertCommandHandler"/> class.
        /// </summary>
        /// <param name="service">The file cabinet service.</param>
        /// <param name="inputValidationRules">The user input validation.</param>
        public InsertCommandHandler(IFileCabinetService service, IUserInputValidation inputValidationRules)
            : base(service)
        {
            this.validationRules = inputValidationRules;
        }

        /// <summary>
        /// Handles 'insert' requests.
        /// </summary>
        /// <param name="appCommand">The application command and options.</param>
        public override void Handle(AppCommandRequest appCommand)
        {
            if (!appCommand.Command.Equals(CommandName, this.stringComparison))
            {
                base.Handle(appCommand);
                return;
            }

            var parameters = appCommand.Parameters.Split(QueryConstants.Values);
            if (parameters.Length != 2)
            {
                Console.WriteLine("Incorrect data entered.");
                return;
            }

            string[] fields = parameters[0].Replace("(", string.Empty, this.stringComparison)
                                           .Replace(")", string.Empty, this.stringComparison)
                                           .Split(',');

            string[] values = parameters[1].Replace("(", string.Empty, this.stringComparison)
                                           .Replace(")", string.Empty, this.stringComparison)
                                           .Replace("'", string.Empty, this.stringComparison)
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
                    var fieldForInsert = Enum.Parse<FileCabinetRecordFields>(fields[i].Trim().ToLowerInvariant(), true);
                    var valueForInsert = values[i].Trim();

                    this.SetNewRecordByField(record, fieldForInsert, valueForInsert);
                }

                this.Service.Insert(record);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void SetNewRecordByField(FileCabinetRecord record, FileCabinetRecordFields fieldForInsert, string valueForInsert)
        {
            switch (fieldForInsert)
            {
                case FileCabinetRecordFields.Id:
                    record.Id = Converters.IntConverter(valueForInsert).Item3;
                    break;
                case FileCabinetRecordFields.FirstName:
                    record.FirstName = UserInputHelpers.Convert(Converters.StringConverter, this.validationRules.ValidateFirstName, valueForInsert);
                    break;
                case FileCabinetRecordFields.LastName:
                    record.LastName = UserInputHelpers.Convert(Converters.StringConverter, this.validationRules.ValidateLastName, valueForInsert);
                    break;
                case FileCabinetRecordFields.DateOfBirth:
                    record.DateOfBirth = UserInputHelpers.Convert(Converters.DateConverter, this.validationRules.ValidateDateOfBirth, valueForInsert);
                    break;
                case FileCabinetRecordFields.Gender:
                    record.Gender = UserInputHelpers.Convert(Converters.CharConverter, this.validationRules.ValidateGender, valueForInsert);
                    break;
                case FileCabinetRecordFields.Height:
                    record.Height = UserInputHelpers.Convert(Converters.ShortConverter, this.validationRules.ValidateHeight, valueForInsert);
                    break;
                case FileCabinetRecordFields.Weight:
                    record.Weight = UserInputHelpers.Convert(Converters.DecimalConverter, this.validationRules.ValidateWeight, valueForInsert);
                    break;
            }
        }
    }
}