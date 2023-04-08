using System;

namespace FileCabinetApp
{
    /// <summary>
    ///  Validate a new record from user input with settings for adults.
    /// </summary>
    public class CustomValidator : CompositeValidator
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomValidator"/> class.
        /// </summary>
        public CustomValidator()
            : base(new IRecordValidator[]
            {
            new FirstNameValidator(2, 15),
            new LastNameValidator(2, 20),
            new DateOfBirthValidator(18, 150),
            new GenderValidator('f', 'm', StringComparison.InvariantCultureIgnoreCase),
            new HeightValidator(145, 250),
            new WeightValidator(40, 300),
            })
        {
        }
    }
}
