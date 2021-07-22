using System;
using VW.Models;
using System.Linq;
using System.Collections.Generic;


namespace VW.Service.Export
{
    class Qualitative_compare{
        public static string warning_massage = "warning";
        public static string error_massage = "error";
        public static string normal_massage = "OK";
        public static float warning_distance = 5F;//mm
        public static float error_distance = 8F;//mm
        public int error_counter = 0;
        public int warning_counter = 0;
        public int normal_counter = 0;
        public int no_UP_counter = 0;
        public int no_UAF_counter = 0;
        public int total_elements = 0;
        public List<Qualitative_point_result> qualitative_list_result = new  List<Qualitative_point_result>();
           

          //point from excel
         

        public Qualitative_compare(Dictionary<string,List<Point>> robot_dic,Dictionary<string,List<Point>> excel_dic){
            //for for excel dic
              //P0001_A__0210_000100_R

            List<string> all_points = new List<string>();
            all_points.AddRange(excel_dic.Keys.ToList());
            all_points.AddRange(robot_dic.Keys.ToList());
            IEnumerable<string> distinct_all_points = all_points.Distinct();

            Qualitative_point_result temp_qualitative_point;
            Deviation_massage deviation_massage = new Deviation_massage(-1,"");

foreach(string name in distinct_all_points){


    if(excel_dic.ContainsKey(name) && robot_dic.ContainsKey(name)){
        List<Point> control_list = new List<Point>();
        control_list = excel_dic[name];

        



         foreach(var real_point in robot_dic[name]){
                        foreach( Point expected_point in control_list){
                            if((expected_point.index1 == real_point.index1) && (expected_point.index2 == real_point.index2)){ 
                                //  Console.WriteLine("--------------");
                                // Console.WriteLine(expected_point.index1 + " " + expected_point.index2);   

                                // Console.Write("expected_point y: ");
                                // Console.WriteLine(expected_point.y);  
                                // Console.Write("expected_point z: ");
                                // Console.WriteLine(expected_point.z);  

                                // Console.Write("real_point y: ");
                                // Console.WriteLine(real_point.y);
                                // Console.Write("real_point z: ");
                                // Console.WriteLine(real_point.z);


                                double temp_y = Math.Abs(expected_point.y - real_point.y); 
                                double temp_z = Math.Abs(expected_point.z - real_point.z);
                                 
                                double point_deviation = Math.Sqrt((temp_y*temp_y)+(temp_z*temp_y));

                                

                                // Console.Write("point_deviation : ");
                                // Console.WriteLine(point_deviation);
                                if(point_deviation<warning_distance){
                                    // Console.WriteLine(normal_massage);

                                    deviation_massage = new Deviation_massage(point_deviation,normal_massage);
                
                                    normal_counter++;
                                } 
                                else if(point_deviation>=warning_distance && point_deviation<error_distance){
                                    //Console.WriteLine(warning_massage);

                                     deviation_massage = new Deviation_massage(point_deviation,warning_massage);

                                    warning_counter++;
                                }
                                else if(point_deviation>=error_distance){
                                    //Console.WriteLine(error_massage);

                                    deviation_massage = new Deviation_massage(point_deviation,error_massage);

                                    error_counter++;
                                }
                                
                                 temp_qualitative_point = new Qualitative_point_result(deviation_massage,expected_point);

                                 qualitative_list_result.Add(temp_qualitative_point);

                                total_elements++;
                               // Console.WriteLine("--------------");
                             
                            }
                        }
                       
                    }



    }
    else if(excel_dic.ContainsKey(name)){

    //jest tylko w excel
    deviation_massage = new Deviation_massage(-1,"No data in UP");

     temp_qualitative_point = new Qualitative_point_result(deviation_massage,excel_dic[name].First());
        qualitative_list_result.Add(temp_qualitative_point);

        no_UP_counter++;
        total_elements++;



  } 
  else if(robot_dic.ContainsKey(name)){
      //jest tylko w robocie
       deviation_massage = new Deviation_massage(-1,"No data in UAF");
       // jak wprowadzić dane as {-1,massage}

        temp_qualitative_point = new Qualitative_point_result(deviation_massage,robot_dic[name].First());
        qualitative_list_result.Add(temp_qualitative_point);
        
        no_UAF_counter++;
        total_elements++;


  }

}






            //  foreach(var element_in_robot_dic in robot_dic){
            //  Qualitative_point_result temp_qualitative_point;
            //  Deviation_massage deviation_massage = new Deviation_massage(-1,"nie znaleziono w UAF");
            //     //Console.WriteLine(element_in_robot_dic.Key);
 
            //     if(excel_dic.ContainsKey(element_in_robot_dic.Key)){
            //         List<Point> control_list = new List<Point>();
            //         control_list = excel_dic[element_in_robot_dic.Key];
                    
                    
            //         foreach(var real_point in element_in_robot_dic.Value){
            //             foreach( Point expected_point in control_list){
            //                 if((expected_point.index1 == real_point.index1) && (expected_point.index2 == real_point.index2)){ 
            //                     //  Console.WriteLine("--------------");
            //                     // Console.WriteLine(expected_point.index1 + " " + expected_point.index2);   

            //                     // Console.Write("expected_point y: ");
            //                     // Console.WriteLine(expected_point.y);  
            //                     // Console.Write("expected_point z: ");
            //                     // Console.WriteLine(expected_point.z);  

            //                     // Console.Write("real_point y: ");
            //                     // Console.WriteLine(real_point.y);
            //                     // Console.Write("real_point z: ");
            //                     // Console.WriteLine(real_point.z);


            //                     double temp_y = Math.Abs(expected_point.y - real_point.y); 
            //                     double temp_z = Math.Abs(expected_point.z - real_point.z);
                                 
            //                     double point_deviation = Math.Sqrt((temp_y*temp_y)+(temp_z*temp_y));

                                

            //                     // Console.Write("point_deviation : ");
            //                     // Console.WriteLine(point_deviation);
            //                     if(point_deviation<warning_distance){
            //                         // Console.WriteLine(normal_massage);

            //                         deviation_massage = new Deviation_massage(point_deviation,normal_massage);
                
            //                         normal_counter++;
            //                     } 
            //                     else if(point_deviation>=warning_distance && point_deviation<error_distance){
            //                         //Console.WriteLine(warning_massage);

            //                          deviation_massage = new Deviation_massage(point_deviation,warning_massage);

            //                         warning_counter++;
            //                     }
            //                     else if(point_deviation>=error_distance){
            //                         //Console.WriteLine(error_massage);

            //                         deviation_massage = new Deviation_massage(point_deviation,error_massage);

            //                         error_counter++;
            //                     }
                                
            //                      temp_qualitative_point = new Qualitative_point_result(deviation_massage,real_point);

            //                      qualitative_list_result.Add(temp_qualitative_point);

            //                     total_elements++;
            //                    // Console.WriteLine("--------------");
                             
            //                 }
            //             }
                       
            //         }
                    
            //         // foreach(var expected_point in control_list){
            //         //     Console.Write("expected_point y: ");
                        
            //         //     Console.WriteLine(expected_point.index1);
            //         //     Console.WriteLine(expected_point.index2);
            //         // }
            //         //  foreach(var real_point in element_in_robot_dic.Value){
            //         //     Console.Write("real_point y: ");
            //         //     Console.WriteLine(real_point.y);
            //         //     Console.WriteLine(real_point.index1);
            //         //     Console.WriteLine(real_point.index2);
            //         // }
            //     }
            //     else {
                    
            //         temp_qualitative_point = new Qualitative_point_result(deviation_massage,element_in_robot_dic.Value.First());
            //         qualitative_list_result.Add(temp_qualitative_point);
            //         total_elements++;
            //     }
                       
            // }

            //  Console.WriteLine("Ilość elementów : " + total_elements.ToString());
            //  Console.WriteLine("Ilość elementów w granicach błędu : " + normal_counter.ToString());
            //  Console.WriteLine("Ilość elementów z ostrzeżeniami : " + warning_counter.ToString());
            //  Console.WriteLine("Ilość elementów z błędami : " + error_counter.ToString());
        }
    }
}