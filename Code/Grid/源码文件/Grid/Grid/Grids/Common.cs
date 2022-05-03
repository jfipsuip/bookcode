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
                    data.Add(dataGridView[i, j].Value.ToString());
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
                    dataGridView[i, j].Value = array[i, j];
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
                    Z = Convert.ToDouble(item[4]),
                };
                points.Add(point);
            }

            return points;
        }
    }
}
