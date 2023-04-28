namespace FileCabinetApp
{
    /// <summary>
    /// representation of search terms in records.
    /// </summary>
    public class Condition
    {
        /// <summary>
        /// Gets or sets the field by which the search will be performed in the records.
        /// </summary>
        /// <value>
        /// The field by which the search will be performed in the records.
        /// </value>
        public FieldsName Field { get; set; }

        /// <summary>
        /// Gets or sets the value contains the search values.
        /// </summary>
        /// <value>
        /// The value contains the search values.
        /// </value>
        public FileCabinetRecord Value { get; set; }
    }
}