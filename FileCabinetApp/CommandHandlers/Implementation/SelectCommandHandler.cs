using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using FileCabinetApp.Constants;
using FileCabinetApp.Enums;
using FileCabinetApp.FileCabinetService;
using FileCabinetApp.Models;
using FileCabinetApp.UserInputValidator;
using FileCabinetApp.Сonstants;

namespace FileCabinetApp.CommandHandlers.Commands
{
    /// <summary>
    /// Represents a command handler for handling select requests.
    /// </summary>
    public class SelectCommandHandler : ServiceCommandHandlerBase
    {
        private const string CommandName = "select";
        private const char HorizontalLine = '-';
        private const char VerticalLine = '|';
        private const char Intersection = '+';
        private const char NewLine = '\n';

        private readonly StringComparison stringComparison = StringComparison.OrdinalIgnoreCase;
        private readonly IUserInputValidation validationRules;

        private readonly FileCabinetRecordFields[] leftAlignedColumns = new[] { FileCabinetRecordFields.FirstName, FileCabinetRecordFields.LastName, FileCabinetRecordFields.DateOfBirth, FileCabinetRecordFields.Gender };
        private readonly FileCabinetRecordFields[] rightAlignedColumns = new[] { FileCabinetRecordFields.Id, FileCabinetRecordFields.Height, FileCabinetRecordFields.Weight };

        /// <summary>
        /// Initializes a new instance of the <see cref="SelectCommandHandler"/> class.
        /// </summary>
        /// <param name="service">The file cabinet service.</param>
        /// <param name="inputValidationRules">The user input validation.</param>
        public SelectCommandHandler(IFileCabinetService service, IUserInputValidation inputValidationRules)
            : base(service)
        {
            this.validationRules = inputValidationRules;
        }

        /// <summary>
        /// Handles 'select' requests.
        /// </summary>
        /// <param name="appCommand">The application command and options.</param>
        public override void Handle(AppCommandRequest appCommand)
        {
            if (!appCommand.Command.Equals(CommandName, this.stringComparison))
            {
                base.Handle(appCommand);
                return;
            }

            var parameters = appCommand.Parameters.Split(QueryConstants.Where, StringSplitOptions.RemoveEmptyEntries);

            var conditionalOperator = UnionType.Or;
            if (parameters.Length > 1 && parameters[1].Contains(QueryConstants.And, this.stringComparison))
            {
                conditionalOperator = UnionType.And;
            }

            try
            {
                var printFields = Array.Empty<string>();
                if (parameters.Length > 0)
                {
                    printFields = parameters[0].ToLowerInvariant().Split(',', StringSplitOptions.RemoveEmptyEntries);
                }

                var searchConditions = Array.Empty<Condition>();
                if (parameters.Length > 1)
                {
                    var searchCriteria = parameters[1].ToLowerInvariant()
                                                      .Replace(" ", string.Empty, this.stringComparison)
                                                      .Replace("'", string.Empty, this.stringComparison)
                                                      .Split(conditionalOperator.ToString().ToLowerInvariant(), StringSplitOptions.RemoveEmptyEntries);

                    searchConditions = Condition.Create(searchCriteria, this.validationRules);
                }

                var fieldsToPrint = GetFieldsToPrint(printFields);
                var recordsToPrint = this.Service.Find(searchConditions, conditionalOperator);

                this.PrintTable(recordsToPrint, fieldsToPrint);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Error: {ex.Message}.");
            }
        }

        private static string GetFieldValueString(FileCabinetRecord record, FileCabinetRecordFields field)
        {
            return field switch
            {
                FileCabinetRecordFields.Id => record.Id.ToString(CultureInfo.InvariantCulture),
                FileCabinetRecordFields.FirstName => record.FirstName,
                FileCabinetRecordFields.LastName => record.LastName,
                FileCabinetRecordFields.DateOfBirth => record.DateOfBirth.ToString(DateTimeConstants.FullDateFormat, CultureInfo.InvariantCulture),
                FileCabinetRecordFields.Gender => record.Gender.ToString(CultureInfo.InvariantCulture),
                FileCabinetRecordFields.Height => record.Height.ToString(CultureInfo.InvariantCulture),
                FileCabinetRecordFields.Weight => record.Weight.ToString(CultureInfo.InvariantCulture),
                _ => throw new ArgumentException($"Unknown field: {field}"),
            };
        }

        private static List<FileCabinetRecordFields> GetFieldsToPrint(string[] fields)
        {
            if (fields == null || fields.Length == 0)
            {
                return new List<FileCabinetRecordFields>((FileCabinetRecordFields[])Enum.GetValues(typeof(FileCabinetRecordFields)));
            }

            var fieldsToPrint = new List<FileCabinetRecordFields>();

            foreach (string field in fields)
            {
                if (!Enum.TryParse<FileCabinetRecordFields>(field, true, out var enumField))
                {
                    throw new ArgumentException("Invalid parameters for printing.");
                }

                fieldsToPrint.Add(enumField);
            }

            return fieldsToPrint;
        }

        private static string GetTableHeader(List<FileCabinetRecordFields> fields, int[] columnWidth)
        {
            var header = new StringBuilder();
            header.Append(GetDemarcationLine(columnWidth));
            header.Append(NewLine);

            for (var i = 0; i < fields.Count; i++)
            {
                header.Append(CultureInfo.InvariantCulture, $"{VerticalLine} {fields[i].ToString().PadRight(columnWidth[i])} ");
            }

            header.Append(VerticalLine);
            header.Append(NewLine);
            header.Append(GetDemarcationLine(columnWidth));

            return header.ToString();
        }

        private static string GetDemarcationLine(int[] columnWidth)
        {
            var line = new StringBuilder();

            foreach (int width in columnWidth)
            {
                line.Append(CultureInfo.InvariantCulture, $"{Intersection}{new string(HorizontalLine, width + 2)}");
            }

            line.Append(Intersection);

            return line.ToString();
        }

        private void PrintTable(IEnumerable<FileCabinetRecord> records, List<FileCabinetRecordFields> fields)
        {
            var columnWidth = new int[fields.Count];

            for (var i = 0; i < fields.Count; i++)
            {
                var maxRecordColumnWidth = records.Select(x => GetFieldValueString(x, fields[i]).Length).Max();
                columnWidth[i] = Math.Max(fields[i].ToString().Length, maxRecordColumnWidth);
            }

            Console.WriteLine(GetTableHeader(fields, columnWidth));

            var table = records.Select(record => fields.Select(field => GetFieldValueString(record, field)).ToArray());

            foreach (string[] rowValues in table)
            {
                Console.WriteLine(this.GetRow(rowValues, fields, columnWidth));
            }

            Console.WriteLine(GetDemarcationLine(columnWidth));
        }

        private string GetRow(string[] values, List<FileCabinetRecordFields> fields, int[] columnWidth)
        {
            var rowBuilder = new StringBuilder();

            for (var i = 0; i < values.Length; i++)
            {
                var field = fields[i];
                var value = values[i];
                var width = columnWidth[i];

                if (this.rightAlignedColumns.Contains(field))
                {
                    rowBuilder.Append(CultureInfo.InvariantCulture, $"{VerticalLine} {value.PadRight(width)} ");
                }

                if (this.leftAlignedColumns.Contains(field))
                {
                    rowBuilder.Append(CultureInfo.InvariantCulture, $"{VerticalLine} {value.PadLeft(width)} ");
                }
            }

            rowBuilder.Append(VerticalLine);

            return rowBuilder.ToString();
        }
    }
}
