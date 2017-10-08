using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku
{
    class Program
    {
        static void Main(string[] args)
        {
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
