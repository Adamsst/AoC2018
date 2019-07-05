using System;
using System.Collections.Generic;
using System.Linq;

namespace testConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> input = new List<int> { 3, 2, 3, 0, 8, 1 };//input
            List<int> recipeResults = new List<int>();
            recipeResults.Add(3);
            recipeResults.Add(7);
            int elf1 = 0;
            int elf2 = 1;
            bool done = false;

            while(!done)
            {
                int result = recipeResults[elf1] + recipeResults[elf2];

                if(result >= 10)
                {
                    recipeResults.Add(1);
                    recipeResults.Add(result % 10);
                }
                else
                {
                    recipeResults.Add(result);
                }

                int temp = 1 + recipeResults[elf1];
                int j = 0;
                while(j < temp)
                {
                    if(elf1 == recipeResults.Count - 1)
                    {
                        elf1 = 0;
                    }
                    else
                    {
                        elf1++;
                    }
                    j++;
                }

                temp = 1 + recipeResults[elf2];
                j = 0;
                while (j < temp)
                {
                    if (elf2 == recipeResults.Count - 1)
                    {
                        elf2 = 0;
                    }
                    else
                    {
                        elf2++;
                    }
                    j++;
                }
                if(recipeResults.Count > 6)
                {
                    if (recipeResults.GetRange(recipeResults.Count - 7, 6).SequenceEqual(input))
                    {
                        done = true;
                    }
                }
            }

            Console.WriteLine(recipeResults.Count - 7);          
            Console.Read();
        }               
    }
}