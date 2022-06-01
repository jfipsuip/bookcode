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
using Surveying.Commons;

namespace Surveying
{
    public partial class Form1 : Form
    {
        Surveying.Draws.DrawHelper draw;
        TIN.TIN.TINHelper tin;
        public Form1()
        {
            InitializeComponent();
            dataGridView1.SetColumnName("点名", "X", "Y", "H");
        }

        private void 打开ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Open();
        }

        private void 保存ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void 保存程序正确性ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveProgramRight();
        }

        private void 保存计算结果ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveResult();
        }

        private void 保存成果图形ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveImage();
        }
        private void 保存报告ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveReport();
        }

        private void 算法ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            JiSuan();
        }

        private void 放大ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FangDa();
        }

        private void 缩小ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SuoXiao();
        }

        private void 帮助ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("帮助内容", "程序介绍");
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            Open();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            JiSuan();
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            FangDa();
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            SuoXiao();
        }

        private void Open()
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string[] content = File.ReadAllLines(openFileDialog1.FileName);
                toolStripTextBox1.Text = content[0].Split(',')[1];

                var lines = content.SkipWhile(t => t.Split(',').Length < 4).ToArray();
                dataGridView1.BindData(lines);

                toolStripStatusLabel1.Text = "导入数据成功！！";
            }
        }
        private void Save()
        {
            switch (tabControl1.SelectedTab.Text)
            {
                case "图形":
                    SaveImage();
                    break;
                case "程序正确性":
                    SaveProgramRight();
                    break;
                case "计算结果":
                    SaveResult();
                    break;
                default:
                    SaveReport();
                    break;
            }


        }
        private void JiSuan()
        {
            tin = new TIN.TIN.TINHelper(dataGridView1.ToList<TIN.TIN.Point>(), 25);
            tin.Calculate();
            richTextBox1.Lines = tin.ReportResult();
            dataGridView2.BindData(tin.ProgramRigth(), true);
            dataGridView3.BindData(tin.CalculateResult(), true);
            draw = tin.GetDrawHelper(pictureBox1);
            draw.Draw();
        }
        private void FangDa()
        {
            draw.Magnify();
        }
        private void SuoXiao()
        {
            draw.Minish();
        }
        private void SaveReport()
        {
            saveFileDialog1.Filter = "文本文件|*.txt";
            saveFileDialog1.FileName = "result";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllLines(saveFileDialog1.FileName, richTextBox1.Lines);
            }
        }
        private void SaveProgramRight()
        {
            saveFileDialog1.Filter = "文本文件|*.txt";
            saveFileDialog1.FileName = "程序正确性";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllLines(saveFileDialog1.FileName, dataGridView2.ToLines());
            }
        }
        private void SaveResult()
        {
            saveFileDialog1.Filter = "文本文件|*.txt";
            saveFileDialog1.FileName = "计算结果";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllLines(saveFileDialog1.FileName, dataGridView3.ToLines());
            }
        }
        private void SaveImage()
        {
            saveFileDialog1.Filter = "图片|*.jpg";
            saveFileDialog1.FileName = "成果文件";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image.Save(saveFileDialog1.FileName);
            }
        }
    }
}
