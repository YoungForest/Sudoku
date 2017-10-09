using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Core;

namespace CoreTestProject
{
    [TestClass]
    public class UnitTestSolver
    {
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

            var puzzle = new Solver(grid);

            Assert.AreEqual(true, puzzle.FillSuccess(2, 2));
        }

        [TestMethod]
        public void TestFillSuccess2()
        {
            int[,] grid = new int[,] {
                { 0, 6, 0, 4, 7, 3, 9, 5, 1},
                { 3, 0, 1, 9, 6, 0, 2, 7, 8},
                { 7, 9, 5, 8, 1, 2, 3, 6, 4},
                { 5, 7, 0, 6, 2, 1, 8, 3, 9},
                { 1, 3, 9, 5, 4, 0, 6, 0, 7},
                { 8, 2, 6, 3, 9, 7, 4, 1, 5},
                { 9, 1, 7, 2, 8, 6, 0, 4, 3},
                { 6, 8, 3, 1, 5, 4, 7, 9, 2},
                { 4, 5, 2, 7, 3, 9, 1, 8, 6}
            };

            grid[2, 2] = 4;

            var puzzle = new Solver(grid);

            Assert.AreEqual(false, puzzle.FillSuccess(2, 2));
        }
    }
}
