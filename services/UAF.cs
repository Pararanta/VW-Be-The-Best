using System;

using System.IO;

using System.Linq;

using System.Text.RegularExpressions;

using NPOI.XSSF.UserModel;
using NPOI.SS.UserModel;

using System.Collections.Generic;

using VW.Models;

namespace VW.Service
{
    public class UAF
    {
        public static List<Point> readUAF(string path){
            List<Point> result = new List<Point>();

            IWorkbook wb;
            using (FileStream file = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                wb = new XSSFWorkbook(file);
            }

            ISheet sheet = wb.GetSheetAt(0);
            for (int row = 0; row <= sheet.LastRowNum; row++)
            {
                if (sheet.GetRow(row) == null) continue;
                float x = (float)sheet.GetRow(row).GetCell(4).NumericCellValue;
                float y = (float)sheet.GetRow(row).GetCell(5).NumericCellValue;
                float z = (float)sheet.GetRow(row).GetCell(6).NumericCellValue;
                string[] index = sheet.GetRow(row).GetCell(45).StringCellValue.Split("-");
                string index1 = index[0];
                string index2 = "";
                if(index.Length > 1) index2 = index[1];
                string original_name = sheet.GetRow(row).GetCell(3).StringCellValue;
                string name = Regex.Replace(original_name.Replace(Regex.Replace(sheet.GetRow(row).GetCell(0).StringCellValue, "\\.", "_"), ""), "_", "");
                string robot_name = sheet.GetRow(row).GetCell(59).StringCellValue;
                result.Add(new Point(
                    x,
                    y,
                    z,
                    index1,
                    index2,
                    name,
                    original_name,
                    robot_name,
                    0
                ));
            }
            return result;
        }
    }
}