using System;
using FileCabinetApp.CommandHandlers.Helpers;
using FileCabinetApp.FileCabinetService;
using FileCabinetApp.Helpers;
using FileCabinetApp.Models;
using FileCabinetApp.UserInputValidator;

namespace FileCabinetApp.CommandHandlers.Commands
{
    /// <summary>
    /// Contain code for handling create requests.
    /// </summary>
    public class CreateCommandHandler : ServiceCommandHandlerBase
    {
        private readonly IUserInputValidation validationRules;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateCommandHandler"/> class.
        /// </summary>
        /// <param name="service">Interface instance IFileCabinetServise.</param>
        /// <param name="inputValidation">Interface instance IUserInputValidation.</param>
        public CreateCommandHandler(IFileCabinetService service, IUserInputValidation inputValidation)
            : base(service)
        {
            validationRules = inputValidation;
        }

        /// <summary>
        /// Handling for create requests.
        /// </summary>
        /// <param name="appCommand">>Configuratiion the application command and options.</param>
        public override void Handle(AppCommandRequest appCommand)
        {
            if (!appCommand.Command.Equals("create", StringComparison.InvariantCultureIgnoreCase))
            {
                base.Handle(appCommand);
                return;
            }

            Console.Write("First name: ");
            var firstName = UserInputHelpers.ReadInput(Converter.StringConverter, validationRules.ValidateFirstName);

            Console.Write("Last name: ");
            var lastName = UserInputHelpers.ReadInput(Converter.StringConverter, validationRules.ValidateLastName);

            Console.Write("Date of birth: ");
            var dateOfBirth = UserInputHelpers.ReadInput(Converter.DateConverter, validationRules.ValidateDateOfBirth);

            Console.Write("Gender (man - 'm' or woman - 'f'): ");
            var gender = UserInputHelpers.ReadInput(Converter.CharConverter, validationRules.ValidateGender);

            Console.Write("Height: ");
            var height = UserInputHelpers.ReadInput(Converter.ShortConverter, validationRules.ValidateHeight);

            Console.Write("Weight: ");
            var weight = UserInputHelpers.ReadInput(Converter.DecimalConverter, validationRules.ValidateWeight);

            FileCabinetRecordNewData fileCabinetRecordNewData = new FileCabinetRecordNewData(firstName, lastName, dateOfBirth, gender, height, weight);
            int recordId = Service.CreateRecord(fileCabinetRecordNewData);

            Console.WriteLine($"Record #{recordId} is created.");
        }
    }
}
