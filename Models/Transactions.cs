using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GProject.Models
{
    internal class Transactions
    {
        public int Id { get;set; }
        public string Type { get;set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal amount { get;set; }
        public DateTime Date { get;set; }
        public string Category { get;set; } = string.Empty;

    }
}
