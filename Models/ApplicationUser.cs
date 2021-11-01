using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace LaMielApp.Models
{
    public class ApplicationUser : IdentityUser
    {
        public String Nombres { get; set; }
        public String Apellidos { get; set; }
        public DateTime? FechaNac { get; set; }
    }
}