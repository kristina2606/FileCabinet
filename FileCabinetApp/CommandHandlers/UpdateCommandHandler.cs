using System;
using System.Collections.Generic;
using System.Globalization;

namespace FileCabinetApp.CommandHandlers
{
    /// <summary>
    /// Contain code for handling update requests.
    /// </summary>
    public class UpdateCommandHandler : ServiceCommandHandlerBase
    {
        private readonly StringComparison stringComparison = StringComparison.InvariantCultureIgnoreCase;
        private readonly IUserInputValidation validationRules;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateCommandHandler"/> class.
        /// </summary>
        /// <param name="service">Interface instance IFileCabinetServise.</param>
        /// <param name="inputValidation">Interface instance IUserInputValidation.</param>
        public UpdateCommandHandler(IFileCabinetService service, IUserInputValidation inputValidation)
            : base(service)
        {
            this.validationRules = inputValidation;
        }

        /// <summary>
        /// Handling for update requests.
        /// </summary>
        /// <param name="appCommand">>Configuratiion the application command and options.</param>
        public override void Handle(AppCommandRequest appCommand)
        {
            if (!appCommand.Command.Equals("update", this.stringComparison))
            {
                base.Handle(appCommand);
                return;
            }

            var parametrs = appCommand.Parameters.ToLowerInvariant().Split("where", StringSplitOptions.RemoveEmptyEntries);

            if (parametrs.Length != 2)
            {
                Console.WriteLine("You have entered an invalid update parameter. Two are needed.");
                return;
            }

            var updateFields = parametrs[0].Replace("set", string.Empty, this.stringComparison).Replace("'", string.Empty, this.stringComparison).Split(',', StringSplitOptions.RemoveEmptyEntries);
            var searchCriteria = parametrs[1].Replace("'", string.Empty, this.stringComparison).Split("and", StringSplitOptions.RemoveEmptyEntries);

            var allRecords = this.Service.GetRecords();
            var fieldValuesToUpdate = GetDictionaryFromFields(updateFields);
            var searchCriteriaValues = GetDictionaryFromFields(searchCriteria);

            try
            {
                var recordsToUpdate = this.GetRecordsToUpdate(allRecords, searchCriteriaValues);

                foreach (var record in recordsToUpdate)
                {
                    var newData = this.GetNewDataFromFields(record, fieldValuesToUpdate);
                    this.Service.EditRecord(record.Id, newData);
                }

                Console.WriteLine($"Record(s) updated.");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Error. {ex.Message}.");
            }
        }

        private static Dictionary<string, string> GetDictionaryFromFields(string[] fields)
        {
            var dictionary = new Dictionary<string, string>();

            foreach (string field in fields)
            {
                string[] fieldParts = field.Trim().Split('=', StringSplitOptions.RemoveEmptyEntries);

                if (fieldParts.Length != 2)
                {
                    throw new ArgumentException($"Invalid search criteria: {field}");
                }

                dictionary[fieldParts[0].Trim()] = fieldParts[1].Trim();
            }

            return dictionary;
        }

        private FileCabinetRecordNewData GetNewDataFromFields(FileCabinetRecord record, Dictionary<string, string> fieldValuesToUpdate)
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

            return new FileCabinetRecordNewData(firstName, lastName, dateOfBirth, gender, height, weight);
        }

        private bool RecordFitsCriteria(FileCabinetRecord record, Dictionary<string, string> searchCriteriaValues)
        {
            foreach (var criteria in searchCriteriaValues)
            {
                switch (criteria.Key)
                {
                    case "id":
                        if (record.Id != int.Parse(criteria.Value, CultureInfo.InvariantCulture))
                        {
                           return false;
                        }

                        break;
                    case "firstname":
                        string firstName = UserInputHelpers.Convert(Converter.StringConverter, this.validationRules.ValidateFirstName, criteria.Value);

                        if (!string.Equals(record.FirstName, firstName, StringComparison.InvariantCultureIgnoreCase))
                        {
                            return false;
                        }

                        break;
                    case "lastname":
                        string lastName = UserInputHelpers.Convert(Converter.StringConverter, this.validationRules.ValidateLastName, criteria.Value);

                        if (!string.Equals(record.LastName, lastName, StringComparison.InvariantCultureIgnoreCase))
                        {
                            return false;
                        }

                        break;
                    case "dateofbirth":
                        DateTime dateOfBirth = UserInputHelpers.Convert(Converter.DateConverter, this.validationRules.ValidateDateOfBirth, criteria.Value);

                        if (record.DateOfBirth != dateOfBirth)
                        {
                            return false;
                        }

                        break;
                    default:
                        throw new ArgumentException($"Unknown search criteria: {criteria.Key}");
                }
            }

            return true;
        }

        private List<FileCabinetRecord> GetRecordsToUpdate(IEnumerable<FileCabinetRecord> allRecords, Dictionary<string, string> searchCriteriaValues)
        {
            var recordsToUpdate = new List<FileCabinetRecord>();

            foreach (var record in allRecords)
            {
                if (this.RecordFitsCriteria(record, searchCriteriaValues))
                {
                    recordsToUpdate.Add(record);
                }
            }

            return recordsToUpdate;
        }
    }
}