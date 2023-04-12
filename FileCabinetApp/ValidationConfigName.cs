using Newtonsoft.Json;

namespace FileCabinetApp
{
    /// <summary>
    /// Create name parametr fron .json configuration file.
    /// </summary>
    public class ValidationConfigName
    {
        /// <summary>
        /// Gets or sets minimum name length validation parametrs.
        /// </summary>
        /// <value>
        /// First name validation parametrs.
        /// </value>
        [JsonProperty("minLenght")]
        public int MinLenght { get; set; }

        /// <summary>
        /// Gets or sets maximum name length validation parametrs.
        /// </summary>
        /// <value>
        /// First name validation parametrs.
        /// </value>
        [JsonProperty("maxLenght")]
        public int MaxLenght { get; set; }
    }
}
