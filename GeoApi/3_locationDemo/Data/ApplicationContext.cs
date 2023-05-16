// inmemory db görevi görecek 
// statik sınıflar statik uyeler içerir dolayısıyla bir ctor olusturdugujmuzda static bir nesnenin newlenmesi mumkun olmadıgından ctor static tanımladık 

using _3_locationDemo.Models;

namespace _3_locationDemo.Data
{
    public static class ApplicationContext////Statik sınıflar, örnekleme yapmadan doğrudan erişilebildikleri için, örneklere ihtiyaç duymazlar. Ancak statik olmayan sınıflar, bir örnek oluşturulduğunda kullanılabilir. Her örnek, aynı sınıftan farklı bir nesne olarak düşünülebilir ve her bir nesnenin özellikleri ve davranışları, o örneğe özgüdür.
    {
        public static List<Location> Locations { get; set; }
        static ApplicationContext()
        {
            Locations = new List<Location>()
            {
             new Location() {Id=1, LocationName="L1", X= 35.1, Y=42.1},
                new Location() {Id=2, LocationName="L2", X= 35.2, Y=42.2},
                new Location() {Id=3, LocationName="L3", X= 35.3, Y=42.3}
            };
        }
    }
}
