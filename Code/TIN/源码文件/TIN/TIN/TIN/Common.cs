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
    }
}
