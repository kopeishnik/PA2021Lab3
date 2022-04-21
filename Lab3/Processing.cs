using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3
{
    internal static class Processing
    {
        public static bool CheckVertexes(bool[,] vs)
        {
            for (int i = 0; i < Program.N; i++) 
            {
                int count = 0;
                for (int j = 0; j < Program.N; j++)
                {
                    if (vs[i, j] == true)
                    {
                        count++;
                    } 
                }
                if (count < 1 || count > 30)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
