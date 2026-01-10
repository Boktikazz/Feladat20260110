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
    [Table("actors")]
    public class Actor
    {
        [Key]
        [Column("actor_id")]
        public int ActorId { get; set; }

        [Required]
        [Column("actor_name")]
        [StringLength(100)]
        public string ActorName { get; set; } = string.Empty;

        [InverseProperty("Actor")]
        public virtual ICollection<Movie> Movies { get; set; } = new List<Movie>();

        public Actor()
        {
            Movies = new HashSet<Movie>();
        }
    }
}