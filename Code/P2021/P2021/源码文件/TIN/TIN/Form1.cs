using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Collections;
using System.Windows.Forms.DataVisualization.Charting;

namespace TIN
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        double h0,He,VZ,wazong,tianzong;
        int v,bbb=0,aaa=0;
        private void 不规则格网体积计算及图形生成ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripButton3_Click( sender,  e);
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            打开OToolStripMenuItem_Click(sender, e);
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            保存报告ToolStripMenuItem_Click( sender,  e);
        }

        private void 数据ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 0;
        }

        private void 凸包多边形ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 1;

        }

        private void 报告ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = report();
            tabControl1.SelectedIndex = 2;

        }

        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 1;
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            chart1.Width = Convert.ToInt32(chart1.Width / 1.2);
            chart1.Height = Convert.ToInt32(chart1.Height / 1.2);

        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            chart1.Width = Convert.ToInt32(chart1.Width * 1.2);
            chart1.Height = Convert.ToInt32(chart1.Height * 1.2);
        }

        private void 打开OToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog shuju = new OpenFileDialog();//导入数据
            shuju.Filter = "文本文件|*.txt";
            shuju.ShowDialog();
            string path = shuju.FileName;
            if (path == "")
            {
                return;
            }
            string[] str = File.ReadAllLines(path, Encoding.Default);
            string[] A = str[0].Split(new char[] { ',' });
            h0 = double.Parse(A[1]);
            toolStripTextBox1.Text = h0.ToString();
            for (int i = 2; i < str.Length; i++)
            {
                string[] B = str[i].Split(new char[] { ',' });
                dataGridView1.Rows.Add(B[0], B[1], B[2], B[3]);
            }

        }
        double[] Vfill = new double[100000];
        double[] Vcut = new double[100000];
        double[] zong = new double[100000];
            ArrayList T1 = new ArrayList();
            ArrayList T2 = new ArrayList();
            ArrayList S = new ArrayList();

        private void 保存报告ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "文本文件|*.txt";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                StreamWriter writer = new StreamWriter(saveFileDialog1.FileName);
                writer.Write(report());
                writer.Close();

            }
        }

        private void 保存图形ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "图形文件|*.Jpeg";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {

                chart1.SaveImage(saveFileDialog1.FileName, ChartImageFormat.Jpeg);
            }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            int row = dataGridView1.Rows.Count - 1;
            double[] x = new double[row];//起算数据信息
            double[] y = new double[row];
            double[] h = new double[row];
            Class1.Point[] points = new Class1.Point[row + 4];
            for (int i = 0; i < row; i++)
            {
                points[i].id = dataGridView1.Rows[i].Cells[0].Value.ToString();
                points[i].x = double.Parse(dataGridView1.Rows[i].Cells[1].Value.ToString());
                points[i].y = double.Parse(dataGridView1.Rows[i].Cells[2].Value.ToString());
                points[i].h = double.Parse(dataGridView1.Rows[i].Cells[3].Value.ToString());

                x[i] = double.Parse(dataGridView1.Rows[i].Cells[1].Value.ToString());
                y[i] = double.Parse(dataGridView1.Rows[i].Cells[2].Value.ToString());
                h[i] = double.Parse(dataGridView1.Rows[i].Cells[3].Value.ToString());

            }
            Class1.Point[] point = new Class1.Point[4];
            double xmin, xmax, ymin, ymax;
            xmin = x.Min();
            xmax = x.Max();
            ymin = y.Min();
            ymax = y.Max();
            point[0].id = "p01";
            point[0].x = xmin - 1;
            point[0].y = ymin - 1;
            point[1].id = "p02";
            point[1].x = xmin - 1;
            point[1].y = ymax + 1;
            point[2].id = "p03";
            point[2].x = xmax + 1;
            point[2].y = ymax + 1;
            point[3].id = "p04";
            point[3].x = xmax + 1;
            point[3].y = ymin - 1;
            point.CopyTo(points, points.Length - 4);
            Class1.Tri tri = new Class1.Tri();
            tri.A = points.Length - 4;
            tri.B = points.Length - 2;
            tri.C = points.Length - 3;
            T1.Add(tri);
            tri = new Class1.Tri();
            tri.A = points.Length - 2;
            tri.B = points.Length - 1;
            tri.C = points.Length - 4;
            T1.Add(tri);

            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < T1.Count; j++)
                {
                    double x0, y0, R, r;
                    Class1.Tri tri1 = (Class1.Tri)T1[j];
                     x0 = ((points[tri1.B].y - points[tri1.A].y) * (points[tri1.C].y * points[tri1.C].y - points[tri1.A].y * points[tri1.A].y + points[tri1.C].x * points[tri1.C].x - points[tri1.A].x * points[tri1.A].x)
                        - (points[tri1.C].y - points[tri1.A].y) * (points[tri1.B].y * points[tri1.B].y - points[tri1.A].y * points[tri1.A].y + points[tri1.B].x * points[tri1.B].x - points[tri1.A].x * points[tri1.A].x))
                        / (2 * (points[tri1.C].x - points[tri1.A].x) * (points[tri1.B].y - points[tri1.A].y) - 2 * (points[tri1.B].x - points[tri1.A].x) * (points[tri1.C].y - points[tri1.A].y));

                     y0 = ((points[tri1.B].x - points[tri1.A].x) * (points[tri1.C].x * points[tri1.C].x - points[tri1.A].x * points[tri1.A].x + points[tri1.C].y * points[tri1.C].y - points[tri1.A].y * points[tri1.A].y)
                       - (points[tri1.C].x - points[tri1.A].x) * (points[tri1.B].x * points[tri1.B].x - points[tri1.A].x * points[tri1.A].x + points[tri1.B].y * points[tri1.B].y - points[tri1.A].y * points[tri1.A].y))
                       / (2 * (points[tri1.C].y - points[tri1.A].y) * (points[tri1.B].x - points[tri1.A].x) - 2 * (points[tri1.B].y - points[tri1.A].y) * (points[tri1.C].x - points[tri1.A].x));
                     r = Class1.juli(x0, points[tri1.B].x, y0, points[tri1.B].y);
                     R = Class1.juli(x0, points[i].x, y0, points[i].y);
                    if (R < r)
                    {
                        T2.Add(tri1);
                        T1.RemoveAt(j);
                        j--;
                    }
                }
                for (int k = 0; k < T2.Count; k++)
                {
                    Class1.Tri tri1 = new Class1.Tri();
                    tri1 = (Class1.Tri)T2[k];
                    Class1.Edge edge1 = new Class1.Edge();                 
                    Class1.Edge edge2 = new Class1.Edge();
                    Class1.Edge edge3 = new Class1.Edge();
                    edge1.strat = tri1.A;
                    edge1.end = tri1.B;
                    S.Add(edge1);
                    edge2.strat = tri1.B;
                    edge2.end = tri1.C;
                    S.Add(edge2);
                    edge3.strat = tri1.C;
                    edge3.end = tri1.A;
                    S.Add(edge3);
                }
                T2.Clear();
                for (int m = 0; m < S.Count-1; m++)
                {
                    int SSS = 0;
                    for (int n = m+1; n < S.Count; n++)
                    {
                        Class1.Edge edge1 = new Class1.Edge();
                        Class1.Edge edge2 = new Class1.Edge();
                        edge1 = (Class1.Edge)S[m];
                        edge2 = (Class1.Edge)S[n];
                        if ((edge1.strat == edge2.strat && edge1.end == edge2.end) || (edge1.strat == edge2.end && edge1.end == edge2.strat))
                        {
                            SSS = 1;
                            S.RemoveAt(n);
                            n--;
                        }                     
                    }
                    if (SSS == 1)
                    {
                        S.RemoveAt(m);
                        m--;
                    }
                }
                for (int k = 0; k < S.Count; k++)
                {
                    Class1.Tri tri1 = new Class1.Tri();
                    Class1.Edge edge1 = new Class1.Edge();
                    edge1 = (Class1.Edge)S[k];
                    tri1.A = i;
                    tri1.B = edge1.strat;
                    tri1.C = edge1.end;
                    T1.Add(tri1);
                }
               S.Clear();
               
            }

            for (int k = 0; k < T1.Count; k++)
            {
                Class1.Tri tri1 = new Class1.Tri();
                tri1 = (Class1.Tri)T1[k];
                Class1.Edge edge1 = new Class1.Edge();
                double p01, p02, p03, p04;
                p01 = points.Length - 4;
                p02 = points.Length - 3;
                p03 = points.Length - 2;
                p04 = points.Length - 1;
                if (tri1.A == p01 || tri1.A == p02 || tri1.A == p03 || tri1.A == p04 || tri1.B == p01 || tri1.B == p02 || tri1.B == p03 || tri1.B == p04 || tri1.C == p01 || tri1.C == p02 || tri1.C == p03 || tri1.C == p04)
                {
                    T1.RemoveAt(k);
                    k--;
                }
            }



            ////////////////////////体积计算
            double[] SI = new double[T1.Count];
            double[] hi = new double[T1.Count];
            double[] up = new double[T1.Count];
            double[] VV = new double[T1.Count];
            for (int i = 0; i < T1.Count; i++)
            {
                Class1.Tri tri1 = new Class1.Tri();
                tri1 = (Class1.Tri)T1[i];
                SI[i] = Math.Abs((points[tri1.B].x - points[tri1.A].x) * (points[tri1.C].y - points[tri1.A].y)
                    - (points[tri1.C].x - points[tri1.A].x) * (points[tri1.B].y - points[tri1.A].y)) / 2;
                hi[i] = (points[tri1.A].h + points[tri1.B].h + points[tri1.C].h) / 3;
                up[i] = SI[i] * hi[i];
            }
            He = up.Sum() / SI.Sum();
            toolStripTextBox2.Text = He.ToString();
            for (int i = 0; i < T1.Count; i++)
            {
                Class1.Tri tri1 = new Class1.Tri();
                tri1 = (Class1.Tri)T1[i];
                SI[i] = Math.Abs((points[tri1.B].x - points[tri1.A].x) * (points[tri1.C].y - points[tri1.A].y)
                    - (points[tri1.C].x - points[tri1.A].x) * (points[tri1.B].y - points[tri1.A].y)) / 2;
                hi[i] = (points[tri1.A].h + points[tri1.B].h + points[tri1.C].h) / 3;
                if (points[tri1.A].h < h0 && points[tri1.B].h < h0 && points[tri1.C].h < h0)
                {
                    Vfill[i] = SI[i] * hi[i];
                    aaa=aaa+1;
                }
                if (points[tri1.A].h > h0 && points[tri1.B].h > h0 && points[tri1.C].h > h0)
                {
                    Vcut[i] = SI[i] * hi[i];
                    bbb=bbb+1;
                }
                if (points[tri1.A].h > h0 && points[tri1.B].h < h0 && points[tri1.C].h < h0)
                {
                    double X1 = points[tri1.A].x + Math.Abs((h0 - points[tri1.A].h) / (points[tri1.B].h - points[tri1.A].h))
                        * (points[tri1.B].x - points[tri1.A].x);
                    double Y1 = points[tri1.A].y + Math.Abs((h0 - points[tri1.A].h) / (points[tri1.B].h - points[tri1.A].h))
                        * (points[tri1.B].y - points[tri1.A].y);
                    double X2 = points[tri1.A].x + Math.Abs((h0 - points[tri1.A].h) / (points[tri1.B].h - points[tri1.A].h))
                        * (points[tri1.B].x - points[tri1.A].x);
                    double Y2 = points[tri1.A].y + Math.Abs((h0 - points[tri1.A].h) / (points[tri1.B].h - points[tri1.A].h))
                        * (points[tri1.B].y - points[tri1.A].y);
                    double S0 = Math.Abs((X1 - points[tri1.A].x) * (Y2 - points[tri1.A].y) - (X2 - points[tri1.A].x)
                        * (Y1 - points[tri1.A].x)) / 2;

                    Vcut[i] = S0 * ((points[tri1.A].h + h0 + h0) / 3 - h0);
                    Vfill[i] = (SI[i] - S0) * ((points[tri1.B].h + points[tri1.C].h + h0 + h0) / 4 - h0);
                }

                if (points[tri1.B].h > h0 && points[tri1.A].h < h0 && points[tri1.C].h < h0)
                {
                    double X1 = points[tri1.B].x + Math.Abs((h0 - points[tri1.B].h) / (points[tri1.A].h - points[tri1.B].h))
                        * (points[tri1.A].x - points[tri1.B].x);
                    double Y1 = points[tri1.B].y + Math.Abs((h0 - points[tri1.B].h) / (points[tri1.A].h - points[tri1.B].h))
                        * (points[tri1.A].y - points[tri1.B].y);
                    double X2 = points[tri1.B].x + Math.Abs((h0 - points[tri1.B].h) / (points[tri1.A].h - points[tri1.B].h))
                        * (points[tri1.A].x - points[tri1.B].x);
                    double Y2 = points[tri1.B].y + Math.Abs((h0 - points[tri1.B].h) / (points[tri1.A].h - points[tri1.B].h))
                        * (points[tri1.A].y - points[tri1.B].y);
                    double S0 = Math.Abs((X1 - points[tri1.B].x) * (Y2 - points[tri1.B].y) - (X2 - points[tri1.B].x)
                        * (Y1 - points[tri1.B].x)) / 2;

                    Vcut[i] = S0 * ((points[tri1.B].h + h0 + h0) / 3 - h0);
                    Vfill[i] = (SI[i] - S0) * ((points[tri1.A].h + points[tri1.C].h + h0 + h0) / 4 - h0);
                }

                if (points[tri1.C].h > h0 && points[tri1.B].h < h0 && points[tri1.A].h < h0)
                {
                    double X1 = points[tri1.C].x + Math.Abs((h0 - points[tri1.C].h) / (points[tri1.B].h - points[tri1.C].h))
                        * (points[tri1.B].x - points[tri1.C].x);
                    double Y1 = points[tri1.C].y + Math.Abs((h0 - points[tri1.C].h) / (points[tri1.B].h - points[tri1.C].h))
                        * (points[tri1.B].y - points[tri1.C].y);
                    double X2 = points[tri1.C].x + Math.Abs((h0 - points[tri1.C].h) / (points[tri1.B].h - points[tri1.C].h))
                        * (points[tri1.B].x - points[tri1.C].x);
                    double Y2 = points[tri1.C].y + Math.Abs((h0 - points[tri1.C].h) / (points[tri1.B].h - points[tri1.C].h))
                        * (points[tri1.B].y - points[tri1.C].y);
                    double S0 = Math.Abs((X1 - points[tri1.C].x) * (Y2 - points[tri1.C].y) - (X2 - points[tri1.C].x)
                        * (Y1 - points[tri1.C].x)) / 2;

                    Vcut[i] = S0 * ((points[tri1.C].h + h0 + h0) / 3 - h0);
                    Vfill[i] = (SI[i] - S0) * ((points[tri1.B].h + points[tri1.A].h + h0 + h0) / 4 - h0);
                }





                if (points[tri1.A].h < h0 && points[tri1.B].h > h0 && points[tri1.C].h > h0)
                {
                    double X1 = points[tri1.A].x + Math.Abs((h0 - points[tri1.A].h) / (points[tri1.B].h - points[tri1.A].h))
                        * (points[tri1.B].x - points[tri1.A].x);
                    double Y1 = points[tri1.A].y + Math.Abs((h0 - points[tri1.A].h) / (points[tri1.B].h - points[tri1.A].h))
                        * (points[tri1.B].y - points[tri1.A].y);
                    double X2 = points[tri1.A].x + Math.Abs((h0 - points[tri1.A].h) / (points[tri1.B].h - points[tri1.A].h))
                        * (points[tri1.B].x - points[tri1.A].x);
                    double Y2 = points[tri1.A].y + Math.Abs((h0 - points[tri1.A].h) / (points[tri1.B].h - points[tri1.A].h))
                        * (points[tri1.B].y - points[tri1.A].y);
                    double S0 = Math.Abs((X1 - points[tri1.A].x) * (Y2 - points[tri1.A].y) - (X2 - points[tri1.A].x)
                        * (Y1 - points[tri1.A].x)) / 2;

                    Vfill[i] = S0 * ((points[tri1.A].h + h0 + h0) / 3 - h0);
                    Vcut[i] = (SI[i] - S0) * ((points[tri1.B].h + points[tri1.C].h + h0 + h0) / 4 - h0);
                }

                if (points[tri1.B].h < h0 && points[tri1.A].h > h0 && points[tri1.C].h > h0)
                {
                    double X1 = points[tri1.B].x + Math.Abs((h0 - points[tri1.B].h) / (points[tri1.A].h - points[tri1.B].h))
                        * (points[tri1.A].x - points[tri1.B].x);
                    double Y1 = points[tri1.B].y + Math.Abs((h0 - points[tri1.B].h) / (points[tri1.A].h - points[tri1.B].h))
                        * (points[tri1.A].y - points[tri1.B].y);
                    double X2 = points[tri1.B].x + Math.Abs((h0 - points[tri1.B].h) / (points[tri1.A].h - points[tri1.B].h))
                        * (points[tri1.A].x - points[tri1.B].x);
                    double Y2 = points[tri1.B].y + Math.Abs((h0 - points[tri1.B].h) / (points[tri1.A].h - points[tri1.B].h))
                        * (points[tri1.A].y - points[tri1.B].y);
                    double S0 = Math.Abs((X1 - points[tri1.B].x) * (Y2 - points[tri1.B].y) - (X2 - points[tri1.B].x)
                        * (Y1 - points[tri1.B].x)) / 2;

                    Vfill[i] = S0 * ((points[tri1.B].h + h0 + h0) / 3 - h0);
                    Vcut[i] = (SI[i] - S0) * ((points[tri1.A].h + points[tri1.C].h + h0 + h0) / 4 - h0);
                }
                if (points[tri1.C].h < h0 && points[tri1.B].h > h0 && points[tri1.A].h > h0)
                {
                    double X1 = points[tri1.C].x + Math.Abs((h0 - points[tri1.C].h) / (points[tri1.B].h - points[tri1.C].h))
                        * (points[tri1.B].x - points[tri1.C].x);
                    double Y1 = points[tri1.C].y + Math.Abs((h0 - points[tri1.C].h) / (points[tri1.B].h - points[tri1.C].h))
                        * (points[tri1.B].y - points[tri1.C].y);
                    double X2 = points[tri1.C].x + Math.Abs((h0 - points[tri1.C].h) / (points[tri1.B].h - points[tri1.C].h))
                        * (points[tri1.B].x - points[tri1.C].x);
                    double Y2 = points[tri1.C].y + Math.Abs((h0 - points[tri1.C].h) / (points[tri1.B].h - points[tri1.C].h))
                        * (points[tri1.B].y - points[tri1.C].y);
                    double S0 = Math.Abs((X1 - points[tri1.C].x) * (Y2 - points[tri1.C].y) - (X2 - points[tri1.C].x)
                        * (Y1 - points[tri1.C].x)) / 2;

                    Vfill[i] = S0 * ((points[tri1.C].h + h0 + h0) / 3 - h0);
                    Vcut[i] = (SI[i] - S0) * ((points[tri1.B].h + points[tri1.A].h + h0 + h0) / 4 - h0);
                }
                zong[i] = Vfill[i] + Vcut[i];

            }
            VZ = Vfill.Sum() +Vcut.Sum();
            wazong = Vcut.Sum();
            tianzong = Vfill.Sum();
            MessageBox.Show("计算成功！");


            chart1.ChartAreas["ChartArea1"].AxisX.Maximum = y.Max();
            chart1.ChartAreas["ChartArea1"].AxisX.Minimum = y.Min();
            chart1.ChartAreas["ChartArea1"].AxisY.Maximum = x.Max();
            chart1.ChartAreas["ChartArea1"].AxisY.Minimum = x.Min();
             v = T1.Count;
            double[,] sanjiao = new double[4*v, 2];
            
            for (int i = 0; i < v; i++)
            {
                Class1.Tri tri1 = new Class1.Tri();
                tri1 = (Class1.Tri)T1[i];
                sanjiao[4 * i, 0] = points[tri1.A].x;
                sanjiao[4 * i, 1] = points[tri1.A].y;
                sanjiao[4 * i+1, 0] = points[tri1.B].x;
                sanjiao[4 * i+1, 1] = points[tri1.B].y;
                sanjiao[4 * i + 2, 0] = points[tri1.C].x;
                sanjiao[4 * i + 2, 1] = points[tri1.C].y;
                sanjiao[4 * i + 3, 0] = points[tri1.A].x;
                sanjiao[4 * i + 3, 1] = points[tri1.A].y;
            Series series;

                chart1.Series.Add(i.ToString()).ChartType = SeriesChartType.Line;
                for (int j = 4 * i; j < 4 * (i + 1); j++)
                {
                    chart1.Series[(i.ToString())].Points.AddXY(sanjiao[j, 1], sanjiao[j, 0]);
                    series = new Series();
                    series.MarkerStyle = MarkerStyle.Circle;
                    series.ChartType = SeriesChartType.Line;
                    series.MarkerColor = Color.Red;
                    series.MarkerSize = 8;
                    series.Points.AddXY(sanjiao[j, 1], sanjiao[j, 0]);
                    chart1.Series.Add(series);
                }

            }



        }
        string report()
        {
            string text = "";
            text += $"{"序号",10}{"点名",10}{"点名",10}{"点名",10}{"挖方体积",20}{"填方体积",20}{"总体积",20}\r\n";
            for (int i = 0; i < v; i++)
            {
                Class1.Tri tri1 = new Class1.Tri();
                tri1 = (Class1.Tri)T1[i];
                text += $"{i.ToString("f0"),5}{tri1.A,5}{tri1.B,5}{tri1.C,5}{Vcut[i],20:f4}{Vfill[i],20:f4}{zong[i],20:f4}\r\n";
            }
            text += $"{bbb ,20}{aaa,20}\r\n";

            return text;
        }












    }
}
