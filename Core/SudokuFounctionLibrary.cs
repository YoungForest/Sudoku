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

        /// <summary>
        /// 生成数独终局
        /// </summary>
        /// <param name="number">数独终局的数目</param>
        /// <param name="result">存储终局的数组</param>
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

        /// <summary>
        /// 得到特定难度特定数目的数独残局
        /// </summary>
        /// <param name="number">生成残局的数目</param>
        /// <param name="mode">生成残局的难度，只能是1（简单）,2（中等）,3（困难）</param>
        /// <param name="result">存储残局的数组</param>
        public static void generate(int number, int mode, ref int[][,] result)
        {

        }

        /// <summary>
        /// 生成数独残局
        /// </summary>
        /// <param name="number">残局数目</param>
        /// <param name="lower">挖空的下界</param>
        /// <param name="upper">挖空的上界</param>
        /// <param name="unique">是否有唯一解</param>
        /// <param name="result">存储结果</param>
        public static void generate(int number, int lower, int upper, bool unique, ref int[][,] result)
        {
            generate(number, ref result);

            for (int index = 0; index < number; index ++)
            {
                // 挖空
                var rnd = new Random();
                var digNumbers = new int[9];

                // 生成9个随机数 range: 2~9;
                do
                {
                    for (int i = 0; i < 9; i++)
                    {
                        digNumbers[i] = rnd.Next(2, 10);
                    }
                }
                while (digNumbers.Sum() > upper || digNumbers.Sum() < lower);

                // 挖空，即标志该位为0
                var grids = new int[,] { {0, 0}, {0, 1}, {0, 2},
                                         {1, 0}, {1, 1}, {1, 2},
                                         {2, 0}, {2, 1}, {2, 2} };
                int[] numbers = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8 };

                for (int i = 0; i < 9; i++)
                {
                    int basex = (i / 3) * 3;
                    int basey = (i % 3) * 3;
                    int[] MyRandomNumbers = numbers.OrderBy(x => rnd.Next()).ToArray();

                    for (int j = 0; j < digNumbers[i]; j++)
                    {
                        int digx = grids[MyRandomNumbers[j], 0];
                        int digy = grids[MyRandomNumbers[j], 1];

                        result[index][basex + digx, basey + digy] = 0;
                    }
                }
            }
        }

        /// <summary>
        ///  求解一个数独
        /// </summary>
        /// <param name="puzzle">被求解的数独</param>
        /// <param name="solution">求解结果</param>
        /// <returns></returns>
        public static bool solve(int[,] puzzle, ref int[,] solution)
        {
            var solver = new Solver(puzzle);

            solver.Solve();

            if (solver.success)
                solution = solver.puzzle;

            return solver.success;
        }
    }
}
