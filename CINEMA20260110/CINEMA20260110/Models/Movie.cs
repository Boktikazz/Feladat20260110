using Mezei_Botond_backend.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Mezei_Botond_backend.Models
{
    [Table("movies")]
    public class Movie
    {
        [Key]
        [Column("movie_id")]
        public int MovieId { get; set; }

        [Required]
        [Column("title")]
        [StringLength(200)]
        public string Title { get; set; } = string.Empty;

        [Column("release_date", TypeName = "date")]
        public DateTime? ReleaseDate { get; set; }

        [Column("actor_id")]
        public int? ActorId { get; set; }

        [Column("film_type_id")]
        public int? FilmTypeId { get; set; }

        [ForeignKey("ActorId")]
        [InverseProperty("Movies")]
        [JsonIgnore] 
        public virtual Actor? Actor { get; set; }

        [ForeignKey("FilmTypeId")]
        [InverseProperty("Movies")]
        [JsonIgnore]
        public virtual FilmType? FilmType { get; set; }
    }
}