using System;

namespace FileCabinetApp
{
    /// <summary>
    /// Represents the type of union operation.
    /// </summary>
    public class UnionType
    {
        private const string OperatorOr = "or";
        private const string OperatorAnd = "and";

        private string operatorType;

        /// <summary>
        /// Gets or sets the type of the join operation.
        /// </summary>
        /// <value>
        /// The type of the join operation.
        /// </value>
        public string OperatorType
        {
            get
            {
                return this.operatorType;
            }

            set
            {
                if (string.Equals(value, OperatorOr, StringComparison.InvariantCultureIgnoreCase))
                {
                    this.operatorType = OperatorOr;
                }
                else if (string.Equals(value, OperatorAnd, StringComparison.InvariantCultureIgnoreCase))
                {
                    this.operatorType = OperatorAnd;
                }
                else
                {
                    throw new ArgumentException("You entered wrong operator.");
                }
            }
        }
    }
}
