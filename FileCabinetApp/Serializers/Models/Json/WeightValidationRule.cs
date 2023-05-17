namespace FileCabinetApp.Serializers.Json
{
    /// <summary>
    /// Represents the weight validation parameters loaded from a JSON configuration file.
    /// </summary>
    public class WeightValidationRule
    {
        /// <summary>
        /// Gets or sets the minimum weight value for validation.
        /// </summary>
        /// <value>The minimum weight value for validation.</value>
        public int MinValue { get; set; }

        /// <summary>
        /// Gets or sets the maximum weight value for validation.
        /// </summary>
        /// <value>The maximum weight value for validation.</value>
        public int MaxValue { get; set; }
    }
}
