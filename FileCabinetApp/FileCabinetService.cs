using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.WebSockets;
using System.Reflection;
using System.Reflection.Metadata;
using System.Text;
using System.Text.RegularExpressions;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace FileCabinetApp
{
    public class FileCabinetService
    {
        private readonly List<FileCabinetRecord> list = new List<FileCabinetRecord>();

        private readonly Dictionary<string, List<FileCabinetRecord>> firstNameDictionary = new Dictionary<string, List<FileCabinetRecord>>();

        private readonly Dictionary<string, List<FileCabinetRecord>> lastNameDictionary = new Dictionary<string, List<FileCabinetRecord>>();

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

            List<FileCabinetRecord> valueFirstNameForDictionary = new List<FileCabinetRecord>();
            valueFirstNameForDictionary.Add(record);

            firstName = firstName.ToLowerInvariant();
            if (this.firstNameDictionary.TryGetValue(firstName, out List<FileCabinetRecord> firstNameValue))
            {
                firstNameValue.Add(record);
            }
            else
            {
                this.firstNameDictionary.Add(firstName, valueFirstNameForDictionary);
            }

            List<FileCabinetRecord> valueLastNameForDictionary = new List<FileCabinetRecord>();
            valueLastNameForDictionary.Add(record);
            lastName = lastName.ToLowerInvariant();
            if (this.lastNameDictionary.TryGetValue(lastName, out List<FileCabinetRecord> lastNameValue))
            {
                lastNameValue.Add(record);
            }
            else
            {
                this.lastNameDictionary.Add(lastName, valueLastNameForDictionary);
            }

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

            EditDictionary(this.firstNameDictionary, result.FirstName.ToLowerInvariant(), id, firstName.ToLowerInvariant());
            EditDictionary(this.lastNameDictionary, result.LastName.ToLowerInvariant(), id, lastName.ToLowerInvariant());

            result.FirstName = firstName;
            result.LastName = lastName;
            result.DateOfBirth = dateOfBirth;
            result.Gender = gender;
            result.Height = height;
            result.Weight = weight;
        }

        public FileCabinetRecord[] FindByFirstName(string firstName)
        {
            if (this.firstNameDictionary.TryGetValue(firstName.ToLowerInvariant().Trim('"'), out List<FileCabinetRecord> value))
            {
                return value.ToArray();
            }
            else
            {
                throw new ArgumentNullException(nameof(firstName));
            }
        }

        public FileCabinetRecord[] FindByLastName(string lastName)
        {
            if (this.lastNameDictionary.TryGetValue(lastName.ToLowerInvariant().Trim('"'), out List<FileCabinetRecord> value))
            {
                return value.ToArray();
            }
            else
            {
                throw new ArgumentNullException(nameof(lastName));
            }
        }

        public FileCabinetRecord[] FindByDateOfBirth(string date)
        {
            List<FileCabinetRecord> findDateOfBirth = new List<FileCabinetRecord>();

            if (DateTime.TryParseExact(date, "yyyy-MMM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dateOfBirth))
            {
                foreach (var record in this.list)
                {
                    if (dateOfBirth == record.DateOfBirth)
                    {
                        findDateOfBirth.Add(record);
                    }
                }

                return findDateOfBirth.ToArray();
            }
            else
            {
                throw new ArgumentException("Error. You introduced the date in the wrong format.");
            }
        }

        public bool IsExist(int id)
        {
            return this.list.Any(x => x.Id == id);
        }

        private static void EditDictionary(Dictionary<string, List<FileCabinetRecord>> dictionary, string parametr, int id, string newKey)
        {
            if (dictionary.Remove(parametr, out List<FileCabinetRecord> value))
            {
                for (var i = 0; i < value.Count; i++)
                {
                    if (value[i].Id == id)
                    {
                        List<FileCabinetRecord> immutablePartOfTheDictionary = new List<FileCabinetRecord>();
                        List<FileCabinetRecord> mutablePartOfTheDictionary = new List<FileCabinetRecord>();

                        immutablePartOfTheDictionary.AddRange(value);
                        immutablePartOfTheDictionary.RemoveAt(i);

                        mutablePartOfTheDictionary.Add(value[i]);

                        if (immutablePartOfTheDictionary.Count > 0)
                        {
                            dictionary.Add(parametr, immutablePartOfTheDictionary);
                        }

                        if (dictionary.TryGetValue(newKey, out List<FileCabinetRecord> existingDictionary))
                        {
                            existingDictionary.Add(value[i]);
                        }
                        else
                        {
                            dictionary.Add(newKey, mutablePartOfTheDictionary);
                        }

                        break;
                    }
                }
            }
        }
    }
}