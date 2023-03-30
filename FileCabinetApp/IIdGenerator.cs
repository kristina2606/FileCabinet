namespace FileCabinetApp
{
    /// <summary>
    /// Generates the following id.
    /// </summary>
    public interface IIdGenerator
    {
        /// <summary>
        /// Generates the following id.
        /// </summary>
        /// <returns>Next id.</returns>
        int GetNext();

        /// <summary>
        /// Sets initial id.
        /// </summary>
        /// <param name="id">Initial id.</param>
        void SkipId(int id);
    }
}
