using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using System.Data.SqlClient;
using Retail.Model;

namespace RetailUsingADONET
{
    class Program
    {        
        static void Main(string[] args)
        {
            /*
            var supplier = new Supplier
            {
                NamaSupplier = "Morgan Bike Accessories",
                Jalan = "6387 Scenic Avenue",
                Kota = "Bothell"
            };
            var result = SaveUsingADONET(supplier);
            */

            /*
            var supplier = new Supplier
            {
                SupplierID = 34,
                NamaSupplier = "Morgan Bike AccessoriesA",
                Jalan = "6387 Scenic AvenueB",
                Kota = "BothellC"
            };
            var result = UpdateUsingADONET(supplier);
            */

            /*
            var supplier = new Supplier
            {
                SupplierID = 34
            };

            var result = DeleteUsingADONET(supplier);
            */

            /*
            var noUrut = 1;

            var daftarSupplier = GetAllUsingADONET();
            foreach (var supplier in daftarSupplier)
            {
                Console.WriteLine("{0}. {1}", noUrut, supplier.NamaSupplier);

                noUrut++;
            }
            */

            /*
            var noUrut = 1;

            var daftarSupplier = GetByNameUsingADONET("new");
            foreach (var supplier in daftarSupplier)
            {
                Console.WriteLine("{0}. {1}", noUrut, supplier.NamaSupplier);

                noUrut++;
            }
            */

            /*
            var supplier = GetByIDUsingADONET(19);
            if (supplier != null)
                Console.WriteLine("{0}", supplier.NamaSupplier);
            */
        }

        private static int SaveUsingADONET(Supplier supplier)
        {
            var result = 0;            

            using (var conn = GetOpenConnection())
            {
                var sql = @"INSERT INTO Supplier (NamaSupplier, Jalan, Kota)
                            VALUES (@1, @2, @3)";

                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@1", supplier.NamaSupplier);
                    cmd.Parameters.AddWithValue("@2", supplier.Jalan);
                    cmd.Parameters.AddWithValue("@3", supplier.Kota);

                    result = cmd.ExecuteNonQuery();
                }
            }

            return result;
        }

        private static int UpdateUsingADONET(Supplier supplier)
        {
            var result = 0;

            using (var conn = GetOpenConnection())
            {
                var sql = @"UPDATE Supplier SET NamaSupplier = @1, Jalan = @2, Kota = @3
                            WHERE SupplierID = @4";

                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@1", supplier.NamaSupplier);
                    cmd.Parameters.AddWithValue("@2", supplier.Jalan);
                    cmd.Parameters.AddWithValue("@3", supplier.Kota);
                    cmd.Parameters.AddWithValue("@4", supplier.SupplierID);

                    result = cmd.ExecuteNonQuery();
                }
            }

            return result;
        }

        private static int DeleteUsingADONET(Supplier supplier)
        {
            var result = 0;

            using (var conn = GetOpenConnection())
            {
                var sql = @"DELETE FROM Supplier
                            WHERE SupplierID = @1";

                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@1", supplier.SupplierID);

                    result = cmd.ExecuteNonQuery();
                }
            }

            return result;
        }

        private static IList<Supplier> GetAllUsingADONET()
        {
            var daftarSupplier = new List<Supplier>();

            using (var conn = GetOpenConnection())
            {
                var sql = @"SELECT SupplierID, NamaSupplier, Jalan, Kota 
                            FROM Supplier
                            ORDER BY  NamaSupplier";

                using (var cmd = new SqlCommand(sql, conn))
                {
                    using (var dtr = cmd.ExecuteReader())
                    {
                        while (dtr.Read())
                        {
                            var supplier = new Supplier
                            {
                                SupplierID = dtr["SupplierID"] is DBNull ? 0 : (int)dtr["SupplierID"],
                                NamaSupplier = dtr["NamaSupplier"] is DBNull ? string.Empty : (string)dtr["NamaSupplier"],
                                Jalan = dtr["Jalan"] is DBNull ? string.Empty : (string)dtr["Jalan"],
                                Kota = dtr["Kota"] is DBNull ? string.Empty : (string)dtr["Kota"]
                            };

                            daftarSupplier.Add(supplier);
                        }
                    }
                }
            }

            return daftarSupplier;
        }

        private static IList<Supplier> GetByNameUsingADONET(string namaSupplier)
        {
            var daftarSupplier = new List<Supplier>();

            using (var conn = GetOpenConnection())
            {
                var sql = @"SELECT SupplierID, NamaSupplier, Jalan, Kota 
                            FROM Supplier
                            WHERE NamaSupplier LIKE @1
                            ORDER BY  NamaSupplier";

                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@1", "%" + namaSupplier + "%");

                    using (var dtr = cmd.ExecuteReader())
                    {
                        while (dtr.Read())
                        {
                            var supplier = new Supplier
                            {
                                SupplierID = dtr["SupplierID"] is DBNull ? 0 : (int)dtr["SupplierID"],
                                NamaSupplier = dtr["NamaSupplier"] is DBNull ? string.Empty : (string)dtr["NamaSupplier"],
                                Jalan = dtr["Jalan"] is DBNull ? string.Empty : (string)dtr["Jalan"],
                                Kota = dtr["Kota"] is DBNull ? string.Empty : (string)dtr["Kota"]
                            };

                            daftarSupplier.Add(supplier);
                        }
                    }
                }
            }

            return daftarSupplier;
        }

        private static Supplier GetByIDUsingADONET(int supplierID)
        {
            Supplier supplier = null;

            using (var conn = GetOpenConnection())
            {
                var sql = @"SELECT SupplierID, NamaSupplier, Jalan, Kota 
                            FROM Supplier
                            WHERE SupplierID = @1";

                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@1", supplierID);

                    using (var dtr = cmd.ExecuteReader())
                    {
                        if (dtr.Read())
                        {
                            supplier = new Supplier();
                            supplier.SupplierID = dtr["SupplierID"] is DBNull ? 0 : (int)dtr["SupplierID"];
                            supplier.NamaSupplier = dtr["NamaSupplier"] is DBNull ? string.Empty : (string)dtr["NamaSupplier"];
                            supplier.Jalan = dtr["Jalan"] is DBNull ? string.Empty : (string)dtr["Jalan"];
                            supplier.Kota = dtr["Kota"] is DBNull ? string.Empty : (string)dtr["Kota"];

                        }
                    }
                }
            }

            return supplier;
        }

        private static SqlConnection GetOpenConnection()
        {
            SqlConnection conn = null;

            try
            {
                var strConn = @"Data Source=.\sqlexpress2008;Initial Catalog=Retail;Integrated Security=True";

                conn = new SqlConnection(strConn);                
                conn.Open();

            }
            catch (Exception)
            {
            }

            return conn;
        }
    }
}
