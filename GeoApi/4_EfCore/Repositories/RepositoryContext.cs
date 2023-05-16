using _4_EfCore.Models;
using _4_EfCore.Repositories.Config;
using Microsoft.EntityFrameworkCore;

namespace _4_EfCore.Repositories
{
    
        public class RepositoryContext : DbContext // EfCore kur
        {
          
            public RepositoryContext(DbContextOptions options) :
                base(options) // RepositoryContext base'i DbContext yani options içindeki baglantı dizesini  DbContexte ilettik  
            {

            }


            public DbSet<Parcel> Parcels { get; set; }
            public DbSet<Building> Buildings { get; set; }



            // type configuration
            //override - kalıtımla devraldığımız bir metodu geçersiz kılacağız
            // model olusturulurken 

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                modelBuilder.ApplyConfiguration(new ParcelConfig());
                modelBuilder.ApplyConfiguration(new BuildingConfig());
            }
        }
    }
