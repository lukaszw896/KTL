using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTL_game.Helper
{
    public class SequenceHelper
    {
        public static List<List<int>> GenerateColors(int total_colors, int rand_colors)
        {
            List<List<int>> colors = new List<List<int>>();
            int liczba_mozliwosci =  all_possible_colors( total_colors, rand_colors);
            Random rand = new Random();
            for (int i = 0; i < liczba_mozliwosci; i++)
            {
                while (true)
                {
                    bool foundRand = true;
                    List<int> rands = randColors (total_colors, rand_colors);
                    if (IsListInLists(colors, rands) == true)
                    {
                        foundRand = false;
                    }
                    if (foundRand == true)
                    {
                        colors.Add(rands);
                        break;
                    }
                }
            }

            return colors;
        }

        public static bool ScrambledEquals<T>(IEnumerable<T> list1, IEnumerable<T> list2)
        {
            var cnt = new Dictionary<T, int>();
            foreach (T s in list1)
            {
                if (cnt.ContainsKey(s))
                {
                    cnt[s]++;
                }
                else
                {
                    cnt.Add(s, 1);
                }
            }
            foreach (T s in list2)
            {
                if (cnt.ContainsKey(s))
                {
                    cnt[s]--;
                }
                else
                {
                    return false;
                }
            }
            return cnt.Values.All(c => c == 0);
        }
        public static bool IsListInLists(List<List<int>> colors, List<int> rands)
        {
            for (int i = 0; i < colors.Count; i++)
            {
                if (ScrambledEquals(colors[i], rands) == true)
                    return true;
            }
            return false;
            
        }
        public static int NTerm(int firstTerm, int step, int n)
        {
            int nTerm = 0;
            nTerm = firstTerm + (n - 1) * step;

            return nTerm;
        }
        public static List<int> randColors(int total_colors, int rand_colors)
        {
            List<int> colors = new List<int>();
            Random rand = new Random();
            //Losuję listę kolorów
            for (int i = 0; i < rand_colors; i++)
            {
                while (true)
                {
                    bool foundRand = true;
                    int tmpcol = rand.Next(total_colors);
                    for (int j = 0; j < colors.Count; j++)
                    {
                        if (colors[j] == tmpcol)
                        {
                            foundRand = false;
                        }
                    }
                    if (foundRand == true)
                    {
                        colors.Add(tmpcol);
                        break;
                    }
                }
            }
            return colors;
        }
        public static int all_possible_colors(int total_colors, int rand_colors)
        {
            int possibilities = 0;
            possibilities = Factorial(total_colors) / (Factorial(rand_colors) * Factorial(total_colors - rand_colors));
            return possibilities;
        }
        public static int Factorial(int i)
        {
            if (i <= 1)
                return 1;
            return i * Factorial(i - 1);
        }
    }
}
