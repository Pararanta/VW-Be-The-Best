using System;
using VW.Models;
using System.Collections.Generic;


namespace VW.Service.Export
{
    class Qualitative_compare{
        public static string warning_massage = "ostrzeżenie";
        public static string error_massage = "błąd";
        public static string normal_massage = "w granicach błędu";
        public static float warning_distance = 5F;//mm
        public static float error_distance = 8F;//mm
        public int error_counter = 0;
        public static int warning_counter = 0;
        public static int normal_counter = 0;
        public static int total_elements = 0;
        public List<Qualitative_point_result> qualitative_list_result = new  List<Qualitative_point_result>();
           

          //point from excel
         

        public Qualitative_compare(Dictionary<string,List<Point>>  robot_dic,Dictionary<string,List<Point>>  excel_dic){
            //for for excel dic
              //P0001_A__0210_000100_R



             foreach(var element_in_robot_dic in robot_dic){
             Qualitative_point_result temp_qualitative_point;
                //Console.WriteLine(element_in_robot_dic.Key);
 
                if(excel_dic.ContainsKey(element_in_robot_dic.Key)){
                    List<Point> control_list = new List<Point>();
                    control_list = excel_dic[element_in_robot_dic.Key];
                    
                    foreach(var real_point in element_in_robot_dic.Value){
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

                                Deviation_massage deviation_massage = new Deviation_massage(-1,"brak");

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
                                
                                 temp_qualitative_point = new Qualitative_point_result(deviation_massage,real_point);

                                 qualitative_list_result.Add(temp_qualitative_point);

                                total_elements++;
                               // Console.WriteLine("--------------");
                             
                            }
                        }
                       
                    }
                    
                    // foreach(var expected_point in control_list){
                    //     Console.Write("expected_point y: ");
                        
                    //     Console.WriteLine(expected_point.index1);
                    //     Console.WriteLine(expected_point.index2);
                    // }
                    //  foreach(var real_point in element_in_robot_dic.Value){
                    //     Console.Write("real_point y: ");
                    //     Console.WriteLine(real_point.y);
                    //     Console.WriteLine(real_point.index1);
                    //     Console.WriteLine(real_point.index2);
                    // }
                }
                else Console.Write("nie znaleziono\n");
                       
            }

             Console.WriteLine("Ilość elementów : " + total_elements.ToString());
             Console.WriteLine("Ilość elementów w granicach błędu : " + normal_counter.ToString());
             Console.WriteLine("Ilość elementów z ostrzeżeniami : " + warning_counter.ToString());
             Console.WriteLine("Ilość elementów z błędami : " + error_counter.ToString());
        }
    }
}