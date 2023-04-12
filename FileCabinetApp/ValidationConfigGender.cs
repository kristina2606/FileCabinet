using Newtonsoft.Json;

namespace FileCabinetApp
{
    /// <summary>
    /// Create gender parametr fron .json configuration file.
    /// </summary>
    public class ValidationConfigGender
    {
        /// <summary>
        /// Gets or sets required first value validation parametrs.
        /// </summary>
        /// <value>
        /// First name validation parametrs.
        /// </value>
        [JsonProperty("requiredFirstValue")]
        public char RequiredFirstValue { get; set; }

        /// <summary>
        /// Gets or sets required second value validation parametrs.
        /// </summary>
        /// <value>
        /// First name validation parametrs.
        /// </value>
        [JsonProperty("requiredSecondValue")]
        public char RequiredSecondValue { get; set; }

        /// <summary>
        /// Gets or sets StringComparison validation parametrs.
        /// </summary>
        /// <value>
        /// First name validation parametrs.
        /// </value>
        [JsonProperty("stringComparison")]
        public string StringComparison { get; set; }
    }
}
