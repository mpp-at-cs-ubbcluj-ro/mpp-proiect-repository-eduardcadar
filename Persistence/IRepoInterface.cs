using System.Collections.Generic;

namespace Persistence
{
    public interface IRepoInterface<T, Tid>
    {
        void Save(T elem);
        T GetById(Tid id);
        IEnumerable<T> GetAll();
    }
}
