using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

[assembly: CLSCompliant(true)]

namespace FileCabinetApp
{
    /// <summary>
    /// Creates, edits and checks in entries. Finds records by parameters.
    /// </summary>
    public class FileCabinetMemoryService : IFileCabinetService
    {
        private readonly List<FileCabinetRecord> list = new List<FileCabinetRecord>();

        private readonly Dictionary<string, List<FileCabinetRecord>> memorizater = new Dictionary<string, List<FileCabinetRecord>>(StringComparer.InvariantCultureIgnoreCase);

        private readonly IIdGenerator idGenerator = new IdGenerator();
        private readonly IRecordValidator validator;

        /// <summary>
        /// Initializes a new instance of the <see cref="FileCabinetMemoryService"/> class.
        /// The class constructor takes a validation parameter.
        /// </summary>
        /// <param name="validator">Validation parameter.</param>
        public FileCabinetMemoryService(IRecordValidator validator)
        {
            this.validator = validator;
        }

        /// <summary>
        /// Creates a new record from user input.
        /// </summary>
        /// <param name="fileCabinetRecordNewData">The new date in the record.</param>
        /// <returns>Returns the id of the created record.</returns>
        /// <exception cref="ArgumentNullException">If the firstName or lastName is equal null.</exception>
        /// <exception cref="ArgumentException">The firstName or lastName length is less than 2 or greater than 60.The dateOfBirth is less than 01-Jun-1950 or greater today's date.
        /// The gender isn't equal 'f' or 'm'. The height is less than 0 or greater than 250. The weight is less than 0.</exception>
        public int CreateRecord(FileCabinetRecordNewData fileCabinetRecordNewData)
        {
            this.validator.ValidateParametrs(fileCabinetRecordNewData);

            var id = this.idGenerator.GetNext();

            var record = new FileCabinetRecord
            {
                Id = id,
                FirstName = fileCabinetRecordNewData.FirstName,
                LastName = fileCabinetRecordNewData.LastName,
                DateOfBirth = fileCabinetRecordNewData.DateOfBirth,
                Gender = fileCabinetRecordNewData.Gender,
                Height = fileCabinetRecordNewData.Height,
                Weight = fileCabinetRecordNewData.Weight,
            };

            this.CreateRecord(record);
            return record.Id;
        }

        /// <summary>
        /// Gets the count of all existed and deleted records.
        /// </summary>
        /// <returns>Returns the count of all existed and deleted records.</returns>
        public (int activeRecords, int deletedRecords) GetStat()
        {
            return (this.list.Count, 0);
        }

        /// <summary>
        /// Update an already existing record by id.
        /// </summary>
        /// <param name="id">The id of the record to be modified.</param>
        /// <param name="fileCabinetRecordNewData">The new date in the record.</param>
        /// <exception cref="ArgumentException">if records with the specified ID do not exist.</exception>
        public void Update(int id, FileCabinetRecordNewData fileCabinetRecordNewData)
        {
            this.memorizater.Clear();

            if (!this.IsExist(id))
            {
                throw new ArgumentException("Record's id isn't exist.");
            }

            this.validator.ValidateParametrs(fileCabinetRecordNewData);

            var result = this.list.FirstOrDefault(x => x.Id == id);

            if (result is null)
            {
                throw new ArgumentException("records with the specified ID do not exist.");
            }

            result.FirstName = fileCabinetRecordNewData.FirstName;
            result.LastName = fileCabinetRecordNewData.LastName;
            result.DateOfBirth = fileCabinetRecordNewData.DateOfBirth;
            result.Gender = fileCabinetRecordNewData.Gender;
            result.Height = fileCabinetRecordNewData.Height;
            result.Weight = fileCabinetRecordNewData.Weight;
        }

        /// <summary>
        /// Passes the state of an object.
        /// </summary>
        /// <returns>Class containing the state of an object.</returns>
        public FileCabinetServiceSnapshot MakeSnapshot()
        {
            return new FileCabinetServiceSnapshot(this.list.ToArray());
        }

        /// <summary>
        /// Adding imported records to existing records.
        /// </summary>
        /// <param name="fileCabinetServiceSnapshot">Ñlass instance.</param>
        public void Restore(FileCabinetServiceSnapshot fileCabinetServiceSnapshot)
        {
            var records = fileCabinetServiceSnapshot.Records;
            Dictionary<int, string> importExceptionByRecordId = new Dictionary<int, string>();
            bool isError = false;

            foreach (var record in records)
            {
                this.idGenerator.SkipId(record.Id);

                var recordNew = new FileCabinetRecordNewData(record.FirstName, record.LastName, record.DateOfBirth, record.Gender, record.Height, record.Weight);
                try
                {
                    this.validator.ValidateParametrs(recordNew);
                    if (this.IsExist(record.Id))
                    {
                        this.Update(record.Id, recordNew);
                    }
                    else
                    {
                        this.CreateRecord(record);
                    }
                }
                catch (Exception ex)
                {
                    importExceptionByRecordId.Add(record.Id, ex.Message);
                    isError = true;
                }
            }

            if (isError)
            {
                throw new ImportException(importExceptionByRecordId);
            }
        }

        /// <summary>
        /// Delete record by id.
        /// </summary>
        /// <param name="id">Record id to remove.</param>
        public void Delete(int id)
        {
            this.memorizater.Clear();

            if (!this.IsExist(id))
            {
                throw new ArgumentException("Record's id isn't exist.");
            }

            var valueForRemove = this.list.Find(x => x.Id == id);

            this.list.Remove(valueForRemove);
        }

        /// <summary>
        /// Defragments the data file. Only for FileCabinetFilesystemService.
        /// </summary>
        /// <returns>Count of purged records.Only for FileCabinetFilesystemService.</returns>
        public int Purge()
        {
            return 0;
        }

        /// <summary>
        /// Checks if records with the specified id exists.
        /// </summary>
        /// <param name="record">New record from user.</param>
        public void Insert(FileCabinetRecord record)
        {
            this.memorizater.Clear();

            if (this.IsExist(record.Id))
            {
                throw new ArgumentException("Record's id is exist.");
            }

            this.CreateRecord(record);
        }

        /// <summary>
        /// Finds records by parameters.
        /// </summary>
        /// <param name="conditions">Contains conditions with search parameters.</param>
        /// <param name="type">Contains an OR or AND operator.</param>
        /// <returns>Returns finded records.</returns>
        public IEnumerable<FileCabinetRecord> Find(Condition[] conditions, UnionType type)
        {
            string key = CreateKeyForMemorization(conditions, type);

            if (this.memorizater.TryGetValue(key, out var records))
            {
                return records;
            }

            var result = this.list.Where(x => RecordMatcher.IsMatch(x, conditions, type));

            this.memorizater.Add(key, result.ToList());
            return result;
        }

        /// <summary>
        /// Checks if records with the specified id exists.
        /// </summary>
        /// <param name="id">The id entered by the user.</param>
        /// <returns>True if records exists and false if records don't exist.</returns>
        public bool IsExist(int id)
        {
            return this.list.Any(x => x.Id == id);
        }

        private static string CreateKeyForMemorization(Condition[] conditions, UnionType type)
        {
            var key = new StringBuilder();
            key.Append(CultureInfo.InvariantCulture, $"Find_ConditionFields:{string.Join(',', conditions.Select(x => x.Field))}");

            var fieldsValueForKey = new List<string>();

            foreach (var condition in conditions)
            {
                switch (condition.Field)
                {
                    case FileCabinetRecordFields.Id:
                        fieldsValueForKey.Add(condition.Value.Id.ToString(CultureInfo.InvariantCulture));
                        break;
                    case FileCabinetRecordFields.FirstName:
                        fieldsValueForKey.Add(condition.Value.FirstName);
                        break;
                    case FileCabinetRecordFields.LastName:
                        fieldsValueForKey.Add(condition.Value.LastName);
                        break;
                    case FileCabinetRecordFields.DateOfBirth:
                        fieldsValueForKey.Add(condition.Value.DateOfBirth.ToString("yyyy-MMM-dd", CultureInfo.InvariantCulture));
                        break;
                    case FileCabinetRecordFields.Gender:
                        fieldsValueForKey.Add(condition.Value.Gender.ToString(CultureInfo.InvariantCulture));
                        break;
                    case FileCabinetRecordFields.Height:
                        fieldsValueForKey.Add(condition.Value.Height.ToString(CultureInfo.InvariantCulture));
                        break;
                    case FileCabinetRecordFields.Weight:
                        fieldsValueForKey.Add(condition.Value.Weight.ToString(CultureInfo.InvariantCulture));
                        break;
                }
            }

            key.Append(CultureInfo.InvariantCulture, $"_ConditionValue:{string.Join(',', fieldsValueForKey)}_UnionType:{type}");
            return key.ToString();
        }

        private void CreateRecord(FileCabinetRecord record)
        {
            this.list.Add(record);
        }
    }
}