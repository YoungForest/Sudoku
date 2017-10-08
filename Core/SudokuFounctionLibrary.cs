using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    /// <summary cref="C < T >">
    /// 一些包内公共常用的函数集和对外接口
    /// </summary>  
    public static class SudokuFounctionLibrary
    {
        private static Random rnd = new Random();
        private static int[] numbers = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

        /// <summary cref="C < T >">
        /// randomly generate a list containing 1 to 9
        /// </summary>  
        public static List<int> GenerateFillList()
        {
            int[] MyRandomNumbers = numbers.OrderBy(x => rnd.Next()).ToArray();

            return MyRandomNumbers.ToList();
        }

        /// <summary>
        /// 判断数独中一个格子[i,j]是否填充成功
        /// </summary>
        /// <param name="puzzle">存储数独中所有的数</param>
        /// <param name="i">待检测格子的位置：第i行</param>
        /// <param name="j">待检测格子的位置：第j列</param>
        /// <returns>是否填充成功</returns>
        public static bool FillSuccess(int[,] puzzle, int i, int j)
        {
            // check column
            for (int ii = i - 1; ii >= 0; ii--)
            {
                if (puzzle[i, j] == puzzle[ii, j])
                    return false;
            }

            // check row
            for (int jj = j - 1; jj >= 0; jj--)
            {
                if (puzzle[i, j] == puzzle[i, jj])
                    return false;
            }
            // check small puzzle
            int basei = i - i % 3;
            int basej = j - j % 3;
            for (int ii = basei; ii < basei + 3 && ii < i; ii++)
            {
                for (int jj = basej; jj < basej + 3; jj++)
                {
                    if (j != jj && puzzle[i, j] == puzzle[ii, jj])
                        return false;
                }
            }

            return true;
        }

        public static void generate(int number, ref int[][,] result)
        {
            Generator g = new Generator(number);
            try
            {
                g.FillNextGrid(0, 0);
            }
            catch(EnoughResultsException)
            {
                result = g.results.ToArray();
            }
        }
    }
}
