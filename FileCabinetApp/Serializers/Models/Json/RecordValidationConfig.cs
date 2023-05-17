using FileCabinetApp.Serializers.Json;

namespace FileCabinetApp.Serializers.Models.Json
{
    /// <summary>
    /// Represents the record validation configuration loaded from a JSON configuration file.
    /// </summary>
    public class RecordValidationConfig
    {
        /// <summary>
        /// Gets or sets the validation parameters for the first name.
        /// </summary>
        /// <value>The validation parameters for the first name.</value>
        public NameValidationRule FirstName { get; set; }

        /// <summary>
        /// Gets or sets the validation parameters for the last name.
        /// </summary>
        /// <value>The validation parameters for the last name.</value>
        public NameValidationRule LastName { get; set; }

        /// <summary>
        /// Gets or sets the validation parameters for the date of birth.
        /// </summary>
        /// <value>The validation parameters for the date of birth.</value>
        public DateOfBirthValidationRule DateOfBirth { get; set; }

        /// <summary>
        /// Gets or sets the validation parameters for the gender.
        /// </summary>
        /// <value>The validation parameters for the gender.</value>
        public GenderValidationRule Gender { get; set; }

        /// <summary>
        /// Gets or sets the validation parameters for the height.
        /// </summary>
        /// <value>The validation parameters for the height.</value>
        public HeightValidationRule Height { get; set; }

        /// <summary>
        /// Gets or sets the validation parameters for the weight.
        /// </summary>
        /// <value>The validation parameters for the weight.</value>
        public WeightValidationRule Weight { get; set; }
    }
}