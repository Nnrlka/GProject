using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
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
    }
}
