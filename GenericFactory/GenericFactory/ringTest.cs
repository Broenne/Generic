using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace GenericFactory
{
    [TestFixture]
    public class ringTest
    {
        [Test]
        public void Test()
        {
            var rin=new ring();
            var res = rin.Do();

            Assert.AreEqual(100, res.Count);

            List<ResultObject> hhh = res.Last().Value;
            //hhh[1].valueRes;
            Assert.GreaterOrEqual(90, hhh[1].valueRes);

            //foreach (KeyValuePair<int, List < ResultObject >> timestamp in res)
            //{
            //    Console.WriteLine("timestamp:" + timestamp.Key);
            //    Console.WriteLine("listcount:" + timestamp.Value.Count);
            //    foreach (ResultObject inner in timestamp.Value)
            //    {
            //        Console.WriteLine("sensor: " + inner.sensor);
            //        Console.WriteLine("inner.valueRes[0]: " + inner.valueRes[0]);
            //        //Console.WriteLine("sensor: " + inner.sensor + "  " + inner.valueRes);
            //    }
                
            //}
            

        }
    }
}
