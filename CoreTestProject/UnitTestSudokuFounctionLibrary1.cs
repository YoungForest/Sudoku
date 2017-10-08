using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Core;
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
        public void TestValid(int[,] puzzle)
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
                    }
                    Assert.AreEqual(true, real);
                }
            }
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

            TestValid(grid);
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

            TestValid(puzzle);
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
                {4, 1, 3, 8, 2, 7, 6, 9, 5}
                };
            TestValid(puzzle);
        }
    }
}
