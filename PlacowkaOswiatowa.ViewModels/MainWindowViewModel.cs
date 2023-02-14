using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PlacowkaOswiatowa.Domain.Commands;
using PlacowkaOswiatowa.Domain.DTOs;
using PlacowkaOswiatowa.Domain.Extensions;
using PlacowkaOswiatowa.Domain.Helpers;
using PlacowkaOswiatowa.Domain.Interfaces.CommonInterfaces;
using PlacowkaOswiatowa.Domain.Resources;
using PlacowkaOswiatowa.ViewModels.Abstract;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Threading;

namespace PlacowkaOswiatowa.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        #region Prywatne pola
        private IServiceProvider _provider;
        private readonly ISignalHub _signal;
        private readonly ILogger<MainWindowViewModel> _logger;
        #endregion

        #region Konstruktor

        public MainWindowViewModel(IServiceProvider serviceProvider, 
            ILogger<MainWindowViewModel> logger)
        {
            _provider = serviceProvider;
            _signal = SignalHub.Instance;
            _sideMenuVisibility = "Collapsed";
            _sideMenuLocation = "Left";
            _workspacesVisibility = "Collapsed";
            _loginViewVisibility = "Collapsed";
            //wyświetlanie panelu logowania
            _isLoggedIn = true;
            _signal.NewMessage += (s, m) => StatusMessage = m;
            _signal.LoggedInChanged += () => IsLoggedIn = true;
            _signal.HideLogingRequest += () => ChangeLoginViewVisibility();
            _signal.NewViewRequested += (s, vh) => OnNewViewRequested(s, vh);
            _logger = logger;
        }

        #endregion

        #region Komendy Menu i Paska narzędzi        

        private ICommand _UrlopPracownikaCommand;
        public ICommand UrlopPracownikaCommand => _UrlopPracownikaCommand ??=
            new BaseCommand(CreateViewAsync<UrlopPracownikaViewModel>);
        
        private ICommand _NowyPracownikCommand;
        public ICommand NowyPracownikCommand => _NowyPracownikCommand ??=
            new BaseCommand(() => OnNewViewRequested(null, 
                new ViewHandler(typeof(NowyPracownikViewModel))));
        
        private ICommand _WszyscyPracownicyCommand;
        public ICommand WszyscyPracownicyCommand => _WszyscyPracownicyCommand ??=
            new BaseCommand(ShowSingletonAsync<WszyscyPracownicyViewModel>);

        private ICommand _ZarobkiPracownikaCommand;
        public ICommand ZarobkiPracownikaCommand => _ZarobkiPracownikaCommand ??=
            new BaseCommand(CreateViewAsync<ZarobkiPracownikaViewModel>);

        private ICommand _NowyUczenCommand;
        public ICommand NowyUczenCommand => _NowyUczenCommand ??=
            new BaseCommand(CreateViewAsync<NowyUczenViewModel>);
        
        private ICommand _WszyscyUczniowieCommand;
        public ICommand WszyscyUczniowieCommand => _WszyscyUczniowieCommand ??=
            new BaseCommand(ShowSingletonAsync<WszyscyUczniowieViewModel>);

        private ICommand _WszystkieAdresyCommand;
        public ICommand WszystkieAdresyCommand => _WszystkieAdresyCommand ??=
            new BaseCommand(ShowSingletonAsync<WszystkieAdresyViewModel>);

        private ICommand _WszystkiePrzedmiotyCommand;
        public ICommand WszystkiePrzedmiotyCommand => _WszystkiePrzedmiotyCommand ??=
            new BaseCommand(ShowSingletonAsync<WszystkiePrzedmiotyViewModel>);

        private ICommand _WszystkieOcenyCommand;
        public ICommand WszystkieOcenyCommand => _WszystkieOcenyCommand ??=
            new BaseCommand(ShowSingletonAsync<WszystkieOcenyViewModel>);

        private ICommand _WszystkieUrlopyCommand;
        public ICommand WszystkieUrlopyCommand => _WszystkieUrlopyCommand ??=
            new BaseCommand(ShowSingletonAsync<WszystkieUrlopyViewModel>);

        private ICommand _NowaUmowaCommand;
        public ICommand NowaUmowaCommand => _NowaUmowaCommand ??=
            new BaseCommand(CreateViewAsync<NowaUmowaViewModel>);

        private ICommand _WszystkieUmowyCommand;
        public ICommand WszystkieUmowyCommand => _WszystkieUmowyCommand ??=
            new BaseCommand(ShowSingletonAsync<WszystkieUmowyViewModel>);

        private ICommand _ChangeSideMenuVisibilityCommand;
        public ICommand ChangeSideMenuVisibilityCommand => _ChangeSideMenuVisibilityCommand ??=
            new BaseCommand(() => ChangeSideMenuVisibility());

        private ICommand _ChangeSideMenuLocationCommand;
        public ICommand ChangeSideMenuLocationCommand => _ChangeSideMenuLocationCommand ??=
            new BaseCommand(() => ChangeSideMenuLocation());

        private ICommand _ZamknijCommand;
        public ICommand ZamknijCommand => _ZamknijCommand ??=
            new BaseCommand(() => Zamknij());

        private ICommand _OProgramieCommand;
        public ICommand OProgramieCommand => _OProgramieCommand ??=
            new BaseCommand(() => OProgramie());
        
        private ICommand _WidokZalogujCommand;
        public ICommand WidokZalogujCommand => _WidokZalogujCommand ??=
            new BaseCommand(() => ChangeLoginViewVisibility());

        #endregion

        #region Przyciski w menu bocznym
        private ReadOnlyCollection<CommandViewModel> _Commands;
        public ReadOnlyCollection<CommandViewModel> Commands
        {
            get
            {
                if(_Commands == null)
                {
                    List<CommandViewModel> cmds = CreateCommands();
                    _Commands = new ReadOnlyCollection<CommandViewModel>(cmds);
                }
                return _Commands;
            }
        }
        private List<CommandViewModel> CreateCommands()
        {
            return new List<CommandViewModel>
            {
                new CommandViewModel(BaseResources.NowyPracownik, new BaseCommand(() => 
                    OnNewViewRequested(null, new ViewHandler(typeof(NowyPracownikViewModel))))),
                new CommandViewModel(BaseResources.WszyscyPracownicy, new BaseCommand(() => 
                    OnNewViewRequested(null, new ViewHandler(typeof(WszyscyPracownicyViewModel), isSingleton: true)))),
                new CommandViewModel(BaseResources.NowyUczen, new BaseCommand(() => 
                    OnNewViewRequested(null, new ViewHandler(typeof(NowyUczenViewModel))))),
                new CommandViewModel(BaseResources.WszyscyUczniowie, new BaseCommand(() => 
                    OnNewViewRequested(null, new ViewHandler(typeof(WszyscyUczniowieViewModel), isSingleton: true))))
            };
        }
        #endregion

        #region Zakładki
        private ObservableCollection<WorkspaceViewModel> _Workspaces;
        public ObservableCollection<WorkspaceViewModel> Workspaces
        {
            get
            {
                if(_Workspaces == null)
                {
                    _Workspaces = new ObservableCollection<WorkspaceViewModel>();
                    _Workspaces.CollectionChanged += this.OnWorkspacesChanged;
                }
                return _Workspaces;
            }
        }
        #endregion

        #region Obsługa zdarzeń
        private void OnWorkspacesChanged(object? sender, NotifyCollectionChangedEventArgs e) 
        {
            if (e.NewItems != null && e.NewItems.Count != 0)
                foreach (WorkspaceViewModel workspace in e.NewItems)
                {
                    workspace.RequestClose += this.OnWorkspaceRequestClose;
                }
            if (e.OldItems != null && e.OldItems.Count != 0)
                foreach (WorkspaceViewModel workspace in e.OldItems)
                {
                    workspace.RequestClose -= this.OnWorkspaceRequestClose;
                }
        }

        private void OnWorkspaceRequestClose(object? sender, EventArgs e)
        {
            WorkspaceViewModel workspace = sender as WorkspaceViewModel;
            //workspace.Dispose();
            if (_Workspaces.Count == 1)
            {
                WorkspacesVisibility = "Collapsed";
                StatusMessage = BaseResources.DefaultStatusMessage;
            }
            this.Workspaces.Remove(workspace); 
        }

        //Obsługa zdarzenia wywyołanie widoku
        private void OnNewViewRequested(object sender, ViewHandler viewHandler)
        {
            try
            {
                _ = viewHandler ?? throw new ArgumentNullException("Nie podano wymaganego argumentu");
                _ = viewHandler.ViewType ?? throw new ArgumentNullException("Nie podano typu zakładki do utworzenia");

                WorkspaceViewModel workspace;

                if (viewHandler.IsSingleton)
                {
                    workspace = this.Workspaces.FirstOrDefault(vm => vm.GetType() == viewHandler.ViewType);

                    if (workspace is not null)
                    {
                        SetActiveWorkspace(workspace);
                        return;
                    }
                }

                workspace = _provider.GetRequiredService(viewHandler.ViewType) as WorkspaceViewModel;
                if (workspace == null)
                    throw new ArgumentException("Zadany typ nie jest zakładką");

                if (viewHandler.ViewType.IsAssignableTo(typeof(ILoadable)))
                {
                    //* (komentarz na dole)
                    Task.Run(() => (workspace as ILoadable).LoadAsync()
                        .SafeFireAndForget((e) => OnTaskException(workspace.GetType().Name, e)));
                }

                if (viewHandler.ItemId != null)
                {
                    if (!viewHandler.ViewType.IsAssignableTo(typeof(IEditable)))
                        throw new ArgumentException("Żądany widok nie służy do edycji");

                    //pobranie rekordu do edycji
                    Task.Run(() => (workspace as IEditable).LoadItem(viewHandler.ItemId)
                         .SafeFireAndForget((e) => OnTaskException(workspace.GetType().Name, e)));
                }

                if (viewHandler.IsModal)
                {
                    CreateWindow(workspace, sender);
                    return;
                }

                Workspaces.Add(workspace);
                SetActiveWorkspace(workspace);
            }
            catch(Exception e)
            {
                MessageBox.Show($"Nie udało się utworzyć widoku. {e.Message}",
                    "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);

                _logger.LogError(
                    "Błąd podczas tworzenia nowego widoku: {error}",
                    e.Message);
            }
        }
        #endregion

        #region Funkcje pomocnicze
        private void SetActiveWorkspace(WorkspaceViewModel workspace)
        {
            if (_Workspaces.Count == 1)
                WorkspacesVisibility = "Visible";
                
            Debug.Assert(this.Workspaces.Contains(workspace));
            StatusMessage = $"Widok: {workspace.DisplayName}";

            ICollectionView collectionView = CollectionViewSource.GetDefaultView(this.Workspaces);
            if (collectionView != null)
                collectionView.MoveCurrentTo(workspace);
        }
        //asynchroniczne wywołanie widoku wszystkich elementów
        private void ShowSingletonAsync<T>() 
            where T : WorkspaceViewModel, ILoadable
        {
            var workspace = this.Workspaces.FirstOrDefault(vm => vm is T) as T;
            if(workspace == null)
                CreateViewAsync<T>();
            else
                this.SetActiveWorkspace(workspace);
        }
        //synchroniczne wywołanie widoku wszystkich elementów
        private void ShowSingleton<T>() where T : WorkspaceViewModel
        {
            var workspace = this.Workspaces.FirstOrDefault(vm => vm is T) as T;
            if (workspace == null)
                workspace = _provider.GetRequiredService<T>();
            this.SetActiveWorkspace(workspace);
        }
        //synchroniczne wywołanie widoku pojedynczego elemtu
        private void CreateView<T>()
            where T : WorkspaceViewModel
        {
            var workspace = _provider.GetRequiredService<T>();
            Workspaces.Add(workspace);
            SetActiveWorkspace(workspace);
        }

        /// <summary>
        /// Metoda tworząca ViewModel, który to model wymaga pobrania danych z bazy
        /// Pobieranie następuje asynchronicznie
        /// </summary>
        /// <typeparam name="T"></typeparam>
        private void CreateViewAsync<T>()
            where T : WorkspaceViewModel, ILoadable
        {
            //Provider tworzy ViewModel z kontenara zależności
            var workspace = _provider.GetRequiredService<T>();
            //Po załadowaniu powiadomi UI że dane są dostępne.
            //W przypadku niepowodzenia metoda LoadAsync wyświetli odpowiedni komunikat
            Task.Run(() => workspace.LoadAsync()
                .SafeFireAndForget((e) => OnTaskException(workspace.GetType().Name, e)));

            Workspaces.Add(workspace);
            SetActiveWorkspace(workspace);
        }

        /// <summary>
        /// Metoda służąca do otworzenia zadanego ViewModelu o zdefiniowanym wcześniej widoku
        /// (DataTemplate w MainWindowResources) w nowym oknie
        /// </summary>
        private void CreateWindow(WorkspaceViewModel viewModel, object listener, 
            string title = null)
        {
            Window window = new Window();
            window.Title = string.IsNullOrEmpty(title) ? BaseResources.BaseTitle : title;
            window.Height = 500;
            window.Width = 900;
            window.Content = viewModel;
            viewModel.RequestClose += (s, e) =>
            {
                window.Close();
                OnWindowRequestClose(s, listener);
            };
            window.Show();
        }

        private void OnWindowRequestClose(object sender, object listener)
        {
            var viewModel = sender as NowyAdresViewModel;
            if (viewModel != null)
            {
                if (Guid.TryParse(listener?.ToString(), out Guid listenerId))
                    _signal.RaiseAddressCreatedDelegate(listenerId, viewModel.Item);
            }
        }

        private void Zamknij()
        {
            Application.Current.Windows[0].Close();
        }

        private void OnTaskException(string viewModelName, Exception e)
        {
            _logger.LogError(
                "Wystąpił błąd podczas ładowania danych widoku {viewModel}: '{error}'",
                viewModelName, e.Message);

            MessageBox.Show(
                $"Nie udało się załadować danych widoku " +
                $"{viewModelName.Remove(viewModelName.Length - "ViewModel".Length)}. " +
                $"Błąd: '{e.Message}'", 
                "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        #endregion

        #region Widocznosc elementów
        private string _sideMenuVisibility;
        public string SideMenuVisibility
        {
            get => _sideMenuVisibility;
            set
            {
                if (value != _sideMenuVisibility)
                {
                    _sideMenuVisibility = value;
                    OnPropertyChanged(() => SideMenuVisibility);
                }
            }
        }
        private string _workspacesVisibility;
        public string WorkspacesVisibility
        {
            get => _workspacesVisibility;
            set
            {
                if (value != _workspacesVisibility)
                {
                    _workspacesVisibility = value;
                    OnPropertyChanged(() => WorkspacesVisibility);
                }
            }
        }
        private string _sideMenuLocation;
        public string SideMenuLocation
        {
            get => _sideMenuLocation;
            set 
            {
                if (value != _sideMenuLocation)
                {
                    _sideMenuLocation = value;
                    OnPropertyChanged(() => SideMenuLocation);
                }
            }
        }

        private string _loginViewVisibility;
        public string LoginViewVisibility
        {
            get => _loginViewVisibility;
            set
            {
                if (value != _loginViewVisibility)
                {
                    _loginViewVisibility = value;
                    OnPropertyChanged(() => LoginViewVisibility);
                }
            }
        }

        private void ChangeSideMenuVisibility() =>
            SideMenuVisibility = SideMenuVisibility == "Visible" ? "Collapsed" : "Visible";

        private void ChangeSideMenuLocation() =>
            SideMenuLocation = SideMenuLocation == "Right" ? "Left" : "Right";

        private void ChangeLoginViewVisibility()
        {
            LoginViewVisibility = LoginViewVisibility == "Visible" ? "Collapsed" : "Visible";
            if(LoginViewVisibility == "Visible")
                _signal.SendMessage(this, "Logowanie do aplikacji...");
            else
                if(!IsLoggedIn)
                    _signal.SendMessage(this, BaseResources.DefaultStatusMessage);
        }
        #endregion

        #region Logowanie
        private bool _isLoggedIn;
        public bool IsLoggedIn
        {
            get  => _isLoggedIn;
            set 
            { 
                _isLoggedIn = value;
                OnPropertyChanged(() => IsLoggedIn);
                if (_isLoggedIn)
                    ChangeSideMenuVisibility();
            }
        }

        private LoginViewModel _loginViewModel;
        public LoginViewModel LoginViewModel
        {
            get 
            {
                if (_loginViewModel == null)
                {
                    _loginViewModel = _provider.GetRequiredService<LoginViewModel>();
                    OnPropertyChanged(() => LoginViewModel);
                }
                return _loginViewModel;
            }
        }
        #endregion

        #region Wiadomości
        private string _statusMessage;
        public string StatusMessage
        {
            get
            {
                if (_statusMessage == null)
                {
                    //przydatne przy inicjacji MainWindow
                    _statusMessage = BaseResources.DefaultStatusMessage;
                    OnPropertyChanged(() => StatusMessage);
                }
                return _statusMessage;
            }
            set 
            {
                if (value != null)
                {
                    _statusMessage = value;
                    OnPropertyChanged(() => StatusMessage);
                }
            }
        }
        #endregion

        #region Zegar
        private DispatcherTimer _dispatchTimer;
        private string _timer;
        public string Timer
        {
            get
            {
                if(_timer == null)
                {
                    _dispatchTimer = new DispatcherTimer();
                    _dispatchTimer.Interval = TimeSpan.FromSeconds(1);
                    _dispatchTimer.Tick += Timer_Tick;
                    _dispatchTimer.Start();
                    _timer = DateTime.Now.ToString("dddd, dd MMMM, HH:mm");
                }
                return _timer;
            }
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            _timer = DateTime.Now.ToString("dddd, dd MMMM, HH:mm");
            this.OnPropertyChanged(() => Timer);
        }
        #endregion

        #region O Programie
        public void OProgramie() =>
            MessageBox.Show("Aplikacja do zarządzania placówką oświatową.", "Placówka Oświatowa",
                    MessageBoxButton.OK, MessageBoxImage.Information);
        #endregion

        //*
        //tutaj można albo zaczekać, żeby najpierw załadowały się potrzebne dane:
        //Task.Run(async () => await (workspace as ILoadable).LoadAsync()).GetAwaiter().GetResult();
        //albo w wersji 'FireAndForget' uruchomić i nie czekać, z tym że wtedy wywoływany
        //ViewModel musi zabezpieczyć się przed wykorzystaniem zajętego DbContext'u
        //ew. założyć że funkcja LoadAsync spodziewa się klasy ogólnej repozytorium
        //jako parametru, a w tym miejscu tworzyć osobne ServiceScope'y IPlacowkaRepository,
        //i przekazywać tak utworzone repozytoria do funkcji
    }
}