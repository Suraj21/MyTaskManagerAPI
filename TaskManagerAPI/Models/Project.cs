using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TaskManagerAPI.Models
{
    public class Project
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ProjectID { get; set; }
        public string ProjectName { get; set; }
        public string DateOfStart { get; set; }
        public int TeamSize { get; set; }
        public bool Active { get; set; }

        public string Status { get; set; }

        //[JsonConverter(typeof(IntToStringConverter))]
        public int ClientLocationID { get; set; }

        [ForeignKey("ClientLocationID")]
        public virtual ClientLocation ClientLocation { get; set; }
    }

    public class ProjectDto
    {
        public string ProjectName { get; set; }
        public string DateOfStart { get; set; }
        public int TeamSize { get; set; }
        public bool Active { get; set; }
        public string Status { get; set; }
        public int ClientLocationID { get; set; }
    }

    //public class TaskManagerDbContext : DbContext
    //{
    //    public DbSet<Project> Projects {get; set;}

    //    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //    {
    //        base.OnConfiguring(optionsBuilder);
    //        optionsBuilder.UseSqlServer("server=.;database=TaskManager;trusted_connection=true;");
    //    }
    //}
}
