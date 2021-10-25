using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LaMielApp.Models
{
    [Table("t_proforma")]
    public class Proforma
    {       
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }
        public String UserID {get; set;}
        public Product Producto {get; set;}
        public int Quantity{get; set;}
        public Decimal Price { get; set; }
    }
}