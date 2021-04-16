using CommonTypes;
using BusinessLogic.Events;
using DataAccess.Connection;
using DataAccess.EntityManager;

namespace BusinessLogic
{
    public class KFZModel : IModel
    {
        public KFZModel()
        {

        }

        private KFZManager _Manager = new KFZManager();
        public event EntitiesLoadedEventHandler EntitiesLoaded;
        public event EntitiesLoadedEventHandler CheckChangedEntities;

        public void OpenConnection(DatabaseType type)
        {
            DatabaseConnection.GetInstannce().Database = type;
            DatabaseConnection.GetInstannce().Connect();
        }

        public void CloseConnection()
        {
            DatabaseConnection.GetInstannce().Disconnect();
        }

        public bool HasConnection()
        {
            bool result = false;
            if (DatabaseConnection.GetInstannce().ActualConnection != null && DatabaseConnection.GetInstannce().ActualConnection.IsConnected)
                result = true;
            return result;
        }

        public void GetAll()
        {
            if (EntitiesLoaded != null) EntitiesLoaded.Invoke(_Manager.Load());
        }

        public void CheckChanges()
        {
            if (CheckChangedEntities != null) CheckChangedEntities.Invoke(_Manager.Load());
        }

        public void LoadTypes()
        {
            if (EntitiesLoaded != null) EntitiesLoaded.Invoke(_Manager.LoadTypes());
        }

        public KFZTyp GetTyp(int id)
        {
            KFZTyp result = null;
            if (id > 0)
            {
                result = _Manager.GetTyp(id);
            }
            return result;
        }

        public void Delete(IEntity entity)
        {
            _Manager.Delete(entity);
        }

        public void Save(IEntity entity)
        {
            _Manager.Save(entity);
        }

        public void Update(IEntity entity)
        {
            _Manager.Update(entity);
        }

        public bool Validate(IEntity entity)
        {
            KFZ casted = (KFZ)entity;
            return !string.IsNullOrEmpty(casted.Kennzeichen) && !string.IsNullOrEmpty(casted.FahrgestellNR) && casted.Leistung > 0;
        }
    }
}
