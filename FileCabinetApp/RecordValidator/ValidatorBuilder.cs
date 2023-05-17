using System;
using System.Collections.Generic;
using FileCabinetApp.RecordValidator.Implementation;

namespace FileCabinetApp.RecordValidator
{
    /// <summary>
    /// Сreates certain validation criteria depending on the user's choice.
    /// </summary>
    public class ValidatorBuilder
    {
        private readonly List<IRecordValidator> validators = new List<IRecordValidator>();

        /// <summary>
        /// Initializes a new instance of the <see cref="FirstNameValidator"/> class.
        /// </summary>
        /// <param name="minLenght">The minimum lenght of the first name allowed.</param>
        /// <param name="maxLenght">The maximum length of the first name allowed.</param>
        /// <returns>Returns ValidatorBuilder type.</returns>
        public ValidatorBuilder ValidateFirstName(int minLenght, int maxLenght)
        {
            var firstName = new FirstNameValidator(minLenght, maxLenght);
            this.validators.Add(firstName);
            return this;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LastNameValidator"/> class.
        /// </summary>
        /// <param name="minLenght">The minimum lenght of the last name allowed.</param>
        /// <param name="maxLenght">The maximum length of the last name allowed.</param>
        /// <returns>Returns ValidatorBuilder type.</returns>
        public ValidatorBuilder ValidateLastName(int minLenght, int maxLenght)
        {
            var lastName = new LastNameValidator(minLenght, maxLenght);
            this.validators.Add(lastName);
            return this;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DateOfBirthValidator"/> class.
        /// </summary>
        /// <param name="from">The minimum date of birth allowed.</param>
        /// <param name="to">The maximum date of birth allowed.</param>
        /// <returns>Returns ValidatorBuilder type.</returns>
        public ValidatorBuilder ValidateDateOfBirth(DateTime from, DateTime to)
        {
            var dateOfBirth = new DateOfBirthValidator(from, to);
            this.validators.Add(dateOfBirth);
            return this;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DateOfBirthValidator"/> class.
        /// </summary>
        /// <param name="minAge">The minimum age allowed.</param>
        /// <param name="maxAge">The maximum age allowed.</param>
        /// <returns>Returns ValidatorBuilder type.</returns>
        public ValidatorBuilder ValidateDateOfBirth(int minAge, int maxAge)
        {
            var dateOfBirth = new DateOfBirthValidator(minAge, maxAge);
            this.validators.Add(dateOfBirth);
            return this;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GenderValidator"/> class.
        /// </summary>
        /// <param name="requiredFirstValue">The first gender identity.</param>
        /// <param name="requiredSecondValue">The second gender identity.</param>
        /// <param name="sc">The string comparison type.</param>
        /// <returns>Returns ValidatorBuilder type.</returns>
        public ValidatorBuilder ValidateGender(char requiredFirstValue, char requiredSecondValue, StringComparison sc)
        {
            var gender = new GenderValidator(requiredFirstValue, requiredSecondValue, sc);
            this.validators.Add(gender);
            return this;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HeightValidator"/> class.
        /// </summary>
        /// <param name="minValue">The minimum allowed height.</param>
        /// <param name="maxValue">The maximum allowed height.</param>
        /// <returns>Returns ValidatorBuilder type.</returns>
        public ValidatorBuilder ValidateHeight(int minValue, int maxValue)
        {
            var heignt = new HeightValidator(minValue, maxValue);
            this.validators.Add(heignt);
            return this;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WeightValidator"/> class.
        /// </summary>
        /// <param name="minValue">The minimum allowed weight.</param>
        /// <param name="maxValue">The maximum allowed weight.</param>
        /// <returns>Returns ValidatorBuilder type.</returns>
        public ValidatorBuilder ValidateWeight(int minValue, int maxValue)
        {
            var weignt = new WeightValidator(minValue, maxValue);
            this.validators.Add(weignt);
            return this;
        }

        /// <summary>
        /// Crerate a set of validators.
        /// </summary>
        /// <returns>Returns a set of validators.</returns>
        public IRecordValidator Create()
        {
            return new CompositeValidator(this.validators);
        }
    }
}
