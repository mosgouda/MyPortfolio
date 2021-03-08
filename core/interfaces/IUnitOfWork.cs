namespace core.interfaces
{
    public interface IUnitOfWork<T> where T : class
    {
        IGenericReposotry<T> Entity { get;  }
        void save();
    }
}
