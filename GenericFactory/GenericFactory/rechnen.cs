using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericFactory
{
    public class rechnen
    {
        public IReadOnlyDictionary<string, double> Calculate(uint gradient, IReadOnlyDictionary<double, double> xy)
        {
            double m = 0, b = 0;
            if (gradient == 1 && xy.Count == 2)
            {
                var y0 = xy.First().Value;
                var x0 = xy.First().Key;
                var y1 = xy.Last().Value;
                var x1 = xy.Last().Key;
                // y1=m*(x1)+b
                // y1 = m*x1+b //y0=m*x0+b -> b=y0-(m*x0)
                // y1 = (m*x1)+y0-(m*x0)
                // y1-y0 = m(x1-x0)
                m = (y1 - y0)/(x1 - x0);
                Debug.WriteLine("m: " + m);
                b = y1 - x1*m;
                Debug.WriteLine("b: " +b);
                
            }
            var dic = new Dictionary<string, double> {{"a0", b}, {"a1", m}};
            return dic;
        }


        public IReadOnlyDictionary<double, double> FillAllBefore(IReadOnlyDictionary<double, double?> ToFill)
        {
            double firstValue = 0, lastValue = 0;
            var dic = new Dictionary<double, double?>((IDictionary<double, double?>) ToFill);

            // get first value in dic
            var count = ToFill.Count;
            for (double i=0;i< count; i++)
            {
                var helper = ToFill[i];
                if (helper != null)
                {
                    firstValue = (double)helper;
                    break;
                }
            }

            // fill all before first value
            for (double i = 0; i < count; i++)
            {
                var helper = ToFill[i];
                if (helper == null)
                {
                    dic[i] = firstValue;
                }
                if (helper != null)
                {
                    break;
                }
            }

            count = ToFill.Count - 1;
            // get lastvalue in dic
            for (double i = count; i >= 0; i--)
            {
                var helper = ToFill[i];
                if (helper != null)
                {
                    lastValue = (double)helper;
                    break;
                }
            }

            // fill all after last value
            for (double i = count; i >= 0; i--)
            {
                var helper = ToFill[i];
                if (helper == null)
                {
                    dic[i] = lastValue;
                }
                if (helper != null)
                {
                    break;
                }
            }

            return ReadOnlyDictionary(dic);
        }

        public IReadOnlyDictionary<double, double> FillAll(IReadOnlyDictionary<double, double?> ToFill)
        {
            var dic = new Dictionary<double, double?>((IDictionary<double, double?>)ToFill);
            if (ToFill.First().Value==null )
            {
                throw new Exception();
            }
            if (ToFill.Last().Value == null)
            {
                throw new Exception();
            }

            // get first value in dic
            var count = ToFill.Count;
            double? localFirst = null;
            double? localFirstKey=null;
            for (double i = 0; i < count; i++)
            {
                Console.WriteLine("i:" + i);
                if (ToFill[i]!=null && localFirst==null)
                { 
                    localFirst = ToFill[i];
                    localFirstKey = i;
                    continue;
                }
                if (ToFill[i] != null && localFirst != null)
                {
                    var localLast=ToFill[i];
                    double? localLastKey=i;
                    var ccc = new Dictionary<double, double?>(); // handle only the needed part of the dictionary
                    this.GetValue(localFirstKey, localLastKey, localFirst, localLast, ccc);
                    foreach (var item in ccc)
                    {
                        dic[item.Key] = item.Value;
                    }
                    localFirst = null; // reset for next part dictionary

                    // hint: have to use try get value, because it is possible to overrun the dictionary
                    double? oneStepBetweenHelper;
                    ToFill.TryGetValue(i + 1, out oneStepBetweenHelper);
                    // when the lastValue from one is the first of the other, we have to recount
                    if (oneStepBetweenHelper == null)
                    { 
                        i--; 
                    }
                }
            }

            return ReadOnlyDictionary(dic);
        }

        private void GetValue(double? localFirstKey, double? localLastKey, double? localFirst, double? localLast, Dictionary<double, double?> dic)
        {
            if (localFirstKey != null && localLastKey != null)
            {
                var calculateHelper = new Dictionary<double, double>();
                calculateHelper.Add((double) localFirstKey, localFirst.Value);
                calculateHelper.Add((double) localLastKey, localLast.Value);
                var res = this.Calculate(1, calculateHelper);
                // fill between
                var m = res["a1"];
                var b = res["a0"];
                for (double j = localFirstKey.Value + 1; j < localLastKey.Value; j++)
                {
                    dic[j] = j*m + b;
                }
            }
        }

        private IReadOnlyDictionary<double, double> ReadOnlyDictionary(Dictionary<double, double?> dic)
        {
            var result = new Dictionary<double, double>();
            foreach (var item in dic)
            {
                result.Add(item.Key, (double)item.Value);
            }
            return result;
        }
    }
}
