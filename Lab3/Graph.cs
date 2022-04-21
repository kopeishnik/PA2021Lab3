using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3
{
    internal class Graph
    {
        public bool[,] Matrix;
        public int Bees = 25;
        public int Scout = 3;
        public int Observers = 22;
        public int[] Nectar;
        public int[] BestColors;
        public int BestNumberOfUsedColors = 0;
        public Graph(bool[,] matrix)
        {
            Matrix = matrix;
            Nectar = new int[Program.N];
            BestColors = new int[Program.N];
            for (int i = 0; i< Program.N; i++)
            {
                BestColors[i] = 0;
            }
            BestNumberOfUsedColors = int.MaxValue;
        }
        public int CountNectar(int[] colors, int number)
        {
            int x = 0;
            for (int i = 0; i < colors.Length; i++)
            {
                if (Matrix[number, i] == true && colors[i] == 0 && number != i)
                {
                    x++;
                }
            }
            return x;
        }
        public int FindColor(int[] matrixColors, int number)
        {
            List<int> vertexes = new();
            for (int i = 0; i < Program.N; i++)
            {
                if (Matrix[number, i] && number != i)
                {
                    vertexes.Add(i);
                }
            }
            List<int> colors = new();
            foreach (int v in vertexes)
            {
                if (!colors.Contains(matrixColors[v]))
                {
                    colors.Add(matrixColors[v]);
                }
            }
            colors.Sort();
            int color = 0;
            for (int i = 1; i < colors[^1]; i++)
            {
                if (!colors.Contains(i))
                {
                    color = i;
                    break;
                }
            }
            if (color == 0)
            {
                color = colors[^1] + 1;
            }
            return color;
        }

        static bool AllColored(int[] colors)
        {
            for (int i = 0; i < colors.Length; i++)
            {
                if (colors[i] == 0) return false;
            }
            return true;
        }
        static int CountUncolored(int[] colors)
        {
            int x = 0;
            for (int i = 0; i < colors.Length; i++)
            {
                if (colors[i] == 0) x++;
            }
            return x;
        }
        public List<int> GetEdgesOf(int[] Colors, int number)
        {
            List<int> edgesw = new();
            for (int i = 0; i < Program.N; i++) 
            {
                if (Matrix[number, i] && Colors[i] == 0)
                {
                    edgesw.Add(i);
                }
            }
            for (int i = 0; i < Program.N; i++)
            {
                if (Matrix[number, i] && Colors[i] != 0)
                {
                    edgesw.Add(i);
                }
            }
            return edgesw;
        }
        public void ABC()
        {
            for (int iter = 0; iter < 1001; iter++)
            {
                int[] Colors = new int[Program.N];
                for (int i = 0; i < Program.N; i++)
                {
                    Colors[i] = 0;
                }
                List<int> used = new();
                //цикл розмальовки
                while (!AllColored(Colors)) // || used.Count < Program.N
                {
                    //Console.WriteLine($"Uncolored {CountUncolored(Colors)}");
                    //розподілимо скаутів
                    int[] scouts = new int[Scout];
                    Random rand = new();
                    for (int i = 0; i < scouts.Length; i++) 
                    {
                        int next = rand.Next(1, 150);
                        while (scouts.Contains(next)) //  || (used.Contains(next) && used.Count < Colors.Length)
                        {
                            next = rand.Next(1, 150);
                        }
                        //Console.WriteLine($"Chosen {next}");
                        scouts[i] = next;
                        if (!used.Contains(next))
                        {
                            used.Add(next);
                        }
                    }
                    //розподілимо бджіл
                    int[] nectars = new int[Scout];
                    for (int i = 0; i < nectars.Length; i++)
                    {
                        nectars[i] = CountNectar(Colors, scouts[i]) + 1;
                    }
                    int nectarSum = 0;
                    foreach (int n in nectars)
                    {
                        nectarSum += n;
                    }
                    int[] observers = new int[Scout];
                    for (int i = 0; i < observers.Length; i++)
                    {
                        observers[i] = Observers * nectars[i] / nectarSum;
                    }
                    //розмальовуємо
                    for (int i = 0; i < observers.Length; i++) 
                    {
                        List<int> edgesWith = GetEdgesOf(Colors, scouts[i]);
                        int bound = observers[i];
                        if (observers[i] > edgesWith.Count) bound = edgesWith.Count;
                        for (int j = 0; j < bound; j++)
                        {
                            Colors[edgesWith[j]] = FindColor(Colors, edgesWith[j]);
                            //Console.WriteLine($"Color {Colors[edgesWith[j]]}");
                        }
                        if (observers[i] >= nectars[i] - 1)
                        {
                            Colors[scouts[i]] = FindColor(Colors, scouts[i]);
                            //Console.WriteLine($"Color {Colors[scouts[i]]}");
                        }
                    }
                }
                // перевірка чи кращий
                int colorNum = CountUsedColors(Colors);
                if (colorNum < BestNumberOfUsedColors)
                {
                    BestNumberOfUsedColors = colorNum;
                    BestColors = Colors;
                }
                // вивід кращого (кожні 20 ітерацій)
                if (iter % 20 == 0)
                {
                    InputOutput.PrintBest(iter, BestNumberOfUsedColors);
                    using StreamWriter sw = File.AppendText(@"C:\Users\zeeel\source\labs\Lab3\Lab3\lastresult.txt");
                    sw.WriteLine(BestNumberOfUsedColors);
                }
            }
        }
        public static int CountUsedColors(int[] colors)
        {
            int biggest = 0;
            for (int i = 0; i < colors.Length; i++)
            {
                if (colors[i] > biggest)
                {
                    biggest = colors[i];
                }
            }
            return biggest;
        }
    }
}
