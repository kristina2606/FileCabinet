﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace FileCabinetApp.CommandHandlers
{
    public class SelectCommandHandler : ServiceCommandHandlerBase
    {
        private readonly StringComparison stringComparison = StringComparison.InvariantCultureIgnoreCase;
        private readonly IUserInputValidation validationRules;
        private UnionType conditionalOperator = UnionType.Default;

        public SelectCommandHandler(IFileCabinetService service, IUserInputValidation validationRules)
            : base(service)
        {
            this.validationRules = validationRules;
        }

        public override void Handle(AppCommandRequest appCommand)
        {
            if (!appCommand.Command.Equals("select", this.stringComparison))
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
                Console.WriteLine("You have entered an invalid select parameter.");
                return;
            }

            if (parametrs[1].Contains("and", StringComparison.InvariantCultureIgnoreCase))
            {
                this.conditionalOperator = UnionType.And;
            }

            if (parametrs[1].Contains("or", StringComparison.InvariantCultureIgnoreCase))
            {
                this.conditionalOperator = UnionType.Or;
            }

            var printFields = parametrs[0].ToLowerInvariant().Split(',', StringSplitOptions.RemoveEmptyEntries);

            var searchCriteria = parametrs[1].ToLowerInvariant()
                                  .Replace(" ", string.Empty, this.stringComparison)
                                  .Replace("'", string.Empty, this.stringComparison)
                                  .Split(this.conditionalOperator.ToString().ToLowerInvariant(), StringSplitOptions.RemoveEmptyEntries);

            try
            {
                Condition[] conditionsToSearch = UserInputHelpers.CreateConditions(searchCriteria, this.validationRules);
                var fieldsToPrint = GetFieldsToPrint(printFields);
                var recordsToPrint = this.Service.Find(conditionsToSearch, this.conditionalOperator);

                PrintTable(recordsToPrint, fieldsToPrint);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Error. {ex.Message}.");
            }
        }

        private static void PrintTable(IEnumerable<FileCabinetRecord> records, List<FileCabinetRecordFields> fields)
        {
            int[] columnWidth = new int[fields.Count];
            for (var i = 0; i < fields.Count; i++)
            {
                var maxRecordColumnWidth = records.Select(x => GetFieldValueInt(x, fields[i])).Max();
                columnWidth[i] = Math.Max(fields[i].ToString().Length, maxRecordColumnWidth);

                if (maxRecordColumnWidth < 0)
                {
                    columnWidth[i] *= -1;
                }
            }

            Console.WriteLine(GetTableHeader(fields, columnWidth));

            var fieldsToPrint = records.Select(record => fields.Select(field => GetFieldValueString(record, field)));

            foreach (var field in fieldsToPrint)
            {
                Console.WriteLine(GetRow(field, columnWidth));
            }

            Console.WriteLine(GetDemarcationLine(columnWidth));
        }

        private static string GetFieldValueString(FileCabinetRecord record, FileCabinetRecordFields field)
        {
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

        private static int GetFieldValueInt(FileCabinetRecord record, FileCabinetRecordFields field)
        {
            return field switch
            {
                FileCabinetRecordFields.Id => record.Id.ToString(CultureInfo.InvariantCulture).Length,
                FileCabinetRecordFields.FirstName => -record.FirstName.Length,
                FileCabinetRecordFields.LastName => -record.LastName.Length,
                FileCabinetRecordFields.DateOfBirth => -record.DateOfBirth.ToString("yyyy-MMM-dd", CultureInfo.InvariantCulture).Length,
                FileCabinetRecordFields.Gender => -record.Gender.ToString(CultureInfo.InvariantCulture).Length,
                FileCabinetRecordFields.Height => record.Height.ToString(CultureInfo.InvariantCulture).Length,
                FileCabinetRecordFields.Weight => record.Weight.ToString(CultureInfo.InvariantCulture).Length,
                _ => throw new ArgumentException($"Unknown field: {field}"),
            };
        }

        private static List<FileCabinetRecordFields> GetFieldsToPrint(string[] fields)
        {
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
            header.Append('\n');

            for (var i = 0; i < fields.Count; i++)
            {
                header.Append(CultureInfo.InvariantCulture, $"| {fields[i].ToString().PadRight(Math.Abs(columnWidht[i]))} ");
            }

            header.Append("|\n");

            header.Append(GetDemarcationLine(columnWidht));

            return header.ToString();
        }

        private static string GetDemarcationLine(int[] columnWidht)
        {
            var line = new StringBuilder();

            foreach (var widht in columnWidht)
            {
                line.Append(CultureInfo.InvariantCulture, $"+{new string('-', Math.Abs(widht) + 2)}");
            }

            line.Append('+');

            return line.ToString();
        }

        private static string GetRow(IEnumerable<string> fieldsToPrint, int[] columnWidth)
        {
            var rowBuilder = new StringBuilder();
            var i = 0;
            foreach (var field in fieldsToPrint)
            {
                var format = columnWidth[i] > 0 ? field.PadRight(columnWidth[i]) : field.PadLeft(Math.Abs(columnWidth[i]));
                rowBuilder.Append(CultureInfo.InvariantCulture, $"| {format} ");
                i++;
            }

            rowBuilder.Append('|');

            return rowBuilder.ToString();
        }
    }
}
