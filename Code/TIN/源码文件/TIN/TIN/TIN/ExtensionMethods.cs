using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TIN.TIN
{
    public static class ExtensionMethods
    {
        /// <summary>
        /// 转换为T类型列表
        /// </summary>
        /// <typeparam name="T">列表的类型</typeparam>
        /// <param name="dataGridView"></param>
        /// <returns></returns>
        public static List<T> ToList<T>(this DataGridView dataGridView) where T : new()
        {
            List<T> list = new List<T>();
            Type type = typeof(T);
            var propertys = type.GetProperties();
            for (int i = 0; i < dataGridView.Rows.Count; i++)
            {
                T ob = new T();
                for (int j = 0; j < propertys.Length; j++)
                {
                    Type propertyType = propertys[j].PropertyType;
                    if (propertyType == typeof(double))
                    {
                        double d = Convert.ToDouble(dataGridView[j, i].Value);
                        propertys[j].SetValue(ob, d);
                    }
                    else
                    {
                        propertys[j].SetValue(ob, dataGridView[j, i].Value);

                    }
                }
                list.Add(ob);
            }
            return list;
        }
        /// <summary>
        /// 绑定数据
        /// </summary>
        /// <param name="dataGridView"></param>
        /// <param name="lines"></param>
        /// <param name="separator"></param>
        public static void BindData(this DataGridView dataGridView, string[] lines, char separator = ',')
        {
            dataGridView.RowCount = lines.Length;
            for (int i = 0; i < lines.Length; i++)
            {
                string[] strs = lines[i].Split(separator);
                for (int j = 0; j < strs.Length; j++)
                {
                    dataGridView[j, i].Value = strs[j];
                }
            }
        }
        /// <summary>
        /// 设置列名
        /// </summary>
        /// <param name="dataGridView"></param>
        /// <param name="columnMames"></param>
        public static void SetColumnName(this DataGridView dataGridView, params string[] columnMames)
        {
            dataGridView.RowHeadersVisible = false;
            dataGridView.ColumnCount = columnMames.Length;
            for (int i = 0; i < dataGridView.ColumnCount; i++)
            {
                dataGridView.Columns[i].Width = (dataGridView.Width - 20) / dataGridView.ColumnCount;
                dataGridView.Columns[i].Name = columnMames[i];
            }
        }

    }
}
