namespace FileCabinetApp
{
    public interface IRecordIterator
    {
        FileCabinetRecord GetNext();

        bool HasMore();
    }
}
