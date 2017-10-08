using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    /// <summary cref="C < T >">
    /// 数独生成器
    /// </summary>  
    public class Generator
    {
        public int[,] grid = new int[9, 9];
        const int LAST = 8;
        public int count = 0;
        public int bound = 0;
        public List<int[,]> results = new List<int[,]>();


        public void FillNextGrid(int i, int j)
        {
            var fillList = SudokuFounctionLibrary.GenerateFillList();

            fillList.ForEach(delegate (int item)
            {
                grid[i, j] = item;
                if (SudokuFounctionLibrary.FillSuccess(grid, i, j))
                {
                    if (i == LAST && j == LAST)
                    {
                        PrintResult();
                        return;
                    }
                    else
                    {
                        int nexti = j == LAST ? i + 1 : i;
                        int nextj = j == LAST ? 0 : j + 1;
                        FillNextGrid(nexti, nextj);
                    }
                }
            });
        }

        private void PrintResult()
        {
            results.Add(grid);
            count++;
            if (count >= bound)
                throw new EnoughResultsException();
        }
    }
}
