using Newtonsoft.Json;

namespace FileCabinetApp
{
    /// <summary>
    /// Create structure to validation parametrs fron .json configuration file.
    /// </summary>
    public class ValidationConfigValidatorStructure
    {
        /// <summary>
        /// Gets or sets first name validation parametrs.
        /// </summary>
        /// <value>
        /// First name validation parametrs.
        /// </value>
        [JsonProperty("firstName")]
        public ValidationConfigName FirstName { get; set; }

        /// <summary>
        /// Gets or sets last name validation parametrs.
        /// </summary>
        /// <value>
        /// Last name validation parametrs.
        /// </value>
        [JsonProperty("lastName")]
        public ValidationConfigName LastName { get; set; }

        /// <summary>
        /// Gets or sets date of birth validation parametrs.
        /// </summary>
        /// <value>
        /// Date of birth name validation parametrs.
        /// </value>
        [JsonProperty("dateOfBirth")]
        public ValidationConfigDateOfBirth DateOfBirth { get; set; }

        /// <summary>
        /// Gets or sets gender validation parametrs.
        /// </summary>
        /// <value>
        /// Gender validation parametrs.
        /// </value>
        [JsonProperty("gender")]
        public ValidationConfigGender Gender { get; set; }

        /// <summary>
        /// Gets or sets height validation parametrs.
        /// </summary>
        /// <value>
        /// Height validation parametrs.
        /// </value>
        [JsonProperty("height")]
        public ValidationConfigAnthropometry Height { get; set; }

        /// <summary>
        /// Gets or sets weight validation parametrs.
        /// </summary>
        /// <value>
        /// Weight validation parametrs.
        /// </value>
        [JsonProperty("weight")]
        public ValidationConfigAnthropometry Weight { get; set; }
    }
}