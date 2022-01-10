using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BEThomasG.Models
{
    public class Archivo
    {
        [Key]
        public int id { get; set; }

        public string nombredocumento { get; set; }

        public string extension { get; set; }

        public string codigo { get; set; }

        public double tamanio { get; set; }

        public string ubicacion { get; set; }
    }
}

    
    
    
    
    
