using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TIN.TIN
{
    public static class ExtensionMethods
    {
        public static List<T> ToList<T>(this System.Windows.Forms.DataGridView dataGridView) where T : new()
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
    }
}
