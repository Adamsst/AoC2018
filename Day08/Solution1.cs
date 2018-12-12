using System;
using System.Collections.Generic;
using System.Linq;

namespace testConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            string line = null;
            System.IO.StreamReader file = new System.IO.StreamReader("input.txt");
            List<int> input = new List<int>();
            List<int> childLeft = new List<int>();
            List<List<aoc>> tree = new List<List<aoc>>();
            int sum = 0;
            int i = 2;
            int level = 0;
            tree.Add(new List<aoc>());

            while ((line = file.ReadLine()) != null)
            {
                string[] temp = line.Split(' ');
                foreach(string s in temp)
                {
                    input.Add(Convert.ToInt32(s));
                }
            }
            tree[level].Add(new aoc(input[0], input[1]));
            childLeft.Add(input[0]);
            level++;
            childLeft.Add(0);
            tree.Add(new List<aoc>());
            

            while (i < input.Count)
            {
                if (childLeft[level-1] == 0)
                {
                    int t = i;
                    while (i < tree[level-1][tree[level-1].Count - 1].meta + t)
                    {
                        tree[level-1][tree[level-1].Count - 1].addEntry(input[i]);
                        i++;
                    }
                    level--;
                    if (level > 0)
                    {
                        childLeft[level - 1]--;
                    }                
                }
                else
                {
                    if (input[i] == 0)
                    {
                        tree[level].Add(new aoc(input[i], input[i + 1]));
                        i += 2;
                        int t = i;
                        while (i < tree[level][tree[level].Count - 1].meta + t)
                        {
                            tree[level][tree[level].Count - 1].addEntry(input[i]);
                            i++;
                        }
                        childLeft[level - 1]--;
                    }
                    else
                    {
                        tree[level].Add(new aoc(input[i], input[i + 1]));
                        if(childLeft.Count < level + 1)
                        {
                            childLeft.Add(input[i]);
                        }
                        else
                        {
                            childLeft[level] = input[i];
                        }                        
                        i += 2;
                        level++;
                        if(tree.Count < level+1)
                        {
                            tree.Add(new List<aoc>());
                        }
                    }
                }
            }

            for(int j = 0; j < tree.Count; j++)
            {
                for(int k = 0; k < tree[j].Count; k++)
                {
                    sum += tree[j][k].entries.Sum();
                }
            }


            Console.WriteLine(sum);
            Console.Read();
        }

        public class aoc
        {
            public int child;
            public int meta;
            public List<int> entries;

            public aoc(int c, int m)
            {
                child = c;
                meta = m;
                entries = new List<int>();
            }

            public void addEntry(int e)
            {
                entries.Add(e);
            }

        }
    }
}