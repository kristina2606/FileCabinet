using System;
using Newtonsoft.Json;

namespace FileCabinetApp
{
    /// <summary>
    /// Create date of birth parametr fron .json configuration file.
    /// </summary>
    public class DateOfBirthValidationRule
    {
        /// <summary>
        /// Gets or sets minimum date of birth validation parametrs.
        /// </summary>
        /// <value>
        /// First name validation parametrs.
        /// </value>
        [JsonProperty("from")]
        public DateTime From { get; set; }

        /// <summary>
        /// Gets or sets maximum date of birth validation parametrs.
        /// </summary>
        /// <value>
        /// First name validation parametrs.
        /// </value>
        [JsonProperty("to")]
        public DateTime To { get; set; }
    }
}
