using System;
using VW.Models;
using System.Collections.Generic;


namespace VW.Service.Export
{
    class Export
    {

        public Dictionary<string,List<Point>> excel_dic = new Dictionary<string,List<Point>>();
        public Dictionary<string,List<Point>> robot_dic = new Dictionary<string,List<Point>>();

        public Dictionary<string,List<Point>> make_dic(List<Point> points_list){
            Dictionary<string,List<Point>> points_dictionary = new Dictionary<string,List<Point>>();
            foreach( var point in points_list){

                if(!points_dictionary.ContainsKey(point.name)) points_dictionary.Add(point.name, new List<Point>());
                points_dictionary[point.name].Add(point);
              
            }
            return points_dictionary;
        }

         public void fill_excel_dictionary( List<Point> points_list_excel){
            excel_dic = make_dic(points_list_excel);
        }
         public void fill_robot_dictionary(List<Point> points_list_robot){
            robot_dic = make_dic(points_list_robot);
        }   

    }
}