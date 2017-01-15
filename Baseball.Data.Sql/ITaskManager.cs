using System.Collections.Generic;

namespace Baseball.Data.Sql
{
    public interface ITaskManager
    {
        void Add(string name);
        void Remove(int id);
        void Complete(int id);
        IEnumerable<TaskItem> GetAll();

    }
}
