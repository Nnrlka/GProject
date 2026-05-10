using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using GProject.Models;
using GProject.Services;

namespace GProject.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private readonly DatabaseService _databaseService;

        public MainWindowViewModel()
        {
            _databaseService = new DatabaseService();
            _transactions = new ObservableCollection<Transactions>();
            LoadTransactions();
        }

        private async void LoadTransactions()
        {
            try
            {
                Status = "Loading transactions...";
                Transactions = await _databaseService.GetAllTransactionsAsync();
                Status = $"Loaded transactions: {Transactions.Count}";
            }
            catch (Exception ex)
            {
                Status = $"Ошибка: {ex.Message}";
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}



