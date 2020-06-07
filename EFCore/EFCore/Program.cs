using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.Data.SqlClient;

namespace EFCore
{
    class Program
    {
        static void Main(string[] args)
        {

            var builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile("appsettings.json");
            var config = builder.Build();
            string connectionString = config.GetConnectionString("DefaultConnection");
            var optionsBuilder = new DbContextOptionsBuilder<ApplicatContext>();
            var options = optionsBuilder
                .UseMySql(connectionString)
                .Options;

             using (ApplicatContext db = new ApplicatContext(options))
             {
                // Create
                Console.WriteLine("Добавление нового тестера");
                db.Testers.Add(new Tester { Name = "Ivanov Ivan" }); //Введите имя, которое хотите добавить
                db.SaveChanges();

                // Update
                Console.WriteLine("Обновление");
                int id = 7; //Введите id тестера, чье имя хотите обновить
                Tester new_tester = db.Testers.Find(id); 
                //Tester new_tester = db.Testers.FirstOrDefault(); //Обновление по первому id
                if (new_tester != null)
                {
                    new_tester.Name = "Socolov Kirill"; //Введите новое имя тестера
                    db.Testers.Update(new_tester);
                    db.SaveChanges();
                }

                // Delete
                Console.WriteLine("Удаление");
                int id_delete = 6; //Введите id тестера, которого хотите удалить
                Tester tester = db.Testers.Find(id_delete);
                //Tester tester = db.Testers.FirstOrDefault(); //Удаление по перому id
                if (tester != null)
                {
                    db.Remove(tester);
                    db.SaveChanges();
                }
            }
        }
    }
}
