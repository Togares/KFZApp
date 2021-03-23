using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonTypes.EntityWrapper
{
    public class KFZTypViewModel
    {
        public KFZTypViewModel(KFZTyp typ)
        {
            Typ = typ;
        }

        public KFZTyp Typ { get; set; }
    }
}
