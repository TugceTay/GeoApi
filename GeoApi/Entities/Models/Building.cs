using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Building
    {
        public int Id { get; set; }
        public int? fKey { get; set; }
        public string? Blok { get; set; }
        public string? Nitelik { get; set; }
        // public Geometry? geom { get; set; }
        public MultiPolygon? geom { get; set; }
    }
}
