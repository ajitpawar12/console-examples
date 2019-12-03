using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestPassword
{
    class Program
    {
        static void Main(string[] args)
        {
            var str=SHA.GenerateSHA256String("igb01@123");
            Console.WriteLine(str);

            var str2 = SHA.GenerateSHA512String("igb01@123");
            Console.WriteLine(str2);
            Console.ReadKey();
        }
    }
}
