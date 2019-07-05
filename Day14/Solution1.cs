using System;
using System.Collections.Generic;

namespace testConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            int input = 323081;//input
            List<int> recipeResults = new List<int>();
            recipeResults.Add(3);
            recipeResults.Add(7);
            int elf1 = 0;
            int elf2 = 1;

            while(recipeResults.Count < (input + 10))
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
            }
            for(int i = input; i < input + 10; i++)
            {
                Console.Write(recipeResults[i]);
            }
            
            Console.Read();
        }               
    }
}