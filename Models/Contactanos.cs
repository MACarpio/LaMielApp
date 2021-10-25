using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LaMielApp.Models
{
    [Table("t_contactanos")]
    public class Contactanos
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        [Column("id")]
        public int Id { get; set; }

        [Column("nombre")]
        public string Nombre { get; set; }

        [Column("asunto")]
        public string Asunto { get; set; }

        [Column("telefono")]
        public string Telefono { get; set; }

        [Column("email")]
        public string Email { get; set; }

        [Column("mensaje")]
        public string Mensaje { get; set; }
        
         [NotMapped]
        public String Status {get; set;} 
    }
}