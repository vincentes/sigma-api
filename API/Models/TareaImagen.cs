using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    public class TareaImagen
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int TareaId { get; set; }

        public int ImagenId { get; set; }
        [ForeignKey("TareaId")]
        public  Tarea Tarea { get; set; }
        [ForeignKey("ImagenId")]
        public Imagen Imagen { get; set; }
    }
}