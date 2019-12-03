using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarPatterns
{
    class Program
    {
        static void Main(string[] args)
        {

           for (int row = 8; row >= 1; --row)
            {
                for (int col = 1; col <= row; ++col)
                {
                    Console.Write("*");
                }

                Console.WriteLine();
            }

            for (int row = 1; row <= 8; ++row)
            {
                for (int col = 1; col <= row; ++col)
                {
                    Console.Write("*");
                }

                Console.WriteLine();
            }

            Console.ReadKey();

        }

       
    }
}
