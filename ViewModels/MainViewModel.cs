using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using wpf_final.Commands;
using wpf_final.Models;
using wpf_final.Views;

namespace wpf_final.ViewModels
{
    public class MainViewModel : ViewModel
    {
        private GuessNumberModel _model;
        private string _userGuess = string.Empty;
        private bool _canClear = false;
        private int _gameMode;
        private int _rbh;
        private int _rbw;
        private Thickness _rbm;
        private int _abh;
        private int _abw;
        private Thickness _abm;

        public int RBH
        {
            get => _rbh;
            set
            {
                _rbh = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(RBH)));
            }
        }
        public int RBW
        {
            get => _rbh;
            set
            {
                _rbw = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(RBW)));
            }
        }
        public int ABH
        {
            get => _abh;
            set
            {
                _abh = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(ABH)));
            }
        }
        public int ABW
        {
            get => _abh;
            set
            {
                _abw = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(ABW)));
            }
        }
        public Thickness RBM
        {
            get => _rbm;
            set
            {
                _rbm = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(RBM)));
            }
        }
        public Thickness ABM
        {
            get => _abm;
            set
            {
                _abm = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(ABM)));
            }
        }

        public ObservableCollection<ObservableCollection<GuessResult>> GuessResults { get; set; }

        public string UserGuess
        {
            get => _userGuess;
            set
            {
                _userGuess = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(UserGuess)));
            }
        }

        public ICommand CheckCommand { get; }
        public ICommand ThreeCommand { get; }
        public ICommand FiveCommand { get; }
        public ICommand SevenCommand { get; }
        public ICommand NineCommand { get; }
        public ICommand RegistrationCommand { get; }
        public ICommand AccountCommand { get; }
        public ICommand LeaderboardsCommand { get; }
        private int attempts = 0;

        private User _user = new User() { Username = string.Empty };

        public MainViewModel(int gameMode, User user)
        {
            _model = new GuessNumberModel(_gameMode);
            _gameMode = gameMode;
            GuessResults = new ObservableCollection<ObservableCollection<GuessResult>>();
            CheckCommand = new DelegateCommand(CheckGuess);
            ThreeCommand = new DelegateCommand(ChangeModeToThree);
            FiveCommand = new DelegateCommand(ChangeModeToFive);
            SevenCommand = new DelegateCommand(ChangeModeToSeven);
            NineCommand = new DelegateCommand(ChangeModeToNine);
            RegistrationCommand = new DelegateCommand(OpenRegistrationView);
            AccountCommand = new DelegateCommand(OpenAccountView);
            LeaderboardsCommand = new DelegateCommand(OpenLeaderboardsView);
            _user = user;
            ABH = 0;
            ABW = 0;
            ABM = new Thickness(0, 0, 0, 0);
            RBH = 40;
            RBW = 40;
            RBM = new Thickness(5, 5, 5, 5);
        }

        private void OpenLeaderboardsView()
        {
            LeaderboardsWindow leaderboardsWindow = new LeaderboardsWindow(_gameMode);
            leaderboardsWindow.ShowDialog();
        }

        private void OpenAccountView()
        {
            PersonalAccountWindow personalAccountWindow = new PersonalAccountWindow(_user, _gameMode);
            personalAccountWindow.ShowDialog();
            if (personalAccountWindow.isLoggedOut)
            {
                _user = new User() { Username = string.Empty };
                ABH = 0;
                ABW = 0;
                ABM = new Thickness(0, 0, 0, 0);
                RBH = 40;
                RBW = 40;
                RBM = new Thickness(5, 5, 5, 5);
            }
        }

        private void OpenRegistrationView()
        {
            RegistrationLoginWindow registrationloginWindow = new RegistrationLoginWindow();
            registrationloginWindow.ShowDialog();
            if (registrationloginWindow.User.Username != string.Empty)
            {
                _user = registrationloginWindow.User;
                ABH = 40;
                ABW = 40;
                ABM = new Thickness(5, 5, 5, 5);
                RBH = 0;
                RBW = 0;
                RBM = new Thickness(0, 0, 0, 0);
            }
        }

        private void CheckGuess()
        {
            if (UserGuess.Length == _gameMode && int.TryParse(UserGuess, out _))
            {
                UserGuess = string.Empty;
                if (_canClear)
                {
                    Clear();
                    _canClear = false;
                }
                var result = _model.CheckGuess(UserGuess);
                attempts++;
                GuessResults.Add(new ObservableCollection<GuessResult>(result));
                if (result.All(g => g.Color == "Green"))
                {
                    var victory = new List<GuessResult>()
                    {
                        new GuessResult(" V ", "Green"),
                        new GuessResult(" I ", "Green"),
                        new GuessResult(" C ", "Green"),
                        new GuessResult(" T ", "Green"),
                        new GuessResult(" O ", "Green"),
                        new GuessResult(" R ", "Green"),
                        new GuessResult(" Y ", "Green")
                    };
                    GuessResults.Add(new ObservableCollection<GuessResult>(victory));
                    _canClear = true;
                    if (_user.Username != string.Empty)
                    {
                        if (10000 / attempts > _user.ModeScore[_gameMode])
                        {
                            _user.ModeScore[_gameMode] = 12345 / attempts;
                        }
                        var database = new UsersDatabase();
                        database.AddUser(_user);
                    }
                }
            }
        }

        public void Clear()
        {
            _model.Update(_gameMode);
            attempts = 0;
            GuessResults.Clear();
        }

        private void ChangeModeToThree()
        {
            _gameMode = 3;
            UserGuess = string.Empty;
            Clear();

        }
        private void ChangeModeToFive()
        {
            _gameMode = 5;
            UserGuess = string.Empty;
            Clear();
        }
        private void ChangeModeToSeven()
        {
            _gameMode = 7;
            UserGuess = string.Empty;
            Clear();
        }
        private void ChangeModeToNine()
        {
            _gameMode = 9;
            UserGuess = string.Empty;
            Clear();
        }
    }
}
