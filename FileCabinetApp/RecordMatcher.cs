using System;

namespace FileCabinetApp
{
    /// <summary>
    /// Checking if the record matches the conditions.
    /// </summary>
    public static class RecordMatcher
    {
        /// <summary>
        /// Checks if the object matches the conditions passed in the array.
        /// </summary>
        /// <param name="record">Record for cheack.</param>
        /// <param name="conditions">Array with conditions.</param>
        /// <param name="type">Сompliance conditions.</param>
        /// <returns>Result if record matches the conditions.</returns>
        /// <exception cref="ArgumentException">If Condition didn't find.</exception>
        public static bool IsMatch(FileCabinetRecord record, Condition[] conditions, UnionType type)
        {
            if (conditions.Length == 0)
            {
                return true;
            }

            foreach (var condition in conditions)
            {
                bool isMatch = condition.Field switch
                {
                    FileCabinetRecordFields.Id => record.Id == condition.Value.Id,
                    FileCabinetRecordFields.FirstName => record.FirstName.Equals(condition.Value.FirstName, StringComparison.InvariantCultureIgnoreCase),
                    FileCabinetRecordFields.LastName => record.LastName.Equals(condition.Value.LastName, StringComparison.InvariantCultureIgnoreCase),
                    FileCabinetRecordFields.DateOfBirth => record.DateOfBirth == condition.Value.DateOfBirth,
                    FileCabinetRecordFields.Gender => record.Gender == condition.Value.Gender,
                    FileCabinetRecordFields.Height => record.Height == condition.Value.Height,
                    FileCabinetRecordFields.Weight => record.Weight == condition.Value.Weight,
                    _ => throw new ArgumentException($"Unknown search criteria: {condition.Field}"),
                };

                if (type == UnionType.And && !isMatch)
                {
                    return false;
                }
                else if (type == UnionType.Or && isMatch)
                {
                    return true;
                }
            }

            return type == UnionType.And;
        }
    }
}
