using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TIN.TIN;

namespace TIN
{
    public partial class Form1 : Form
    {

        OpenFileDialog OpenFileDialog = new OpenFileDialog();
        DataGridView dataGridView;

        public Form1()
        {
            InitializeComponent();
        }

        private void 新建ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewFile();
        }
        private void 打开ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Open();
        }

        private void 保存ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void 文件ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void NewFile(int n = 11)
        {
            tabPage1.Controls.Clear();
            dataGridView = new DataGridView();
            tabPage1.Controls.Add(dataGridView);
            dataGridView.Dock = DockStyle.Fill;

            Common.NewGrid(dataGridView, n);

            tabControl1.SelectedTab = tabPage1;

        }

        public void Open()
        {
            OpenFileDialog.Filter = "*.txt|*.txt";
            if (OpenFileDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            string path = OpenFileDialog.FileName;
            string[] lines = File.ReadAllLines(path);

            string[] points = lines.Where(t => t.Split(',').Length >= 4).ToArray();


            tabControl1.SelectedTab = tabPage1;
            NewFile(points.Length);
            Common.BindData(dataGridView, points);
        }
        public void Calculate()
        {
            Common.Calculate(dataGridView);
        }

        private void 计算三角网体积ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Calculate();
        }
    }
}
