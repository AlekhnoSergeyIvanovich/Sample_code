using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;

namespace DAL.Configurations
{
    public class FilmConfiguration : EntityTypeConfiguration<Film>
    {
        public FilmConfiguration()
        {
            ToTable("Films")
                .HasKey(p => p.Id);

            Property(p => p.NameFilm)
                .IsRequired()
                .HasColumnName("Name");
        }
    }
}
