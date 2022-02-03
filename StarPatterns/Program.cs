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
            //**
            //*
            Console.WriteLine("Pattern1\n");
            Pattern1();

            //*
            //**
            Console.WriteLine("Pattern2\n");
            Pattern2();

            // *
            //***
            // *
            Console.WriteLine("Pattern3\n");
            Pattern3();

            //  *
            // **
            //***
            Console.WriteLine("Pattern4\n");
            Pattern4();

            //* *
            //** **
            //*** ***
            Console.WriteLine("Pattern5\n");
            Pattern5();


            //******
            //*    *
            //******
            Console.WriteLine("Pattern6\n");
            Pattern6();

            Console.ReadKey();

        }

        public static void Pattern1()
        {
            for (int row = 5; row >= 1; --row)
            {
                for (int col = 1; col <= row; ++col)
                {
                    Console.Write("*");
                }

                Console.WriteLine();
            }

            
        }

        public static void Pattern2()
        {
            for (int row = 1; row <= 5; ++row)
            {
                for (int col = 1; col <= row; ++col)
                {
                    Console.Write("*");
                }

                Console.WriteLine();
            }
        }
       
        public static void Pattern3()
        {
            int number, i, k, count = 1;
            //Console.Write("Enter number of rows\n");
            //number = int.Parse(Console.ReadLine());
            number = 5;
            count = number - 1;
            for (k = 1; k <= number; k++)
            {
                for (i = 1; i <= count; i++)
                    Console.Write(" ");
                count--;
                for (i = 1; i <= 2 * k - 1; i++)
                    Console.Write("*");
                Console.WriteLine();
            }
            count = 1;
            for (k = 1; k <= number - 1; k++)
            {
                for (i = 1; i <= count; i++)
                    Console.Write(" ");
                count++;
                for (i = 1; i <= 2 * (number - k) - 1; i++)
                    Console.Write("*");
                Console.WriteLine();
            }
        }

        public static void Pattern4()
        {
            int val = 8;
            int i, j, k;
            for (i = 1; i <= val; i++)
            {
                for (j = 1; j <= val - i; j++)
                {
                    Console.Write(" ");
                }
                for (k = 1; k <= i; k++)
                {
                    Console.Write("*");
                }
                Console.WriteLine("");
            }
        }

        public static void Pattern5()
        {
            int number = 5;

            for (int i = 0; i < number; ++i)
            {
                for (int j = 0; j <= i; ++j)
                {
                    Console.Write("*");
                }

                if (i != number - 1)
                {
                    Console.Write(" ");
                }
                else
                {
                    Console.Write(" * ");
                }
                for (int j = 0; j <= i; ++j)
                {
                    Console.Write("*");
                }

                Console.WriteLine();
            }
        }

        public static void Pattern6()
        {
            int number = 7;

            for (int i = 0; i < number; i++)
            {
                if (i == 0 || i == 6)
                {
                    for (int j = 0; j < number; j++)
                    {
                        Console.Write("*");
                    }
                    Console.WriteLine();
                }
                if (i >= 1 && i <= 5)
                {
                    for (int j = 0; j < number; j++)
                    {
                        if (j == 0 || j == 6)
                        {
                            Console.Write("*");
                        }
                        else if (j >= 1 && j <= 5)
                        {
                            Console.Write(" ");
                        }
                    }
                    Console.WriteLine();
                }
            }

        }
    }
}
