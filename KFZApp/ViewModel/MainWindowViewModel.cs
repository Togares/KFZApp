using Utility;
using KFZApp.View;
using BusinessLogic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Linq;
using System.Collections.Generic;
using CommonTypes;
using CommonTypes.EntityWrapper;
using System.Windows.Threading;

namespace KFZApp.ViewModel
{
    class MainWindowViewModel : BaseViewModel
    {
        public MainWindowViewModel(IView view, IModel model)
        {
            View = view;
            Model = model;
            View.DataContext = this;
            Model.EntitiesLoaded += Model_EntitiesLoaded;
            Model.CheckChangedEntities += Model_CheckChangedEntities;
            OnAdd();
            View.Show();
            _ReloadDataTimer = new DispatcherTimer();
            _ReloadDataTimer.Interval = new System.TimeSpan(0, 0, 0, 15, 0);
            _ReloadDataTimer.Tick += _ReloadDataTimer_Tick;
        }

        #region Fields

        private DispatcherTimer _ReloadDataTimer;

        #endregion Fields

        #region Properties

        private ObservableCollection<KFZTypViewModel> _AllTypes = new ObservableCollection<KFZTypViewModel>();
        public ObservableCollection<KFZTypViewModel> AllTypes
        {
            get { return _AllTypes; }
            set
            {
                _AllTypes = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<KFZViewModel> _KFZList = new ObservableCollection<KFZViewModel>();
        public ObservableCollection<KFZViewModel> KFZList
        {
            get { return _KFZList; }
            set
            {
                _KFZList = value;
                OnPropertyChanged();
            }
        }

        private KFZViewModel _SelectedKFZ;
        public KFZViewModel SelectedKFZ
        {
            get { return _SelectedKFZ; }
            set
            {
                _SelectedKFZ = value;
                OnPropertyChanged();
            }
        }

        private string _KennzeichenFilter = string.Empty;
        public string KennzeichenFilter
        {
            get { return _KennzeichenFilter; }
            set
            {
                _KennzeichenFilter = value;
                OnPropertyChanged();
                Filter();
            }
        }

        private string _FahrgestellNrFilter = string.Empty;
        public string FahrgestellNrFilter
        {
            get { return _FahrgestellNrFilter; }
            set
            {
                _FahrgestellNrFilter = value;
                OnPropertyChanged();
                Filter();
            }
        }

        private KFZTypViewModel _TypFilter = null;
        public KFZTypViewModel TypFilter
        {
            get => _TypFilter;
            set
            {
                _TypFilter = value;
                OnPropertyChanged();
                Filter();
            }
        }

        public DatabaseType SelectedConnectionType { get; set; } = DatabaseType.MySql;

        #endregion Properties

        #region Callbacks

        private void OnGetKFZs()
        {
            if (KFZList == null) KFZList = new ObservableCollection<KFZViewModel>();
            if (KFZList?.Count() > 0) KFZList?.Clear();
            Model.GetAll();
        }

        private void OnConnect()
        {
            ((KFZModel)Model).OpenConnection(SelectedConnectionType);
            ((KFZModel)Model).LoadTypes();
            if (((KFZModel)Model).HasConnection())
            {
                _ReloadDataTimer.Start();
            }
        }

        private void OnDisconnect()
        {
            _ReloadDataTimer.Stop();
            ((KFZModel)Model).CloseConnection();
        }

        private void OnDelete()
        {
            Model.Delete(SelectedKFZ.Entity);
            KFZList.Remove(SelectedKFZ);
        }

        private void Model_EntitiesLoaded(List<IEntity> entities)
        {
            if (entities.Count > 0)
            {
                // this is a terrible way of doing this
                if (entities[0] is KFZ)
                {
                    foreach (var item in entities)
                    {
                        var typ = new KFZTypViewModel(((KFZModel)Model).GetTyp((item as KFZ).IDTyp));
                        (item as KFZ).Typ = typ.Typ;
                        KFZList.Add(new KFZViewModel((KFZ)item));
                    }
                }
                else if (entities[0] is KFZTyp)
                {
                    AllTypes.Clear();
                    foreach (var item in entities)
                    {
                        KFZTypViewModel typ = new KFZTypViewModel((KFZTyp)item);
                        AllTypes.Add(typ);
                    }
                }
            }
        }

        private void CheckChanges()
        {
            // save selected item to restore later
            var prevSelection = SelectedKFZ;

            if (KFZList == null) KFZList = new ObservableCollection<KFZViewModel>();
            if (KFZList?.Count() > 0) KFZList?.Clear();
            Model.CheckChanges();

            SelectedKFZ = prevSelection;
        }

        private void Model_CheckChangedEntities(List<IEntity> entities)
        {
            var prevSelection = SelectedKFZ;

            // all entities loaded from the event
            List<KFZViewModel> loadedEntities = new List<KFZViewModel>();
            foreach (var item in entities)
            {
                var typ = new KFZTypViewModel(((KFZModel)Model).GetTyp((item as KFZ).IDTyp));
                (item as KFZ).Typ = typ.Typ;
                loadedEntities.Add(new KFZViewModel((KFZ)item));
            }

            // exclude selected item from updates
            var allUnselected = KFZList.Where(x => x.Entity != SelectedKFZ.Entity).ToList();

            foreach (var item in loadedEntities)
            {
                // add new items 
                if (!allUnselected.Contains(item))
                    allUnselected.Add(item);

                // remove deleted items
                foreach (var deleted in allUnselected.Where(x => !loadedEntities.Contains(x)).ToList())
                {
                    allUnselected.Remove(deleted);
                }

                // update existing items
                int index = allUnselected.IndexOf(allUnselected.Where(x => x.ID == item.ID && !x.Entity.Equals(item.Entity)).FirstOrDefault());
                if (index > 0 && index < allUnselected.Count - 1)
                    allUnselected[index] = item;
            }

            // upadate list
            KFZList.Clear();
            foreach (var item in allUnselected)
            {
                KFZList.Add(item);
            }
            if (prevSelection != null && prevSelection.ID != 0)
                KFZList.Add(prevSelection);
        }

        private void OnSave()
        {
            SelectedKFZ.Entity.IDTyp = SelectedKFZ.Typ.Typ.ID;
            if (SelectedKFZ.IsNew)
            {
                Model.Save(SelectedKFZ.Entity);
                KFZList.Add(SelectedKFZ);
                SelectedKFZ.IsNew = false;
            }
            else
            {
                Model.Update(SelectedKFZ.Entity);
            }
        }

        private void OnAdd()
        {
            SelectedKFZ = new KFZViewModel(new KFZ());
            SelectedKFZ.IsNew = true;
        }

        private void Filter()
        {
            OnGetKFZs();
            ObservableCollection<KFZViewModel> filtered = new ObservableCollection<KFZViewModel>();

            // Kennzeichen Filter 
            if (!string.IsNullOrEmpty(KennzeichenFilter))
            {
                foreach (var item in KFZList.Where(x => x.Kennzeichen.ToLower().Contains(KennzeichenFilter.ToLower())).ToList())
                {
                    if (!filtered.Contains(item))
                        filtered.Add(item);
                }
            }

            // Fahrgestellnummer Filter
            if (!string.IsNullOrEmpty(FahrgestellNrFilter))
            {
                foreach (var item in KFZList.Where(x => x.FahrgestellNR.ToLower().Contains(FahrgestellNrFilter.ToLower())).ToList())
                {
                    if (!filtered.Contains(item))
                        filtered.Add(item);
                }
            }

            // Typ Filter
            if (TypFilter != null)
            {
                foreach (var item in KFZList.Where(x => x.Typ.Typ.Beschreibung.Equals(TypFilter.Typ.Beschreibung)).ToList())
                {
                    if (!filtered.Contains(item))
                        filtered.Add(item);
                }
            }

            KFZList = filtered;
        }

        private void RemoveFilter()
        {
            KennzeichenFilter = FahrgestellNrFilter = string.Empty;
            TypFilter = null;
            OnGetKFZs();
        }

        private void _ReloadDataTimer_Tick(object sender, System.EventArgs e)
        {
            CheckChanges();
        }

        #endregion Callbacks

        #region Commands

        private ICommand _GetKFZsCommand;
        public ICommand GetKFZsCommand
        {
            get
            {
                if (_GetKFZsCommand == null)
                    _GetKFZsCommand = new RelayCommand<object>(x =>
                    {
                        OnGetKFZs();
                    }, x => Model != null);
                return _GetKFZsCommand;
            }
        }

        private ICommand _ConnectCommand;
        public ICommand ConnectCommand
        {
            get
            {
                if (_ConnectCommand == null)
                    _ConnectCommand = new RelayCommand<object>(x =>
                    {
                        OnConnect();
                    }, x => !((KFZModel)Model).HasConnection());
                return _ConnectCommand;
            }
        }

        private ICommand _DisconnectCommand;
        public ICommand DisconnectCommand
        {
            get
            {
                if (_DisconnectCommand == null)
                    _DisconnectCommand = new RelayCommand<object>(x =>
                    {
                        OnDisconnect();
                    }, x => ((KFZModel)Model).HasConnection());
                return _DisconnectCommand;
            }
        }

        private ICommand _DeleteCommand;
        public ICommand DeleteCommand
        {
            get
            {
                if (_DeleteCommand == null)
                    _DeleteCommand = new RelayCommand<object>(x =>
                    {
                        OnDelete();
                    }, x => SelectedKFZ != null && !SelectedKFZ.IsNew);
                return _DeleteCommand;
            }
        }

        private ICommand _SaveCommand;
        public ICommand SaveCommand
        {
            get
            {
                if (_SaveCommand == null)
                    _SaveCommand = new RelayCommand<object>(x =>
                    {
                        OnSave();
                    }, x => SelectedKFZ != null && Model.Validate(SelectedKFZ.Entity));
                return _SaveCommand;
            }
        }

        private ICommand _AddCommand;
        public ICommand AddCommand
        {
            get
            {
                if (_AddCommand == null)
                    _AddCommand = new RelayCommand<object>(x =>
                    {
                        OnAdd();
                    }, x => Model != null);
                return _AddCommand;
            }
        }

        private ICommand _RemoveFilterCommand;
        public ICommand RemoveFilterCommand
        {
            get
            {
                if (_RemoveFilterCommand == null)
                    _RemoveFilterCommand = new RelayCommand<object>(x =>
                    {
                        RemoveFilter();
                    }, x => true);
                return _RemoveFilterCommand;
            }
        }

        #endregion Commands
    }
}
