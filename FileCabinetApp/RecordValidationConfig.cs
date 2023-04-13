using Newtonsoft.Json;

namespace FileCabinetApp
{
    /// <summary>
    /// Create structure to validation parametrs fron .json configuration file.
    /// </summary>
    public class RecordValidationConfig
    {
        /// <summary>
        /// Gets or sets first name validation parametrs.
        /// </summary>
        /// <value>
        /// First name validation parametrs.
        /// </value>
        [JsonProperty("firstName")]
        public NameValidationRule FirstName { get; set; }

        /// <summary>
        /// Gets or sets last name validation parametrs.
        /// </summary>
        /// <value>
        /// Last name validation parametrs.
        /// </value>
        [JsonProperty("lastName")]
        public NameValidationRule LastName { get; set; }

        /// <summary>
        /// Gets or sets date of birth validation parametrs.
        /// </summary>
        /// <value>
        /// Date of birth name validation parametrs.
        /// </value>
        [JsonProperty("dateOfBirth")]
        public DateOfBirthValidationRule DateOfBirth { get; set; }

        /// <summary>
        /// Gets or sets gender validation parametrs.
        /// </summary>
        /// <value>
        /// Gender validation parametrs.
        /// </value>
        [JsonProperty("gender")]
        public GenderValidationRule Gender { get; set; }

        /// <summary>
        /// Gets or sets height validation parametrs.
        /// </summary>
        /// <value>
        /// Height validation parametrs.
        /// </value>
        [JsonProperty("height")]
        public HeightValidationRule Height { get; set; }

        /// <summary>
        /// Gets or sets weight validation parametrs.
        /// </summary>
        /// <value>
        /// Weight validation parametrs.
        /// </value>
        [JsonProperty("weight")]
        public WeightValidationRule Weight { get; set; }
    }
}