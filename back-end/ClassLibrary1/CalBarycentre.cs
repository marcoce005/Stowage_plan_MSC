using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace calbarycentre
{
    class CalBarycentre
    {
        public CalBarycentre() { }
        public double Barycentre(List<container.Container> list, double ideal_x, double ideal_y, double ideal_z)
        {
            double x = 0;
            double y = 0;
            double z = 0;
            double tot = 0;

            foreach (container.Container c in list)
            {
                x += c.tons * c.x;
                y += c.tons * c.y;
                z += c.tons * c.z;
                tot += c.tons;
            }

            double x_barycentre = Math.Abs(x / tot);
            double y_barycentre = Math.Abs(y / tot);
            double z_barycentre = Math.Abs(z / tot);

            return Math.Sqrt(
                                Math.Pow(x_barycentre - ideal_x, 2) +
                                Math.Pow(y_barycentre - ideal_y, 2) +
                                Math.Pow(z_barycentre - ideal_z, 2)
                            );
        }
    }
}
