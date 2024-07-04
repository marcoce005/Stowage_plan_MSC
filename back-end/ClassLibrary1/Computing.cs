using container;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Linq;
using System.Numerics;
using System.Diagnostics;
using System.Collections.Immutable;
using System.Linq.Expressions;
using System.ComponentModel;
using calbarycentre;

namespace computing
{
    class Computing
    {
        public Computing() { }
        public List<container.Container> SortByPriorityTons(List<container.Container> list)
        {
            List<container.Container> order_by_p = list.OrderBy(container => container.priority).Reverse().ToList();          // ordina la lista in base alla priorità di scarico [+alta prima scende]

            List<container.Container> final = new List<container.Container>();
            List<container.Container> temp = new List<container.Container>();

            int priority = new int();
            int count = 0;
            foreach (container.Container c in order_by_p)         // ordina i blocci di stessa priorità in ordine decrescente per peso
            {
                if (count == 0)
                    priority = c.priority;

                if (priority != c.priority)
                {
                    priority = c.priority;
                    List<container.Container> order_by_weight = temp.OrderBy(container => container.tons).Reverse().ToList();
                    temp.Clear();
                    final = final.Concat(order_by_weight).ToList();
                }
                temp.Add(c);

                if (count == list.Count - 1)
                {
                    List<container.Container> order_by_weight = temp.OrderBy(container => container.tons).Reverse().ToList();
                    temp.Clear();
                    final = final.Concat(order_by_weight).ToList();
                }
                count++;
            }
            return final;
        }

        public List<container.Container> SortByTons(List<container.Container> list) { return list.OrderBy(container => container.tons).Reverse().ToList(); }

        public List<container.Container> SortForSpecial(List<container.Container> list, int z)
        {
            List<container.Container> layer = new List<container.Container>();
            List<container.Container> tmp2 = new List<container.Container>();
            int limit = list.Count / z;
            int l = limit, cont = 2;
            for (int i = 0; i < list.Count; i++)
            {
                layer.Add(list[i]);
                if (i == l - 1)
                {
                    layer = SortByTons(layer);
                    tmp2 = tmp2.Concat(layer).ToList();
                    layer.Clear(); l = cont++ * limit;
                }
            }
            List<container.Container> even = new List<container.Container>();
            List<container.Container> odd = new List<container.Container>();
            List<container.Container> final = new List<container.Container>();
            List<container.Container> tmp = new List<container.Container>();
            l = tmp2.Count / z; cont = 2;
            Debug.WriteLine($"\n\n{tmp2.Count()}");
            foreach (container.Container c in tmp2)
                Debug.WriteLine("ID: {0} - priority: {1} - tons: {2}", c.ID, c.priority, c.tons);
            for (int i = 0; i < tmp2.Count; i++)
            {
                if (i == l - 1)
                {
                    final.Add(tmp2[l - limit]);
                    for (int j = l - limit; j < l; j++)
                    {
                        if ((j + 1 - (l - limit)) % 4 == 0 && j != l - limit)
                        {
                            final.Add(tmp2[j]);
                            try
                            {
                                if (j + 1 < l)
                                    final.Add(tmp2[++j]);
                            }
                            catch { }
                        }
                        else if (j != l - limit)
                        {
                            tmp.Add(tmp2[j]);
                        }
                    }
                    final = final.Concat(Enumerable.Reverse(tmp).ToList()).ToList();
                    odd.Clear(); even.Clear(); l = cont++ * limit; tmp.Clear();
                }
            }
            Debug.WriteLine($"\n\n{final.Count()}");
            foreach (container.Container c in final)
                Debug.WriteLine("ID: {0} - priority: {1} - tons: {2}", c.ID, c.priority, c.tons);
            return final;
        }

        public void FillerSpecial(List<container.Container> c, int x, int y, int z, ship.Ship StowagePlan)
        {
            float ideal_x = x / 2;
            float ideal_y = y / 2;
            float ideal_z = 0;
            int cont = 0;
            float[,,] MatForCalBarycentre = new float[x, y, z];
            c = SortByPriorityTons(c);
            c = SortForSpecial(c, z);
            for (int i = 0; i < z; i++)
                for (int k = 0; k < x; k++)
                    for (int j = 0; j < y; j++)
                    {
                        MatForCalBarycentre[k, j, i] = c[cont].tons;
                        StowagePlan.matrix3D[k, j, i] = c[cont].ID;
                        c[cont].x = (j - ideal_x) * c[cont].size / 2;
                        c[cont].y = (k - ideal_y) * c[cont].size / 2;
                        c[cont].z = (i - ideal_z) * c[cont++].size / 2;
                    }
            Console.Write("\n\nDisposizione container tramite ID");
            for (int i = 0; i < z; i++)
            {
                Console.WriteLine($"\n\nLayer {i}");
                for (int j = 0; j < y; j++)
                {
                    Console.Write("\n");
                    for (int k = 0; k < x; k++)
                    {

                        if (MatForCalBarycentre[k, j, i] <= 15)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write($"{StowagePlan.matrix3D[k, j, i]}\t");
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                        else if (MatForCalBarycentre[k, j, i] <= 22)
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.Write($"{StowagePlan.matrix3D[k, j, i]}\t");
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write($"{StowagePlan.matrix3D[k, j, i]}\t");
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                    }
                }
            }
            calbarycentre.CalBarycentre b = new CalBarycentre();
            Console.WriteLine($"\n\nDistanza baricentro ideale = {b.Barycentre(c, (double)ideal_x, (double)ideal_y, (double)ideal_z)}");
        }
    }
}