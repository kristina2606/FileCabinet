using System;
using System.Collections.Generic;
using System.Linq;
using FileCabinetApp.Enums;
using FileCabinetApp.FileCabinetService;
using FileCabinetApp.Models;
using FileCabinetApp.UserInputValidator;
using FileCabinetApp.Сonstants;

namespace FileCabinetApp.CommandHandlers.Commands
{
    /// <summary>
    /// Represents a command handler for handling delete requests.
    /// </summary>
    public class DeleteCommandHandler : ServiceCommandHandlerBase
    {
        private const string CommandName = "delete";

        private readonly StringComparison stringComparison = StringComparison.OrdinalIgnoreCase;
        private readonly IUserInputValidation validationRules;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteCommandHandler"/> class.
        /// </summary>
        /// <param name="service">The file cabinet service.</param>
        /// <param name="inputValidationRules">The user input validation.</param>
        public DeleteCommandHandler(IFileCabinetService service, IUserInputValidation inputValidationRules)
                  : base(service)
        {
            this.validationRules = inputValidationRules;
        }

        /// <summary>
        /// Handles 'delete' requests.
        /// </summary>
        /// <param name="appCommand">The application command and options.</param>
        public override void Handle(AppCommandRequest appCommand)
        {
            if (!appCommand.Command.Equals(CommandName, this.stringComparison))
            {
                base.Handle(appCommand);
                return;
            }

            if (!appCommand.Parameters.Contains(QueryConstants.Where, this.stringComparison))
            {
                Console.WriteLine("Invalid command syntax. Missing 'where' clause.");
                return;
            }

            var conditionalOperator = UnionType.Or;
            if (appCommand.Parameters.Contains(QueryConstants.And, this.stringComparison))
            {
                conditionalOperator = UnionType.And;
            }

            var parameters = appCommand.Parameters.ToLowerInvariant()
                                                  .Replace(QueryConstants.Where, string.Empty, this.stringComparison)
                                                  .Replace("'", string.Empty, this.stringComparison)
                                                  .Split(conditionalOperator.ToString().ToLowerInvariant(), StringSplitOptions.RemoveEmptyEntries);

            try
            {
                var deletedRecords = new List<int>();
                var searchConditions = Condition.Create(parameters, this.validationRules);
                var recordsForDelete = this.Service.Find(searchConditions, conditionalOperator);

                foreach (int id in recordsForDelete.Select(record => record.Id).ToList())
                {
                    this.Service.Delete(id);

                    deletedRecords.Add(id);
                }

                if (deletedRecords.Count == 0)
                {
                    Console.WriteLine($"'{string.Join(",", searchConditions.Select(condition => condition.Value))}' value for deletion not found.");
                    return;
                }

                Console.WriteLine($"Records #{string.Join(", #", deletedRecords)} have been deleted.");
            }
            catch
            {
                Console.WriteLine($"Record with parametr {parameters[0]}  does not exist.");
            }
        }
    }
}
