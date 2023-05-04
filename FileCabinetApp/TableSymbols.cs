namespace FileCabinetApp
{
    /// <summary>
    /// Symbols to build a table.
    /// </summary>
    public static class TableSymbols
    {
        /// <summary>
        /// Used to separate cells in table rows.
        /// </summary>
        public const char HorizontalLine = '-';

        /// <summary>
        /// Used to separate cells in table columns.
        /// </summary>
        public const char VerticalLine = '|';

        /// <summary>
        /// Used to separate cells at table corners.
        /// </summary>
        public const char Intersection = '+';

        /// <summary>
        /// Used to move to a new table row.
        /// </summary>
        public const char NewLine = '\n';
    }
}
