using System;
using CommonTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace BusinessLogic.Events
{
    public delegate void EntitiesLoadedEventHandler(List<IEntity> entities);
}