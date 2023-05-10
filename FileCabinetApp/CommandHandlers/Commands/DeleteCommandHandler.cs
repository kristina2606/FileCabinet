using System;
using System.Collections.Generic;
using System.Linq;
using FileCabinetApp.CommandHandlers.Helpers;
using FileCabinetApp.FileCabinetService;
using FileCabinetApp.Models;
using FileCabinetApp.UserInputValidator;

namespace FileCabinetApp.CommandHandlers.Commands
{
    /// <summary>
    /// Represents a command handler for handling delete requests.
    /// </summary>
    public class DeleteCommandHandler : ServiceCommandHandlerBase
    {
        private readonly StringComparison stringComparison = StringComparison.OrdinalIgnoreCase;
        private readonly IUserInputValidation validationRules;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteCommandHandler"/> class.
        /// </summary>
        /// <param name="service">The file cabinet service.</param>
        /// <param name="inputValidation">The user input validation.</param>
        public DeleteCommandHandler(IFileCabinetService service, IUserInputValidation inputValidation)
                  : base(service)
        {
            this.validationRules = inputValidation;
        }

        /// <summary>
        /// Handles 'delete' requests.
        /// </summary>
        /// <param name="appCommand">The application command and options.</param>
        public override void Handle(AppCommandRequest appCommand)
        {
            if (!appCommand.Command.Equals("delete", this.stringComparison))
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

            if (appCommand.Parameters.Contains(QueryConstants.And, StringComparison.InvariantCultureIgnoreCase))
            {
                conditionalOperator = UnionType.And;
            }

            var parametrs = appCommand.Parameters.ToLowerInvariant()
                                                 .Replace(QueryConstants.Where, string.Empty, this.stringComparison)
                                                 .Replace("'", string.Empty, this.stringComparison)
                                                 .Split(conditionalOperator.ToString().ToLowerInvariant(), StringSplitOptions.RemoveEmptyEntries);

            try
            {
                var deletedRecords = new List<int>();
                Condition[] conditionsToSearch = UserInputHelpers.CreateConditions(parametrs, this.validationRules);
                var recordsForDelete = this.Service.Find(conditionsToSearch, conditionalOperator);

                foreach (var recordId in recordsForDelete.Select(x => x.Id).ToList())
                {
                    this.Service.Delete(recordId);

                    deletedRecords.Add(recordId);
                }

                if (deletedRecords.Count == 0)
                {
                    Console.WriteLine($"'{string.Join(",", conditionsToSearch.Select(x => x.Value))}' value for deletion not found.");
                    return;
                }

                Console.WriteLine($"Records #{string.Join(", #", deletedRecords)} have been deleted.");
            }
            catch
            {
                Console.WriteLine($"Record with parametr {parametrs[0]}  does not exist.");
            }
        }
    }
}
