using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GProject.Models;
using GProject.Services;

namespace GProject.ViewModels
{
    public partial class MainWindowViewModel : ObservableObject
    {
        private readonly DatabaseService _databaseService;

        public MainWindowViewModel()
        {
            _databaseService = new DatabaseService();
            _ = LoadTransactionsAsync();
        }

        [ObservableProperty]
        private ObservableCollection<Transactions> transactions = new();

        [ObservableProperty]
        private string status = "Загрузка...";

        [ObservableProperty]
        private string newType = string.Empty;

        [ObservableProperty]
        private string newDescription = string.Empty;

        [ObservableProperty]
        private string newAmountText = string.Empty;

        [ObservableProperty]
        private DateTimeOffset? newDate = DateTimeOffset.Now;

        [ObservableProperty]
        private string newCategory = string.Empty;

        [RelayCommand]
        private async Task LoadTransactionsAsync()
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

        [RelayCommand]
        private async Task AddTransactionAsync()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(NewType))
                {
                    Status = "Type of transactions.";
                    return;
                }

                var normalizedAmount = NewAmountText.Trim().Replace(',', '.');
                if (!decimal.TryParse(normalizedAmount, NumberStyles.Number, CultureInfo.InvariantCulture, out var amount))
                {
                    Status = "Sum must be numberr";
                    return;
                }

                if (NewDate is null)
                {
                    Status = "Choose date";
                    return;
                }

                var transaction = new Transactions
                {
                    Type = NewType.Trim(),
                    Description = NewDescription.Trim(),
                    amount = amount,
                    Date = NewDate.Value.DateTime,
                    Category = string.IsNullOrWhiteSpace(NewCategory) ? string.Empty : NewCategory.Trim()
                };

                await _databaseService.AddTransactionAsync(transaction);
                Status = "Transaction added";
                NewType = string.Empty;
                NewDescription = string.Empty;
                NewAmountText = string.Empty;
                NewDate = DateTimeOffset.Now;
                NewCategory = string.Empty;
                await LoadTransactionsAsync();
            }
            catch (Exception ex)
            {
                Status = $"Problem with adding: {ex.Message}";
            }
        }
    }
}


