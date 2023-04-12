using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using static System.Net.Mime.MediaTypeNames;

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
            IConfigurationRoot builder = GetConfigurationFromJsonFile();

            var config = builder.Get<ValidationConfig>().Default;

            return CreateConfiguration(validatorBuilder, config);
        }

        /// <summary>
        /// Crerate a set of custom validator.
        /// </summary>
        /// <param name="validatorBuilder">The class to which the extension method belongs.</param>
        /// <returns>Returns a set of custom validator.</returns>
        public static IRecordValidator CreateCustom(this ValidatorBuilder validatorBuilder)
        {
            IConfigurationRoot builder = GetConfigurationFromJsonFile();

            var config = builder.Get<ValidationConfig>().Custom;

            return CreateConfiguration(validatorBuilder, config);
        }

        private static IConfigurationRoot GetConfigurationFromJsonFile()
        {
            return new ConfigurationBuilder()
                            .SetBasePath(Directory.GetCurrentDirectory())
                            .AddJsonFile("validation-rules.json")
                            .Build();
        }

        private static IRecordValidator CreateConfiguration(ValidatorBuilder validatorBuilder, ValidationConfigValidatorStructure config)
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
