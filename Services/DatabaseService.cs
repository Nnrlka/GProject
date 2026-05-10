using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using GProject.Data;
using GProject.Models;
using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL;
using Npgsql;


namespace GProject.Services
{
    public class DatabaseService
    {
        private readonly DbContextOptions<FinanceDbContext> _options;

        public DatabaseService()
        {
            const string connectionString = "Host=localhost;Port=5432;Database=FinanceDB;Username=postgres;Password=postgres";
            _options = new DbContextOptionsBuilder<FinanceDbContext>()
                .UseNpgsql(connectionString)
                .Options;
        }

        public async Task<ObservableCollection<Transactions>> GetAllTransactionsAsync()
        {
            await using var db = new FinanceDbContext(_options);
            var list = await db.Transactions
                .AsNoTracking()
                .OrderByDescending(t => t.Date)
                .ToListAsync();

            return new ObservableCollection<Transactions>(list);
        }

        public async Task AddTransactionAsync(Transactions transaction)
        {
            await using var db = new FinanceDbContext(_options);
            db.Transactions.Add(transaction);
            await db.SaveChangesAsync();
        }

        public async Task UpdateTransactionAsync(Transactions transaction)
        {
            await using var db = new FinanceDbContext(_options);
            var existing = await db.Transactions.FirstOrDefaultAsync(t => t.Id == transaction.Id);
            if (existing is null)
            {
                return;
            }
            existing.Type = transaction.Type;
            existing.Description = transaction.Description;
            existing.amount = transaction.amount;
            existing.Date = transaction.Date;
            existing.Category = transaction.Category;

            await db.SaveChangesAsync();
        }
        public async Task DeleteTransactionAsync(int id)
        {
            await using var db = new FinanceDbContext(_options);
            var existing = await db.Transactions.FirstOrDefaultAsync(t => t.Id == id);
            if (existing is null)
            {
                return;
            }

            db.Transactions.Remove(existing);
            await db.SaveChangesAsync();
        }
    }
}
