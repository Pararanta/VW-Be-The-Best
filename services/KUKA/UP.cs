using System;

using System.IO;

using System.Text.RegularExpressions;

using System.Collections.Generic;

using VW.Models;

namespace VW.Service.KUKA
{
    class UP
    {
        Dictionary<int, DataPoint> data_points = new Dictionary<int, DataPoint>();
        List<ProgramPoint> program_points = new List<ProgramPoint>();

        public string comment;

        Regex program_comment_regex = new Regex(@"(?<=&COMMENT ).*", RegexOptions.IgnoreCase);
        Regex name_regex = new Regex("(?<=M_COMMENT\\(\")(?!INDEX).+(?=\"\\))", RegexOptions.IgnoreCase);
        Regex index_regex = new Regex("(?<=M_COMMENT\\(\"INDEX ).+(?=\"\\))", RegexOptions.IgnoreCase);
        Regex point_index_regex = new Regex(@"(?<=Act_P1 = P)\d*", RegexOptions.IgnoreCase);

        Regex data_point_index_regex = new Regex(@"(?<=E6POS P)\d*(?= ?=)", RegexOptions.IgnoreCase);
        Regex data_point_x_regex = new Regex(@"(?<=X )[-\d\.]*(?=,)", RegexOptions.IgnoreCase);
        Regex data_point_y_regex = new Regex(@"(?<=Y )[-\d\.]*(?=,)", RegexOptions.IgnoreCase);
        Regex data_point_z_regex = new Regex(@"(?<=Z )[-\d\.]*(?=,)", RegexOptions.IgnoreCase);

        public void loadProgram(Stream file){
            using (var stream = new StreamReader(file)) {
                string line, name = null, index1 = null, index2 = null;
                while((line = stream.ReadLine()) != null)  
                {
                    if(line.StartsWith("&COMMENT ")){
                        comment = program_comment_regex.Match(line).Value;
                    }
                    Match match;
                    if((match = name_regex.Match(line)).Success) name = match.Value;
                    if((match = index_regex.Match(line)).Success){
                        string[] indexes = match.Value.Split('-');
                        index1 = indexes[0];
                        index2 = indexes[1];
                    }
                    if(name != null && index1 != null && (match = point_index_regex.Match(line)).Success){
                        program_points.Add(new ProgramPoint(
                            Regex.Replace(name, "_", ""),
                            name,
                            int.Parse(match.Value),
                            index1,
                            index2
                            ));
                        name = null;
                        index1 = null;
                        index2 = null;
                    }
                }  
            }
        }

        public void loadData(Stream file){
            using (var stream = new StreamReader(file)) {
                string line, name = null, index1 = null, index2 = null;
                while((line = stream.ReadLine()) != null)  
                {
                    if(!line.Contains("DECL E6POS")) continue;
                    int index = int.Parse(data_point_index_regex.Match(line).Value);
                    data_points.Add(index, new DataPoint(
                        float.Parse(data_point_x_regex.Match(line).Value),
                        float.Parse(data_point_y_regex.Match(line).Value),
                        float.Parse(data_point_z_regex.Match(line).Value)
                    ));
                }  
            }
        }

        public List<Point> getPoints(string robot_name, int robot_type){
            List<Point> result = new List<Point>();
            foreach(ProgramPoint program_point in program_points){
                if(!data_points.ContainsKey(program_point.data_point_index)){
                    Console.WriteLine("Missing data P" + program_point.data_point_index + " for point " + program_point.name);
                    continue;
                }
                result.Add(new Point(program_point, data_points[program_point.data_point_index], robot_name, robot_type));
            }
            return result;
        }
    }
}