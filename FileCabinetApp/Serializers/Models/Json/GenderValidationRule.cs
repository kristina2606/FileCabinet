namespace FileCabinetApp.Serializers.Json
{
    /// <summary>
    /// Represents the gender validation rule loaded from a JSON configuration file.
    /// </summary>
    public class GenderValidationRule
    {
        /// <summary>
        /// Gets or sets the required first value for gender validation.
        /// </summary>
        /// <value>The required first value for gender validation.</value>
        public char RequiredFirstValue { get; set; }

        /// <summary>
        /// Gets or sets the required second value for gender validation.
        /// </summary>
        /// <value>The required second value for gender validation.</value>
        public char RequiredSecondValue { get; set; }

        /// <summary>
        /// Gets or sets the string comparison method for gender validation.
        /// </summary>
        /// <value>The string comparison method for gender validation.</value>
        public string StringComparison { get; set; }
    }
}
