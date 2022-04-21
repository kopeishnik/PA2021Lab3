using System;
using System.Collections.Generic;

namespace Lab3
{
    internal class Program
    {
        public static readonly int N = 150;
        public static void Main()
        {
            //Console.BackgroundColor = ConsoleColor.White;
            //Console.ForegroundColor = ConsoleColor.Black;
            Console.Clear();
            var matrix = InputOutput.ReadFile();
            if (matrix == null)
            {
                Console.WriteLine("End of program because of wrong input file.");
            }
            else
            {
                Console.WriteLine("Readed");
                if (!Processing.CheckVertexes(matrix))
                {
                    Console.WriteLine("Wrong number of valence.");
                    //return;
                }
                Console.WriteLine("Checked");
                Graph graph = new(matrix);
                Console.WriteLine("Created graph");
                graph.ABC();
                Console.WriteLine("Done");
                InputOutput.PrintBest(graph);
                //InputOutput.WriteFile(graph);
            }
        }
    }
}