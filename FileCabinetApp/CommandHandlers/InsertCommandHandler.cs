using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            var parametrs = appCommand.Parameters.Replace("values", "-", this.stringComparison).Split('-');

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
                        var value = values[i].Trim();

                        switch (fields[i].Trim().ToLowerInvariant())
                        {
                            case "id":
                                record.Id = Converter.IntConverter(value).Item3;
                                break;
                            case "firstname":
                                record.FirstName = Convert(Converter.StringConverter, this.validationRules.ValidateFirstName, value);
                                break;
                            case "lastname":
                                record.LastName = Convert(Converter.StringConverter, this.validationRules.ValidateLastName, value);
                                break;
                            case "dateofbirth":
                                record.DateOfBirth = Convert(Converter.DateConverter, this.validationRules.ValidateDateOfBirth, value);
                                break;
                            case "gender":
                                record.Gender = Convert(Converter.CharConverter, this.validationRules.ValidateGender, value);
                                break;
                            case "height":
                                record.Height = Convert(Converter.ShortConverter, this.validationRules.ValidateHeight, value);
                                break;
                            case "weight":
                                record.Weight = Convert(Converter.DecimalConverter, this.validationRules.ValidateWeight, value);
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