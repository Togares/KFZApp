using Utility;
using CommonTypes;

namespace KFZApp.ViewModel.EntityWrapper
{
    public class KFZViewModel : BindableBase
    {
        public KFZViewModel(KFZ entity)
        {
            _Entity = entity;
        }

        private KFZ _Entity;

        #region EntityProperties

        public KFZ Entity 
        {
            get { return _Entity; }
        }

        public int ID
        {
            get { return _Entity.ID; }
            set
            {
                _Entity.ID = value;
                OnPropertyChanged();
            }
        }

        public string Typ
        {
            get { return _Entity.Typ; }
            set
            {
                _Entity.Typ = value;
                OnPropertyChanged();
            }
        }

        public string FahrgestellNR
        {
            get { return _Entity.FahrgestellNR; }
            set
            {
                _Entity.FahrgestellNR = value;
                OnPropertyChanged();
            }
        }

        public string Kennzeichen
        {
            get { return _Entity.Kennzeichen; }
            set
            {
                _Entity.Kennzeichen = value;
                OnPropertyChanged();
            }
        }

        public int Leistung
        {
            get { return _Entity.Leistung; }
            set
            {
                _Entity.Leistung = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region NonEntityProperties

        private bool _IsDirty = false;
        public bool IsDirty
        {
            get { return _IsDirty; }
            set
            {
                _IsDirty = value;
                OnPropertyChanged();
            }
        }

        private bool _IsNew = false;
        public bool IsNew 
        { 
            get { return _IsNew; }
            set
            {
                _IsNew = value;
                OnPropertyChanged();
            }
        }

        #endregion
    }
}
