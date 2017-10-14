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
<<<<<<< HEAD

            



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
=======
            if (args.Length == 2)
>>>>>>> 4523cf552ef7b325e2f88c756ec1837248f7c133
            {
                if (args[0] == "-c")
                {
                    try
                    {
                        int num = Int32.Parse(args[1]);
                        if (num > 1000000 || num < 1)
                        {
                            System.Console.WriteLine("Your input <N> is not " +
                                "between 0 and 1000,000");
                            return;
                        }

                        // begin generate
                        int[][,] results = null;
                        Core.SudokuFounctionLibrary.generate(num, ref results);
                        Core.SudokuFounctionLibrary.PrintPuzzleToFile(@"sudoku.txt", ref results);
                    }
                    catch (System.FormatException)
                    {
                        System.Console.WriteLine("Input <N> was not in a correct format(integer).");
                        return;
                    }
                }
                else if (args[0] == "-s")
                {
                    // sudoku solve
                    try
                    {
                        int[][,] initPuzzles = null;
                        Core.SudokuFounctionLibrary.ReadPuzzleFromFile(args[1], ref initPuzzles);
                        List<int[,]> results = new List<int[,]>();

                        for(int i = 0; i < initPuzzles.Length; i++)
                        {
                            int[,] result = null;
                            Core.SudokuFounctionLibrary.solve(initPuzzles[i], ref result);
                            results.Add(result);
                        }
                        var puzzlesArray = results.ToArray();
                        Core.SudokuFounctionLibrary.PrintPuzzleToFile(@"sudoku.txt", ref puzzlesArray);
                    }
                    catch (System.IO.FileNotFoundException ex)
                    {
                        System.Console.WriteLine(ex.Message);
                    }
                }
                else
                {
                    HelpMessageOutput();
                    return;
                }
            }
            else if (args.Length == 4 || args.Length == 5)
            {
                if (args[0] == "-n")
                {
                    try
                    {
                        int num = Int32.Parse(args[1]);
                        if (num > 1000 || num < 1)
                        {
                            System.Console.WriteLine("Your input <N> is not " +
                                "between 0 and 1,000");
                            HelpMessageOutput();
                            return;
                        }

                        if (args[2] == "-m" && args.Length == 4)
                        {
                            int mode = Int32.Parse(args[3]);
                            if (mode < 4 && mode > 0)
                            {
                                int[][,] results = null;
                                Core.SudokuFounctionLibrary.generate(num, mode, ref results);
                                Core.SudokuFounctionLibrary.PrintPuzzleToFile(@"sudoku.txt", ref results);
                            }
                            else
                            {
                                Console.WriteLine("<mode> has to be 1, 2 or 3");
                                HelpMessageOutput();
                                return;
                            }
                        }
                        else if (args[2] == "-r")
                        {
                            var bound = args[3];
                            var temp = bound.Split('~');
                            try
                            {
                                var lower = Int32.Parse(temp[0]);
                                var upper = Int32.Parse(temp[1]);
                                bool unique;
                                if (args.Length == 5 && args[4] == "-u")
                                    unique = true;
                                else
                                    unique = false;
                                int[][,] r = null;
                                Core.SudokuFounctionLibrary.generate(num, lower, upper, unique, ref r);
                                Core.SudokuFounctionLibrary.PrintPuzzleToFile(@"sudoku.txt", ref r);
                            }
                            catch (FormatException ex)
                            {
                                Console.WriteLine(ex);
                            }
                        }
                        else
                        {
                            HelpMessageOutput();
                            return;
                        }
                    }
                    catch (System.FormatException)
                    {
                        System.Console.WriteLine("Input <N> was not in a correct format(integer).");
                        return;
                    }
                }
            }
            else
            {
                System.Console.WriteLine("{0} arguments detected.", args.Length);
                HelpMessageOutput();
                return;
            }
        }

        /// <summary>
        /// 打印错误提示
        /// </summary>
        public static void HelpMessageOutput()
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
