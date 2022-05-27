using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TIN.TIN
{
    public static class ExcelHelper
    {
        public static void Save<T>(this List<T> list, string path)
        {
            //新建工作簿
            IWorkbook workbook = new XSSFWorkbook();
            //新建1个Sheet工作表  
            ISheet sheet = workbook.CreateSheet();
            //对工作表先添加行，下标从0开始
            var row = sheet.CreateRow(0);
            Type type = typeof(T);
            var propertys = type.GetProperties();
            for (int i = 0; i < propertys.Length; i++)
            {
                row.CreateCell(i).SetCellValue(propertys[i].Name);

            }
            // 填充数据
            for (int i = 0; i < list.Count(); i++)
            {
                var data = list[i];
                // 创建行
                IRow row2 = sheet.CreateRow(i + 1); ;
                for (int j = 0; j < propertys.Length; j++)
                {
                    var property = propertys[j];
                    type = property.PropertyType;
                    if (type == typeof(double) || type == typeof(int))
                    {
                        double value = Convert.ToDouble(propertys[j].GetValue(data));
                        row2.CreateCell(j).SetCellValue(value);
                    }
                    else
                    {
                        string value = propertys[j].GetValue(data).ToString();
                        row2.CreateCell(j).SetCellValue(value);
                    }
                    //创建之后就可以赋值了
                }
            }
            FileStream stream = new FileStream(path, FileMode.Create);
            workbook.Write(stream);
            stream.Close();
            workbook.Close();
        }
    }
}
