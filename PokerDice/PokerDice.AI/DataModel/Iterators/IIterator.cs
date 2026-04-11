namespace Iterator.Model.Iterators
{
    public interface IIterator<T>
    {
        bool HasNext();
        T Next();
    }
}
