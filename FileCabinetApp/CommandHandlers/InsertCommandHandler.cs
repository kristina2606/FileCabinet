﻿using System;

namespace FileCabinetApp.CommandHandlers
{
    public class InsertCommandHandler : ServiceCommandHandlerBase
    {
        private readonly StringComparison stringComparison = StringComparison.InvariantCultureIgnoreCase;
        private readonly IUserInputValidation validationRules;

        public InsertCommandHandler(IFileCabinetService service, IUserInputValidation inputValidation)
            : base(service)
        {
            this.validationRules = inputValidation;
        }

        public override void Handle(AppCommandRequest appCommand)
        {
            var parametrs = appCommand.Parameters.Split("values");

            if (appCommand.Command.Equals("insert", this.stringComparison))
            {
                if (parametrs.Length != 2)
                {
                    Console.WriteLine("You introduced an incorrect data.");
                    return;
                }

                string[] fields = parametrs[0].Replace("(", string.Empty, this.stringComparison).Replace(")", string.Empty, this.stringComparison).Split(',');
                string[] values = parametrs[1].Replace("(", string.Empty, this.stringComparison).Replace(")", string.Empty, this.stringComparison).Replace("'", string.Empty, this.stringComparison).Split(',');

                if (fields.Length != values.Length)
                {
                    Console.WriteLine("You introduced an incorrect data.");
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
                                record.FirstName = UserInputHelpers.Convert(Converter.StringConverter, this.validationRules.ValidateFirstName, valueForInsert);
                                break;
                            case "lastname":
                                record.LastName = UserInputHelpers.Convert(Converter.StringConverter, this.validationRules.ValidateLastName, valueForInsert);
                                break;
                            case "dateofbirth":
                                record.DateOfBirth = UserInputHelpers.Convert(Converter.DateConverter, this.validationRules.ValidateDateOfBirth, valueForInsert);
                                break;
                            case "gender":
                                record.Gender = UserInputHelpers.Convert(Converter.CharConverter, this.validationRules.ValidateGender, valueForInsert);
                                break;
                            case "height":
                                record.Height = UserInputHelpers.Convert(Converter.ShortConverter, this.validationRules.ValidateHeight, valueForInsert);
                                break;
                            case "weight":
                                record.Weight = UserInputHelpers.Convert(Converter.DecimalConverter, this.validationRules.ValidateWeight, valueForInsert);
                                break;
                        }
                    }

                    this.Service.Insert(record);
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            else
            {
                base.Handle(appCommand);
            }
        }
    }
}