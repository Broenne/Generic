using System;
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
    }
}
