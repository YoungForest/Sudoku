using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SudokuData;
using Core;

namespace Sudoku
{
    class Program
    {
        static void Main(string[] args)
        {

            



        }

        public static void TestSolve()
        {
            int[,] result1 = {
                { 0 ,0 ,0 ,0 ,0 ,6 ,2 ,0 ,0 },
                { 7, 0, 0 ,0 ,2 ,0 ,0, 3 ,1 },
                { 0 ,0, 8, 3 ,0 ,0, 0, 0 ,6 },
                { 0 ,0, 3, 0 ,0, 7 ,0 ,0 ,0 },
                { 1 ,0 ,9 ,0 ,0 ,0 ,0 ,0 ,0 },
                { 8 ,0 ,0, 0 ,6, 0 ,3, 0 ,4 },
                { 2, 0 ,0 ,0, 7, 0, 4 ,0, 0 },
                { 0, 4, 0 ,6 ,0 ,0, 0 ,0, 0 },
                { 0, 8 ,0 ,2, 0, 0, 0, 5, 0 }
            };

            Table t = new Table();
            t.creat(result1);
            int s = t.solve();
            Console.WriteLine(s);

            Console.ReadLine();
        }

        public static void Test()
        {
            int[][,] result = null;
            const int number = 1;
            const int lower = 20;
            const int upper = 55;
            const int size = 9;
            int[] keys = new int[number];
            int[] digs = new int[number];
            SudokuFounctionLibrary.generate(number, lower, upper, true, ref result);

            // test
            for (int i = 0; i < number; i++)
            {
                int[,] puzzle = result[i];

                // 测试挖空数目
                int count = 0;

                for (int j = 0; j < size; j++)
                {
                    for (int k = 0; k < size; k++)
                    {
                        if (puzzle[j, k] == 0)
                            count++;
                    }
                }
                digs[i] = count;
                Console.WriteLine("puzzle:");
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
                Table t = new Table();
                t.creat(puzzle);
                int r = t.solve();
                Console.WriteLine(r);

                //Assert.AreEqual(expected, real);

                // 测试唯一解
                Solver s = new Solver(puzzle);
                s.Solve();
                


                //Assert.AreEqual(expected, real);
            }
            Console.ReadLine();
        }

        /// <summary>
        /// 打印错误提示
        /// </summary>
        private void HelpMessageOutput()
        {
            System.Console.WriteLine("Usuage1: sudoku.exe -c <N>");
            System.Console.WriteLine("Generate <N> sudoku final puzzles.Results are stored in 'sudoku.txt'." +
                " <N> is an integer between 1" +
                " and 1000,000(1 and 1000,000 included) which controls" +
                " the number of sudoku's output.");
            System.Console.WriteLine();
            System.Console.WriteLine("Usuage2: sudoku.exe -s <absolute_path_of_puzzlefile>");
            System.Console.WriteLine("Read unfinished puzzles from a file and solve it." +
                " Results are stored in 'sudoku.txt'." +
                " <absolute_path_of_puzzlefile> is " +
                "a text file containing the puzzles of sudoku you" +
                " want to solve");
            System.Console.WriteLine();
            System.Console.WriteLine("Usuage3: sudoku.exe -n <N>");
            System.Console.WriteLine("Generate <N> sudoku game' puzzles. " +
                "Results are stored in 'sudoku.txt'." +
                "<N> is an integer between 1" +
                " and 10,000(1 and 10,000 included) which controls" +
                " the number of sudoku's output.");
            System.Console.WriteLine();
            System.Console.WriteLine();
            System.Console.WriteLine("Usuage4: sudoku.exe -n <N> -m <Level>");
            System.Console.WriteLine("<level> can be 1(Easy), 2(Meduim), 3(Hard) only.");
            System.Console.WriteLine();
            System.Console.WriteLine("Usuage5: sudoku.exe -n <N> -r <begin>~<end>");
            System.Console.WriteLine("Limit the number of digging holes into <begin>~<end>" +
                "<begin> and <end> are between 20 and 55(both included), " +
                "and <begin> have to be less than or equal to <end>.");
            System.Console.WriteLine();
            System.Console.WriteLine("Usuage6: sudoku.exe -n <N> -u");
            System.Console.WriteLine("Generate <N> sudoku game's puzzles. " +
                "Each of them has only one solution.");
            System.Console.WriteLine();
        }
    }
}
