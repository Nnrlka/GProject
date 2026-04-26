using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Controls.Mixins;
using GProject.Models;
using Npgsql;


namespace GProject.Services
{
    public class DatabaseService
    {
        private readonly string _connectionString;

        public DatabaseService()
        {
            _connectionString = "Host=localhost; Port=5432; Database=FinanceDB; Username=postgres; password=postgres";
        }

        public async Task<ObservableCollection<Transactions>> GetTransactionsAsync()
        {
            var transactions = new ObservableCollection<Transactions>();
            await using var connection = new NpgsqlConnection(_connectionString);
            await connection.OpenAsync();

            var command = new NpgsqlCommand("SELECT id,type,description, amount, date, category FROM transactions ORDER BY date DESC", connection);

            await using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                transactions.Add(new Transactions
                {
                    Id = reader.GetInt32("id"),
                    Type = reader.GetString("type"),
                    Description = reader.GetString("description"),
                    amount = reader.GetDecimal("amount"),
                    Date = reader.GetDateTime("date"),
                    Category = reader.IsDBNull("category") ? string.Empty : reader.GetString("category")
                });
            }

            return transactions;
        }
    }
}
