using System;

namespace FileCabinetApp.Helpers
{
    /// <summary>
    /// Generates unique identifiers.
    /// </summary>
    public class IdGenerator : IIdGenerator
    {
        private int currentId = 1;

        /// <summary>
        /// Generates the next unique identifier.
        /// </summary>
        /// <returns>The next unique identifier.</returns>
        public int GetNext()
        {
            return this.currentId++;
        }

        /// <summary>
        /// Sets the initial unique identifier.
        /// </summary>
        /// <param name="id">The initial unique identifier.</param>
        public void SkipId(int id)
        {
            this.currentId = Math.Max(this.currentId, id + 1);
        }
    }
}
