using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Lab3
{
    internal static class InputOutput
    {
        public static bool[,]? ReadFile()
        {
            Console.WriteLine("Enter file path or empty string to use default file:");
            string? path = Console.ReadLine();
            if (path == null || path == "")
            {
                Console.WriteLine("Using standart file.");
                path = @"C:\Users\zeeel\source\labs\Lab3\Lab3\input.txt";
            }
            else if (!File.Exists(@"C:\Users\zeeel\source\labs\Lab3\Lab3\" + path + @".txt"))
            {
                Console.WriteLine("File does not exist. Using standart file.");
                path = @"C:\Users\zeeel\source\labs\Lab3\Lab3\input.txt";
            }
            else
            {
                path = @"C:\Users\zeeel\source\labs\Lab3\Lab3\" + path + @".txt";
            }
            StreamReader sr = new(path);
            var strings = sr.ReadToEnd().Split(new char[] { '\n', ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (strings.Length != Program.N)
            {
                Console.WriteLine($"Number of strings is not {Program.N}");
                return null;
            }
            bool[,] result = new bool[strings.Length, strings.Length];
            for (int i = 0; i < strings.Length; i++)
            {
                if (strings[i].Length != Program.N)
                {
                    Console.WriteLine($"Symbols number in string {i + 1} is not {Program.N}");
                    return null;
                }
                for (int j = 0; j < strings[i].Length; j++)
                {
                    if (strings[i][j] != '0' && strings[i][j] != '1')
                    {
                        Console.WriteLine($"Wrong symbol at ({i + 1}, {j + 1})");
                        return null;
                    }
                    result[i, j] = strings[i][j] == '1';
                }
            }
            return result;
        }
        public static void WriteFile(Graph graph)
        {
            Console.WriteLine("Enter file name: ");
            string? name = Console.ReadLine();
            if (name == null || name == "")
            {
                name = "result";
            }
            string path = @"C:\Users\zeeel\source\labs\Lab3\Lab3\" + name + @".txt";
            StreamWriter streamWriter = new StreamWriter(path);
            streamWriter.WriteLine($"Best solution ever: {graph.BestNumberOfUsedColors} colors");
            for (int i = 0; i < graph.BestColors.Length; i++)
            {
                streamWriter.WriteLine($"Vertex {i + 1}: color {graph.BestColors[i]}");
            }
        }
        public static void PrintBest(int iters, int number)
        {
            Console.WriteLine($"Iterations: {iters}. Best solution: {number} colors");
        }

        public static void PrintBest(int[] colors, int number, int iters)
        {
            Console.WriteLine($"Iterations: {iters}. Best solution: {number} colors");
            for (int i = 0; i < colors.Length; i++) 
            {
                Console.WriteLine($"Vertex {i + 1}: color {colors[i]}");
            }
        }

        public static void PrintBest(int[] colors, int number)
        {
            Console.WriteLine($"Best solution now: {number} colors");
            for (int i = 0; i < colors.Length; i++)
            {
                Console.WriteLine($"Vertex {i + 1}: color {colors[i]}");
            }
        }
        public static void PrintBest(Graph graph)
        {
            Console.WriteLine($"Best solution ever: {graph.BestNumberOfUsedColors} colors");
            for (int i = 0; i < graph.BestColors.Length; i++)
            {
                Console.WriteLine($"Vertex {i + 1}: color {graph.BestColors[i]}");
            }
        }
    }
}
