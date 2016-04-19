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

            var result = new Dictionary<double,double>();
            foreach (var item in dic)
            {
                result.Add(item.Key, (double)item.Value);
            } 
            return result;
        }

    }
}
