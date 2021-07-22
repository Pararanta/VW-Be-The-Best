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
    class Excel_Export_Qualitative{

         public static void writeQualitative(Qualitative_compare qualitative_result_obj){
            

            XSSFWorkbook workbook = new XSSFWorkbook();
            

            List<Qualitative_point_result> quali_list_res = qualitative_result_obj.qualitative_list_result;

            ISheet sheet = workbook.CreateSheet("My sheet");

            IDataFormat dataformat = workbook.CreateDataFormat();
            ICellStyle style = workbook.CreateCellStyle();
            style.DataFormat = dataformat.GetFormat ( "0.00");


            ICellStyle styleMiddle = workbook.CreateCellStyle();
            styleMiddle.Alignment = HorizontalAlignment.Center;
            styleMiddle.VerticalAlignment = VerticalAlignment.Center;



            IRow row = sheet.CreateRow(0);

                ICell cell = row.CreateCell(0);
                cell.SetCellValue("Name");
                cell.CellStyle = styleMiddle;

                cell = row.CreateCell(1);
                cell.SetCellValue("Y");
                cell.CellStyle = styleMiddle;

                cell = row.CreateCell(2);
                cell.SetCellValue("Z");
                cell.CellStyle = styleMiddle;
                
                cell = row.CreateCell(3);
                cell.SetCellValue("Index");
                cell.CellStyle = styleMiddle;

                cell = row.CreateCell(4);
                cell.SetCellValue("Point deviation");
                cell.CellStyle = styleMiddle;

                cell = row.CreateCell(5);
                cell.SetCellValue("Massage");
                cell.CellStyle = styleMiddle;
                



            
            int counter = 1;
           foreach(var quali_res in quali_list_res){

                row = sheet.CreateRow(counter);

                cell = row.CreateCell(0);
                cell.SetCellValue(quali_res.point.name);

                cell = row.CreateCell(1);
                cell.SetCellValue(quali_res.point.y);
                cell.CellStyle = style;

                cell = row.CreateCell(2);
                cell.SetCellValue(quali_res.point.z);
                cell.CellStyle = style;


                cell = row.CreateCell(3);   
                cell.SetCellValue(quali_res.point.index1+quali_res.point.index2);
                cell.CellStyle = styleMiddle;
                
                cell = row.CreateCell(4);
                if(quali_res.deviation_massage.point_deviation!=-1)
                cell.SetCellValue(quali_res.deviation_massage.point_deviation);
                cell.CellStyle = style;

                cell = row.CreateCell(5);
                cell.SetCellValue(quali_res.deviation_massage.massage);
                cell.CellStyle = styleMiddle;
                
                counter++;
                
           }

           counter++;

           row = sheet.CreateRow(counter);

                cell = row.CreateCell(0);
                cell.SetCellValue("total elements: ");
                
                cell = row.CreateCell(1);
                cell.SetCellValue(qualitative_result_obj.total_elements);

            row = sheet.CreateRow(counter+1);

                cell = row.CreateCell(0);
                cell.SetCellValue("Small diviation: ");
                
                cell = row.CreateCell(1);
                cell.SetCellValue(qualitative_result_obj.normal_counter);

            row = sheet.CreateRow(counter+2);

                cell = row.CreateCell(0);
                cell.SetCellValue("warnings: ");
                
                cell = row.CreateCell(1);
                cell.SetCellValue(qualitative_result_obj.warning_counter);

            row = sheet.CreateRow(counter+3);

                cell = row.CreateCell(0);
                cell.SetCellValue("errors: ");
                
                cell = row.CreateCell(1);
                cell.SetCellValue(qualitative_result_obj.error_counter);   
                
            row = sheet.CreateRow(counter+4);

                cell = row.CreateCell(0);
                cell.SetCellValue("No UPS data: ");
                
                cell = row.CreateCell(1);
                cell.SetCellValue(qualitative_result_obj.no_UP_counter);

            row = sheet.CreateRow(counter+5);

                cell = row.CreateCell(0);
                cell.SetCellValue("No UAF data: ");
                
                cell = row.CreateCell(1);
                cell.SetCellValue(qualitative_result_obj.no_UAF_counter);



; 



for (int i = 0; i <= 20; i++) sheet.AutoSizeColumn(i);


            using (FileStream stream = new FileStream("./outfile.xlsx", FileMode.Create, FileAccess.Write))
            {
                workbook.Write(stream);
            } 
        }   

    }
}