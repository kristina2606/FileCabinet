using System;

namespace FileCabinetApp
{
    /// <summary>
    /// Define extension methods for ValidatorBuilder.
    /// </summary>
    public static class ValidatorCreation
    {
        /// <summary>
        /// Crerate a set of default validator.
        /// </summary>
        /// <param name="validatorBuilder">The class to which the extension method belongs.</param>
        /// <returns>Returns a set of default validator.</returns>
        public static IRecordValidator CreateDefault(this ValidatorBuilder validatorBuilder)
        {
            return validatorBuilder
                        .ValidateFirstName(2, 60)
                        .ValidateLastName(2, 60)
                        .ValidateDateOfBirth(new DateTime(1950, 1, 1), DateTime.Now)
                        .ValidateGender('f', 'm', StringComparison.InvariantCulture)
                        .ValidateHeight(0, 250)
                        .ValidateWeight(0, 300)
                        .Create();
        }

        /// <summary>
        /// Crerate a set of custom validator.
        /// </summary>
        /// <param name="validatorBuilder">The class to which the extension method belongs.</param>
        /// <returns>Returns a set of custom validator.</returns>
        public static IRecordValidator CreateCustom(this ValidatorBuilder validatorBuilder)
        {
            return validatorBuilder
                        .ValidateFirstName(2, 15)
                        .ValidateLastName(2, 20)
                        .ValidateDateOfBirth(18, 150)
                        .ValidateGender('f', 'm', StringComparison.InvariantCultureIgnoreCase)
                        .ValidateHeight(145, 250)
                        .ValidateWeight(40, 300)
                        .Create();
        }
    }
}
