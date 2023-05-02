using System;

namespace FileCabinetApp.CommandHandlers
{
    /// <summary>
    /// Contain code for handling update requests.
    /// </summary>
    public class UpdateCommandHandler : ServiceCommandHandlerBase
    {
        private readonly StringComparison stringComparison = StringComparison.InvariantCultureIgnoreCase;
        private readonly IUserInputValidation validationRules;
        private UnionType conditionalOperator = UnionType.Or;

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

            var parameters = appCommand.Parameters.Split(QueryConstants.Where, StringSplitOptions.RemoveEmptyEntries);

            var updateFields = parameters[0].Replace(QueryConstants.Set, string.Empty, this.stringComparison)
                                           .Replace("'", string.Empty, this.stringComparison)
                                           .Split(',', StringSplitOptions.RemoveEmptyEntries);

            if (parameters.Length > 1 && parameters[1].Contains(QueryConstants.And, StringComparison.InvariantCultureIgnoreCase))
            {
                this.conditionalOperator = UnionType.And;
            }

            try
            {
                Condition[] conditionsToSearch = Array.Empty<Condition>();

                if (parameters.Length > 1)
                {
                    var searchCriteria = parameters[1].ToLowerInvariant()
                                  .Replace("'", string.Empty, this.stringComparison)
                                  .Split(this.conditionalOperator.ToString().ToLowerInvariant(), StringSplitOptions.RemoveEmptyEntries);

                    conditionsToSearch = UserInputHelpers.CreateConditions(searchCriteria, this.validationRules);
                }

                Condition[] conditionsToUpdate = UserInputHelpers.CreateConditions(updateFields, this.validationRules);

                var recordsToUpdate = this.Service.Find(conditionsToSearch, this.conditionalOperator);

                foreach (var record in recordsToUpdate)
                {
                    var newData = GetNewDataFromFields(record, conditionsToUpdate);
                    this.Service.EditRecord(record.Id, newData);
                }

                Console.WriteLine($"Record(s) updated.");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Error. {ex.Message}.");
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

            foreach (var condition in conditionsToUpdate)
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