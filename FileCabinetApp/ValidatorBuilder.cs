using System;
using System.Collections.Generic;

namespace FileCabinetApp
{
    public class ValidatorBuilder
    {
        private List<IRecordValidator> validators = new List<IRecordValidator>();

        public ValidatorBuilder ValidateFirstName(int minLenght, int maxLenght)
        {
            var firstName = new FirstNameValidator(minLenght, maxLenght);
            this.validators.Add(firstName);
            return this;
        }

        public ValidatorBuilder ValidateLastName(int minLenght, int maxLenght)
        {
            var lastName = new LastNameValidator(minLenght, maxLenght);
            this.validators.Add(lastName);
            return this;
        }

        public ValidatorBuilder ValidateDateOfBirth(int from, int to)
        {
            var dateOfBirth = new DateOfBirthValidator(from, to);
            this.validators.Add(dateOfBirth);
            return this;
        }

        public ValidatorBuilder ValidateGender(char requiredFirstValue, char requiredSecondValue, StringComparison sc)
        {
            var gender = new GenderValidator(requiredFirstValue, requiredSecondValue, sc);
            this.validators.Add(gender);
            return this;
        }

        public ValidatorBuilder ValidateHeight(int min, int max)
        {
            var heignt = new HeightValidator(min, max);
            this.validators.Add(heignt);
            return this;
        }

        public ValidatorBuilder ValidateWeight(int min, int max)
        {
            var weignt = new WeightValidator(min, max);
            this.validators.Add(weignt);
            return this;
        }

        public IRecordValidator Create()
        {
            return new CompositeValidator(this.validators);
        }

        public IRecordValidator CreateDefault()
        {
            IRecordValidator defaultValidator = new ValidatorBuilder()
            .ValidateFirstName(2, 60)
            .ValidateLastName(2, 60)
            .ValidateDateOfBirth(0, 75)
            .ValidateGender('f', 'm', StringComparison.InvariantCulture)
            .ValidateHeight(0, 250)
            .ValidateWeight(0, 300)
            .Create();

            return defaultValidator;
        }

        public IRecordValidator CreateCustom()
        {
            IRecordValidator customValidator = new ValidatorBuilder()
            .ValidateFirstName(2, 15)
            .ValidateLastName(2, 20)
            .ValidateDateOfBirth(18, 150)
            .ValidateGender('f', 'm', StringComparison.InvariantCultureIgnoreCase)
            .ValidateHeight(145, 250)
            .ValidateWeight(40, 300)
            .Create();

            return customValidator;
        }
    }
}
