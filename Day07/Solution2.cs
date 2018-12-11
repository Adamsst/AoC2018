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
            var info = new List<KeyValuePair<string, string>>();
            List<string> answer = new List<string>();
            List<string> processed = new List<string>();
            List<string> letters = new List<string>();
            Dictionary<string, int> steps2 = new Dictionary<string, int>();
            int totalLines = 0;

            //string alphabet = "ABCDEF";
            string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            for (int i = 0; i < alphabet.Length; i++)
            {
                letters.Add(alphabet[i].ToString());
                // steps2.Add(alphabet[i].ToString(), i + 1);
                steps2.Add(alphabet[i].ToString(),60+ i + 1);
                totalLines++;
            }

            while ((line = file.ReadLine()) != null)
            {
                info.Add(new KeyValuePair<string, string>(line.Substring(5, 1), line.Substring(36, 1)));
            }

            string prev = "JMQZELVYXTIGPHFNSOADKWBRUC";
            //string prev = "CABFDE";

            List<string> worked = new List<string>();

            int sum = 1;
            int track = 0;

            while (sum > 0)
            {
                sum = 0;
                List<string> working = new List<string>();
                int x = 0;
                while(x < 5)
                {
                    foreach(char c in prev)
                    {
                        if (!worked.Contains(c.ToString()) && !working.Contains(c.ToString()))//not finsihed or used already
                        {
                            int use = 0;
                            foreach(var kvp in info)
                            {
                                if(kvp.Value == c.ToString())
                                {
                                    use++;
                                    if (worked.Contains(kvp.Key))
                                    {
                                        use--;
                                    }
                                }           
                            }  
                            if(use == 0)
                            {
                                working.Add(c.ToString());
                                break;
                            }
                        }
                    }
                    x++;
                }
                Console.Write("We Subtracted ");
                foreach(var s in working)
                {
                    Console.Write(s + " ");
                    steps2[s]--;
                }
                foreach (var s in steps2)
                {
                    sum += s.Value;
                    if(s.Value == 0)
                    {
                        worked.Add(s.Key);
                    }
                }
                Console.Write(" on track " + track);
                Console.WriteLine();
                track++;
            }

            Console.Write(track);

            Console.Read();
        }

    }
}