using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using FileCabinetApp.Helpers;
using FileCabinetApp.Models;

namespace FileCabinetApp.Serializers.Reader
{
    /// <summary>
    /// Reads data from a CSV file and imports it.
    /// </summary>
    public class FileCabinetRecordCsvReader
    {
        private readonly StreamReader reader;

        /// <summary>
        /// Initializes a new instance of the <see cref="FileCabinetRecordCsvReader"/> class.
        /// </summary>
        /// <param name="reader">The StreamReader for the CSV file.</param>
        public FileCabinetRecordCsvReader(StreamReader reader)
        {
            this.reader = reader;
        }

        /// <summary>
        /// Reads all data from the CSV file and returns it as a list of FileCabinetRecord objects.
        /// </summary>
        /// <returns>A list of FileCabinetRecord objects.</returns>
        public IList<FileCabinetRecord> ReadAll()
        {
            var records = new List<FileCabinetRecord>();

            this.reader.ReadLine();
            while (this.reader.Peek() != -1)
            {
                var csvLine = this.reader.ReadLine();
                string[] values = csvLine.Split(',')
                                         .Select(p => p.Trim())
                                         .ToArray();

                records.Add(new FileCabinetRecord
                {
                    Id = Converters.IntConverter(values[0]).Item3,
                    FirstName = Converters.StringConverter(values[1]).Item3,
                    LastName = Converters.StringConverter(values[2]).Item3,
                    DateOfBirth = Convert.ToDateTime(values[3], CultureInfo.InvariantCulture),
                    Gender = Converters.CharConverter(values[4]).Item3,
                    Height = Converters.ShortConverter(values[5]).Item3,
                    Weight = Converters.DecimalConverter(values[6]).Item3,
                });
            }

            return records;
        }
    }
}
