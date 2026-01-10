using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Mezei_Botond_backend.Models;


namespace Mezei_Botond_backend.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<Actor> Actors { get; set; }
        public DbSet<FilmType> FilmTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Actor>(entity =>
            {
                entity.HasKey(e => e.ActorId);
                entity.Property(e => e.ActorName).IsRequired().HasMaxLength(100);
            });

            modelBuilder.Entity<FilmType>(entity =>
            {
                entity.HasKey(e => e.TypeId);
                entity.Property(e => e.TypeName).IsRequired().HasMaxLength(100);
            });

            modelBuilder.Entity<Movie>(entity =>
            {
                entity.HasKey(e => e.MovieId);
                entity.Property(e => e.Title).IsRequired().HasMaxLength(200);
                entity.HasOne(m => m.Actor)
                      .WithMany(a => a.Movies)
                      .HasForeignKey(m => m.ActorId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(m => m.FilmType)
                      .WithMany(f => f.Movies)
                      .HasForeignKey(m => m.FilmTypeId);
            });
        }
    }
}