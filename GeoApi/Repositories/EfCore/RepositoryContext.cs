using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.EfCore.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;


namespace Repositories.EfCore
{
    public class RepositoryContext : DbContext // EfCore kur
    {

        public RepositoryContext(DbContextOptions<RepositoryContext> options) :
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

            // Enable support for PostGIS
            modelBuilder.HasPostgresExtension("postgis");

             
        }
    }
}
