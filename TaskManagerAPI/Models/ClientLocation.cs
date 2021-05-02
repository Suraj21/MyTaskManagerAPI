using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TaskManagerAPI.Models
{
    public class ClientLocation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [JsonConverter(typeof(IntToStringConverter))]
        public int ClientLocationID { get; set; }
        public string ClientLocationName { get; set; }
    }
}
