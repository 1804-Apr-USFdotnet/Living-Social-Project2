using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateAccountDb
{
    public class Program
    {
        static void Main(string[] args)
        {
            Account account = new Account();
            

            Console.WriteLine("Creating DB...............");
            DataDbContext accountDb = new DataDbContext();

           
            Console.WriteLine("Db Created..............");

            account.Email = "test@email.com";
            account.Password = "testpassword";

            accountDb.Accounts.Add(account);
            accountDb.SaveChanges();
            Console.WriteLine("Db Changes Saved..............");
        }
    }
}
