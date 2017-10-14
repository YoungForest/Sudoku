using System;
using System.Collections.Generic;
using System.Linq;
using SudokuData;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Core
{
    /// <summary cref="C < T >">
    /// 一些包内公共常用的函数集和对外接口
    /// </summary>  
    public static class SudokuFounctionLibrary
    {
        private static Random rnd = new Random();
        private const int SIZE = 9;
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
            if (number > 1000000 || number < 1)
            {
                throw new GenerateNumberOutOfRange();
            }
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
        /// <param name="mode">生成残局的难度，只能是1（简单）,2（中等）,3（困难）,其他按困难处理</param>
        /// <param name="result">存储残局的数组</param>
        public static void generate(int number, int mode, ref int[][,] result)
        {
            switch (mode)
            {
                case 1:
                    generate(number, 45, 49, true,ref result);
                    break;
                case 2:
                    generate(number, 50, 54, true, ref result);
                    break;
                case 3:
                    generate(number, 55, 59, true, ref result);
                    break;
                default:
                    throw new ModeOutOfRange();
            }


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
            if (number > 1000 || number < 1)
            {
                throw new GenerateNumberOutOfRange();
            }
            if (lower < 20 || upper > 55)
            {
                throw new BoundOutOfRange();
            }
            generate(number, ref result);
            
            int theNumber = 0;
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
                int target = digNumbers.Sum();
                int diged = 0;
                var tried = new int[9, 9];
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
                        theNumber = result[index][basex + digx, basey + digy];
                        result[index][basex + digx, basey + digy] = 0;
                        tried[basex + digx, basey + digy] = 1;
                        diged++;
                        if (unique)
                        {
                            Table t = new Table();
                            t.creat(result[index]);
                            int s = t.solve();
                            if (s !=1)
                            {
                                result[index][basex + digx, basey + digy] = theNumber;
                                diged--;
                            }
                        }
                    }
                }
                if (unique)
                {
                    while (diged < target)
                    {
                        int dx = 0;
                        int dy = 0;
                        do
                        {
                            dx = rnd.Next(0, 9);
                            dy = rnd.Next(0, 9);
                        }
                        while (tried[dx, dy] == 0);
                        theNumber = result[index][dx, dy];
                        result[index][dx, dy] = 0;
                        tried[dx, dy] = 1;
                        diged++;
                        Table t = new Table();
                        t.creat(result[index]);
                        int s = t.solve();
                        if (s != 1)
                        {
                            result[index][dx, dy] = theNumber;
                            diged--;
                        }
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
                solution = solver.solution;

            return solver.success;
        }
       
        //打印数独到标准输出
        public static void printTable(int[,] puzzle)
        {
            for (int o = 0; o < 9; o++)
            {
                for (int p = 0; p < 9; p++)
                {
                    Console.Write(puzzle[o, p]);
                    if (p == 8)
                    {
                        Console.Write("\n");
                    }
                    else
                    {
                        Console.Write(" ");
                    }
                }
                if (o == 8)
                {
                    Console.Write("\n");
                }
            }
        }

        /// <summary>
        /// 输出数独到文件
        /// </summary>
        /// <param name="filename">目标文件路径</param>
        /// <param name="results">存储数独</param>
        public static void PrintPuzzleToFile(string filename, ref int[][,] results)
        {
            // clear the content of target file
            using (System.IO.StreamWriter outputfile =
     new System.IO.StreamWriter(filename))
            {
                int len = results.Length;
                for (int i = 0; i < len; i++)
                {
                    int[,] puzzle = results[i];
                    for (int j = 0; j < SIZE; j++)
                    {
                        for (int k = 0; k < SIZE; k++)
                        {
                            outputfile.Write("{0} ", puzzle[j, k]);
                        }
                        outputfile.WriteLine();
                    }
                    outputfile.WriteLine();
                }
            }
        }

        /// <summary>
        /// 从一个文本文件中读取数独
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="results">存储所读取的数独内容</param>
        public static void ReadPuzzleFromFile(string filename, ref int[][,] results)
        {
            try
            {
                List<int[,]> puzzles = new List<int[,]>();
                // Read each line of the file into a string array. Each element
                // of the array is one line of the file.
                string[] lines = System.IO.File.ReadAllLines(filename);
                string[] sublines1 = new string[9];
                Array.Copy(lines, 0, sublines1, 0, 9);
                int[,] puzzle1 = null;
                ReadIntoPuzzle(sublines1, ref puzzle1);
                puzzles.Add(puzzle1);

                for (int i = 10; i < lines.Length; i += 10)
                {
                    string[] sublines = new string[9];
                    Array.Copy(lines, i, sublines, 0, 9);
                    int[,] puzzle = null;
                    ReadIntoPuzzle(sublines, ref puzzle);
                    puzzles.Add(puzzle);
                }

                results = puzzles.ToArray();
            }
            catch (System.IO.FileNotFoundException ex)
            {
                System.Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// 从字符串数组中读取数独
        /// </summary>
        /// <param name="lines">9行的数独字符串</param>
        /// <param name="puzzle">存储数独的数组</param>
        public static void ReadIntoPuzzle(string[] lines, ref int[,] puzzle)
        {
            puzzle = new int[SIZE, SIZE];
            for (int i = 0; i <= SIZE; i++)
            {
                char[] delimiterChars = { ' ' };
                string[] words = lines[i].Split(delimiterChars);

                for (int j = 0; j <= SIZE; j++)
                {
                    try
                    {
                        puzzle[i, j] = Int32.Parse(words[j]);
                    }
                    catch (FormatException ex)
                    {
                        System.Console.WriteLine(ex.Message);
                    }
                }
            }
        }
    }

    [Serializable]
    public class BoundOutOfRange : Exception
    {
        public BoundOutOfRange()
        {
        }

        public BoundOutOfRange(string message) : base(message)
        {
        }

        public BoundOutOfRange(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected BoundOutOfRange(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }

    [Serializable]
    public class GenerateNumberOutOfRange : Exception
    {
        public GenerateNumberOutOfRange()
        {
        }

        public GenerateNumberOutOfRange(string message) : base(message)
        {
        }

        public GenerateNumberOutOfRange(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected GenerateNumberOutOfRange(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }

    [Serializable]
    public class ModeOutOfRange : Exception
    {
        public ModeOutOfRange()
        {
        }

        public ModeOutOfRange(string message) : base(message)
        {
        }

        public ModeOutOfRange(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ModeOutOfRange(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
