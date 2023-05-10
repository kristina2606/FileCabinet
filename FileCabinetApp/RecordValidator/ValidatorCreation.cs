using System;
using FileCabinetApp.Serializers.Json;
using Microsoft.Extensions.Configuration;

namespace FileCabinetApp.RecordValidator
{
    /// <summary>
    /// Define extension methods for ValidatorBuilder.
    /// </summary>
    public static class ValidatorCreation
    {
        /// <summary>
        /// Crerates a set of default validator.
        /// </summary>
        /// <param name="validatorBuilder">The ValidatorBuilder instance.</param>
        /// <returns>Returns a set of default validator.</returns>
        public static IRecordValidator CreateDefault(this ValidatorBuilder validatorBuilder)
        {
            IConfigurationRoot config = GetConfigurationFromJsonFile();
            string validationRule = "default";

            var recordValidationConfig = config.GetSection(validationRule).Get<RecordValidationConfig>();

            return CreateConfiguration(validatorBuilder, recordValidationConfig);
        }

        /// <summary>
        /// Crerates a set of custom validator.
        /// </summary>
        /// <param name="validatorBuilder">The ValidatorBuilder instance.</param>
        /// <returns>Returns a set of custom validator.</returns>
        public static IRecordValidator CreateCustom(this ValidatorBuilder validatorBuilder)
        {
            IConfigurationRoot config = GetConfigurationFromJsonFile();
            string validationRule = "custom";

            var recordValidationConfig = config.GetSection(validationRule).Get<RecordValidationConfig>();

            return CreateConfiguration(validatorBuilder, recordValidationConfig);
        }

        private static IConfigurationRoot GetConfigurationFromJsonFile()
        {
            return new ConfigurationBuilder()
                            .AddJsonFile("validation-rules.json")
                            .Build();
        }

        private static IRecordValidator CreateConfiguration(ValidatorBuilder validatorBuilder, RecordValidationConfig config)
        {
            string genderParametrStringComparison = config.Gender.StringComparison;

            return validatorBuilder
                        .ValidateFirstName(config.FirstName.MinLenght, config.FirstName.MaxLenght)
                        .ValidateLastName(config.LastName.MinLenght, config.LastName.MaxLenght)
                        .ValidateDateOfBirth(config.DateOfBirth.From, config.DateOfBirth.To)
                        .ValidateGender(config.Gender.RequiredFirstValue, config.Gender.RequiredSecondValue, Enum.Parse<StringComparison>(genderParametrStringComparison))
                        .ValidateHeight(config.Height.MinValue, config.Height.MaxValue)
                        .ValidateWeight(config.Weight.MinValue, config.Weight.MaxValue)
                        .Create();
        }
    }
}
