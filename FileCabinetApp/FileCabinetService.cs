using System;
using System.Collections.Generic;
using System.Linq;

namespace FileCabinetApp
{
    public class FileCabinetService
    {
        private readonly List<FileCabinetRecord> list = new List<FileCabinetRecord>();

        private readonly Dictionary<string, List<FileCabinetRecord>> firstNameDictionary = new Dictionary<string, List<FileCabinetRecord>>();

        private readonly Dictionary<string, List<FileCabinetRecord>> lastNameDictionary = new Dictionary<string, List<FileCabinetRecord>>();

        private readonly Dictionary<DateTime, List<FileCabinetRecord>> dateOfBirthDictionary = new Dictionary<DateTime, List<FileCabinetRecord>>();

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

            AddToIndex(record, this.firstNameDictionary, firstName.ToLowerInvariant());
            AddToIndex(record, this.lastNameDictionary, lastName.ToLowerInvariant());
            AddToIndex(record, this.dateOfBirthDictionary, dateOfBirth);

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
            var result = this.list.FirstOrDefault(x => x.Id == id);

            if (result is null)
            {
                throw new ArgumentException("records with the specified ID do not exist.");
            }

            EditDictionary(this.firstNameDictionary, result.FirstName, result, firstName);

            EditDictionary(this.lastNameDictionary, result.LastName, result, lastName);

            if (this.dateOfBirthDictionary.TryGetValue(result.DateOfBirth, out List<FileCabinetRecord> allValueOfKey))
            {
                allValueOfKey.Remove(result);
            }

            AddToIndex(result, this.dateOfBirthDictionary, dateOfBirth);

            result.FirstName = firstName;
            result.LastName = lastName;
            result.DateOfBirth = dateOfBirth;
            result.Gender = gender;
            result.Height = height;
            result.Weight = weight;
        }

        public FileCabinetRecord[] FindByFirstName(string firstName)
        {
            if (this.firstNameDictionary.TryGetValue(firstName.ToLowerInvariant(), out List<FileCabinetRecord> allValueOfKey))
            {
                return allValueOfKey.ToArray();
            }
            else
            {
                return Array.Empty<FileCabinetRecord>();
            }
        }

        public FileCabinetRecord[] FindByLastName(string lastName)
        {
            if (this.lastNameDictionary.TryGetValue(lastName.ToLowerInvariant(), out List<FileCabinetRecord> allValueOfKey))
            {
                return allValueOfKey.ToArray();
            }
            else
            {
                return Array.Empty<FileCabinetRecord>();
            }
        }

        public FileCabinetRecord[] FindByDateOfBirth(DateTime dateOfBirth)
        {
            if (this.dateOfBirthDictionary.TryGetValue(dateOfBirth, out List<FileCabinetRecord> allValueOfKey))
            {
                return allValueOfKey.ToArray();
            }
            else
            {
                return Array.Empty<FileCabinetRecord>();
            }
        }

        public bool IsExist(int id)
        {
            return this.list.Any(x => x.Id == id);
        }

        private static void EditDictionary(Dictionary<string, List<FileCabinetRecord>> dictionary, string existingKey, FileCabinetRecord record, string newKey)
        {
            existingKey = existingKey.ToLowerInvariant();
            if (dictionary.TryGetValue(existingKey, out List<FileCabinetRecord> allValueOfExistingKey))
            {
                allValueOfExistingKey.Remove(record);
            }

            AddToIndex(record, dictionary, newKey.ToLowerInvariant());
        }

        private static void AddToIndex(FileCabinetRecord record, Dictionary<string, List<FileCabinetRecord>> dictionary, string key)
        {
            if (dictionary.TryGetValue(key, out List<FileCabinetRecord> allValueOfKey))
            {
                allValueOfKey.Add(record);
            }
            else
            {
                List<FileCabinetRecord> valueForDictionary = new List<FileCabinetRecord>() { record };
                dictionary.Add(key, valueForDictionary);
            }
        }

        private static void AddToIndex(FileCabinetRecord record, Dictionary<DateTime, List<FileCabinetRecord>> dictionary, DateTime key)
        {
            if (dictionary.TryGetValue(key, out List<FileCabinetRecord> allValueOfKey))
            {
                allValueOfKey.Add(record);
            }
            else
            {
                List<FileCabinetRecord> valueForDictionary = new List<FileCabinetRecord>() { record };
                dictionary.Add(key, valueForDictionary);
            }
        }
    }
}