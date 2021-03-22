
using System.ComponentModel.DataAnnotations;

namespace CommonTypes
{
    public class KFZ : IEntity
    {
        public KFZ()
        {

        }

        [Key]
        public int ID { get; set; }
        public string Typ { get; set; }
        public string FahrgestellNR { get; set; }
        public string Kennzeichen { get; set; }
        public int Leistung { get; set; }

        public bool Equals(KFZ other)
        {
            return ID == other.ID &&
                    Typ == other.Typ &&
                    FahrgestellNR == other.FahrgestellNR &&
                    Kennzeichen == other.Kennzeichen &&
                    Leistung == other.Leistung;
        }
    }
}
