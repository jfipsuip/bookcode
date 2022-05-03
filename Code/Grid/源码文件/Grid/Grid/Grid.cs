using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Grid.Grids;
using Grid.Draws;

namespace TIN
{
    public partial class GridForm : Form
    {
        Grid.Grids.Grid grid;
        public GridForm()
        {
            InitializeComponent();
            point_h.Checked = false;
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
        }

        private void Grid_Load(object sender, EventArgs e)
        {

        }


        /// <summary>
        /// 新建数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            NewFile();
        }


        private void Open_Click(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// 打开文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            OpenFile();
        }
        /// <summary>
        /// Tin计算
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            Calculate();
        }
        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void toolStripTextBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {

        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
        }

        private void 数据表格ToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void 示意图ToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void 计算报告ToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }


        private void Document_Click(object sender, EventArgs e)
        {

        }

        private void 计算三角网及体积ToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void 新建NToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void 保存SToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void 查看帮助ToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void toolStripButton8_Click(object sender, EventArgs e)
        {
        }

        private void point_h_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void point_h_Click(object sender, EventArgs e)
        {
        }

        #region 通用代码

        private void NewFile(int n = 11)
        {
            tabPage1.Controls.Clear();
            dataGridView1 = new DataGridView();
            tabPage1.Controls.Add(dataGridView1);
            dataGridView1.Dock = DockStyle.Fill;

            Common.NewGrid(dataGridView1, n);

            tabControl1.SelectedTab = tabPage1;

        }

        public void OpenFile()
        {
            openFileDialog1.Filter = "*.txt|*.txt";
            if (openFileDialog1.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            string path = openFileDialog1.FileName;
            string[] lines = File.ReadAllLines(path);

            string[] points = lines.Where(t => t.Split(',').Length >= 4).ToArray();

            string[,] array = new string[points.Length, 4];
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    string[] strs = points[i].Split(',');
                    array[i, j] = strs[j];
                }
            }
            Common.NewGrid(dataGridView1, array.Length);
            Common.BindData(dataGridView1, array);

            tabControl1.SelectedTab = tabPage1;
        }
        public void Calculate()
        {
            var points = Common.GetPoints(dataGridView1);
            var h = Convert.ToDouble(toolStripTextBox1.Text);
            grid = new Grid.Grids.Grid(points, h);
            grid.Calculate(10);


            DrawHelper draw = new DrawHelper(pictureBox1);
            draw.Points = grid.Points;
            // draw.PointLines = ps;
            draw.Draw();
        }


        #endregion 
    }
}
