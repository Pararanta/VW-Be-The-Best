using System;
using VW.Models;
using VW.Service;
using VW.Service.KUKA;
using VW.Service.Export;
namespace VW
{
    class Program
    {
        static void Main(string[] args)
        {
            var robot = new Robot(@"C:\Users\Konrad\Desktop\kaadac214010r01.zip");
            var export = new Export();

            export.fill_robot_dictionary(robot.getAllPoints());
            export.fill_excel_dictionary(UAF.readUAF(@"C:\Users\Konrad\Desktop\UAF.xlsx")); 
            Qualitative_compare qualitative_compare = new Qualitative_compare(export.robot_dic,export.excel_dic);

           //Qualitative_compare.qualitative_compare(export.robot_dic,export.excel_dic);
           Excel_Export_Qualitative.writeQualitative(qualitative_compare, @"C:\Users\Konrad\Desktop\outfile.xlsx");
        }
    }
}

