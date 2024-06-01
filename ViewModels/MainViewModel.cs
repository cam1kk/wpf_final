using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using wpf_final.Commands;
using wpf_final.Models;

namespace wpf_final.ViewModels
{
    public class MainViewModel : ViewModel
    {
        private GuessNumberModel _model;
        private string _userGuess = string.Empty;
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

        public MainViewModel()
        {
            _model = new GuessNumberModel();
            GuessResults = new ObservableCollection<ObservableCollection<GuessResult>>();
            CheckCommand = new DelegateCommand(CheckGuess);
        }

        private void CheckGuess()
        {
            if (UserGuess.Length == 5 && int.TryParse(UserGuess, out _))
            {
                var result = _model.CheckGuess(UserGuess);
                GuessResults.Add(new ObservableCollection<GuessResult>(result));
            }
        }
    }
}
