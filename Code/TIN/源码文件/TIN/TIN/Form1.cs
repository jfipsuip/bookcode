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
        TIN.TINHelper tINHelper;
        Draws.DrawHelper draw;


        OpenFileDialog OpenFileDialog = new OpenFileDialog();
        SaveFileDialog saveFileDialog = new SaveFileDialog();

        public Form1()
        {
            InitializeComponent();
            pictureBox1.MouseWheel += PictureBox1_MouseWheel;
            dataGridView1.SetColumnName("点名", "X分量", "Y分量", "H分量");
            dataGridView1.RowCount = 20;

        }

        private void PictureBox1_MouseWheel(object sender, MouseEventArgs e)
        {

        }

        private void 新建ToolStripMenuItem_Click(object sender, EventArgs e)
        {
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
        public void Open()
        {
            OpenFileDialog.Filter = "*.txt|*.txt";
            if (OpenFileDialog.ShowDialog() == DialogResult.OK)
            {
                string[] comtents = File.ReadAllLines(OpenFileDialog.FileName);
                string[] lines = comtents.SkipWhile(t => t.Split(',').Length < 4).ToArray();

                dataGridView1.BindData(lines);

                tabControl1.SelectedTab = tabPage1;
                toolStripStatusLabel1.Text = "数据导入成功！";
            }
            else
            {
                toolStripStatusLabel1.Text = "数据导入取消！";

            }
        }
        public void Calculate()
        {
            tINHelper = new TINHelper(dataGridView1.ToList<TIN.Point>(), 25);
            tINHelper.Calculate();
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
            saveFileDialog.Filter = "文本文件|*.xlsx";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                contents.Save(saveFileDialog.FileName);
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
