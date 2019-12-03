using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqliteDemo
{
    class Program
    {
        static void Main(string[] args)
        {

            SQLiteConnection con;
            SQLiteCommand cmd;
            SQLiteDataReader dr;

            if (!File.Exists("MyDatabase.sqlite"))
            {
                SQLiteConnection.CreateFile("MyDatabase.sqlite");

                string sql = @"CREATE TABLE Student(
                               ID INTEGER PRIMARY KEY AUTOINCREMENT ,
                               FirstName           TEXT      NOT NULL,
                               LastName            TEXT       NOT NULL
                            );";
                con = new SQLiteConnection("Data Source=MyDatabase.sqlite;Version=3;");
                con.Open();
                cmd = new SQLiteCommand(sql, con);
                cmd.ExecuteNonQuery();
                con.Close();

            }
            else
            {
                using (var conn = new SQLiteConnection("Data Source=MyDatabase.sqlite;Version=3;"))
                {
                    // Be sure you already created the Person Table!

                    conn.Open();

                    var stopwatch = new Stopwatch();
                    stopwatch.Start();

                    using (var commd = new SQLiteCommand(conn))
                    {
                        using (var transaction = conn.BeginTransaction())
                        {
                            // 100,000 inserts
                            for (var i = 0; i < 1000000; i++)
                            {
                                commd.CommandText = "INSERT INTO Student (FirstName, LastName) VALUES ('John', 'Doe');";
                                commd.ExecuteNonQuery();
                            }

                            transaction.Commit();
                        }
                    }

                    Console.WriteLine("{0} seconds with one transaction.",stopwatch.Elapsed.TotalSeconds);

                    conn.Close();
                }
            }
        }
    }
}
