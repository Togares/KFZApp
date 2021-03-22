using CommonTypes;
using BusinessLogic.Events;
using DataAccess.EntityManager;

namespace BusinessLogic
{
    public class KFZModel : IModel
    {
        public KFZModel()
        {
            DataAccess.Connection.MySqlConnection.GetInstance().Connect();
        }

        private KFZManager _Manager = new KFZManager();
        public event EntitiesLoadedEventHandler EntitiesLoaded;

        public void GetAll()
        {
            if(EntitiesLoaded != null) EntitiesLoaded.Invoke(_Manager.Load());
        }

        public void Delete(IEntity entity)
        {
            
        }

        public void Save(IEntity entity)
        {
                       
        }

        public void Update(IEntity entity)
        {
           
        }

        public bool Validate(IEntity entity)
        {
            KFZ casted = (KFZ)entity;
            return !string.IsNullOrEmpty(casted.Kennzeichen) && !string.IsNullOrEmpty(casted.FahrgestellNR) && !string.IsNullOrEmpty(casted.Typ) && casted.Leistung > 0;
        }
    }
}
