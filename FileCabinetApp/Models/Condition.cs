namespace FileCabinetApp.Models
{
    /// <summary>
    /// Represents a search condition for records.
    /// </summary>
    public class Condition
    {
        /// <summary>
        /// Gets or sets the field by which the search will be performed in the records.
        /// </summary>
        /// <value>The field by which the search will be performed in the records.</value>
        public FileCabinetRecordFields Field { get; set; }

        /// <summary>
        /// Gets or sets the value that contains the search values.
        /// </summary>
        /// <value>The value that contains the search values.</value>
        public FileCabinetRecord Value { get; set; }
    }
}