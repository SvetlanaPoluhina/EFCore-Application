using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFCore
{
    public class ApplicatContext : DbContext
    {
        public DbSet<Tester> Testers { get; set; }

        public ApplicatContext(DbContextOptions<ApplicatContext> options)
            :base(options)
        {
            Database.EnsureCreated();
        }
        
       /* protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(ConfigurationManager.ConnectionStrings["SystemManagerTestsDatabase"].ConnectionString);
        } */
    }

    [Table("tester")]
    public class Tester
    {
        [Column("id_tester")]
        public int Id { get; set; }
        [Column("name_tester")]
        public string Name { get; set; }
    }

}
