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
        public int[][,] results;

        public Generator(int number)
        {
            this.bound = number;
            results = new int[number][,];
        }

        /// <summary>
        /// 填充数独的下一个格子
        /// </summary>
        /// <param name="i">格子位置：第i行</param>
        /// <param name="j">格子位置：第j列</param>
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

        /// <summary>
        /// 打印结果到 results数组中
        /// </summary>
        private void PrintResult()
        {
            results[count] = (int[,])grid.Clone();
            count++;
            if (count >= bound)
                throw new EnoughResultsException();
        }
    }
}
