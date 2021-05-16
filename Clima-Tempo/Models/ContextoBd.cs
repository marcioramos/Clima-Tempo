using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace Clima_Tempo.Models
{
    public partial class ContextoBd : DbContext
    {
        public ContextoBd()
            : base("name=ContextoBd")
        {
        }

        public virtual DbSet<Cidade> Cidade { get; set; }
        public virtual DbSet<Estado> Estado { get; set; }
        public virtual DbSet<PrevisaoClima> PrevisaoClima { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cidade>()
                .HasMany(e => e.PrevisaoClima)
                .WithRequired(e => e.Cidade)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Estado>()
                .HasMany(e => e.Cidade)
                .WithRequired(e => e.Estado)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PrevisaoClima>()
                .Property(e => e.TemperaturaMinima)
                .HasPrecision(3, 1);

            modelBuilder.Entity<PrevisaoClima>()
                .Property(e => e.TemperaturaMaxima)
                .HasPrecision(3, 1);
        }
    }
}
