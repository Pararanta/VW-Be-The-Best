using System;
using VW.Models;
using  VW.Service.KUKA;
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
        }
    }
}
