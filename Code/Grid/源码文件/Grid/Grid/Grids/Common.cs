using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Grid.Grids
{
    class Common
    {

        public static List<List<string>> ToArray(DataGridView dataGridView)
        {
            List<List<string>> list = new List<List<string>>(); ;

            for (int i = 0; i < dataGridView.Rows.Count; i++)
            {
                List<string> data = new List<string>();
                for (int j = 0; j < dataGridView.Columns.Count; j++)
                {
                    data.Add(dataGridView[j, i].Value.ToString());
                }
                list.Add(data);
            }

            return list;
        }

        public static void BindData(DataGridView dataGridView, string[,] array)
        {
            dataGridView.RowCount = array.GetLength(0);
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    dataGridView[j, i].Value = array[i, j];
                }
            }
        }

        public static List<Point> GetPoints(DataGridView dataGrid)
        {
            List<Point> points = new List<Point>();

            List<List<string>> list = ToArray(dataGrid);

            foreach (var item in list)
            {
                Point point = new Point()
                {
                    Name = item[0],
                    X = Convert.ToDouble(item[1]),
                    Y = Convert.ToDouble(item[2]),
                    H = Convert.ToDouble(item[3]),
                };
                points.Add(point);
            }

            return points;
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

    }
}
