using NonFraud.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NonFraud.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            TransactionRepo repo = new TransactionRepo();
            var data = repo.GetTransactions();          
            Console.ReadLine();
        }
    }
}
