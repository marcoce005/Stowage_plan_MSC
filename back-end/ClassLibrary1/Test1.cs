using System;
using ship;
using container;
using computing;
using static computing.Computing;
using bruteforce;
using static bruteforce.BruteForce;
using System.Diagnostics;

namespace Test
{
    public class Program
    {
        public int x {  get; set; }
        public int y { get; set; }
        public int z { get; set; }
        public float size { get; set; }

        public Program(int a, int b, int c, float s)
        {
            x = a; y = b; z = c; size = s;
        }

        public int[,,] ex1(float[] weight, int[] discharge, int[] id)
        {
            ship.Ship ship1 = new Ship(x, y, z);

            List<container.Container> ContainerList = new List<container.Container>();      // lista di Container
            //Random random = new Random();
            for (int i = 0; i < x * y * z; i++)
                ContainerList.Add(new Container(id[i], discharge[i], weight[i], size));
              
            foreach (var c in ContainerList)
                Debug.WriteLine("ID: {0} - priority: {1} - tons: {2}", c.ID, c.priority, c.tons);

            //bruteforce.BruteForce brute = new BruteForce();
            //brute.BruteApproch(ContainerList, new Permutator(), x, y, z);

            computing.Computing work = new computing.Computing();
            work.FillerSpecial(ContainerList, x, y, z, ship1);

            return ship1.matrix3D;
        }
    }
}
