using System;

namespace FileCabinetApp
{
    /// <summary>
    /// Validate a new record from user input.
    /// </summary>
    public class DefaultValidator : CompositeValidator
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultValidator"/> class.
        /// </summary>
        public DefaultValidator()
          : base(new IRecordValidator[]
          {
            new FirstNameValidator(2, 60),
            new LastNameValidator(2, 60),
            new DateOfBirthValidator(0, 75),
            new GenderValidator('f', 'm', StringComparison.InvariantCulture),
            new HeightValidator(0, 250),
            new WeightValidator(0, 300),
          })
        {
        }
    }
}