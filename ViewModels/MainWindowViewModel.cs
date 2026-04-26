using System.Collections.ObjectModel;
using System.ComponentModel;
using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using GProject.Models;
using GProject.Services;
using Avalonia.Markup.Xaml.MarkupExtensions;
using System.Security.Cryptography.X509Certificates;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;

namespace GProject.ViewModels
{
    public partial class MainWindowViewModel : INotifyPropertyChanged
    {
        private readonly DatabaseService _databaseService;
        private ObservableCollection<Transactions> _transactions;
        private string _status = "Загрузка...";

        public ObservableCollection<Transactions> Transactions
        {
            get => _transactions;
            set
            {
                _transactions = value;
                OnPropertyChanged();
            }
        }

        public string Status
        {
            get => _status;
            set
            {
                _status = value;
                OnPropertyChanged();
            }
        }

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
                Status = "Загрузка транзакций...";
                Transactions = await _databaseService.GetTransactionsAsync();
                Status = $"Загруженно {Transactions.Count} транзакций";
            }
            catch (Exception ex)
            {
                Status = $"Ошибка {ex.Message}";
            }
        }

            public event PropertyChangedEventHandler? PropertyChanged;
            
            protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
                
        }


    }

