using OSGeo.GDAL;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO.Ports;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using GDAL = OSGeo.GDAL;

namespace Grid
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            GdalConfiguration.ConfigureGdal();
            GdalConfiguration.ConfigureOgr();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new TIN.GridForm());
        }
        //static void Main(string[] args)
        //{
        //    Test();
        //}
        static void Test2()
        {
            GdalConfiguration.ConfigureGdal();
            GdalConfiguration.ConfigureOgr();

            string path = @"C:\Users\HP\Pictures\2015bak\MOD&MYD10A1.A2015168.Snow_Albedo_0-100 原始.tif";
            string path2 = @"C:\Users\HP\Pictures\2015bak\MOD&MYD10A1.A2015152.Snow_Albedo_0-100 - 副本.tif";
            string name = "结果.tif";
            Geotiff geotiff = new Geotiff();
            var v1 = geotiff.GetRasterValuesFromFilePath(path, false);
            var v2 = geotiff.GetRasterValuesFromFilePath(path2, false);
            System.Array v3 = Array.CreateInstance(typeof(byte), v1.GetLength(0), v1.GetLength(1));

            for (int i = 0; i < v1.GetLength(0); i++)
            {
                for (int j = 0; j < v1.GetLength(1); j++)
                {
                    List<byte?> list = new List<byte?>();
                    byte n1 = (byte)v1.GetValue(i, j);
                    byte n2 = (byte)v2.GetValue(i, j);
                    byte n3 = Average(n1, n2);
                    v3.SetValue(n3, i, j);
                }
            }

            geotiff.CreateRasterFromModuleFilePath(@"C: \Users\HP\Pictures\2015bak", name, "TIFF", path2, v3, "0");
        }
        static void Test1()
        {
            string imagePath = @"C:\Users\HP\Pictures\2015\MOD&MYD10A1.A2015152.Snow_Albedo_0-100.tif";
            // 配置，暂时不用那个 ogr，那个是矢量的
            GdalConfiguration.ConfigureGdal();
            GdalConfiguration.ConfigureOgr();
            // 这个像c 那个注册
            GDAL.Gdal.AllRegister();
            // 只读模式打开一个图片
            //GDAL.Dataset ds = GDAL.Gdal.Open(imagePath,GDAL.Access.GA_ReadOnly);
            //ds.Dispose();
            // c#的语法，等同上面
            using (GDAL.Dataset ds = GDAL.Gdal.Open(imagePath, GDAL.Access.GA_ReadOnly))
            {
                // 这个序号依旧是从1开始
                GDAL.Band band = ds.GetRasterBand(1);
                int xsize = band.XSize;
                int ysize = band.YSize;
                // 我的图片是 uint8 格式的
                int[] buf = new int[xsize];
                for (int i = 0; i < ysize; i++)
                {
                    // buf 只能是 int byte short float double
                    band.ReadRaster(0, i, xsize, 1, buf, xsize, 1, 0, 0);
                    for (int j = 0; j < xsize; j++)
                    {
                        Console.Write(buf[j] + "\t");
                    }
                    Console.WriteLine();
                }
            }
            Console.ReadKey();
        }
        static byte Average(params byte[] values)
        {
            int sum = 0, count = 0, average;
            foreach (var item in values)
            {
                if(item!=255)
                {
                    sum += item;
                    count++;
                }
            }
            if (count==0)
            {
                average = -1;
            }
            else
            {
                average = sum / count;
            }
            return (byte)average;
        }
    }
}
