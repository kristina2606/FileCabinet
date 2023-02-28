using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace FileCabinetApp
{
    public class FileCabinetService
    {
        private readonly List<FileCabinetRecord> list = new List<FileCabinetRecord>();

        public int CreateRecord(string firstName, string lastName, DateTime dateOfBirth, char gender, short height, decimal weight)
        {
            if (string.IsNullOrEmpty(firstName))
            {
                throw new ArgumentNullException(nameof(firstName));
            }

            if (firstName.Length < 2 || firstName.Length > 60)
            {
                throw new ArgumentException("first name length is less than 2 or greater than 60");
            }

            if (string.IsNullOrEmpty(lastName))
            {
                throw new ArgumentNullException(nameof(lastName));
            }

            if (lastName.Length < 2 || lastName.Length > 60)
            {
                throw new ArgumentException("last name length is less than 2 or greater than 60");
            }

            if (dateOfBirth > DateTime.Now || dateOfBirth < new DateTime(1950, 1, 1))
            {
                throw new ArgumentException("date of birth is less than 01-Jun-1950 or greater today's date");
            }

            if (gender != 'f' && gender != 'm')
            {
                throw new ArgumentException("gender must be 'f' or 'm'.");
            }

            if (height <= 0 || height > 250)
            {
                throw new ArgumentException("height very small or very large.");
            }

            if (weight <= 0)
            {
                throw new ArgumentException("weight very small or very large.");
            }

            var record = new FileCabinetRecord
            {
                Id = this.list.Count + 1,
                FirstName = firstName,
                LastName = lastName,
                DateOfBirth = dateOfBirth,
                Gender = gender,
                Height = height,
                Weight = weight,
            };

            this.list.Add(record);

            return record.Id;
        }

        public FileCabinetRecord[] GetRecords()
        {
            return this.list.ToArray();
        }

        public int GetStat()
        {
            return this.list.Count;
        }

        public void EditRecord(int id, string firstName, string lastName, DateTime dateOfBirth, char gender, short height, decimal weight)
        {
            foreach (var record in this.list)
            {
                if (record.Id == id)
                {
                    record.FirstName = firstName;
                    record.LastName = lastName;
                    record.DateOfBirth = dateOfBirth;
                    record.Gender = gender;
                    record.Height = height;
                    record.Weight = weight;
                    break;
                }
            }
        }

        public void ChekId(int id)
        {
            var isNoRecord = true;
            foreach (var record in this.list)
            {
                if (record.Id == id)
                {
                    isNoRecord = false;
                    break;
                }
            }

            if (isNoRecord)
            {
                throw new ArgumentException("records with the specified ID do not exist.");
            }
        }
    }
}