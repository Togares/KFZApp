using CommonTypes;
using System.Collections.Generic;

namespace DataAccess.EntityManager
{
    public abstract class EntityManager
    {
        public abstract List<IEntity> Load();
        public abstract void Save(IEntity entity);
        public abstract void Update(IEntity entity);
        public abstract void Delete(IEntity entity);
    }
}
