using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicPrograms
{
    class Program
    {
        static void Main(string[] args)
        {
            //******* remove duplicate string *********//


            //Console.WriteLine("enter string : ");
            //var inputStr = Console.ReadLine();
            //var resStr = string.Empty;

            ////using simple way
            //for (var i = 0; i < inputStr.Length; i++)
            //{
            //    if (!resStr.Contains(inputStr[i]))
            //    {
            //        resStr += inputStr[i];
            //    }
            //}

            //using hasset

            //var uniqueChar = new HashSet<char>(inputStr);

            //foreach(var c in uniqueChar)
            //{
            //    resStr += c;
            //}

            //using linq

            //var uniqueCharArray = inputStr.ToCharArray().Distinct().ToArray();

            //resStr = new string(uniqueCharArray);

            //Console.WriteLine(resStr);

            //******* remove duplicate string *******//

            //****** star patterns ******//


            //for (int r = 5; r >= 1; r--)
            //{
            //    for (int c = 1; c <= r; c++)
            //    {
            //        Console.Write("*");
            //    }

            //    Console.WriteLine();
            //}

            //for (int r = 1; r <= 5; r++)
            //{
            //    for (int c = 1; c <= r; c++)
            //    {
            //        Console.Write("*");
            //    }

            //    Console.WriteLine();
            //}

            //int val = 5;
            //int i, j, k;

            //for (i = 1; i <= val; i++)
            //{
            //    for (j = 1; j <= val - i; j++)
            //    {
            //        Console.Write(" ");
            //    }

            //    for (k = 1; k <= i; k++)
            //    {
            //        Console.Write("*");
            //    }
            //    Console.WriteLine();
            //}

            //****** star patterns ******//


            //****** check number is even or odd ******//
            //Console.WriteLine("enter number : ");
            //var read=Console.ReadLine();

            //var num = int.Parse(read);
            //if (num % 2 == 0)
            //    Console.WriteLine("even number");
            //else
            //    Console.WriteLine("odd number");

            //****** prime number *******//

            Console.WriteLine("enter number : ");
            var read = Console.ReadLine();
            var num = int.Parse(read);

            int i = 0;
            for (i = 3; i < num; i++)
            {
                if (num % i == 0)
                {
                    Console.WriteLine("{0} is not a prime Number", num);
                    break;
                }
            }

            if (i == num)
            {
                Console.WriteLine("{0} is a prime Number", num);
            }

            Console.ReadKey();
        }
    }
}
