using System;
using VW.Models;
using System.Collections.Generic;

using System.IO;



using System.Linq;

using System.Text.RegularExpressions;

using NPOI.XSSF.UserModel;
using NPOI.SS.UserModel;



namespace VW.Service.Export
{
    public class Excel_Export_Qualitative{

         public static void writeQualitative(Qualitative_compare qualitative_result_obj, string path){
            

            XSSFWorkbook workbook = new XSSFWorkbook();
            

            List<Qualitative_point_result> quali_list_res = qualitative_result_obj.qualitative_list_result;

            ISheet sheet = workbook.CreateSheet("Report");



            
            int counter = 1;
           foreach(var quali_res in quali_list_res){
                IRow row = sheet.CreateRow(counter);

                ICell cell = row.CreateCell(0);
                cell.SetCellValue(quali_res.point.name);

                cell = row.CreateCell(1);
                cell.SetCellValue(quali_res.point.y);

                cell = row.CreateCell(2);
                cell.SetCellValue(quali_res.point.z);

                cell = row.CreateCell(3);
                cell.SetCellValue(quali_res.deviation_massage.point_deviation);

                cell = row.CreateCell(4);
                cell.SetCellValue(quali_res.deviation_massage.massage);
                counter++;
                
           }


for (int i = 0; i <= 20; i++) sheet.AutoSizeColumn(i);


            using (FileStream stream = new FileStream(path, FileMode.Create, FileAccess.Write))
            {
                workbook.Write(stream);
            } 
        }   

    }
}