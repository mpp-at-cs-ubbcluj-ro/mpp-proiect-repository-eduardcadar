using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AgentiiDeTurism.src.repository
{
    public interface IRepoInterface<T, Tid>
    {
        void save(T elem);
        void delete(T elem);
        void deleteById(Tid id);
        void update(T elem, Tid id);
        T getById(Tid id);
        ICollection<T> getAll();
        void clear();
    }
}
