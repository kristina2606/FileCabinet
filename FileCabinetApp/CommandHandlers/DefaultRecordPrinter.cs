using System;
using System.Collections.Generic;
using System.Globalization;

namespace FileCabinetApp.CommandHandlers
{
    /// <summary>
    /// Print with screen print default style.
    /// </summary>
    public class DefaultRecordPrinter : IRecordPrinter
    {
        /// <summary>
        /// Print with screen print default style.
        /// </summary>
        /// <param name="records">All exiting records.</param>
        public void Print(IEnumerable<FileCabinetRecord> records)
        {
            foreach (var record in records)
            {
                var id = record.Id;
                var firstName = record.FirstName;
                var lastName = record.LastName;
                var dateOfBirth = record.DateOfBirth.ToString("yyyy-MMM-dd", CultureInfo.InvariantCulture);
                var gender = record.Gender;
                var height = record.Height;
                var weight = record.Weight;

                Console.WriteLine($"#{id}, {firstName}, {lastName}, {dateOfBirth}, {gender}, {height}, {weight}");
            }
        }
    }
}
