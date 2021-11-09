using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace LaMielApp.Models
{
    //CARRITO
    [Table("t_pago")]
    public class Pago
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }
        public String Nombres { get; set; }
        public String Apellidos { get; set; }
        public String Direccion { get; set; }
        public DateTime PaymentDate { get; set; }
        public String NombreTarjeta { get; set; }
        [NotMapped]
        public String NumeroTarjeta { get; set; }
        [NotMapped]
        public String DueDateYYMM { get; set; }
        [NotMapped]
        public String Cvv { get; set; }
        public Decimal MontoTotal { get; set; }
        public String UserID { get; set; }
        public DateTime FechaEntrega { get; set; }

    }
}