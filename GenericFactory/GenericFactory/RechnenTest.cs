﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace GenericFactory
{
    [TestFixture]
    public class RechnenTest
    {
        [Test]
        public void Test()
        {
            var ccc=new rechnen();
            var dic=new Dictionary<double, double>();
            dic.Add(2,3);
            dic.Add(5,6);

            var result = ccc.Calculate(1, dic);
            Assert.AreEqual( 1, result["a0"]);
            Assert.AreEqual( 1, result["a1"]);
        }

        [Test]
        public void Test2()
        {
            var ccc = new rechnen();
            var dic = new Dictionary<double, double?>();
            dic.Add(0, null);
            dic.Add(1, null);
            dic.Add(2, 4);
            dic.Add(3, null);
            dic.Add(4, null);

            var result = ccc.FillAllBefore(dic);
            Assert.AreEqual(4, result[3]);
            Assert.AreEqual(4, result[4]);
            Assert.AreEqual(4, result[0]);
        }

        [Test]
        public void SimpleFiller()
        {
            var ccc = new rechnen();
            var dic = new Dictionary<double, double?>();
            dic.Add(0, 0);
            dic.Add(1, null);
            dic.Add(2, null);
            dic.Add(3, null);
            dic.Add(4, 8);

            var result = ccc.FillAll(dic);
            foreach (var item in result)
            {
                Console.WriteLine(item.Key + "  " +item.Value);
            }
            Assert.IsNotNull(result[1]);
        }

        [Test]
        public void HalfSimpleFiller()
        {
            var ccc = new rechnen();
            var dic = new Dictionary<double, double?>();
            dic.Add(0, 0);
            dic.Add(1, null); // 2
            dic.Add(2, null); // 4
            dic.Add(3, null); // 6
            dic.Add(4, 8);
            dic.Add(5, 10);
            dic.Add(6, null);
            dic.Add(7, null);
            dic.Add(8, null);
            dic.Add(9, 20);

            var result = ccc.FillAll(dic);
            foreach (var item in result)
            {
                Console.WriteLine(item.Key + "  " + item.Value);
            }
            Assert.AreEqual(2.0, result[1]);
            Assert.AreEqual(15.0,result[7]);
        }

        [Test]
        public void NotSimpleFiller()
        {
            var ccc = new rechnen();
            var dic = new Dictionary<double, double?>();
            dic.Add(0, 0);
            dic.Add(1, null); // 2
            dic.Add(2, null); // 4
            dic.Add(3, null); // 6
            dic.Add(4, 8);
            dic.Add(5, null);
            dic.Add(6, null);
            dic.Add(7, null);
            dic.Add(8, 24);
            dic.Add(9, null);
            dic.Add(10, 26);

            var result = ccc.FillAll(dic);
            foreach (var item in result)
            {
                Console.WriteLine(item.Key + "  " + item.Value);
            }
            Assert.AreEqual(2.0, result[1]);
            Assert.AreEqual(20.0, result[7]);
            Assert.AreEqual(25.0, result[9]);
        }


    }
}
