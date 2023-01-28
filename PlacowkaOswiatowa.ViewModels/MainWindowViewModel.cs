using Microsoft.Extensions.DependencyInjection;
using PlacowkaOswiatowa.Domain.Commands;
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
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Threading;

namespace PlacowkaOswiatowa.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        #region Konstruktor
        private IServiceProvider _provider;
        private readonly ISignalHub<string> _signal;
        private readonly ISignalHub<ViewHandler> _signalView;

        public MainWindowViewModel(IServiceProvider serviceProvider)
        {
            _provider = serviceProvider;
            _signal = SignalHub<string>.Instance;
            _signalView = SignalHub<ViewHandler>.Instance;
            _sideMenuVisibility = "Collapsed";
            _sideMenuLocation = "Left";
            _workspacesVisibility = "Collapsed";
            _loginViewVisibility = "Collapsed";
            //wyświetlanie panelu logowania
            _isLoggedIn = true;
            _signal.NewMessage += (s, m) => StatusMessage = m;
            _signal.LoggedInChanged += () => IsLoggedIn = true;
            _signal.HideLogingRequest += () => ChangeLoginViewVisibility();
            _signalView.NewViewRequested += (s, vh) => OnNewEditViewRequested(s, vh);
        }

        #endregion

        #region Komendy Menu i Paska narzędzi
        //TODO: dodać prywatne pola ICommand dla każdej komendy, żeby niepotrzebnie nie
        //inicjować komendy jeżeli jest wywoływana ponownie
        public ICommand UrlopPracownikaCommand =>
            new BaseCommand(CreateViewAsync<UrlopPracownikaViewModel>);
        
        public ICommand NowyPracownikCommand => 
            new BaseCommand(CreateView<NowyPracownikViewModel>);
        
        public ICommand WszyscyPracownicyCommand => 
            new BaseCommand(ShowSingletonAsync<WszyscyPracownicyViewModel>);

        public ICommand ZarobkiPracownikaCommand =>
            new BaseCommand(CreateViewAsync<ZarobkiPracownikaViewModel>);

        public ICommand NowyUczenCommand => 
            new BaseCommand(CreateViewAsync<NowyUczenViewModel>);
        
        public ICommand WszyscyUczniowieCommand => 
            new BaseCommand(ShowSingletonAsync<WszyscyUczniowieViewModel>);

        public ICommand WszystkieAdresyCommand =>
            new BaseCommand(ShowSingletonAsync<WszystkieAdresyViewModel>);

        public ICommand WszystkiePrzedmiotyCommand =>
            new BaseCommand(ShowSingletonAsync<WszystkiePrzedmiotyViewModel>);

        public ICommand WszystkieOcenyCommand =>
            new BaseCommand(ShowSingletonAsync<WszystkieOcenyViewModel>);

        public ICommand WszystkieUrlopyCommand =>
            new BaseCommand(ShowSingletonAsync<WszystkieUrlopyViewModel>);

        public ICommand NowaUmowaCommand =>
            new BaseCommand(CreateViewAsync<NowaUmowaViewModel>);

        public ICommand WszystkieUmowyCommand =>
            new BaseCommand(ShowSingletonAsync<WszystkieUmowyViewModel>);

        public ICommand ChangeSideMenuVisibilityCommand => 
            new BaseCommand(() => ChangeSideMenuVisibility());
        
        public ICommand ChangeSideMenuLocationCommand => 
            new BaseCommand(() => ChangeSideMenuLocation());
        
        public ICommand ZamknijCommand => 
            new BaseCommand(() => Zamknij());
        
        public ICommand OProgramieCommand => 
            new BaseCommand(() => OProgramie());
        
        public ICommand WidokZalogujCommand => 
            new BaseCommand(() => ChangeLoginViewVisibility());

        #endregion

        #region Przyciski w menu z lewej strony
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
                new CommandViewModel(BaseResources.NowyPracownik, 
                    new BaseCommand(() => CreateView<NowyPracownikViewModel>())),
                new CommandViewModel(BaseResources.WszyscyPracownicy, 
                    new BaseCommand(ShowSingletonAsync<WszyscyPracownicyViewModel>)),
                new CommandViewModel(BaseResources.NowyUczen, 
                    new BaseCommand(CreateViewAsync<NowyUczenViewModel>)),
                new CommandViewModel(BaseResources.WszyscyUczniowie,
                    new BaseCommand(ShowSingletonAsync<WszyscyUczniowieViewModel>))
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
                    workspace.RequestCreateView += this.OnWorkspaceRequestCreateView;
                }
            if (e.OldItems != null && e.OldItems.Count != 0)
                foreach (WorkspaceViewModel workspace in e.OldItems)
                {
                    workspace.RequestClose -= this.OnWorkspaceRequestClose;
                    workspace.RequestCreateView -= this.OnWorkspaceRequestCreateView;
                }
        }

        //MainWindow nasłuchuje zdarzenia RequestCreateView
        //i na podstawie zadanego typu tworzy nową zakładkę
        private void OnWorkspaceRequestCreateView(object? sender, Type type)
        {
            try
            {
                _ = type ?? throw new ArgumentNullException(
                        "Nie podano typu zakładki do utworzenia");
                //Pobranie nazwy metody do wywołania (alternatywa do nameof())
                Expression<Action> create = () => CreateView<WorkspaceViewModel>();
                //tutaj z racji na to żeby pobrać nazwę metody generycznej, której paramatrem
                //jest klasa dziedzicząca po WorkspaceViewModel i implementująca ILoadable
                //przykładowo podaję WszyscyPracownicyViewModel
                Expression<Action> createAsync = () => CreateViewAsync<WszyscyPracownicyViewModel>();
                var action = type.IsAssignableTo(typeof(ILoadable)) ? createAsync : create;
                var methodName = (action.Body as MethodCallExpression).Method.Name;
                //Pobranie metody i wywołanie
                MethodInfo method = this.GetType().GetMethod(methodName,
                    BindingFlags.NonPublic | BindingFlags.Instance);
                MethodInfo genericMethod = method.MakeGenericMethod(type);
                genericMethod.Invoke(this, null);
            }
            catch(Exception e)
            {
                MessageBox.Show($"Nie udało się utworzyć widoku. {e.Message}",
                    "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
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

        //Wywyołanie widoku edycji rekordu
        private void OnNewEditViewRequested(object sender, ViewHandler viewHandler)
        {
            try
            {
                _ = viewHandler ?? throw new ArgumentNullException("Nie podano wymaganego argumentu");
                _ = viewHandler.Value ?? throw new ArgumentNullException("Wybrano rekordu do edycji");
                if (!viewHandler.ViewType.IsAssignableTo(typeof(IEditable)))
                    throw new ArgumentException("Żądany widok nie służy do edycji");

                var workspace = _provider.GetRequiredService(viewHandler.ViewType) as WorkspaceViewModel;
                if (viewHandler.ViewType.IsAssignableTo(typeof(ILoadable)))
                {
                    //tutaj można albo zaczekać, żeby najpierw załadowały się potrzebne dane:
                    //Task.Run(async () => await (workspace as ILoadable).LoadAsync()).GetAwaiter().GetResult();
                    //albo w wersji 'FireAndForget' uruchomić i nie czekać, z tym że wtedy wywoływany
                    //ViewModel musi zabezpieczyć się przed wykorzystaniem zajętego DbContext'u
                    //ew. założyć że funkcja LoadAsync spodziewa się klasy ogólnej repozytorium
                    //jako parametru, a w tym miejscu tworzyć osobne ServiceScope'y IPlacowkaRepository,
                    //i przekazywać tak utworzone repozytoria do funkcji
                    Task.Run(async () => await (workspace as ILoadable).LoadAsync());
                }

                //następnie pobrany zostanie rekord do edycji
                Task.Run(async () => await (workspace as IEditable).LoadItem(viewHandler.Value));
                Workspaces.Add(workspace);
                SetActiveWorkspace(workspace);
            }
            catch(Exception e)
            {
                MessageBox.Show($"Nie udało się utworzyć widoku do edycji. {e.Message}",
                    "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
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
        private void ShowSingletonAsync<T>() 
            where T : WorkspaceViewModel, ILoadable
        {
            var workspace = this.Workspaces.FirstOrDefault(vm => vm is T) as T;
            if(workspace == null)
                //workspace = Activator.CreateInstance(typeof(T)) as T;
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
                //workspace = Activator.CreateInstance(typeof(T)) as T;
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
        /// Pobiaranie następuje asynchronicznie
        /// </summary>
        /// <typeparam name="T"></typeparam>
        private void CreateViewAsync<T>()
            where T : WorkspaceViewModel, ILoadable
        {
            //Provider tworzy ViewModel z kontenara zależności
            var workspace = _provider.GetRequiredService<T>();
            //Po załadowaniu powiadomi UI że dane są dostępne.
            //W przypadku niepowodzenia metoda LoadAsync wyświetli odpowiedni komunikat
            Task.Run(async () => await workspace.LoadAsync());

            Workspaces.Add(workspace);
            SetActiveWorkspace(workspace);
        }

        private void Zamknij()
        {
            Application.Current.Windows[0].Close();
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
    }
}