using System;
using FileCabinetApp.Models;

namespace FileCabinetApp.Helpers
{
    /// <summary>
    /// Provides methods for checking if a record matches specified conditions.
    /// </summary>
    public static class RecordMatcher
    {
        /// <summary>
        /// Checks if the record matches the conditions specified in the array.
        /// </summary>
        /// <param name="record">The record to check.</param>
        /// <param name="conditions">An array of conditions.</param>
        /// <param name="type">The type of condition matching (UnionType.And or UnionType.Or).</param>
        /// <returns>True if the record matches the conditions; otherwise, false.</returns>
        /// <exception cref="ArgumentException">Thrown when an unknown search criteria is encountered.</exception>
        public static bool IsMatch(FileCabinetRecord record, Condition[] conditions, UnionType type)
        {
            if (conditions.Length == 0)
            {
                return true;
            }

            foreach (Condition condition in conditions)
            {
                bool isMatch = condition.Field switch
                {
                    FileCabinetRecordFields.Id => record.Id == condition.Value.Id,
                    FileCabinetRecordFields.FirstName => record.FirstName.Equals(condition.Value.FirstName, StringComparison.OrdinalIgnoreCase),
                    FileCabinetRecordFields.LastName => record.LastName.Equals(condition.Value.LastName, StringComparison.OrdinalIgnoreCase),
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
