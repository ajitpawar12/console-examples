using EFTransctions.Context;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFTransctions
{
    class Program
    {
        static void Main(string[] args)
        {
            var context = new SchoolDBEntities();

           // context.Database.Connection.Close();
            context.Database.Connection.Open();
            var standard = context.Students.Add(new Student() { FirstName = "1st S" });
            context.Students.Add(standard);
            using (System.Data.Common.DbTransaction transaction = context.Database.Connection.BeginTransaction(IsolationLevel.ReadCommitted))
                {
                    try
                    {   
                        context.SaveChanges();
                    // throw exectiopn to test roll back transactionransaction.Commit();
                    transaction.Commit();
                        throw new Exception();
                        
                        //context.SaveChanges();

                        
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        Console.WriteLine("Error occurred.");
                    }
                
            }
        }
    }
}
