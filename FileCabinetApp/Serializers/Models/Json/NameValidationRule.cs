namespace FileCabinetApp.Serializers.Json
{
    /// <summary>
    /// Represents the name validation rule loaded from a JSON configuration file.
    /// </summary>
    public class NameValidationRule
    {
        /// <summary>
        /// Gets or sets the minimum name length for validation.
        /// </summary>
        /// <value>The minimum name length for validation.</value>
        public int MinLenght { get; set; }

        /// <summary>
        /// Gets or sets the maximum name length for validation.
        /// </summary>
        /// <value>The maximum name length for validation.</value>
        public int MaxLenght { get; set; }
    }
}
