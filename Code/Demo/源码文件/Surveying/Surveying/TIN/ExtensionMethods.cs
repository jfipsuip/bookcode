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
            for (int i = 0; i < dataGridView.RowCount; i++)
            {
                T ob = new T();
                for (int j = 0; j < dataGridView.ColumnCount; j++)
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
        public static List<string> ToLines(this DataGridView dataGridView, char separator = ',')
        {
            List<string> list = new List<string>();
            string str = string.Empty;
            for (int i = 0; i < dataGridView.ColumnCount; i++)
            {
                str += $"{dataGridView.Columns[i].Name}{separator}";
            }
            list.Add(str.Trim(separator));
            for (int i = 0; i < dataGridView.RowCount; i++)
            {
                str = string.Empty;
                for (int j = 0; j < dataGridView.ColumnCount; j++)
                {
                    str += $"{dataGridView[j, i].Value}{separator}";
                }
                list.Add(str.Trim(separator));
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
            for (int i = 0; i < dataGridView.RowCount; i++)
            {
                string[] strs = lines[i].Split(separator);
                for (int j = 0; j < dataGridView.ColumnCount; j++)
                {
                    dataGridView[j, i].Value = strs[j];
                }
            }
        }
        public static void BindData<T>(this DataGridView dataGridView, List<T> list) where T : new()
        {
            dataGridView.RowCount = list.Count;
            Type type = typeof(T);
            var propertys = type.GetProperties();
            for (int i = 0; i < dataGridView.RowCount; i++)
            {
                var data = list[i];
                for (int j = 0; j < dataGridView.ColumnCount; j++)
                {
                    var property = propertys[j];
                    dataGridView[j, i].Value = propertys[j].GetValue(data);
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
