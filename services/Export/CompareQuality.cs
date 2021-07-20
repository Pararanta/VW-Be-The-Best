using System;
using VW.Models;
using System.Collections.Generic;


namespace VW.Service.Export
{
    class Qualitative_compare{
        public string warning_massage = "ostrzeżenie";
        public string error_massage = "błąd";
        public float warning_distance = 5F;//mm
        public float error_distance = 8F;//mm

         Point point_excel = new Point();

          //point from excel
         

        public void qualitative_compare(Dictionary<string,List<Point>>  robot_dic){
            //for for excel dic
             point_excel.name = "P0001_A__0210_000100_R";
          point_excel.x = 3224.6F;
          point_excel.y = 466.7F;
          point_excel.z = 1366.1F;
          point_excel.index_1 = "008-06391";
          point_excel.index_2 = "008-06391";
          point_excel.robot_name = "4010R01";
          point_excel.robot_type = "ABAC";

            if(robot_dic.ContainsKey(point_excel.name)) {
                
                 foreach(var element_value in robot_dic[point_excel.name]){
                Console.WriteLine(dic_element.Key);
                foreach(var list_point in dic_element.Value){
                     Console.WriteLine(list_point.x);
                }
            }

        }
    }
}
}