using CommonTypes;
using BusinessLogic.Events;
using System.Collections.Generic;

namespace BusinessLogic
{
    public interface IModel
    {
        bool Validate(IEntity entity);
        void GetAll();
        event EntitiesLoadedEventHandler EntitiesLoaded;
        void Save(IEntity entity);
        void Delete(IEntity entity);
        void Update(IEntity entity);
    }
}
