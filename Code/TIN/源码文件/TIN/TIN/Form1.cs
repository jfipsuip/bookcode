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

namespace TIN
{
    public partial class Form1 : Form
    {
        TIN.TINHelper tINHelper;
        Draws.DrawHelper draw;


        OpenFileDialog OpenFileDialog = new OpenFileDialog();
        SaveFileDialog saveFileDialog = new SaveFileDialog();

        public Form1()
        {
            InitializeComponent();
            pictureBox1.MouseWheel += PictureBox1_MouseWheel;
        }

        private void PictureBox1_MouseWheel(object sender, MouseEventArgs e)
        {

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
            var contents = tINHelper?.ReportResult();
            saveFileDialog.Filter = "文本文件|*.txt";
            if (saveFileDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            string path = saveFileDialog.FileName;

            File.WriteAllLines(path, contents);
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

            TIN.Common.NewGrid(dataGridView1, n);

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
            TIN.Common.BindData(dataGridView1, points);

            toolStripStatusLabel1.Text = "数据导入成功！";
        }
        public void Calculate()
        {
            tINHelper = TIN.Common.Calculate(dataGridView1);
            richTextBox1.Lines = tINHelper.ReportResult();
            draw = tINHelper.GetDrawHelper(pictureBox1);
            draw.Draw();
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
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllLines(saveFileDialog.FileName, contents);
            }
        }
        private void 保存计算结果ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var contents = tINHelper?.CalculateResult();
            saveFileDialog.Filter = "文本文件|*.txt";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllLines(saveFileDialog.FileName, contents);
            }
        }

        private void 保存ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            saveFileDialog.Filter = "图形文件|*.jpg";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image?.Save(saveFileDialog.FileName, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
        }



        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
        }
        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
        }
        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
        private void pictureBox1_DoubleClick(object sender, EventArgs e)
        {
        }

    }
}
