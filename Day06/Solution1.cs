using System;
using System.Collections.Generic;

namespace testConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            string line = null;
            System.IO.StreamReader file = new System.IO.StreamReader("input.txt");
            List<myPoint> pointList = new List<myPoint>();
            List<int> pointScore = new List<int>();
            List<myPoint> allPoints = new List<myPoint>();
            List<owner> allValues = new List<owner>();
            int minX, maxX, minY, maxY;
            int maxscore = 0;

            while ((line = file.ReadLine()) != null)
            {
                pointList.Add(new myPoint(Convert.ToInt32(line.Split(',')[0].Trim()), Convert.ToInt32(line.Split(',')[1].Trim())));
            }
            minX = pointList[0].x;
            maxX = pointList[0].x;
            minY = pointList[0].y;
            maxY = pointList[0].y;

            foreach (myPoint mp in pointList)
            {
                minX = (mp.x < minX) ? mp.x : minX;
                maxX = (mp.x > maxX) ? mp.x : maxX;
                minY = (mp.y < minY) ? mp.y : minY;
                maxY = (mp.y > maxY) ? mp.y : maxY;
            }

            for (int i = 0; i < pointList.Count; i++)//make upper left point 0,0
            {
                pointList[i] = new myPoint(pointList[i].x - minX, pointList[i].y - minY);
                pointScore.Add(0);
            }
            maxX -= minX;
            maxY -= minY;
            minX = 0;
            minY = 0;

            for (int i = minX; i <= maxX; i++)//set up board with min x and y at 0,0 max x and y depending on input
            {
                for (int j = minY; j <= maxY; j++)
                {
                    allPoints.Add(new myPoint(i, j));
                    allValues.Add(new owner(0, (maxX * maxY)));
                }
            }

            for (int i = 0; i < pointList.Count; i++)//labels each possible point with an owner, set to -1 if  > 1 owner
            {
                for (int j = 0; j < allPoints.Count; j++)
                {
                    if (GetMan(pointList[i], allPoints[j]) < allValues[j].mag)
                    {
                        allValues[j].mag = GetMan(pointList[i], allPoints[j]);
                        allValues[j].id = i;
                    }
                    else if (GetMan(pointList[i], allPoints[j]) == allValues[j].mag)
                    {
                        allValues[j].id = -1;
                    }
                }
            }

            for (int i = 0; i < pointList.Count; i++)//Counts the score for each owner in allValues
            {
                for (int j = 0; j < allValues.Count; j++)
                {
                    if (allValues[j].id == i)
                    {
                        pointScore[i]++;
                    }
                }
            }

            for (int k = 0; k < pointList.Count; k++)//This will set all edge case (infinite) owners to a score of -1
            {
                for (int i = minX; i <= maxX; i++)
                {
                    for (int j = minY; j <= maxY; j++)
                    {
                        if ((i == minX || i == (maxX)) || (j == minY || j == (maxY)))
                        {
                            if (allValues[i + (i * maxY) + j].id == k)//this little doodad is because indexed by integer instead of a point
                            {
                                pointScore[k] = -1;
                            }
                        }
                    }
                }
            }

            for (int i = 0; i < pointScore.Count; i++)//finds max score
            {
                if (pointScore[i] > maxscore)
                {
                    maxscore = pointScore[i];
                }
            }

            Console.WriteLine(maxscore);
            Console.Read();
        }

        public static int GetMan(myPoint m1, myPoint m2)//gets manhatten distance between 2 myPoints
        {
            return (Math.Abs(m1.x - m2.x) + Math.Abs(m1.y - m2.y));
        }
    }
}

public struct myPoint
{
    public int x, y;
    public myPoint(int X, int Y)
    {
        x = X;
        y = Y;
    }
}

public class owner//class so we can modify values
{
    public int id, mag;
    public owner(int i, int m)
    {
        id = i;
        mag = m;
    }
}