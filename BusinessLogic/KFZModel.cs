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

        public void OpenConnection()
        {
            DatabaseConnection.GetInstannce().Connect();
        }

        public void CloseConnection()
        {
            DatabaseConnection.GetInstannce().Disconnect();
        }

        public bool HasConnection()
        {
            return DatabaseConnection.GetInstannce().ActualConnection.IsConnected;
        }

        public void GetAll()
        {
            if (EntitiesLoaded != null) EntitiesLoaded.Invoke(_Manager.Load());
        }

        public void LoadTypes()
        {
            if (EntitiesLoaded != null) EntitiesLoaded.Invoke(_Manager.LoadTypes());
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
            return !string.IsNullOrEmpty(casted.Kennzeichen) && !string.IsNullOrEmpty(casted.FahrgestellNR) && !string.IsNullOrEmpty(casted.Typ?.Beschreibung) && casted.Leistung > 0;
        }
    }
}
