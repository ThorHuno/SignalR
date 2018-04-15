using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SignalR.Models
{
    [Table("Vuelos")]
    public class Vuelo
    {
        public int Id { get; set; }
        [Column(TypeName ="varchar")]
        [MaxLength(20)]
        [Required]
        public string Codigo { get; set; }
        [Column(TypeName = "varchar")]
        [MaxLength(30)]
        [Required]
        public string Origen { get; set; }
        [Column(TypeName = "varchar")]
        [MaxLength(30)]
        [Required]
        public string Destino { get; set; }
        public bool EsDirecto { get; set; }
        [Column(TypeName = "datetime")]
        [Required]
        public DateTime HoraSalida { get; set; }
        [Column(TypeName = "datetime")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yy}")]
        [Required]
        public DateTime HoraLlegada { get; set; }
    }
}