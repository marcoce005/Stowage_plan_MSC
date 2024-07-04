using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using calbarycentre;

namespace bruteforce
{
    class BruteForce
    {
        public BruteForce() { }

        //INIZIO BRUTE APPROCH


        //var permutation = _permutator.MakePerm(request.Containers.Count);
        //  do {
        //    permutation = _permutator.Successor(permutation);
        // } while (permutation is not null);


        public interface IPermutator
        {
            IList<IList<T>> Permute<T>(T[] nums);
            int[] MakePerm(int order);
            int[]? Successor(int[] perm);
            ulong Factorial(ulong n);
        }

        public class Permutator : IPermutator
        {
            public ulong Factorial(ulong n)
            {
                if (n == 0 || n == 1)
                    return 1;



                ulong ans = ulong.Parse("1");  // alternative
                for (ulong i = 1; i <= n; ++i)
                    ans *= i;
                return ans;
            }
            public int[] MakePerm(int order)
            {
                int[] result = new int[order];
                for (int i = 0; i < order; i++)
                    result[i] = i;
                return result;
            }



            public bool IsLast(int[] perm)
            {
                // is perm like [5,4,3,2,1,0] ?
                int order = perm.Length;
                if (perm[0] != order - 1) return false;
                if (perm[order - 1] != 0) return false;
                for (int i = 0; i < order - 1; ++i)
                {
                    if (perm[i] < perm[i + 1])
                        return false;
                }
                return true;
            }
            public int[]? Successor(int[] perm)
            {
                int order = perm.Length;  // ex: order 5



                // is perm like [4,3,2,1,0] so no successor?
                if (IsLast(perm) == true)
                    return null;



                int[] result = new int[order];
                for (int k = 0; k < order; ++k)
                    result[k] = perm[k];



                int left, right;



                left = order - 2;  // find left value 
                while ((result[left] > result[left + 1]) && (left >= 1))
                    --left;



                right = order - 1;  // first value gt left
                while (result[left] > result[right])
                    --right;



                int tmp = result[left];  // swap [left] and [right]
                result[left] = result[right];
                result[right] = tmp;



                int i = left + 1;  // order the tail
                int j = order - 1;
                while (i < j)
                {
                    tmp = result[i];
                    result[i++] = result[j];
                    result[j--] = tmp;
                }



                return result;
            }
            public IList<IList<T>> Permute<T>(T[] nums)
            {
                var list = new List<IList<T>>();
                return DoPermute(nums, 0, nums.Length - 1, list);
            }
            public IList<IList<T>> DoPermute<T>(T[] nums, int start, int end, IList<IList<T>> list)
            {
                if (start == end)
                {
                    // We have one of our possible n! solutions,
                    // add it to the list.
                    list.Add(new List<T>(nums));
                }
                else
                {
                    for (var i = start; i <= end; i++)
                    {
                        Swap(ref nums[start], ref nums[i]);
                        DoPermute(nums, start + 1, end, list);
                        Swap(ref nums[start], ref nums[i]);
                    }
                }
                return list;
            }
            private static void Swap<T>(ref T a, ref T b)
            {
                (a, b) = (b, a);
            }
        }

        public void BruteApproch(List<container.Container> list, IPermutator permutator, int x, int y, int z)
        {
            List<List<int>> CaseList = new List<List<int>>();
            var permutation = permutator.MakePerm(list.Count);
            int counter = 1;
            do
            {
                CaseList.Add(permutation.ToList());
                counter++;
                permutation = permutator.Successor(permutation);
            } while (permutation is not null);

            Dictionary<double, List<int>> results = new Dictionary<double, List<int>>();        // dizionario KEY = differenza dal bilanciamento ideale, VALUE = ordine di carico per ID
            foreach (List<int> sublist in CaseList)
            {
                if (results.ContainsKey(FillMatrixForBruteForce(sublist, list, x, y, z)))
                    continue;
                else
                    results.Add(FillMatrixForBruteForce(sublist, list, x, y, z), sublist);
            }

            double traslation = results.Aggregate((x, y) => x.Key <= y.Key ? x : y).Key;     // ricerca della minima KEY nel dizionario
            List<int> sequence = results[traslation];

            Console.WriteLine("\n\nSpostamento del baricentro:\t{0}\nOrdine di carico:\t", traslation);             // stampa risultati
            foreach (int o in sequence)
                Console.Write("{0} ", o);
            Console.WriteLine("\nMappa dell'imbarcazione");
            int cont = 0;
            for (int i = 0; i < z; i++)
            {
                Console.Write("\n");
                for (int j = 0; j < y; j++)
                {
                    Console.Write("\n");
                    for (int k = 0; k < x; k++)
                        Console.Write("{0} ", sequence[cont++]);
                }
            }
        }

        public double FillMatrixForBruteForce(List<int> combination, List<container.Container> c, int x, int y, int z)
        {
            float ideal_x = x / 2;
            float ideal_y = y / 2;
            float ideal_z = 0;
            List<float> comb = combination.Select<int, float>(i => i).ToList();     // conversione della matrice int in float

            float[,,] float_ship = new float[x, y, z];

            for (int i = 0; i < comb.Count; i++)            // sostituisce all'ID il peso corrispondente del Container
            {
                comb[i] = c[combination.IndexOf(i)].tons;
            }

            int cont = 0;
            for (int i = 0; i < z; i++)             // riempe la matrice con i pesi dei Container seguendo l'ordine dato dal brute-force
                for (int j = 0; j < y; j++)
                    for (int k = 0; k < x; k++)
                    {
                        float_ship[i, j, k] = comb[cont];
                        c[cont].x = (j - ideal_x) * c[cont].size / 2;
                        c[cont].y = (k - ideal_y) * c[cont].size / 2;
                        c[cont].z = (i - ideal_z) * c[cont++].size / 2;
                    }
            calbarycentre.CalBarycentre b = new CalBarycentre();
            return b.Barycentre(c, (float)ideal_x, (float)ideal_y, (float)ideal_z);
        }
    }
}
