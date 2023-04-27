﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace FileCabinetApp.CommandHandlers
{
    /// <summary>
    /// Contain code for handling delete requests.
    /// </summary>
    public class DeleteCommandHandler : ServiceCommandHandlerBase
    {
        private readonly StringComparison stringComparison = StringComparison.InvariantCultureIgnoreCase;
        private readonly IUserInputValidation validationRules;

        private string conditionalOperator = "or";

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteCommandHandler"/> class.
        /// </summary>
        /// <param name="service">Interface instance IFileCabinetServise.</param>
        /// <param name="inputValidation">Interface instance IUserInputValidation.</param>
        public DeleteCommandHandler(IFileCabinetService service, IUserInputValidation inputValidation)
            : base(service)
        {
            this.validationRules = inputValidation;
        }

        /// <summary>
        /// Handling for delete requests.
        /// </summary>
        /// <param name="appCommand">>Configuratiion the application command and options.</param>
        public override void Handle(AppCommandRequest appCommand)
        {
            if (!appCommand.Command.Equals("delete", this.stringComparison))
            {
                base.Handle(appCommand);
                return;
            }

            if (!appCommand.Parameters.Contains("where", this.stringComparison))
            {
                Console.WriteLine("Invalid command syntax. Missing 'where' clause.");
                return;
            }

            if (appCommand.Parameters.Contains("and", StringComparison.InvariantCultureIgnoreCase))
            {
                this.conditionalOperator = "and";
            }

            var parametrs = appCommand.Parameters.ToLowerInvariant()
                                                 .Replace("where ", string.Empty, this.stringComparison)
                                                 .Replace("'", string.Empty, this.stringComparison)
                                                 .Split(this.conditionalOperator, StringSplitOptions.RemoveEmptyEntries);

            try
            {
                var deletedRecords = new List<int>();
                Condition[] conditionsToSearch = UserInputHelpers.CreateConditions(parametrs, this.validationRules);
                var recordsForDelete = this.Service.Find(conditionsToSearch, new UnionType { OperatorType = this.conditionalOperator });

                foreach (var recordId in recordsForDelete.Select(x => x.Id).ToList())
                {
                    this.Service.Remove(recordId);

                    deletedRecords.Add(recordId);
                }

                if (deletedRecords.Count == 0)
                {
                    Console.WriteLine($"'{string.Join(",", conditionsToSearch.Select(x => x.Value))}' value for delete not found.");
                    return;
                }

                Console.WriteLine($"Records #{string.Join(", #", deletedRecords)} are deleted.");
            }
            catch
            {
                Console.WriteLine($"Record with {parametrs[0]} parametr doesn't exists.");
            }
        }
    }
}
