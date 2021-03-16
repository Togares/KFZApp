using DataAccess;
using CommonTypes;
using System.Collections.Generic;
using BusinessLogic.Events;

namespace BusinessLogic
{
    public class KFZModel : IModel
    {
        public KFZModel()
        {
            _KFZTable = new TableKFZ();
        }

        private TableKFZ _KFZTable;

        public event EntitiesLoadedEventHandler EntitiesLoaded;

        public void GetAll()
        {
            var result = _KFZTable.GetData();
            if(EntitiesLoaded != null) EntitiesLoaded(result);
        }

        public void Delete(IEntity entity)
        {
            _KFZTable.Delete(entity);
        }

        public void Save(IEntity entity)
        {
            _KFZTable.Save(entity);
        }

        public void Update(IEntity entity)
        {
            _KFZTable.Update(entity);
        }

        public bool Validate(IEntity entity)
        {
            KFZ casted = (KFZ)entity;
            return !string.IsNullOrEmpty(casted.Kennzeichen) && !string.IsNullOrEmpty(casted.FahrgestellNR) && !string.IsNullOrEmpty(casted.Typ) && casted.Leistung > 0;
        }
    }
}
