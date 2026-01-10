using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mezei_Botond_backend.Models
{
    [Table("film_type")]
    public class FilmType
    {
        [Key]
        [Column("type_id")]
        public int TypeId { get; set; }

        [Required]
        [Column("type_name")]
        [StringLength(100)]
        public string TypeName { get; set; } = string.Empty;

        [InverseProperty("FilmType")]
        public virtual ICollection<Movie> Movies { get; set; } = new List<Movie>();

        public FilmType()
        {
            Movies = new HashSet<Movie>();
        }
    }
}