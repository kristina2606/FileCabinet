using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace FileCabinetApp
{
    public class FileCabinetRecordCsvReader
    {
        private readonly StreamReader reader;

        public FileCabinetRecordCsvReader(StreamReader reader)
        {
            this.reader = reader;
        }

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
                    DateOfBirth = Converter.DateConverter(values[3]).Item3,
                    Gender = Converter.CharConverter(values[4]).Item3,
                    Height = Converter.ShortConverter(values[5]).Item3,
                    Weight = Converter.DecimalConverter(values[6]).Item3,
                });
            }

            return records;
        }
    }
}
