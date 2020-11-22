using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BFS
{
    public class Field
    {
        private int[,] _tiles;
        private int _depth;
        public Field(int size)
        {
            _tiles = new int[size, size];
            List<int> unusedValues = Enumerable.Range(0, size * size).ToList();
            Random random = new Random();
            int counter = 0;
            int nextValueIndex;
            while (counter < size * size)
            {
                nextValueIndex = random.Next(unusedValues.Count());
                _tiles[counter / size, counter % size] =
                    unusedValues[nextValueIndex];
                unusedValues.RemoveRange(nextValueIndex, 1);
                counter++;
            }
            Id = Guid.NewGuid();
        }
        public Field(int[,] tiles)
        {
            _tiles = tiles;
            Id = Guid.NewGuid();
        }
        public Field(int[,] tiles, Guid parentId, int parentDepth)
        {
            ParentId = parentId;
            _tiles = tiles;
            _depth = parentDepth + 1;
            Id = Guid.NewGuid();
        }
        public Guid ParentId { get; }
        public Guid Id { get; }
        public int Distance
        {
            get => _depth + CalculateDistance();
        }
        public (int, int) PointerPosition
        {
            get 
            {
                int pointerI = 0;
                int pointerJ = 0;
                for (int i = 0; i < _tiles.GetLength(0); i++)
                {
                    for (int j = 0; j < _tiles.GetLength(1); j++)
                    {
                        if (_tiles[i, j] == 0)
                        {
                            pointerI = i;
                            pointerJ = j;
                        }
                    }
                }
                return (pointerI, pointerJ);
            }
        }

        public bool MoveLeftAvailable()
        {
            (int pointerI, int pointerJ) = PointerPosition;
            if (pointerJ > 0)
                return true;
            return false;
        }
        public bool MoveRightAvailable()
        {
            (int pointerI, int pointerJ) = PointerPosition;
            if (pointerJ < _tiles.GetLength(0) - 1)
                return true;
            return false;
        }
        public bool MoveUpAvailable()
        {
            (int pointerI, int pointerJ) = PointerPosition;
            if (pointerI > 0)
                return true;
            return false;
        }
        public bool MoveDownAvailable()
        {
            (int pointerI, int pointerJ) = PointerPosition;
            if (pointerI < _tiles.GetLength(1) - 1)
                return true;
            return false;
        }

        public Field MoveLeft()
        {
            int[,] tilesCopy = (int[,]) _tiles.Clone();
            (int pointerI, int pointerJ) = PointerPosition;
            tilesCopy[pointerI, pointerJ] =
                 tilesCopy[pointerI, pointerJ - 1];
            tilesCopy[pointerI, pointerJ - 1] = 0;
            return new Field(tilesCopy, Id, _depth);
        }
        public Field MoveRight()
        {
            int[,] tilesCopy = (int[,])_tiles.Clone();
            (int pointerI, int pointerJ) = PointerPosition;
            tilesCopy[pointerI, pointerJ] =
                 tilesCopy[pointerI, pointerJ + 1];
            tilesCopy[pointerI, pointerJ + 1] = 0;
            return new Field(tilesCopy, Id, _depth);
        }
        public Field MoveUp()
        {
            int[,] tilesCopy = (int[,])_tiles.Clone();
            (int pointerI, int pointerJ) = PointerPosition;
            tilesCopy[pointerI, pointerJ] =
                 tilesCopy[pointerI - 1, pointerJ];
            tilesCopy[pointerI - 1, pointerJ] = 0;
            return new Field(tilesCopy, Id, _depth);
        }
        public Field MoveDown()
        {
            int[,] tilesCopy = (int[,])_tiles.Clone();
            (int pointerI, int pointerJ) = PointerPosition;
            tilesCopy[pointerI, pointerJ] =
                 tilesCopy[pointerI + 1, pointerJ];
            tilesCopy[pointerI + 1, pointerJ] = 0;
            return new Field(tilesCopy, Id, _depth);
        }
        public bool IsSolved()
        {
            if (CalculateDistance() == 0)
                return true;
            return false;
        }
        public void PrintTiles()
        {
            Console.WriteLine($"Id: {Id}");
            Console.WriteLine($"Parent Id: {ParentId}");
            for (int i = 0; i < _tiles.GetLength(0); i++)
            {
                for (int j = 0; j < _tiles.GetLength(1); j++)
                {
                    Console.Write($"{_tiles[i, j]} ");
                }
                Console.WriteLine("");
            }
            Console.WriteLine("");
        }

        private int CalculateDistance()
        {
            int sum = 0;
            (int pointerI, int pointerJ) = PointerPosition;
            for (int i = 0; i < _tiles.GetLength(0); i++)
            {
                for (int j = 0; j < _tiles.GetLength(1); j++)
                {
                    if ((i, j) == (pointerI, pointerJ))
                    {
                        sum += Math.Abs(_tiles.GetLength(0) - 1 - i)
                            + Math.Abs(_tiles.GetLength(0) - 1 - j);
                    }
                    else
                    {
                        int value = _tiles[i, j];
                        sum += Math.Abs((value - 1) / _tiles.GetLength(0) - i)
                            + Math.Abs((value - 1) % _tiles.GetLength(0) - j);
                    }
                }
            }
            return sum;
        }
        public bool TilesEqual(Field field)
        {
            for (int i = 0; i < _tiles.GetLength(0); i++)
            {
                for (int j = 0; j < _tiles.GetLength(1); j++)
                {
                    if (_tiles[i, j] != field._tiles[i, j])
                        return false;
                }
            }
            return true;
        }
    }
}
