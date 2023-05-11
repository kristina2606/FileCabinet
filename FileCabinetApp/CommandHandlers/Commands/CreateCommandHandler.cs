using System;
using FileCabinetApp.CommandHandlers.Helpers;
using FileCabinetApp.FileCabinetService;
using FileCabinetApp.Helpers;
using FileCabinetApp.Models;
using FileCabinetApp.UserInputValidator;

namespace FileCabinetApp.CommandHandlers.Commands
{
    /// <summary>
    /// Represents a command handler for handling create requests.
    /// </summary>
    public class CreateCommandHandler : ServiceCommandHandlerBase
    {
        private readonly IUserInputValidation validationRules;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateCommandHandler"/> class.
        /// </summary>
        /// <param name="service">The file cabinet service.</param>
        /// <param name="inputValidationRules">The user input validation.</param>
        public CreateCommandHandler(IFileCabinetService service, IUserInputValidation inputValidationRules)
            : base(service)
        {
            this.validationRules = inputValidationRules;
        }

        /// <summary>
        /// Handles 'create' requests.
        /// </summary>
        /// <param name="appCommand">The application command and options.</param>
        public override void Handle(AppCommandRequest appCommand)
        {
            if (!appCommand.Command.Equals("create", StringComparison.OrdinalIgnoreCase))
            {
                base.Handle(appCommand);
                return;
            }

            Console.Write("First name: ");
            var firstName = UserInputHelpers.ReadInput(Converter.StringConverter, this.validationRules.ValidateFirstName);

            Console.Write("Last name: ");
            var lastName = UserInputHelpers.ReadInput(Converter.StringConverter, this.validationRules.ValidateLastName);

            Console.Write("Date of birth: ");
            var dateOfBirth = UserInputHelpers.ReadInput(Converter.DateConverter, this.validationRules.ValidateDateOfBirth);

            Console.Write("Gender (man - 'm' or woman - 'f'): ");
            var gender = UserInputHelpers.ReadInput(Converter.CharConverter, this.validationRules.ValidateGender);

            Console.Write("Height: ");
            var height = UserInputHelpers.ReadInput(Converter.ShortConverter, this.validationRules.ValidateHeight);

            Console.Write("Weight: ");
            var weight = UserInputHelpers.ReadInput(Converter.DecimalConverter, this.validationRules.ValidateWeight);

            var newData = new FileCabinetRecordNewData(firstName, lastName, dateOfBirth, gender, height, weight);
            int recordId = this.Service.CreateRecord(newData);

            Console.WriteLine($"Record #{recordId} is created.");
        }
    }
}
