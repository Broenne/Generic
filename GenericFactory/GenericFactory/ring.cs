using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework.Internal;

namespace GenericFactory
{
    public class ring
    {
        public Dictionary<int, List<ResultObject>> Do()
        {
            var one = new data[100];
            var two = new data[100];
            var random = new Random();
            int i = 0;
            for (int h = 0; i < 100; h++)
            {
                one[h] = new data();
                two[h] = new data();
                var timestamp = DateTime.Now.Millisecond;
                one[h].Time = timestamp;
                one[h].randomValue = random.Next(5, 100);
                if (i > 10)
                {
                    two[i].Time = timestamp;
                    two[i].randomValue = random.Next(90, 100);
                }
                Thread.Sleep(1);
                i++;
            }

            var dic = new Dictionary<int, data[]>();
            dic.Add(0, one);
            dic.Add(1, two);



            // key ist timestamp
            var result = new Dictionary<int, List<ResultObject>>();
            // get all timestamps and add to result
            data[] first = dic[0];
            for(int j = 0; j < 100; j++)
            {
                var timeSt = first[j].Time;
                result.Add(timeSt, new List<ResultObject>());
            }

            // von jedem erstmal die werte holen und sensoren
            var sensorList=new List<ResultObjectHelper>();
            //foreach (var sensor in dic)
            //{
                int hh = 0;
                foreach (var VARIABLE in dic)
                {
                    for(int u = 0; u < 100 ; u++)
                    { 
                        var res = new ResultObjectHelper();
                        res.sensor = VARIABLE.Key;
                        res.valueRes = VARIABLE.Value[u].randomValue;
                        res.Time = VARIABLE.Value[u].Time;
                        hh++;
                        sensorList.Add(res);
                    }
                }
            //}

            foreach (var VARIABLE in result)
            {
                int timestamp = VARIABLE.Key;
                for (int j = first.Length-1; j > 0; j--)
                {
                    if (timestamp == sensorList[j].Time)
                    {
                        var obj=new ResultObject();
                        obj.sensor = sensorList[j].sensor;
                        obj.valueRes = sensorList[j].valueRes;
                        var resList=result[timestamp];// = obj;
                        resList.Add(obj);
                    }
                }
            }
            
            return result;
        }
    }

    public class ResultObject
    {
        public int sensor;
        public double valueRes;
    }

    public class ResultObjectHelper
    {
        public int Time;
        public int sensor;
        public double valueRes;
    }

    public class data
    {
        public int Time;
        public double randomValue;
    }
}
