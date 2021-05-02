using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace TaskManagerAPI.Models
{
    public class Country
    {
        [Key]
        public int CountryID { get; set; }
        public string CountryName { get; set; }
    }
}
