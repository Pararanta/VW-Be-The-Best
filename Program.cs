using System;
using VW.Models;
using VW.Service.KUKA;
using VW.Service;
namespace VW
{
    class Program
    {
        static void Main(string[] args)
        {
            var robot = new Robot();
            robot.readBackup(@"C:\Users\Konrad\Desktop\kaadac214010r01.zip");
            foreach(Point point in robot.getAllPoints()){
                Console.WriteLine(point);
            }
            Console.WriteLine("----");
            foreach(Point point in UAF.readUAF(@"C:\Users\Konrad\Desktop\UAF.xlsx")){
                Console.WriteLine(point);
            }
        }
    }
}
