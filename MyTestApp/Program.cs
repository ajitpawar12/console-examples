using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringContainsLogic
{
    class Program
    {
        static void Main(string[] args)
        {

            bool isIntString = "mm".All(char.IsDigit);

            Console.WriteLine("Welcome !");
            var str = "15264FHIL";
            var rlist = new List<string>();
            var strArry=new String[]
            {
                "1815HA",
                "TRACTOR_FMUC",
                "TRACTOR_FMUC",
                "1815HTL",
                "1816HI",
                "25554FSI",
                "TRACTOR_FMUC",
                "45504AFLSKB",
                "8560FLSK",
                "AMMW102",
                "FMUQTEST",
                "KBSSD4",
                "KMWML25",
                "KMWML278SK",
                "15264FHIDIR",
                "15264FHA",
                "15264FHAL",
                "15264FHI",
                "15264FHIL",
                "15264FHILB",
                "A",
                "A",
                "A"
            };

            //var strarry = strArry.Distinct().ToArray();

            Console.WriteLine("Main String = " + str);

            //exact match
            var exactMatch = strArry.Where(x => x==str).ToList();

            rlist.AddRange(exactMatch);

            //3 less char

            var threecharless = strArry.Where(x => x.StartsWith(str.Substring(0, str.Length - 3))).Except(exactMatch).ToList();

            Console.WriteLine("three char less = " + threecharless?.Count);


            //L
            var lcontain = str.Substring(str.Length - 3)?.Contains("L");
          
            var lconatainlist = threecharless.Where(x => x.Substring(x.Length - 3).Contains("L")).ToList();

            rlist.AddRange(lconatainlist);

            Console.WriteLine("L contains in "+ str.Substring(str.Length - 3) + "= "+ lcontain);

            //B
            var bcontain = str.Substring(str.Length - 3)?.Contains("B");

            var bconatainlist = threecharless.Where(x => x.Substring(x.Length - 3).Contains("B")).ToList();

            rlist.AddRange(bconatainlist.Except(lconatainlist));

            Console.WriteLine("B contains in " + str.Substring(str.Length - 3) + "= " + bcontain);

            //M
            var mcontain = str.Substring(str.Length - 3)?.Contains("M");

            var mconatainlist = threecharless.Where(x => x.Substring(x.Length - 3).Contains("M")).ToList();
            
            rlist.AddRange(mconatainlist.Except(lconatainlist).Except(bconatainlist));

            Console.WriteLine("M contains in " + str.Substring(str.Length - 3) + "= " + mcontain);

            //DIR
            var dircontain = str.Substring(str.Length - 3)?.Contains("DIR");

            var dircontainlist = threecharless.Where(x => x.Substring(x.Length - 3).Contains("DIR")).ToList();

            rlist.AddRange(dircontainlist.Except(lconatainlist).Except(bconatainlist).Except(mconatainlist));

            Console.WriteLine("DIR contains in " + str.Substring(str.Length - 3) + "= " + mcontain);

            //Others
            var othercontain = threecharless.Except(lconatainlist).Except(bconatainlist).Except(mconatainlist).Except(dircontainlist).ToList();
            

            for (int f = 0; f < othercontain.Count; f++)
            {
                rlist.Add(othercontain[f]);
            }

            var finalList = rlist;

            for (int i = 0; i < rlist.Count; i++)
            {
                Console.WriteLine("Count= " + i + " material number= " + rlist[i]);
            }
            
            Console.ReadKey();
        }
    }
}
