using Npgsql;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQL_Abfrage_Beispiel
{
    class Program
    {
        static void Main(string[] args)
        {


            PostGres();

            Console.ReadLine();
        }

        public static void PostGres()
        {
            var l = new List<string>();

            using (var conn = new NpgsqlConnection($@"Host=192.168.178.20;Username=florian;Password=time2go;Database=TestDB"))
            using (var cmd = new NpgsqlCommand("SELECT * FROM TestTable"))
            {
                conn.Open();
                cmd.Connection = conn;

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        l.Add(reader.GetString(reader.GetOrdinal("name")));
                    }
                }
            }

            foreach (var item in l)
            {
                Console.WriteLine(item);
            }
        }

        public static void Sql()
        {

            var l = new List<string>();

            using (var con = new SqlConnection(@"Data Source = (localdb)\MSSQLLocalDB; Initial Catalog = TestDB; Integrated Security = True"))
            using (var com = new SqlCommand(@"Select * from Personen", con))
            {
                con.Open();

                using (var reader = com.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            l.Add(reader.GetString(reader.GetOrdinal("Name")));


                            //falls mal was null sein kann die spalte prüfen auf null!
                            //int middleNameIndex = reader.GetOrdinal("MiddleName");
                            //// If a column is nullable always check for DBNull...
                            //if (!reader.IsDBNull(middleNameIndex))
                            //{
                            //    p.MiddleName = reader.GetString(middleNameIndex);
                            //}
                        }
                    }
                }
            }

            l.ForEach(m => Console.WriteLine(m));
        }
    }
}
