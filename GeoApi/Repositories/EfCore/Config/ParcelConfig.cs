using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Models;

namespace Repositories.EfCore.Config
{
    public class ParcelConfig : IEntityTypeConfiguration<Parcel>
    {
        public void Configure(EntityTypeBuilder<Parcel> builder)
        {
            builder.HasData(
               new Parcel { Id = 1, ParselNo = 1, Pafta = "p1", Ada = 1, il = "il1", ilce = "ilce1", mahalle = "m1", nitelik = "n1" },
               new Parcel { Id = 2, ParselNo = 2, Pafta = "p2", Ada = 2, il = "il2", ilce = "ilce2", mahalle = "m2", nitelik = "n2" }


           );
        }
    }
}
