using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace FileCabinetApp
{
    /// <summary>
    /// Imports data from a csv file.
    /// </summary>
    public class FileCabinetRecordCsvReader
    {
        private readonly StreamReader reader;

        /// <summary>
        /// Initializes a new instance of the <see cref="FileCabinetRecordCsvReader"/> class.
        /// </summary>
        /// <param name="reader">Path to import a csv file with records.</param>
        public FileCabinetRecordCsvReader(StreamReader reader)
        {
            this.reader = reader;
        }

        /// <summary>
        /// Imports all datas from a csv file.
        /// </summary>
        /// <returns>Returns all datas from a csv file.</returns>
        public IList<FileCabinetRecord> ReadAll()
        {
            IList<FileCabinetRecord> records = new List<FileCabinetRecord>();

            this.reader.ReadLine();
            while (this.reader.Peek() != -1)
            {
                var csvLine = this.reader.ReadLine();
                string[] values = csvLine
                    .Split(',')
                    .Select(p => p.Trim())
                    .Where(p => !string.IsNullOrWhiteSpace(p))
                    .ToArray();

                records.Add(new FileCabinetRecord
                {
                    Id = Converter.IntConverter(values[0]).Item3,
                    FirstName = Converter.StringConverter(values[1]).Item3,
                    LastName = Converter.StringConverter(values[2]).Item3,
                    DateOfBirth = Convert.ToDateTime(values[3], CultureInfo.CurrentCulture),
                    Gender = Converter.CharConverter(values[4]).Item3,
                    Height = Converter.ShortConverter(values[5]).Item3,
                    Weight = Converter.DecimalConverter(values[6]).Item3,
                });
            }

            return records;
        }
    }
}
