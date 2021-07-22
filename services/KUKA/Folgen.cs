using System;

using System.IO;

using System.Text.RegularExpressions;

using System.Collections.Generic;

using VW.Models;

namespace VW.Service.KUKA
{
    public class Folgen
    {
        Regex up_id_regex = new Regex(@"(?<=SEL_RES=SELECT\(#UP,)\d*", RegexOptions.IgnoreCase);
        Regex up_condition_regex = new Regex(@"(?<=,)[^,]*(?=\))", RegexOptions.IgnoreCase);

        public Dictionary<int, List<string>> ups = new Dictionary<int, List<string>>();

        public void loadProgram(Stream file){
            using (var stream = new StreamReader(file)) {
                string line, name = null, index1 = null, index2 = null;
                while((line = stream.ReadLine()) != null)  
                {
                    if(!line.Contains("SELECT") || !line.Contains("#UP")) continue;
                    int key = int.Parse(up_id_regex.Match(line).Value);
                    if(!ups.ContainsKey(key)) ups.Add(key, new List<string>());
                    ups[key].Add(up_condition_regex.Match(line).Value);
                }  
            }
        }
    }
}
