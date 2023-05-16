using _4_EfCore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Security.Cryptography;

namespace _4_EfCore.Repositories.Config
{
    public class ParcelConfig : IEntityTypeConfiguration<Parcel>
    {
        public void Configure(EntityTypeBuilder<Parcel> builder)
        {
            builder.HasData(
               new Parcel { Id = 1, ParcelNo=1, Pafta="p1", Ada= 1 , il="il1", ilce="ilce1", mahalle ="m1", nitelik="n1" },
               new Parcel { Id = 2, ParcelNo = 2, Pafta = "p2", Ada = 2, il = "il2", ilce = "ilce2", mahalle = "m2", nitelik = "n2" }
               

           );
        }
    }
}
