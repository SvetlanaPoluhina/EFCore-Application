using System;
using System.Linq;
using Microsoft.Extensions.Configuration;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Xunit;
using System.Configuration;
using System.ComponentModel.DataAnnotations.Schema;
//using EFCore;


namespace UnitTestsProject
{
    using EFCore;
    using System.Security.Cryptography;

    public class ProgramTests
    {
        [Fact]
        public void CreateTester()
        {

            var builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory());
            //конфигурационный файл appsettings.json находится в EFCore/UnitTestsProject/bin/Debug/netcoreapp3.1
            builder.AddJsonFile("appsettings.json");
            var config = builder.Build();
            string connectionString = config.GetConnectionString("DefaultConnection");
            var optionsBuilder = new DbContextOptionsBuilder<ApplicatContext>();
            var options = optionsBuilder
                .UseMySql(connectionString)
                .Options;

            using (ApplicatContext db = new ApplicatContext(options))
            {
                db.Testers.Add(new Tester { Name = "Lisov Fedor" }); 
                db.SaveChanges();
         

                int last = (from m in db.Testers select m.Id).ToList().Last();
                Tester new_tester = db.Testers.Find(last);

                string actual = new_tester.Name;
                string expected = "Lisov Fedor";
                
                Assert.Equal(expected, actual);
            }
           
        }

        [Fact]
        public void UpdateTester()
        {

            var builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory());
            //конфигурационный файл appsettings.json находится в EFCore/UnitTestsProject/bin/Debug/netcoreapp3.1
            builder.AddJsonFile("appsettings.json");
            var config = builder.Build();
            string connectionString = config.GetConnectionString("DefaultConnection");
            var optionsBuilder = new DbContextOptionsBuilder<ApplicatContext>();
            var options = optionsBuilder
                .UseMySql(connectionString)
                .Options;

            using (ApplicatContext db = new ApplicatContext(options))
            {
                int id = 7;
                Tester new_tester = db.Testers.Find(id);
                if (new_tester != null)
                {
                    new_tester.Name = "Socolov Petr";
                    db.Testers.Update(new_tester);
                    db.SaveChanges();
                }

                string actual = new_tester.Name;
                string expected = "Socolov Petr";

                Assert.Equal(expected, actual);
            }

        }

        [Fact]
        public void DeleteTester()
        {

            var builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory());
            //конфигурационный файл appsettings.json находится в EFCore/UnitTestsProject/bin/Debug/netcoreapp3.1
            builder.AddJsonFile("appsettings.json");
            var config = builder.Build();
            string connectionString = config.GetConnectionString("DefaultConnection");
            var optionsBuilder = new DbContextOptionsBuilder<ApplicatContext>();
            var options = optionsBuilder
                .UseMySql(connectionString)
                .Options;

            using (ApplicatContext db = new ApplicatContext(options))
            {
                int id_delete = 6; 
                Tester tester = db.Testers.Find(id_delete);
                
                if (tester != null)
                {
                    db.Remove(tester);
                    db.SaveChanges();
                }

                Tester actual = tester;
                Tester expected = null;

                Assert.Equal(expected, actual);
            }

        }


    }
}
