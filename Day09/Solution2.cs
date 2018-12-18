using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AOC
{
    class Program
    {
        static int numPlayers = 465;//input
        static int numMarbles = 7149800;//input
        static int nextPlayer = 1;
        static int nextMarble = 0;
        static Dictionary<int,double> playerScores = new Dictionary<int, double>();
        static LinkedList<int> marbles = new LinkedList<int>();      

        static void Main(string[] args)
        {
            marbles.AddFirst(0);
            nextMarble++;
            var node = marbles.First;
            marbles.AddAfter(node,nextMarble);
            nextPlayer++;
            nextMarble++;
            marbles.AddAfter(node, 2);
            nextPlayer++;
            nextMarble++;
            
            for(int i = 1; i <= numPlayers; i++)
            {
                playerScores.Add(i, 0);
            }

            while (nextMarble <= numMarbles)
            {
                if (nextMarble % 23 != 0)
                {
                    if (node.Next == null)
                    {
                        node = marbles.First;
                    }
                    else 
                    {
                        node = node.Next;
                    }

                    if (node.Next == null)
                    {
                        node = marbles.First;
                    }
                    else
                    {
                        node = node.Next;
                    }
                    marbles.AddAfter(node, nextMarble);
                }            
                else
                {
                    playerScores[nextPlayer] += nextMarble;
                    for(int i = 0; i < 6; i++)
                    {
                        if(node.Previous != null)
                        {
                            node = node.Previous;
                        }
                        else
                        {
                            node = marbles.Last;
                        }
                    }
                    playerScores[nextPlayer] += node.Value;

                    LinkedListNode<int> remove = marbles.Find(node.Value);
					//This still runs to slow and I believe this "Find" is the culprit
                    node = remove.Previous;                        
                    marbles.Remove(remove);                    
                }
               
                nextMarble++;
                nextPlayer++;
                if (nextPlayer > numPlayers)
                {
                    nextPlayer = 1;
                }
            }

            Console.WriteLine(playerScores.Values.Max());
            Console.Read();
        }
    }
}
