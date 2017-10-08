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
    }
}
