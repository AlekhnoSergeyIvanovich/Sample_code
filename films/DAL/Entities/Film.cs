using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    public class Film
    {
        [Key]
        public int Id { get; set; }
        [DataType(DataType.Text)]
        [StringLength(25, MinimumLength = 3)]
        [Required]
        [Index("UniqueNameFilmIndex", IsUnique = true)]
        public string NameFilm { get; set; }
        [Required]
        public int Year { get; set; }
        [DataType(DataType.Text)]
        [StringLength(30, MinimumLength = 3)]
        [Required]
        public string Producer { get; set; }
        [DataType(DataType.ImageUrl)]
        [Display(Name = "Image")]
        public byte[] BImage { get; set; }
        public string IdUser { get; set; }
    }
}