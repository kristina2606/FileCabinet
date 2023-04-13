﻿using System;
using System.Collections.Generic;

namespace FileCabinetApp
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
        /// <param name="minLenght">Min lenght of first name.</param>
        /// <param name="maxLenght">Max lenght of first name.</param>
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
        /// <param name="minLenght">Min lenght of last name.</param>
        /// <param name="maxLenght">Max lenght of last name.</param>
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
        /// <param name="from">Min date of birth.</param>
        /// <param name="to">Max date of birth.</param>
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
        /// <param name="minAge">Min age.</param>
        /// <param name="maxAge">Max age.</param>
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
        /// <param name="requiredFirstValue">First gender identities.</param>
        /// <param name="requiredSecondValue">Second gender identities.</param>
        /// <param name="sc">Specifies culture and case.</param>
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
        /// <param name="minValue">Min height.</param>
        /// <param name="maxValue">Max height.</param>
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
        /// <param name="minValue">Min weight.</param>
        /// <param name="maxValue">Max weight.</param>
        /// <returns>Returns ValidatorBuilder type.</returns>
        public ValidatorBuilder ValidateWeight(int minValue, int maxValue)
        {
            var weignt = new WeightValidator(minValue, maxValue);
            this.validators.Add(weignt);
            return this;
        }

        /// <summary>
        /// Crerate a set of validator.
        /// </summary>
        /// <returns>Returns a set of validator.</returns>
        public IRecordValidator Create()
        {
            return new CompositeValidator(this.validators);
        }
    }
}
