using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Parcel
    {
        public int Id { get; set; }
        public int? ParselNo { get; set; }
        public string? Pafta { get; set; }
        public int? Ada { get; set; }
        public string? il { get; set; }
        public string? ilce { get; set; }
        public string? mahalle { get; set; }
        public string? nitelik { get; set; }
        public MultiPolygon? geom { get; set; }
    }
}
