using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data.Entity;

using Retail.Model;
using RetailUsingEntityFramework.Mapping;

namespace RetailUsingEntityFramework
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
            var result = SaveUsingEF(supplier);
            */

            /*
            var supplier = new Supplier
            {
                SupplierID = 34,
                NamaSupplier = "Morgan Bike AccessoriesA",
                Jalan = "6387 Scenic AvenueB",
                Kota = "BothellC"
            };
            var result = UpdateUsingEF(supplier);
            */

            /*
            var supplier = new Supplier
            {
                SupplierID = 34
            };

            var result = DeleteUsingEF(supplier);
            */

            /*
            var noUrut = 1;

            var daftarSupplier = GetAllUsingEF();
            foreach (var supplier in daftarSupplier)
            {
                Console.WriteLine("{0}. {1}", noUrut, supplier.NamaSupplier);

                noUrut++;
            }
            */

            /*
            var noUrut = 1;

            var daftarSupplier = GetByNameUsingEF("new");
            foreach (var supplier in daftarSupplier)
            {
                Console.WriteLine("{0}. {1}", noUrut, supplier.NamaSupplier);

                noUrut++;
            }
            */

            /*
            var supplier = GetByIDUsingEF(19);
            if (supplier != null)
                Console.WriteLine("{0}", supplier.NamaSupplier);
            */
        }

        private static int SaveUsingEF(Supplier supplier)
        {
            var result = 0;

            using (var db = new RetailContext())
            {
                db.Entry(supplier).State = EntityState.Added;
                db.SaveChanges();

                result = 1;
            }

            return result;
        }

        private static int UpdateUsingEF(Supplier supplier)
        {
            var result = 0;

            using (var db = new RetailContext())
            {
                db.Entry(supplier).State = EntityState.Modified;
                db.SaveChanges();

                result = 1;
            }

            return result;
        }

        private static int DeleteUsingEF(Supplier supplier)
        {
            var result = 0;

            using (var db = new RetailContext())
            {
                db.Entry(supplier).State = EntityState.Deleted;
                db.SaveChanges();

                result = 1;
            }

            return result;
        }

        private static IList<Supplier> GetAllUsingEF()
        {
            var daftarSupplier = new List<Supplier>();

            using (var db = new RetailContext())
            {
                daftarSupplier = db.Suppliers
                                   .OrderBy(s => s.NamaSupplier)
                                   .ToList();
            }

            return daftarSupplier;
        }

        private static IList<Supplier> GetByNameUsingEF(string namaSupplier)
        {
            var daftarSupplier = new List<Supplier>();

            using (var db = new RetailContext())
            {
                daftarSupplier = db.Suppliers
                                   .Where(s => s.NamaSupplier.Contains(namaSupplier))
                                   .OrderBy(s => s.NamaSupplier)
                                   .ToList();
            }

            return daftarSupplier;
        }

        private static Supplier GetByIDUsingEF(int supplierID)
        {
            Supplier supplier = null;

            using (var db = new RetailContext())
            {
                supplier = db.Suppliers
                             .Where(s => s.SupplierID == supplierID)
                             .SingleOrDefault();
            }

            return supplier;
        }
    }
}
