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
        private string conditionalOperator = "or";

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

            if (!appCommand.Parameters.Contains("where", this.stringComparison))
            {
                Console.WriteLine("Invalid command syntax. Missing 'where' clause.");
                return;
            }

            var parametrs = appCommand.Parameters.Split("where", StringSplitOptions.RemoveEmptyEntries);

            if (parametrs.Length != 2)
            {
                Console.WriteLine("You have entered an invalid update parameter. Two are needed.");
                return;
            }

            var updateFields = parametrs[0].Replace("set", string.Empty, this.stringComparison)
                                           .Replace("'", string.Empty, this.stringComparison)
                                           .Split(',', StringSplitOptions.RemoveEmptyEntries);

            if (parametrs[1].Contains("and", StringComparison.InvariantCultureIgnoreCase))
            {
                this.conditionalOperator = "and";
            }

            var searchCriteria = parametrs[1].ToLowerInvariant()
                                              .Replace("'", string.Empty, this.stringComparison)
                                              .Split(this.conditionalOperator, StringSplitOptions.RemoveEmptyEntries);

            try
            {
                Condition[] conditionsToSearch = UserInputHelpers.CreateConditions(searchCriteria, this.validationRules);
                Condition[] conditionsToUpdate = UserInputHelpers.CreateConditions(updateFields, this.validationRules);
                var @operator = new UnionType { OperatorType = this.conditionalOperator };

                var recordsToUpdate = this.Service.Find(conditionsToSearch, @operator);

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
                    case "firstname":
                        firstName = condition.Value.FirstName;
                        break;
                    case "lastname":
                        lastName = condition.Value.LastName;
                        break;
                    case "dateofbirth":
                        dateOfBirth = condition.Value.DateOfBirth;
                        break;
                    case "gender":
                        gender = condition.Value.Gender;
                        break;
                    case "height":
                        height = condition.Value.Height;
                        break;
                    case "weight":
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