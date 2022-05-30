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
            dataGridView2.SetColumnName("序号", "点名1", "点名2", "点名3", "挖方体积", "填方体积", "总体积");
        }

        private void PictureBox1_MouseWheel(object sender, MouseEventArgs e)
        {

        }

        private void 新建ToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }




        private void 文件ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void 打开ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Open();
        }
        public void Open()
        {
            OpenFileDialog.Filter = "*.txt|*.txt";
            if (OpenFileDialog.ShowDialog() == DialogResult.OK)
            {
                string[] contents = File.ReadAllLines(OpenFileDialog.FileName);

                toolStripTextBox1.Text = contents[0].Split(',')[1];
                string[] lines = contents.SkipWhile(t => t.Split(',').Length < 4).ToArray();

                dataGridView1.BindData(lines);
                draw = new Draws.DrawHelper(pictureBox1);
                draw.Points = dataGridView1.ToList<TIN.Point>();
                draw.Draw();

                tabControl1.SelectedTab = tabPage1;
                toolStripStatusLabel1.Text = "数据导入成功！";
            }
            else
            {
                toolStripStatusLabel1.Text = "数据导入取消！";

            }
        }


        private void 计算三角网体积ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Calculate();
        }
        public void Calculate()
        {
            double h = Convert.ToDouble(toolStripTextBox1.Text);
            tINHelper = new TINHelper(dataGridView1.ToList<TIN.Point>(), h);
            tINHelper.Calculate();
            richTextBox1.Lines = tINHelper.ReportResult();
            var list = tINHelper.CalculateResult();
            dataGridView2.BindData(list);
            draw = tINHelper.GetDrawHelper(pictureBox1);
            draw.Draw();

            toolStripStatusLabel1.Text = "解算成功！！！";
        }


        private void 保存ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog.Filter = "文本文件|*.txt";
            if (saveFileDialog.ShowDialog() != DialogResult.OK)
            {
                File.WriteAllLines(saveFileDialog.FileName, richTextBox1.Lines);
            }
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
                File.WriteAllLines(saveFileDialog.FileName, dataGridView2.ToLines());
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

        private void 放大ToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void 缩小ToolStripMenuItem_Click(object sender, EventArgs e)
        {
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

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            Open();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            Calculate();
        }
    }
}
