using OSGeo.GDAL;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grid
{
    internal class Geotiff
    {
        public void CreateRasterFromModuleFilePath(string pDirectory, string pFileName,
    string pRasterFormat, string pFilePath_Module, System.Array pArray, object pNoDataValue)
        {
            if (pDirectory == null || pDirectory == "" ||
                pFileName == null || pFileName == "" ||
                pRasterFormat == null || pRasterFormat == "")
                return;

            // create a temporary directory
            string pDirectoryRandom = System.IO.Path.GetTempPath();
            pDirectoryRandom = System.IO.Path.GetDirectoryName(pDirectoryRandom);

            //if equal, create a new temp directory  
            if (pDirectoryRandom == pDirectory)
            {
                string pRandom = System.Guid.NewGuid().ToString().Substring(0, 5);
                pDirectoryRandom = pDirectory + pRandom;
                System.IO.Directory.CreateDirectory(pDirectoryRandom);
            }

            Gdal.AllRegister();
            Dataset ds = Gdal.Open(pFilePath_Module, Access.GA_ReadOnly);

            // columns(xSize) and rows(ySize) of rasters
            int xSize = ds.RasterXSize;
            int ySize = ds.RasterYSize;
            //important for making sure the cellsize   
            int bandCount = ds.RasterCount;

            // columns and rows of the pArray
            int countRow = pArray.GetLength(0);     //1342
            int countColumn = pArray.GetLength(1);  //1694

            // Make sure the new Geotiff file named pFileName has the same cellsize.
            bool diagnosis = ((xSize == countColumn) && (ySize == countRow));
            Debug.Assert(diagnosis, "The size should be the same between input Array and geotiff module!");

            double[] transform = new double[6] { 0, 0, 0, 0, 0, 0 };
            ds.GetGeoTransform(transform);//

            // get the information of Spatial Reference
            string projection = ds.GetProjection();//

            DataType dataType = ds.GetRasterBand(1).DataType;
            string rasterFormat = null;

            if (pRasterFormat == "TIFF") rasterFormat = "GTiff";
            else Debug.Assert(true, "unsupported data type now for RasterFormat!");

            OSGeo.GDAL.Driver driver = Gdal.GetDriverByName(rasterFormat);
            //string geotiffOutputPath = pDirectoryRandom + "\\" + pFileName;
            string geotiffOutputPath = pFileName;

            //delete the old version of file
            if (System.IO.File.Exists(geotiffOutputPath))
            {
                System.IO.File.Delete(geotiffOutputPath);
            }


            Dataset result = driver.Create(geotiffOutputPath, countColumn, countRow, bandCount, dataType, null);

            //set attributes
            result.SetGeoTransform(transform);
            result.SetProjection(projection);

            OSGeo.GDAL.CPLErr check = 0;

            if (pArray.GetValue(0, 0).GetType() == typeof(int))
            {
                int[] tmpData = new int[countColumn * countRow];
                int tmp = 0;
                for (int i = 0; i < countRow; ++i)
                {
                    for (int j = 0; j < countColumn; ++j)
                    {
                        tmp = (int)pArray.GetValue(i, j);
                        tmpData[i * countColumn + j] = tmp;
                    }
                }
                //writer geotiff data to file
                try
                {
                    //result.GetRasterBand(1).SetNoDataValue((int)pNoDataValue);    // Langping's implementation in file ALSAToolsAE.cs but with some warnings when transferring                                                                                                       
                    //  result.GetRasterBand(1).SetNoDataValue(ALSAGlobal.pNoDataInt); // john's improvement
                }
                catch (System.Exception ex)
                {
                    Console.WriteLine("{0}: result.GetRasterBand(1).SetNoDataValue((int)pNoDataValue)  failed !", ex.ToString());
                }

                // The value of xoff and yoff indicates the location that the upper left corner of the pixel block is to write.
                check = result.GetRasterBand(1).WriteRaster(0, 0, countColumn, countRow, tmpData, countColumn, countRow, 0, 0);
            }
            else if (pArray.GetValue(0, 0).GetType() == typeof(float))
            {
                float[] tmpData = new float[countColumn * countRow];
                float tmp = 0;
                for (int i = 0; i < countRow; ++i)
                {
                    for (int j = 0; j < countColumn; ++j)
                    {
                        tmp = (float)pArray.GetValue(i, j);
                        tmpData[i * countColumn + j] = tmp;
                    }
                }
                //writer geotiff data to file
                try
                {
                    // result.GetRasterBand(1).SetNoDataValue((float)pNoDataValue); // Lang ping's implementation in file ALSAToolsAE.cs but with some warnings when transferring
                    // result.GetRasterBand(1).SetNoDataValue(ALSAGlobal.pNoDataFloat);// john's improvement
                }
                catch (System.Exception ex)
                {
                    Console.WriteLine("{0}: result.GetRasterBand(1).SetNoDataValue((int)pNoDataValue)  failed !", ex.ToString());
                }
                // The value of xoff and yoff indicates the location that the upper left corner of the pixel block is to write.
                check = result.GetRasterBand(1).WriteRaster(0, 0, countColumn, countRow, tmpData, countColumn, countRow, 0, 0);
            }
            else if (pArray.GetValue(0, 0).GetType() == typeof(byte))
            {
                float[] tmpData = new float[countColumn * countRow];
                float tmp = 0;
                for (int i = 0; i < countRow; ++i)
                {
                    for (int j = 0; j < countColumn; ++j)
                    {
                        tmp = (byte)pArray.GetValue(i, j);
                        tmpData[i * countColumn + j] = tmp;
                    }
                }
                //writer geotiff data to file
                try
                {
                    // result.GetRasterBand(1).SetNoDataValue((float)pNoDataValue); // Lang ping's implementation in file ALSAToolsAE.cs but with some warnings when transferring
                    result.GetRasterBand(1).SetNoDataValue(255);// john's improvement
                    //result.GetRasterBand(1).DeleteNoDataValue();
                }
                catch (System.Exception ex)
                {
                    Console.WriteLine("{0}: result.GetRasterBand(1).SetNoDataValue((int)pNoDataValue)  failed !", ex.ToString());
                }
                // The value of xoff and yoff indicates the location that the upper left corner of the pixel block is to write.
                check = result.GetRasterBand(1).WriteRaster(0, 0, countColumn, countRow, tmpData, countColumn, countRow, 0, 0);
            }
            else
            {
                Debug.Assert(true, "unsupported data type now for pArray!");
            }

            result.GetRasterBand(1).FlushCache();
            result.FlushCache();

            if (check == OSGeo.GDAL.CPLErr.CE_Failure || check == OSGeo.GDAL.CPLErr.CE_Fatal)
                Debug.Assert(false, "write tmp geotiff failed!");

        }
        public System.Array GetRasterValuesFromFilePath(string pFilePath, bool pBoolBool)
        {
            if (pFilePath == null)
            {
                //if null
                return null;
            }
            if (!System.IO.File.Exists(pFilePath))
            {
                return null;
            }

            Gdal.AllRegister();
            Dataset ds = Gdal.Open(pFilePath, Access.GA_ReadOnly);
            Band band = ds.GetRasterBand(1);
            int width = ds.RasterXSize;
            int height = ds.RasterYSize;
            {

            }
            if (band.DataType == DataType.GDT_Float32)
            {
                System.Array resultArray = Array.CreateInstance(typeof(float), height, width);
                float[] buffer = new float[width * height];

                band.ReadRaster(0, 0, width, height, buffer, width, height, 0, 0);
                for (int i = 0; i < buffer.Length; i++)
                    resultArray.SetValue(buffer[i], i / width, i % width);
                return resultArray;
            }

            if (band.DataType == DataType.GDT_Byte)
            {
                if (pBoolBool)
                {
                    // transform geotiff file to bool array
                    System.Array resultArray = Array.CreateInstance(typeof(bool), height, width);
                    byte[] buffer = new byte[width * height];

                    band.ReadRaster(0, 0, width, height, buffer, width, height, 0, 0);
                    for (int i = 0; i < buffer.Length; i++)
                    {
                        resultArray.SetValue((buffer[i] == 0 ? false : true), i / width, i % width);

                    }
                    return resultArray;
                }
                else
                {
                    System.Array resultArray = Array.CreateInstance(typeof(byte), height, width);

                    byte[] buffer = new byte[width * height];

                    band.ReadRaster(0, 0, width, height, buffer, width, height, 0, 0);
                    byte[,] bytes = new byte[height, width];
                    for (int i = 0; i < buffer.Length; i++)
                    {
                        resultArray.SetValue(buffer[i], i / width, i % width);
                        bytes[i / width, i % width] = buffer[i];
                    }
                    StringBuilder sb = new StringBuilder();
                    for (int i = 0; i < bytes.GetLength(0); i++)
                    {
                        for (int j = 0; j < bytes.GetLength(1); j++)
                        {
                            sb.Append($"{bytes[i, j]},");
                        }
                        sb.AppendLine();
                    }
                    File.WriteAllText(@"C:\Users\HP\Documents\数据.txt", sb.ToString());
                    return resultArray;
                }
            }

            ds.Dispose();
            Debug.Assert(true, "unsupported data type now!");
            return null;
        }
    }
}
