using Newtonsoft.Json;

namespace FileCabinetApp
{
    /// <summary>
    /// Create anthropometry parametr fron .json configuration file.
    /// </summary>
    public class HeightValidationRule
    {
        /// <summary>
        /// Gets or sets minimum height value validation parametrs.
        /// </summary>
        /// <value>
        /// First name validation parametrs.
        /// </value>
        [JsonProperty("minValue")]
        public int MinValue { get; set; }

        /// <summary>
        /// Gets or sets maximum height value validation parametrs.
        /// </summary>
        /// <value>
        /// First name validation parametrs.
        /// </value>
        [JsonProperty("maxValue")]
        public int MaxValue { get; set; }
    }
}
