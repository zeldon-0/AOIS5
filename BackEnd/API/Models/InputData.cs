using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class InputData
    {
        public int[] Tiles { get; set; }
        public int[,] GetTilesMatrix()
        {
            int size = (int)Math.Sqrt(Tiles.Length);
            int[,] matrix = new int[size, size];
            for (int i = 0; i < size * size; i++)
            {
                matrix[i / size, i % size] = Tiles[i];
            }
            return matrix;
        }
    }
}
