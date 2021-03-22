using System;
using CommonTypes;
using System.Linq;
using DataAccess.DatabaseContext;
using System.Collections.Generic;

namespace DataAccess.EntityManager
{
    public class KFZManager : EntityManager
    {
        #region Overrides

        public override List<IEntity> Load()
        {
            using (var context = new KFZContext())
            {
                List<IEntity> result = new List<IEntity>();
                foreach (var item in context.Fahrzeuge)
                {
                    result.Add(item);
                }
                return result;
            }
        }

        public override void Delete(IEntity entity)
        {
            using (var context = new KFZContext())
            {
                try
                {
                    context.Fahrzeuge.Remove((KFZ)entity);
                    context.Entry(entity).State = System.Data.Entity.EntityState.Deleted;
                    context.SaveChanges();
                }
                catch (Exception e)
                {
                    throw e;
                }

            }
        }

        public override void Save(IEntity entity)
        {
            using (var context = new KFZContext())
            {
                try
                {
                    context.Fahrzeuge.Add((KFZ)entity);
                    context.Entry(entity).State = System.Data.Entity.EntityState.Added;
                    context.SaveChanges();
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }

        public override void Update(IEntity entity)
        {
            using (var context = new KFZContext())
            {
                try
                {
                    var old = Find(entity.ID);
                    if (old != null)
                    {
                        old = entity;
                        context.Entry(entity).State = System.Data.Entity.EntityState.Modified;
                        context.SaveChanges();
                    }
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }

        #endregion

        public IEntity Find(int id)
        {
            using (var context = new KFZContext())
            {
                return context.Fahrzeuge.Where(x => x.ID == id).First();
            }
        }       
    }
}
