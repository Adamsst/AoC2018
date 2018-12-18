using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AOC
{
    class Program
    {
        static int numPlayers = 465;//input
        static int numMarbles = 71498;//input
        static int nextPlayer = 1;
        static int nextMarble = 0;
        static Dictionary<int,int> playerScores = new Dictionary<int,int>();
        static List<int> marbles = new List<int>();
        static int position = 1;

        static void Main(string[] args)
        {
            marbles.Add(0);
            nextMarble++;
            marbles.Add(1);
            nextPlayer++;
            nextMarble++;
            marbles.Insert(position, 2);
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
                    if (position == marbles.Count - 1)
                    {
                        position = 1;
                    }
                    else
                    {
                        position += 2;
                    }
                    marbles.Insert(position, nextMarble);
                }            
                else
                {
                    playerScores[nextPlayer] += nextMarble;
                    int tempIndex = position - 7;
                    if(tempIndex >= 0)
                    {
                        playerScores[nextPlayer] += marbles[tempIndex];
                        marbles.RemoveAt(tempIndex);
                        position = tempIndex;
                    }
                    else
                    {
                        playerScores[nextPlayer] += marbles[(marbles.Count + tempIndex + 1)];
                        marbles.RemoveAt((marbles.Count + tempIndex + 1));
                        position = marbles.Count + tempIndex + 1;
                    }
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