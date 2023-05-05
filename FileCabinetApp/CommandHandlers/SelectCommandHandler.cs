using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace FileCabinetApp.CommandHandlers
{
    /// <summary>
    /// Contain code for handling select requests.
    /// </summary>
    public class SelectCommandHandler : ServiceCommandHandlerBase
    {
        private readonly StringComparison stringComparison = StringComparison.InvariantCultureIgnoreCase;
        private readonly IUserInputValidation validationRules;

        private readonly List<string> leftAlignedColumns = new List<string>();
        private readonly List<string> rightAlignedColumns = new List<string>();

        /// <summary>
        /// Initializes a new instance of the <see cref="SelectCommandHandler"/> class.
        /// </summary>
        /// <param name="service">Interface instance IFileCabinetServise.</param>
        /// <param name="inputValidation">Interface instance IUserInputValidation.</param>
        public SelectCommandHandler(IFileCabinetService service, IUserInputValidation inputValidation)
            : base(service)
        {
            this.validationRules = inputValidation;
        }

        /// <summary>
        /// Handling for select requests.
        /// </summary>
        /// <param name="appCommand">>Configuratiion the application command and options.</param>
        public override void Handle(AppCommandRequest appCommand)
        {
            if (!appCommand.Command.Equals("select", this.stringComparison))
            {
                base.Handle(appCommand);
                return;
            }

            var parameters = appCommand.Parameters.Split(QueryConstants.Where, StringSplitOptions.RemoveEmptyEntries);

            var conditionalOperator = UnionType.Or;
            if (parameters.Length > 1 && parameters[1].Contains(QueryConstants.And, StringComparison.InvariantCultureIgnoreCase))
            {
                conditionalOperator = UnionType.And;
            }

            try
            {
                string[] printFields = Array.Empty<string>();
                if (parameters.Length > 0)
                {
                    printFields = parameters[0].ToLowerInvariant().Split(',', StringSplitOptions.RemoveEmptyEntries);
                }

                Condition[] conditionsToSearch = Array.Empty<Condition>();
                if (parameters.Length > 1)
                {
                    var searchCriteria = parameters[1].ToLowerInvariant()
                                  .Replace(" ", string.Empty, this.stringComparison)
                                  .Replace("'", string.Empty, this.stringComparison)
                                  .Split(conditionalOperator.ToString().ToLowerInvariant(), StringSplitOptions.RemoveEmptyEntries);

                    conditionsToSearch = UserInputHelpers.CreateConditions(searchCriteria, this.validationRules);
                }

                var fieldsToPrint = GetFieldsToPrint(printFields);
                var recordsToPrint = this.Service.Find(conditionsToSearch, conditionalOperator);

                this.PrintTable(recordsToPrint, fieldsToPrint);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Error. {ex.Message}.");
            }
        }

        private void PrintTable(IEnumerable<FileCabinetRecord> records, List<FileCabinetRecordFields> fields)
        {
            int[] columnWidth = new int[fields.Count];
            for (var i = 0; i < fields.Count; i++)
            {
                var maxRecordColumnWidth = records.Select(x => this.GetFieldValueString(x, fields[i]).Length).Max();
                columnWidth[i] = Math.Max(fields[i].ToString().Length, maxRecordColumnWidth);
            }

            Console.WriteLine(GetTableHeader(fields, columnWidth));

            var fieldsToPrint = records.Select(record => fields.Select(field => this.GetFieldValueString(record, field)).ToArray());

            foreach (var field in fieldsToPrint)
            {
                Console.WriteLine(this.GetRow(field, columnWidth));
            }

            Console.WriteLine(GetDemarcationLine(columnWidth));
        }

        private string GetFieldValueString(FileCabinetRecord record, FileCabinetRecordFields field)
        {
            this.AddAlignmentType(record, field);

            return field switch
            {
                FileCabinetRecordFields.Id => record.Id.ToString(CultureInfo.InvariantCulture),
                FileCabinetRecordFields.FirstName => record.FirstName,
                FileCabinetRecordFields.LastName => record.LastName,
                FileCabinetRecordFields.DateOfBirth => record.DateOfBirth.ToString("yyyy-MMM-dd", CultureInfo.InvariantCulture),
                FileCabinetRecordFields.Gender => record.Gender.ToString(CultureInfo.InvariantCulture),
                FileCabinetRecordFields.Height => record.Height.ToString(CultureInfo.InvariantCulture),
                FileCabinetRecordFields.Weight => record.Weight.ToString(CultureInfo.InvariantCulture),
                _ => throw new ArgumentException($"Unknown field: {field}"),
            };
        }

        private void AddAlignmentType(FileCabinetRecord record, FileCabinetRecordFields field)
        {
            switch (field)
            {
                case FileCabinetRecordFields.Id:
                    this.rightAlignedColumns.Add(record.Id.ToString(CultureInfo.InvariantCulture));
                    break;
                case FileCabinetRecordFields.FirstName:
                    this.leftAlignedColumns.Add(record.FirstName);
                    break;
                case FileCabinetRecordFields.LastName:
                    this.leftAlignedColumns.Add(record.LastName);
                    break;
                case FileCabinetRecordFields.DateOfBirth:
                    this.leftAlignedColumns.Add(record.DateOfBirth.ToString("yyyy-MMM-dd", CultureInfo.InvariantCulture));
                    break;
                case FileCabinetRecordFields.Gender:
                    this.leftAlignedColumns.Add(record.Gender.ToString(CultureInfo.InvariantCulture));
                    break;
                case FileCabinetRecordFields.Height:
                    this.rightAlignedColumns.Add(record.Height.ToString(CultureInfo.InvariantCulture));
                    break;
                case FileCabinetRecordFields.Weight:
                    this.rightAlignedColumns.Add(record.Weight.ToString(CultureInfo.InvariantCulture));
                    break;
                default:
                    throw new ArgumentException($"Unknown field: {field}");
            }
        }

        private static List<FileCabinetRecordFields> GetFieldsToPrint(string[] fields)
        {
            if (fields == null || fields.Length == 0)
            {
                return new List<FileCabinetRecordFields>((FileCabinetRecordFields[])Enum.GetValues(typeof(FileCabinetRecordFields)));
            }

            var fieldsToPrinr = new List<FileCabinetRecordFields>();

            foreach (var field in fields)
            {
                if (!Enum.TryParse<FileCabinetRecordFields>(field, true, out var enumField))
                {
                    throw new ArgumentException("You entered wrong parameters for print.");
                }

                fieldsToPrinr.Add(enumField);
            }

            return fieldsToPrinr;
        }

        private static string GetTableHeader(List<FileCabinetRecordFields> fields, int[] columnWidht)
        {
            var header = new StringBuilder();
            header.Append(GetDemarcationLine(columnWidht));
            header.Append(TableSymbols.NewLine);

            for (var i = 0; i < fields.Count; i++)
            {
                header.Append(CultureInfo.InvariantCulture, $"{TableSymbols.VerticalLine} {fields[i].ToString().PadRight(columnWidht[i])} ");
            }

            header.Append(TableSymbols.VerticalLine);
            header.Append(TableSymbols.NewLine);

            header.Append(GetDemarcationLine(columnWidht));

            return header.ToString();
        }

        private static string GetDemarcationLine(int[] columnWidht)
        {
            var line = new StringBuilder();

            foreach (var widht in columnWidht)
            {
                line.Append(CultureInfo.InvariantCulture, $"{TableSymbols.Intersection}{new string(TableSymbols.HorizontalLine, widht + 2)}");
            }

            line.Append(TableSymbols.Intersection);

            return line.ToString();
        }

        private string GetRow(string[] fieldsToPrint, int[] columnWidth)
        {
            var rowBuilder = new StringBuilder();

            for (var i = 0; i < fieldsToPrint.Length; i++)
            {
                var field = fieldsToPrint[i];
                var width = columnWidth[i];

                if (this.rightAlignedColumns.Contains(field))
                {
                    rowBuilder.Append(CultureInfo.InvariantCulture, $"{TableSymbols.VerticalLine} {field.PadRight(width)} ");
                }

                if (this.leftAlignedColumns.Contains(field))
                {
                    rowBuilder.Append(CultureInfo.InvariantCulture, $"{TableSymbols.VerticalLine} {field.PadLeft(width)} ");
                }
            }

            rowBuilder.Append(TableSymbols.VerticalLine);

            return rowBuilder.ToString();
        }

        private static class TableSymbols
        {
            public const char HorizontalLine = '-';
            public const char VerticalLine = '|';
            public const char Intersection = '+';
            public const char NewLine = '\n';
        }
    }
}
