using System;
using System.Diagnostics.SymbolStore;

namespace ship
{
    class Ship
    {
        public int[,,] matrix3D;
        public Ship(int x, int y, int z)
        {
            matrix3D = new int[x, y, z];
            for (int i = 0; i < z; i++)
                for (int j = 0; j < y; j++)
                    for (int k = 0; k < x; k++)
                        matrix3D[k, j, i] = -1;
        }
    }
}
