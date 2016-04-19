using System;
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
    }
}
