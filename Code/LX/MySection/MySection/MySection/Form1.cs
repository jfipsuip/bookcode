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

namespace MySection
{
    public partial class Form1 : Form
    {
        DataGridView data;
        public Form1()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 打开文件夹
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 打开ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog
            {
                Filter = "*.txt|*.txt"
            };
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                data = new DataGridView();
                string[] alllines = File.ReadAllLines(openFileDialog1.FileName);
                string[] line;
                int n = alllines.Length - 5;
                DataViewPoint(out data, n);
                for (int i = 0; i < alllines.Length; i++)
                {
                    line = alllines[i].Split(',');
                    if (i == 0) tabControl1.Text = line[1];
                    else if (i >= 5)
                    {
                        data[0, i - 5].Value = line[0];
                        data[1, i - 5].Value = line[1];
                        data[2, i - 5].Value = line[2];
                        data[3, i - 5].Value = line[3];

                    }


                }

            }

        }
        /// <summary>
        /// 新建表格
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 新建表格ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            tabPage1_Click(sender, e);
        }
        /// <summary>
        /// 引用表格
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tabPage1_Click(object sender, EventArgs e)
        {
            DataViewPoint(out data, 50);
        }
        /// <summary>
        /// 搭建表格
        /// </summary>
        /// <param name="data"></param>
        /// <param name="n"></param>
        private void DataViewPoint(out DataGridView data, int n)
        {
            tabPage1.Controls.Clear();
            data = new DataGridView();
            tabPage1.Controls.Add(data);
            data.Dock = DockStyle.Fill;
            data.ColumnCount = 4;
            data.RowCount = n;
            for (int i = 0; i < data.ColumnCount; i++) data.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            for (int i = 0; i < data.ColumnCount; i++) data.Columns[i].Width = (data.Width - 70) / data.ColumnCount;
            data.RowHeadersVisible = false;
            data.Columns[0].HeaderText = "点名";
            data.Columns[1].HeaderText = "X(m)";
            data.Columns[2].HeaderText = "Y(m)";
            data.Columns[3].HeaderText = "H(m)";
            tabControl1.SelectedTab = tabPage1;

        }

        private void 计算ToolStripMenuItem_Click(object sender, EventArgs e)
        {
          

        }

        private void 计算纵断面面积ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ZongDuanMian.JiSuan();
        }

        private void 横断面ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //hengduanmian.;
        }
    }
}
