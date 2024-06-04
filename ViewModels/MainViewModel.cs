using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using System.Windows.Media;
using wpf_final.Commands;
using wpf_final.Models;

namespace wpf_final.ViewModels
{
    public class MainViewModel : ViewModel
    {
        private GuessNumberModel _model;
        private string _userGuess = string.Empty;
        private bool _canClear = false;
        private int _gameMode;
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
        private int attempts = 0;

        public User User { get; set; } = new User() { Username = string.Empty};

        public MainViewModel(int gameMode)
        {
            _model = new GuessNumberModel(_gameMode);
            _gameMode = gameMode;
            GuessResults = new ObservableCollection<ObservableCollection<GuessResult>>();
            CheckCommand = new DelegateCommand(CheckGuess);
            ThreeCommand = new DelegateCommand(ChangeModeToThree);
            FiveCommand = new DelegateCommand(ChangeModeToFive);
            SevenCommand = new DelegateCommand(ChangeModeToSeven);
            NineCommand = new DelegateCommand(ChangeModeToNine);
            this.User = User;
        }

        private void CheckGuess()
        {
            if (UserGuess.Length == _gameMode && int.TryParse(UserGuess, out _))
            {
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
                    if (User.Username != string.Empty)
                    {
                        if (10000 / attempts > User.ModeScore[_gameMode])
                        {
                            User.ModeScore[_gameMode] = 10000 / attempts;
                        }
                        var database = new UsersDatabase();
                        database.AddUser(User);
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
            Clear();

        }
        private void ChangeModeToFive()
        {
            _gameMode = 5;
            Clear();
        }
        private void ChangeModeToSeven()
        {
            _gameMode = 7;
            Clear();
        }
        private void ChangeModeToNine()
        {
            _gameMode = 9;
            Clear();
        }
    }
}
