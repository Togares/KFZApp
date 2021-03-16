using CommonTypes;
using System.Collections.Generic;

namespace DataAccess
{
    public abstract class Table<T>
    {
        public Table()
        {
            Initialize();
        }

        public List<T> GetData()
        {
            if (_Connection != null && _Connection.GetAdapter() != null)
            {
                if (_LoadedData == null)
                {
                    LoadObjects(out _LoadedData);
                }
            }
            return _LoadedData;
        }

        public void Delete(IEntity entity)
        {
            string query = string.Format("DELETE FROM {0} WHERE {1} = {2}", TableName, IDColumnName, entity.ID);
            _Connection.GetAdapter().Adapter.ExecuteSQL(query);
        }

        public abstract void Save(IEntity entity);
        public abstract void Update(IEntity entity);

        protected IConnection _Connection;
        protected List<T> _LoadedData = null;
        protected string TableName { get; set; }
        protected string IDColumnName { get; set; }

        /// <summary>
        /// Called in the constructor.
        /// Initlialize _Connection here
        /// </summary>
        protected abstract void Initialize();

        /// <summary>
        /// Called during <code>GetData()</code>
        /// Load objects from the specific table here.
        /// </summary>
        /// <param name="result"></param>
        protected abstract void LoadObjects(out List<T> result);
    }
}
