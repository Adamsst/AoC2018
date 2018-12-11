using System;//answer
using System.Collections.Generic;

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
            int totalLines = 0;

            string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            for(int i =0;  i<26; i++)
            {
                letters.Add(alphabet[i].ToString());
                totalLines++;
            }
            totalLines *= 3;//arbitrary

            while ((line = file.ReadLine()) != null)
            {
                info.Add(new KeyValuePair<string, string>(line.Substring(5, 1), line.Substring(36, 1)));
            }

            List<string> temp = new List<string>();
            for (int i = 0; i < info.Count; i++)
            {
                if (!temp.Contains(info[i].Key))
                {
                    temp.Add(info[i].Key);
                    bool remove = false;
                    foreach (var kvp in info)
                    {
                        if (kvp.Value == info[i].Key)
                        {
                            remove = true;
                        }
                    }
                    if (remove)
                    {
                        temp.Remove(info[i].Key);
                    }
                }
            }
            temp.Sort();
            answer.Add(temp[0]);
            temp.RemoveAt(0);

            int tracker = 0;
            while(tracker < totalLines)
            {
                for (int i = 0; i < letters.Count; i++)
                {
                    int add = 0;
                    bool isVal = false;
                    if (!answer.Contains(letters[i]))
                    {
                        foreach (var kvp in info)
                        {
                            if (kvp.Value == letters[i])
                            {
                                isVal = true;
                            }

                            foreach (var kvp2 in info)
                            {
                                if (kvp2.Value == letters[i])
                                {
                                    add++;
                                    if (answer.Contains(kvp2.Key))
                                    {
                                        add--;
                                    }
                                }
                            }
                        }
                    }
                    if (add == 0 && isVal)
                    {
                        answer.Add(letters[i]);
                        i = -1;
                    }
                    else if(i == 25 && temp.Count > 0)
                    {
                        answer.Add(temp[0]);
                        temp.RemoveAt(0);
                    }                   
                }
                tracker++;
            }


            foreach (string s in answer)
            {
                Console.Write(s);
            }

            Console.Read();
        }

    }
}
