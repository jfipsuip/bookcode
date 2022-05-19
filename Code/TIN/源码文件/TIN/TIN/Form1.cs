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

        //记录鼠标位置
        double Mouse_X1, Mouse_Y1;

        //判断是否进行图形的移动
        bool isMove = false;

        //pictubox的初始位置
        int P1_X = 0, P1_Y = 0;

        //图形移动辅助参数
        int P1_X1 = 0, P1_Y1 = 0;

        Point position, go, location;

        OpenFileDialog OpenFileDialog = new OpenFileDialog();
        SaveFileDialog saveFileDialog = new SaveFileDialog();

        public Form1()
        {
            InitializeComponent();
            pictureBox1.MouseWheel += PictureBox1_MouseWheel;
        }

        private void PictureBox1_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta > 0)
            {
                draw?.Magnify();
            }
            else if (e.Delta <= 0)
            {
                draw?.Minish();
            }
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
            pictureBox1.Location = new Point();
            Calculate();
        }

        private void 放大ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            draw.Magnify();
        }

        private void 缩小ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            draw.Minish();
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
            position = Cursor.Position;
            isMove = true;
        }
        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            isMove = false;
            location = go;
        }
        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (isMove)
            {
                //X、Y方向上的移动距离
                Point move = new Point
                {
                    X = Cursor.Position.X - position.X,
                    Y = Cursor.Position.Y - position.Y,
                };
                //图形移动后的位置
                go.X = location.X + move.X;
                go.Y = location.Y + move.Y;


                //pictureBox1.Location = location;
                draw.Go = go;
                draw.Draw();
            }
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
        private void pictureBox1_DoubleClick(object sender, EventArgs e)
        {
            draw.Reset();
        }

    }
}
