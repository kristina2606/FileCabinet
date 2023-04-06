﻿using System;

namespace FileCabinetApp.CommandHandlers
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
            this.validationRules = inputValidation;
        }

        /// <summary>
        /// Handling for create requests.
        /// </summary>
        /// <param name="appCommand">>Configuratiion the application command and options.</param>
        public override void Handle(AppCommandRequest appCommand)
        {
            if (appCommand.Command.Equals("create", StringComparison.InvariantCultureIgnoreCase))
            {
                Console.Write("First name: ");
                var firstName = ReadInput(Converter.StringConverter, this.validationRules.ValidateFirstName);

                Console.Write("Last name: ");
                var lastName = ReadInput(Converter.StringConverter, this.validationRules.ValidateLastName);

                Console.Write("Date of birth: ");
                var dateOfBirth = ReadInput(Converter.DateConverter, this.validationRules.ValidateDateOfBirth);

                Console.Write("Gender (man - 'm' or woman - 'f'): ");
                var gender = ReadInput(Converter.CharConverter, this.validationRules.ValidateGender);

                Console.Write("Height: ");
                var height = ReadInput(Converter.ShortConverter, this.validationRules.ValidateHeight);

                Console.Write("Weight: ");
                var weight = ReadInput(Converter.DecimalConverter, this.validationRules.ValidateWeight);

                FileCabinetRecordNewData fileCabinetRecordNewData = new FileCabinetRecordNewData(firstName, lastName, dateOfBirth, gender, height, weight);
                int recordId = this.service.CreateRecord(fileCabinetRecordNewData);

                Console.WriteLine($"Record #{recordId} is created.");
            }
            else if (appCommand.Command != null)
            {
                base.Handle(appCommand);
            }
        }

        private static T ReadInput<T>(Func<string, Tuple<bool, string, T>> converter, Func<T, Tuple<bool, string>> validator)
        {
            do
            {
                T value;

                var input = Console.ReadLine();
                var conversionResult = converter(input);

                if (!conversionResult.Item1)
                {
                    Console.WriteLine($"Conversion failed: {conversionResult.Item2}. Please, correct your input.");
                    continue;
                }

                value = conversionResult.Item3;

                var validationResult = validator(value);
                if (!validationResult.Item1)
                {
                    Console.WriteLine($"Validation failed: {validationResult.Item2}. Please, correct your input.");
                    continue;
                }

                return value;
            }
            while (true);
        }
    }
}
