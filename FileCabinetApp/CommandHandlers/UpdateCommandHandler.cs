using System;
using System.Collections.Generic;
using System.Globalization;

namespace FileCabinetApp.CommandHandlers
{
    public class UpdateCommandHandler : ServiceCommandHandlerBase
    {
        private readonly StringComparison stringComparison = StringComparison.InvariantCultureIgnoreCase;
        private readonly IUserInputValidation validationRules;

        public UpdateCommandHandler(IFileCabinetService service, IUserInputValidation inputValidation)
            : base(service)
        {
            this.validationRules = inputValidation;
        }

        public override void Handle(AppCommandRequest appCommand)
        {
            if (appCommand.Command.Equals("update", this.stringComparison))
            {
                var parametrs = appCommand.Parameters.ToLowerInvariant().Split("where", StringSplitOptions.RemoveEmptyEntries);

                if (parametrs.Length != 2)
                {
                    Console.WriteLine("You have entered an invalid update parameter. Two are needed.");
                    return;
                }

                var updateFields = parametrs[0].Replace("set", string.Empty, this.stringComparison).Replace("'", string.Empty, this.stringComparison).Split(',', StringSplitOptions.RemoveEmptyEntries);
                var searchCriteria = parametrs[1].Replace("'", string.Empty, this.stringComparison).Split("and", StringSplitOptions.RemoveEmptyEntries);

                var allRecords = this.Service.GetRecords();

                Dictionary<string, string> fieldValuesToUpdate = new Dictionary<string, string>();

                foreach (string field in updateFields)
                {
                    string[] fieldParts = field.Trim().Split('=', StringSplitOptions.RemoveEmptyEntries);

                    if (fieldParts.Length != 2)
                    {
                        Console.WriteLine($"Invalid update field: {field}");
                        return;
                    }

                    fieldValuesToUpdate[fieldParts[0].Trim()] = fieldParts[1].Trim();
                }

                Dictionary<string, string> searchCriteriaValues = new Dictionary<string, string>();

                foreach (string criteria in searchCriteria)
                {
                    string[] criteriaParts = criteria.Trim().Split('=', StringSplitOptions.RemoveEmptyEntries);

                    if (criteriaParts.Length != 2)
                    {
                        Console.WriteLine($"Invalid search criteria: {criteria}");
                        return;
                    }

                    searchCriteriaValues[criteriaParts[0].Trim()] = criteriaParts[1].Trim();
                }

                try
                {
                    var recordsToUpdate = new List<FileCabinetRecord>();

                    foreach (var record in allRecords)
                    {
                        bool fitsAllParametrs = true;

                        foreach (var criteria in searchCriteriaValues)
                        {
                            switch (criteria.Key)
                            {
                                case "id":
                                    if (record.Id != int.Parse(criteria.Value, CultureInfo.InvariantCulture))
                                    {
                                        fitsAllParametrs = false;
                                    }

                                    break;
                                case "firstname":
                                    string firstName = UserInputHelpers.Convert(Converter.StringConverter, this.validationRules.ValidateFirstName, criteria.Value);

                                    if (!string.Equals(record.FirstName, firstName, StringComparison.InvariantCultureIgnoreCase))
                                    {
                                        fitsAllParametrs = false;
                                    }

                                    break;
                                case "lastname":
                                    string lastName = UserInputHelpers.Convert(Converter.StringConverter, this.validationRules.ValidateLastName, criteria.Value);

                                    if (!string.Equals(record.LastName, lastName, StringComparison.InvariantCultureIgnoreCase))
                                    {
                                        fitsAllParametrs = false;
                                    }

                                    break;
                                case "dateofbirth":
                                    DateTime dateOfBirth = UserInputHelpers.Convert(Converter.DateConverter, this.validationRules.ValidateDateOfBirth, criteria.Value);

                                    if (record.DateOfBirth != dateOfBirth)
                                    {
                                        fitsAllParametrs = false;
                                    }

                                    break;
                                default:
                                    throw new ArgumentException($"Unknown search criteria: {criteria.Key}");
                            }

                            if (!fitsAllParametrs)
                            {
                                break;
                            }
                        }

                        if (fitsAllParametrs)
                        {
                            recordsToUpdate.Add(record);
                        }
                    }

                    foreach (var record in recordsToUpdate)
                    {
                        var firstName = record.FirstName;
                        var lastName = record.LastName;
                        var dateOfBirth = record.DateOfBirth;
                        var gender = record.Gender;
                        var height = record.Height;
                        var weight = record.Weight;

                        foreach (var field in fieldValuesToUpdate)
                        {
                            switch (field.Key)
                            {
                                case "firstname":
                                    firstName = UserInputHelpers.Convert(Converter.StringConverter, this.validationRules.ValidateFirstName, field.Value);
                                    break;
                                case "lastname":
                                    lastName = UserInputHelpers.Convert(Converter.StringConverter, this.validationRules.ValidateLastName, field.Value);
                                    break;
                                case "dateofbirth":
                                    dateOfBirth = UserInputHelpers.Convert(Converter.DateConverter, this.validationRules.ValidateDateOfBirth, field.Value);
                                    break;
                                case "gender":
                                    gender = UserInputHelpers.Convert(Converter.CharConverter, this.validationRules.ValidateGender, field.Value);
                                    break;
                                case "height":
                                    height = UserInputHelpers.Convert(Converter.ShortConverter, this.validationRules.ValidateHeight, field.Value);
                                    break;
                                case "weight":
                                    weight = UserInputHelpers.Convert(Converter.DecimalConverter, this.validationRules.ValidateWeight, field.Value);
                                    break;
                                default:
                                    throw new ArgumentException($"Unknown field to update: {field.Key}");
                            }
                        }

                        var newData = new FileCabinetRecordNewData(firstName, lastName, dateOfBirth, gender, height, weight);
                        this.Service.EditRecord(record.Id, newData);
                    }

                    Console.WriteLine($"Record(s) updated.");
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"Error. {ex.Message}.");
                }
            }
            else
            {
                base.Handle(appCommand);
            }
        }
    }
}