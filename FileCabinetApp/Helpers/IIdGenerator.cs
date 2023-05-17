namespace FileCabinetApp.Helpers
{
    /// <summary>
    /// Generates unique identifiers.
    /// </summary>
    public interface IIdGenerator
    {
        /// <summary>
        /// Generates the next unique identifier.
        /// </summary>
        /// <returns>The next unique identifier.</returns>
        int GetNext();

        /// <summary>
        /// Sets the initial unique identifier.
        /// </summary>
        /// <param name="id">The initial unique identifier.</param>
        void SkipId(int id);
    }
}
