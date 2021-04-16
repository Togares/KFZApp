using System;
using System.ComponentModel.DataAnnotations;

namespace CommonTypes
{
    public class KFZTyp : IEntity
    {
        public KFZTyp()
        {

        }

        [Key]
        public int ID { get; set; }
        public string Beschreibung { get; set; }

        public bool Equals(KFZTyp other)
        {
            return ID == other.ID &&
                Beschreibung == other.Beschreibung;
        }
    }
}
