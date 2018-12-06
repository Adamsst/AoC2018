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

            foreach(myPoint mp in pointList){
                minX = (mp.x < minX) ? mp.x : minX;
                maxX = (mp.x > maxX) ? mp.x : maxX;
                minY = (mp.y < minY) ? mp.y : minY;
                maxY = (mp.y > maxY) ? mp.y : maxY;
            }

            for(int i = 0; i < pointList.Count; i++)
            {
                pointList[i] = new myPoint(pointList[i].x - minX, pointList[i].y - minY);
                pointScore.Add(0);
            }
            maxX -= minX;
            maxY -= minY;
            minX = 0;
            minY = 0;

            for(int i = minX; i <= maxX; i++)
            {
                for(int j = minY; j <= maxY; j++)
                {
                    allPoints.Add(new myPoint(i,j));
                }
            }

            foreach(myPoint p in allPoints){
                int tempScore = 0;
                foreach(myPoint p2 in pointList)
                {
                    tempScore += GetMan(p, p2);
                }
                if(tempScore < 10000)
                {
                    maxscore++;
                }
            }

            Console.WriteLine(maxscore);
            Console.Read();
        }

        public static int GetMan(myPoint m1, myPoint m2)
        {
            return (Math.Abs(m1.x - m2.x) + Math.Abs(m1.y-m2.y));
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