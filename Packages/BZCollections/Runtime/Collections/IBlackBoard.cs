namespace BZ.Core.Collections
{
    public interface IBlackBoard
    {
        void Write<T>(string name, T value);
        T Read<T>(string name);
        bool ContainsParameter(string name);
        void Clear();
        bool Delete(string name);
    }
}
