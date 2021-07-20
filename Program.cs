using System;
using VW.Models;
<<<<<<< Updated upstream
using VW.Service.KUKA;
using VW.Service;
=======
using  VW.Service.KUKA;
using  VW.Service.Export;
>>>>>>> Stashed changes
namespace VW
{
    class Program
    {
        static void Main(string[] args)
        {
            var robot = new Robot();
<<<<<<< Updated upstream
            robot.readBackup(@"C:\Users\Konrad\Desktop\kaadac214010r01.zip");
            foreach(Point point in robot.getAllPoints()){
                Console.WriteLine(point);
            }
            Console.WriteLine("----");
            foreach(Point point in UAF.readUAF(@"C:\Users\Konrad\Desktop\UAF.xlsx")){
                Console.WriteLine(point);
            }
=======
            var export = new Export();
            var Qualitative_compare = new Qualitative_compare();
            robot.readBackup(@"./zip_folder/kaadac214010r01.zip");
            
            export.fill_robot_dictionary(robot.getAllPoints());
           
           Qualitative_compare.qualitative_compare(export.robot_dic);
               
>>>>>>> Stashed changes
        }
    }
}

