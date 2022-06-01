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

namespace 规则网
{
    public partial class Form1 : Form
    {
        DataGridView data;
        List<Tpoint> tpoints = new List<Tpoint>();//
        public double[] zuobiao;
        RichTextBox textBox;
        //List<XYlist> Xylis = new List<XYlist>();


        public Form1()
        {
            InitializeComponent();
        }

        private void 计算ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripButton1_Click(sender, e);
        }

        private void 新建NToolStripButton_Click(object sender, EventArgs e)
        {
            DataProcess(out data, 11);
        }
        private void DataProcess(out DataGridView data,int n)
        {
            tabPage1.Controls.Clear();
            data = new DataGridView();
            tabPage1.Controls.Add(data);

            data.ColumnCount = 10;
            data.RowCount = n;

            data.Dock = DockStyle.Fill;
            for (int i = 0; i < data.ColumnCount; i++) data.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            for (int i = 0; i < data.ColumnCount; i++) data.Columns[i].Width = (Width - 20) / data.ColumnCount;

            data.Columns[0].HeaderText = "数据项";
            data.Columns[1].HeaderText = "x";
            data.Columns[2].HeaderText = "y";
            data.Columns[3].HeaderText = "f";
            data.Columns[4].HeaderText = "Xs";
            data.Columns[5].HeaderText = "Ys";
            data.Columns[6].HeaderText = "Zs";
            data.Columns[7].HeaderText = "a";
            data.Columns[8].HeaderText = "w";
            data.Columns[9].HeaderText = "k";


            data.RowHeadersVisible = false;
            data.Show();
        }
        private void InputGrid()
        {
            OpenFileDialog op = new OpenFileDialog
            {
                Filter = "*.txt|*.txt"
            };
            if (op.ShowDialog() == DialogResult.OK)
            {
                string[] alllines = File.ReadAllLines(op.FileName);
                string[] line;
                int n = alllines.Length;
                DataProcess(out data, n);
                Tpoint tpo = new Tpoint();
                for (int i = 0; i < alllines.Length; i++)
                {
                    line = alllines[i].Split(',');

                    if (i == 0) { }
                    else if (i > 0)
                    {
                        data[0, i - 1].Value = line[0];
                        data[1, i - 1].Value = line[1];
                        data[2, i - 1].Value = line[2];
                        data[3, i - 1].Value = line[3];
                        data[4, i - 1].Value = line[4];
                        data[5, i - 1].Value = line[5];
                        data[6, i - 1].Value = line[6];
                        data[7, i - 1].Value = line[7];
                        data[8, i - 1].Value = line[8];
                        data[9, i - 1].Value = line[9];
                        tpo.x = double.Parse(line[1]);
                        tpo.y = double.Parse(line[2]);
                        tpo.f = double.Parse(line[3]);
                        tpo.Xs = double.Parse(line[4]);
                        tpo.Ys = double.Parse(line[5]);
                        tpo.Zs = double.Parse(line[6]);
                        tpo.a = double.Parse(line[7]);
                        tpo.w = double.Parse(line[8]);
                        tpo.k = double.Parse(line[9]);
                        double[] fuzhuzuob1 = 计算空间辅助坐标.XiShu(tpo.a, tpo.w, tpo.k, tpo.x, tpo.y, tpo.f);
                        tpo.u = fuzhuzuob1[0];
                        tpo.v = fuzhuzuob1[1];
                        tpo.ww = fuzhuzuob1[2];
                        tpoints.Add(tpo);
                        
                    }                
                }
            }
        }

        private void 打开OToolStripButton_Click(object sender, EventArgs e)
        {
            InputGrid();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            double[] N = 获得投影系数.Touying(tpoints[0], tpoints[1]);
            double[] zuobiao = 计算地面坐标.zuobiao(tpoints[0], tpoints[1], N[0], N[1]);
            INput(N,zuobiao);

        }
        private void INput(double[] N,double []zuobiao)
        {
            tabPage2.Controls.Clear();
            string line = "--------------------投影系数---------------\r\n";
            line += $"N1:{N[0]}-N2:{N[1]}\r\n";
            line += "------------------地面坐标------------------\r\n";
            line += $"X:{zuobiao[0]}\r\n";
            line += $"Y:{zuobiao[1]}\r\n";
            line += $"Z:{zuobiao[2]}\r\n";
            textBox = new RichTextBox();
            tabPage2.Controls.Add(textBox);
            textBox.Dock = DockStyle.Fill;
            textBox.Multiline = true;
            textBox.Text = line;
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            SaveFileDialog savefile = new SaveFileDialog();
            savefile.Filter = "*.txt|*.txt";
            if (savefile.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllText(savefile.FileName, textBox.Text);
            }
        }

        private void 保存成果报告ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripButton2_Click(sender, e);
        }

        private void 打开数据ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            打开OToolStripButton_Click(sender, e);
        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void 帮助LToolStripButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("1.计算空间辅助坐标\r\n" + "2.获取投影系数\r\n" +
                "3.计算地面坐标\r\n");
                
        }

        private void 帮助ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            帮助LToolStripButton_Click(sender, e);
        }
    }
}
