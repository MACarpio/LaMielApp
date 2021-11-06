using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LaMielApp.Models
{
    [Table("t_order_detail")]
    public class DetallePedido
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int ID {get; set;}
        public Product Producto {get; set;}
        public int Quantity{get; set;}
        public Decimal Price { get; set; }
        public Pedido pedido {get; set;}
    }
}