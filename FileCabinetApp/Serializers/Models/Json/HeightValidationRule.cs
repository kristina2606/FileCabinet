namespace FileCabinetApp.Serializers.Json
{
    /// <summary>
    /// Represents the height validation rule loaded from a JSON configuration file.
    /// </summary>
    public class HeightValidationRule
    {
        /// <summary>
        /// Gets or sets the minimum height value for validation.
        /// </summary>
        /// <value>The minimum height value for validation.</value>
        public int MinValue { get; set; }

        /// <summary>
        /// Gets or sets  the maximum height value for validation.
        /// </summary>
        /// <value>The maximum height value for validation.</value>
        public int MaxValue { get; set; }
    }
}
