using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TIN.Draws;

namespace TIN.TIN
{
    public class Common
    {
        public static void NewFile()
        {

        }


        public static void BindData(DataGridView data, string[] lines)
        {
            string[] strs;

            for (int i = 0; i < lines.Length; i++)
            {
                strs = lines[i].Split(',');
                data[0, i].Value = strs[0];
                data[1, i].Value = strs[1];
                data[2, i].Value = strs[2];
                data[3, i].Value = strs[3];
            }
        }
        public static DataGridView NewGrid(DataGridView data, int n = 11)
        {
            data.ColumnCount = 4;
            data.RowCount = n;

            for (int i = 0; i < data.ColumnCount; i++)
            {
                data.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                data.Columns[i].Width = (data.Width - 20) / data.ColumnCount;
            }
            data.RowHeadersVisible = false;
            data.Columns[0].HeaderText = "点名";
            data.Columns[1].HeaderText = "X分量";
            data.Columns[2].HeaderText = "Y分量";
            data.Columns[3].HeaderText = "H分量";

            data.Show();
            return data;
        }

        public static TINHelper Calculate(DataGridView dataGrid, RichTextBox richTextBox, PictureBox pictureBox)
        {
            List<Point> points;

            points = GetPoints(dataGrid);

            var tin = Common.GetTIN(points, 25);

            richTextBox.Text = tin.Report();

            List<Point> ps = new List<Point>();
            tin.Triangles.ForEach(t =>
            {
                ps.Add(t.PointA);
                ps.Add(t.PointB);
                ps.Add(t.PointA);
                ps.Add(t.PointC);
                ps.Add(t.PointB);
                ps.Add(t.PointC);
            });

            DrawHelper draw = new DrawHelper(pictureBox);
            draw.Points = tin.Points;
            draw.PointLines = ps;
            draw.Draw();

            return tin;
        }
        private static List<Point> GetPoints(DataGridView dataGrid)
        {
            List<Point> points = new List<Point>();
            for (int i = 0; i < dataGrid.Rows.Count; i++)
            {
                Point point = GetPoint(dataGrid.Rows[i]);
                points.Add(point);
            }

            return points;
        }

        private static Point GetPoint(DataGridViewRow dataGrid)
        {
            string name;
            double x, y, h;
            Point point;

            name = Convert.ToString(dataGrid.Cells[0].Value);
            x = Convert.ToDouble(dataGrid.Cells[1].Value);
            y = Convert.ToDouble(dataGrid.Cells[2].Value);
            h = Convert.ToDouble(dataGrid.Cells[3].Value);

            point = new Point(name, x, y, h);

            return point;
        }

        public static void GetTIN(string path)
        {
            List<Point> points = GetPoints(path);
            var tin = new TINHelper(points, 9);
            tin.Calculate();
        }

        public static TINHelper GetTIN(List<Point> points, double h)
        {
            var tin = new TINHelper(points, h);
            tin.Calculate();

            return tin;
        }
        private static List<Point> GetPoints(string path)
        {
            string[] lines = File.ReadAllLines(path);
            string[] strs = lines.Where(t => t.Split(',').Length >= 4).ToArray();
            List<Point> points = strs.Select(t => new Point(t)).ToList();

            return points;
        }
    }
}
