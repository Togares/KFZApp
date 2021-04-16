using CommonTypes;
using BusinessLogic.Events;

namespace BusinessLogic
{
    public interface IModel
    {
        bool Validate(IEntity entity);
        void GetAll();
        void CheckChanges();
        event EntitiesLoadedEventHandler EntitiesLoaded;
        event EntitiesLoadedEventHandler CheckChangedEntities;
        void Save(IEntity entity);
        void Delete(IEntity entity);
        void Update(IEntity entity);
    }
}
