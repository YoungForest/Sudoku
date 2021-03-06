﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class Solver
    {
        public int[,] puzzle;
        public int[,] solution = new int[9, 9];
        public const int SIZE = 8;
        public bool success = false;
        public int count = 0;

        public Solver(int[,] p)
        {
            puzzle = p;
        }

        public bool IsUniqueSolution()
        {
            return count == 1;
        }

        public void FillNextpuzzle(int i, int j)
        {
            if (puzzle[i, j] != 0)
            {
                if (i == SIZE && j == SIZE)
                {
                    success = true;
                    recordSolution();
                    count++;
                    if (count > 1)
                    {
                        throw new PuzzleCompleteException();
                    }
                }
                else
                {
                    int nexti = j == SIZE ? i + 1 : i;
                    int nextj = j == SIZE ? 0 : j + 1;
                    FillNextpuzzle(nexti, nextj);
                    return;
                }
                return;
            }

            var fillList = SudokuFounctionLibrary.GenerateFillList();

            fillList.ForEach(delegate (int item)
            {
                puzzle[i, j] = item;
                if (FillSuccess(i, j))
                {
                    if (i == SIZE && j == SIZE)
                    {
                        success = true;
                        count++;
                        recordSolution();
                        if (count > 1)
                        {
                            throw new PuzzleCompleteException();
                        }
                    }
                    else
                    {
                        int nexti = j == SIZE ? i + 1 : i;
                        int nextj = j == SIZE ? 0 : j + 1;
                        FillNextpuzzle(nexti, nextj);
                    }
                }
            });
            puzzle[i, j] = 0;
        }

        // 由于所填格子之后的数是定的，所以无法与Generater共用此函数
        public bool FillSuccess(int i, int j)
        {
            // check column
            for (int ii = 0; ii <= SIZE; ii++)
            {
                if (ii != i && puzzle[i, j] == puzzle[ii, j])
                    return false;
            }

            // check row
            for (int jj = 0; jj <= SIZE; jj++)
            {
                if (jj != j && puzzle[i, j] == puzzle[i, jj])
                    return false;
            }
            // check small puzzle
            int basei = i - i % 3;
            int basej = j - j % 3;
            for (int ii = basei; ii < basei + 3; ii++)
            {
                for (int jj = basej; jj < basej + 3; jj++)
                {
                    if (i != ii && j != jj && puzzle[i, j] == puzzle[ii, jj])
                        return false;
                }
            }

            return true;
        }

        public void Solve()
        {
            try
            {
                FillNextpuzzle(0, 0);
            }
            catch (PuzzleCompleteException ex)
            {
                System.Console.WriteLine(ex);
            }
        }
        public void recordSolution()
        {
            for (int i = 0; i < 9; i++)
                for (int j = 0; j < 9; j++)
                    solution[i, j] = puzzle[i, j];
        }
    }

    [Serializable]
    internal class PuzzleCompleteException : Exception
    {
        public PuzzleCompleteException()
        {
        }
    }
}