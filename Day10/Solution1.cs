using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace PointPlotter
{
    public partial class Form1 : Form
    {
        public List<int[]> positions = new List<int[]>();
        public List<Point> velocity = new List<Point>();
        System.IO.StreamReader file = new System.IO.StreamReader("input.txt");
        string line = null;
        int x = 0;
        public Form1()
        {
            InitializeComponent();
            chart1.ChartAreas[0].Axes[0].Title = "X Title";
            chart1.ChartAreas[0].Axes[1].Title = "Y Title";
            chart1.ChartAreas[0].AxisY.IsReversed = true;
            chart1.Series[0].ChartType = SeriesChartType.Point;
            chart1.Series[0].MarkerStyle = MarkerStyle.Circle;
            chart1.Series[0].MarkerSize = 10;
            chart1.ChartAreas[0].AxisY.MajorGrid.LineWidth = 0;
            chart1.ChartAreas[0].AxisX.MajorGrid.LineWidth = 0;
            timer1.Interval = 10;  
        }

        private void button1_Click(object sender, EventArgs e)//Start
        {
            timer1.Start();
        }

        private void button2_Click(object sender, EventArgs e)//Stop
        {
            timer1.Stop();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            while ((line = file.ReadLine()) != null)
            {
                positions.Add(new int[2] { Convert.ToInt32(line.Substring(10, 6).Trim()), Convert.ToInt32(line.Substring(18, 6).Trim())});
                velocity.Add(new Point(Convert.ToInt32(line.Substring(36, 2).Trim()), Convert.ToInt32(line.Substring(39, 3).Trim())));
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            chart1.Series[0].Points.Clear();
            for (int i = 0; i < positions.Count; i++)
            {
                positions[i][0] += velocity[i].X;
                positions[i][1] += velocity[i].Y;
                chart1.Series[0].Points.Add(new DataPoint(positions[i][0], positions[i][1]));
            }
            x++;
            chart1.Refresh();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            chart1.Series[0].Points.Clear();
            for (int i = 0; i < positions.Count; i++)
            {
                positions[i][0] += velocity[i].X;
                positions[i][1] += velocity[i].Y;
                chart1.Series[0].Points.Add(new DataPoint(positions[i][0], positions[i][1]));
            }
            chart1.Refresh();
            x++;
        }
    }
}
