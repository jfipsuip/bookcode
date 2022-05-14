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
        TINHelper tINHelper;
        OpenFileDialog OpenFileDialog = new OpenFileDialog();
        SaveFileDialog saveFileDialog = new SaveFileDialog();

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
            dataGridView1 = new DataGridView();
            tabPage1.Controls.Add(dataGridView1);
            dataGridView1.Dock = DockStyle.Fill;

            Common.NewGrid(dataGridView1, n);

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
            Common.BindData(dataGridView1, points);
        }
        public void Calculate()
        {
           tINHelper= Common.Calculate(dataGridView1, richTextBox1, pictureBox1);
        }

        private void 计算三角网体积ToolStripMenuItem_Click(object sender, EventArgs e)
        {
           Calculate();
        }

        private void 放大ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void 缩小ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void 保存程序正确性ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var contents = tINHelper?.ProgramRigth();
            saveFileDialog.Filter = "文本文件|*.txt";
            if (saveFileDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            string path = saveFileDialog.FileName;

            File.WriteAllLines(path, contents);
        }

        private void 保存计算结果ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void 保存ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            saveFileDialog.Filter = "图形文件|*.Jpeg";
            if (saveFileDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            string path = saveFileDialog.FileName;

            pictureBox1.Image?.Save(path, System.Drawing.Imaging.ImageFormat.Jpeg);
        }
    }
}
