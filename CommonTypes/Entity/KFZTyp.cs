using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonTypes
{
    public class KFZTyp : IEntity
    {
        public KFZTyp()
        {

        }

        public int ID { get; set; }
        public string Beschreibung { get; set; }
    }
}
