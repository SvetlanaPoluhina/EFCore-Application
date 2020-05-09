using System;
using System.Linq;

namespace EFCore
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new ApplicatContext())
            {
                // Create
                Console.WriteLine("Добавление нового тестера");
                db.Add(new Tester { Name = "Ivanov Ivan" }); //Введите имя, которое хотите добавить
                db.SaveChanges();

                // Update
                Console.WriteLine("Обновление");
                int id = 3; //Введите id тестера, чье имя хотите обновить
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
                if (new_tester != null)
                {
                    db.Remove(tester);
                    db.SaveChanges();
                }
            }
        }
    }
}
