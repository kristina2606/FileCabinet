using System;
using FileCabinetApp.CommandHandlers.Helpers;
using FileCabinetApp.FileCabinetService;
using FileCabinetApp.Models;
using FileCabinetApp.UserInputValidator;

namespace FileCabinetApp.CommandHandlers.Commands
{
    /// <summary>
    /// Represents a command handler for handling update requests.
    /// </summary>
    public class UpdateCommandHandler : ServiceCommandHandlerBase
    {
        private readonly StringComparison stringComparison = StringComparison.OrdinalIgnoreCase;
        private readonly IUserInputValidation validationRules;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateCommandHandler"/> class.
        /// </summary>
        /// <param name="service">The file cabinet service.</param>
        /// <param name="inputValidationRules">The user input validation.</param>
        public UpdateCommandHandler(IFileCabinetService service, IUserInputValidation inputValidationRules)
            : base(service)
        {
            this.validationRules = inputValidationRules;
        }

        /// <summary>
        /// Handles 'update' requests.
        /// </summary>
        /// <param name="appCommand">The application command and options.</param>
        public override void Handle(AppCommandRequest appCommand)
        {
            if (!appCommand.Command.Equals("update", this.stringComparison))
            {
                base.Handle(appCommand);
                return;
            }

            var parameters = appCommand.Parameters.Split(QueryConstants.Where, StringSplitOptions.RemoveEmptyEntries);

            var updateFields = parameters[0].Replace(QueryConstants.Set, string.Empty, this.stringComparison)
                                            .Replace("'", string.Empty, this.stringComparison)
                                            .Split(',', StringSplitOptions.RemoveEmptyEntries);

            var conditionalOperator = UnionType.Or;
            if (parameters.Length > 1 && parameters[1].Contains(QueryConstants.And, this.stringComparison))
            {
                conditionalOperator = UnionType.And;
            }

            try
            {
                var searchConditions = Array.Empty<Condition>();
                if (parameters.Length > 1)
                {
                    var searchCriteria = parameters[1].ToLowerInvariant()
                                                      .Replace("'", string.Empty, this.stringComparison)
                                                      .Split(conditionalOperator.ToString().ToLowerInvariant(), StringSplitOptions.RemoveEmptyEntries);

                    searchConditions = UserInputHelpers.CreateConditions(searchCriteria, this.validationRules);
                }

                var conditionsToUpdate = UserInputHelpers.CreateConditions(updateFields, this.validationRules);
                var recordsToUpdate = this.Service.Find(searchConditions, conditionalOperator);

                foreach (FileCabinetRecord record in recordsToUpdate)
                {
                    var newData = GetNewDataFromFields(record, conditionsToUpdate);
                    this.Service.Update(record.Id, newData);
                }

                Console.WriteLine($"Record(s) updated.");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Error: {ex.Message}.");
            }
        }

        private static FileCabinetRecordNewData GetNewDataFromFields(FileCabinetRecord record, Condition[] conditionsToUpdate)
        {
            var firstName = record.FirstName;
            var lastName = record.LastName;
            var dateOfBirth = record.DateOfBirth;
            var gender = record.Gender;
            var height = record.Height;
            var weight = record.Weight;

            foreach (Condition condition in conditionsToUpdate)
            {
                switch (condition.Field)
                {
                    case FileCabinetRecordFields.FirstName:
                        firstName = condition.Value.FirstName;
                        break;
                    case FileCabinetRecordFields.LastName:
                        lastName = condition.Value.LastName;
                        break;
                    case FileCabinetRecordFields.DateOfBirth:
                        dateOfBirth = condition.Value.DateOfBirth;
                        break;
                    case FileCabinetRecordFields.Gender:
                        gender = condition.Value.Gender;
                        break;
                    case FileCabinetRecordFields.Height:
                        height = condition.Value.Height;
                        break;
                    case FileCabinetRecordFields.Weight:
                        weight = condition.Value.Weight;
                        break;
                    default:
                        throw new ArgumentException($"Unknown field to update: {condition.Field}");
                }
            }

            return new FileCabinetRecordNewData(firstName, lastName, dateOfBirth, gender, height, weight);
        }
    }
}