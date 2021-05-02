using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TaskManagerAPI.Identity;

namespace TaskManagerAPI.Models
{
    public class Skill
    {
        [Key]
        public int SkillID { get; set; }
        public string SkillName { get; set; }
        public string SkillLevel { get; set; }
        public string Id { get; set; }

        [ForeignKey("Id")]
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
