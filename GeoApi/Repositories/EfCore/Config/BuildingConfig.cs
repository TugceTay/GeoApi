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
    public class BuildingConfig : IEntityTypeConfiguration<Building>
    {
        public void Configure(EntityTypeBuilder<Building> builder)
        {
            builder.HasData(
               new Building { Id = 1, fKey = 1, Blok = "b1", Nitelik = "n1" },
               new Building { Id = 2, fKey = 2, Blok = "b2", Nitelik = "n2"  }

           );
        }
    }
}
