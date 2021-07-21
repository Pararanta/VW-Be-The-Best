using System;
using VW.Models;
using VW.Service;
using  VW.Service.KUKA;
using  VW.Service.Export;
namespace VW
{
    class Program
    {
        static void Main(string[] args)
        {
            var robot = new Robot();
            var export = new Export();

            robot.readBackup(@"./zip_folder/kaadac214030r01.zip");

            export.fill_robot_dictionary(robot.getAllPoints());
            export.fill_excel_dictionary(UAF.readUAF(@"./zip_folder/UAF.xlsx")); 
            Qualitative_compare qualitative_compare = new Qualitative_compare(export.robot_dic,export.excel_dic);

           //Qualitative_compare.qualitative_compare(export.robot_dic,export.excel_dic);
           Excel_Export_Qualitative.writeQualitative(qualitative_compare);

           
            // foreach(Point point in robot.getAllPoints()){
            //     Console.WriteLine(point);
            // }
            // Console.WriteLine("----");
            // foreach(Point point in UAF.readUAF(@"./zip_folder/UAF.xlsx")){
            //     Console.WriteLine(point);
            // }
           
        }
    }
}

