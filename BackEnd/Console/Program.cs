using System;
using System.Collections.Generic;
using System.Text;
using BFS;
namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            /*Field field = new Field
            (
                new int[,]
                {
                    { 8, 0, 6},
                    { 5, 4, 7},
                    { 2, 3, 1}

                }
            );*/
            Field field = new Field
            (
                new int[,]
                {
                    { 1, 2, 4, 7},
                    { 5, 12, 3, 8},
                    { 14, 0, 6, 10},
                    { 9, 13, 11, 15}

                }
            );

            BFSSolver solver = new BFSSolver(field);
            List<Field> fieldsPassed =  solver.Search();
            foreach(var fieldPassed in fieldsPassed)
            {
                fieldPassed.PrintTiles();
            }
        }
    }
}
