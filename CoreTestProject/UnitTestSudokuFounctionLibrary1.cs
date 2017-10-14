using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Core;
using SudokuData;
using System.Collections.Generic;
using System.Linq;

namespace CoreTestProject
{
    /// <summary>
    /// 对Core计算模块中SudokuFounctionLibrary类进行测试
    /// </summary>
    [TestClass]
    public class UnitTestSudokuFounctionLibrary1
    {
        /// <summary>
        /// 测试GenerateFillList方法，判断确实随机
        /// </summary>
        [TestMethod]
        public void TestGenerateFillList1()
        {
            var list1 = SudokuFounctionLibrary.GenerateFillList();
            var list2 = SudokuFounctionLibrary.GenerateFillList();

            Assert.AreEqual(false, list1.SequenceEqual(list2));

            var expect = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            Assert.AreEqual(false, expect.SequenceEqual(list1));
            Assert.AreEqual(false, expect.SequenceEqual(list2));

            list1.Sort();
            list2.Sort();

            Assert.AreEqual(true, expect.SequenceEqual(list1));
            Assert.AreEqual(true, expect.SequenceEqual(list2));
        }

        /// <summary>
        /// 测试一个数独的有效性
        /// </summary>
        public bool TestValid(int[,] puzzle)
        {
            const int SIZE = 9;
            for (int i = 0; i < SIZE; i++)
            {
                for (int j = 0; j < SIZE; j++)
                {
                    var real = SudokuFounctionLibrary.FillSuccess(puzzle, i, j);
                    if (real == false)
                    {
                        using (System.IO.StreamWriter outputfile =
                 new System.IO.StreamWriter(@"ErrorLog.txt", true))
                        {
                            outputfile.Write("i = {0}, j = {1};", i, j);
                        }
                        return false;
                    }
                }
            }
            return true;
        }

        [TestMethod]
        public void TestFillSuccess1()
        {
            int[,] grid = new int[,] {
                { 2, 6, 8, 4, 7, 3, 9, 5, 1},
                { 3, 4, 1, 9, 6, 5, 2, 7, 8},
                { 7, 9, 5, 8, 1, 2, 3, 6, 4},
                { 5, 7, 4, 6, 2, 1, 8, 3, 9},
                { 1, 3, 9, 5, 4, 8, 6, 2, 7},
                { 8, 2, 6, 3, 9, 7, 4, 1, 5},
                { 9, 1, 7, 2, 8, 6, 5, 4, 3},
                { 6, 8, 3, 1, 5, 4, 7, 9, 2},
                { 4, 5, 2, 7, 3, 9, 1, 8, 6}
            };

            Assert.AreEqual(true, SudokuFounctionLibrary.FillSuccess(grid, 2, 2));
        }

        [TestMethod]
        public void TestFillSuccess2()
        {
            int[,] grid = new int[,] {
                { 2, 6, 8, 4, 7, 3, 9, 5, 1},
                { 3, 4, 1, 9, 6, 5, 2, 7, 8},
                { 7, 9, 5, 8, 1, 2, 3, 6, 4},
                { 5, 7, 4, 6, 2, 1, 8, 3, 9},
                { 1, 3, 9, 5, 4, 8, 6, 2, 7},
                { 8, 2, 6, 3, 9, 7, 4, 1, 5},
                { 9, 1, 7, 2, 8, 6, 5, 4, 3},
                { 6, 8, 3, 1, 5, 4, 7, 9, 2},
                { 4, 5, 2, 7, 3, 9, 1, 8, 6}
            };

            grid[2, 2] = 4;
            Assert.AreEqual(false, SudokuFounctionLibrary.FillSuccess(grid, 2, 2));
        }

        [TestMethod]
        public void TestSudokuValid1()
        {
            int[,] grid = new int[,] {
                { 2, 6, 8, 4, 7, 3, 9, 5, 1},
                { 3, 4, 1, 9, 6, 5, 2, 7, 8},
                { 7, 9, 5, 8, 1, 2, 3, 6, 4},
                { 5, 7, 4, 6, 2, 1, 8, 3, 9},
                { 1, 3, 9, 5, 4, 8, 6, 2, 7},
                { 8, 2, 6, 3, 9, 7, 4, 1, 5},
                { 9, 1, 7, 2, 8, 6, 5, 4, 3},
                { 6, 8, 3, 1, 5, 4, 7, 9, 2},
                { 4, 5, 2, 7, 3, 9, 1, 8, 6}
            };

            Assert.AreEqual(true, TestValid(grid));
        }

        [TestMethod]
        public void TestSudokuValid2()
        {
            int[,] puzzle = {
                {5, 8, 1, 2, 7, 3, 9, 6, 4},
                {9, 6, 4, 1, 5, 8, 3, 2, 7},
                {2, 3, 7, 6, 4, 9, 8, 5, 1},
                {1, 7, 9, 3, 6, 5, 2, 4, 8},
                {3, 5, 2, 9, 8, 4, 7, 1, 6},
                {6, 4, 8, 7, 1, 2, 5, 3, 9},
                {8, 9, 6, 5, 2, 1, 4, 7, 3},
                {4, 1, 5, 8, 3, 7, 6, 9, 2},
                {7, 2, 3, 4, 9, 6, 1, 8, 5}
                };

            Assert.AreEqual(true, TestValid(puzzle));
        }

        [TestMethod]
        public void TestSudokuValid3()
        {
            int[,] puzzle = {
                {5, 8, 1, 2, 7, 3, 9, 6, 4},
                {9, 6, 4, 1, 5, 8, 3, 2, 7},
                {2, 3, 7, 6, 4, 9, 8, 5, 1},
                {1, 7, 9, 3, 6, 5, 2, 4, 8},
                {3, 5, 2, 9, 8, 4, 7, 1, 6},
                {6, 4, 8, 7, 1, 2, 5, 3, 9},
                {8, 9, 6, 5, 3, 1, 4, 7, 2},
                {7, 2, 5, 4, 9, 6, 1, 8, 3},
                {4, 1, 3, 8, 2, 7, 6, 2, 5}
                };

            Assert.AreEqual(false, TestValid(puzzle));
        }

        /// <summary>
        /// 测试接口1 void generate(int number, ref int[][,] result) 的正确性
        /// </summary>
        [TestMethod]
        public void TestGenerate1()
        {
            const int number = 10;
            int[][,] result = new int[number][,];

            SudokuFounctionLibrary.generate(number, ref result);

            for (int i = 0; i < number; i++)
            {
                Assert.AreEqual(true, TestValid(result[i]));
            }
        }

        /// <summary>
        /// 测试 void generate(int number, int lower, int upper, bool unique, ref int[][,] result)接口
        /// </summary>
        [TestMethod]
        public void TestGenerateUnique()
        {
            int[][,] result = null;
            const int number =20;
            const int lower = 20;
            const int upper = 55;
            const int size = 9;
            int[] keys = new int[number];
            int[] digs = new int[number];
            SudokuFounctionLibrary.generate(number, lower, upper, true, ref result);

            // test
            for (int i = 0; i < number; i++)
            {
                int [,] puzzle = result[i];

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
                bool real = count <= upper && count >= lower;
                bool expected = true;

                //Assert.AreEqual(expected, real);

                // 测试唯一解
                Solver s = new Solver(puzzle);
                s.Solve();
                real = s.IsUniqueSolution();

                Assert.AreEqual(expected, real);
                /*Table t = new Table();
                t.creat(puzzle);
                keys[i] = t.solve();
                Assert.AreEqual(1, keys[i]);*/

            }
            //Assert.AreEqual(1, keys[0]);
            //Assert.AreEqual(true, digs[0] <= upper && digs[0] >= lower);

        }

        
    }
}
