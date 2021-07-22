using System;

using System.IO;
using System.IO.Compression;

using System.Collections.Generic;

using System.Text.RegularExpressions;

using VW.Models;

namespace VW.Service.KUKA
{
    public class Robot
    {
        public Dictionary<int, UP> ups = new Dictionary<int, UP>();
        public Dictionary<int, Folgen> folgens = new Dictionary<int, Folgen>();
        public string name = "";
        public Robot(string path){
            name = Path.GetFileNameWithoutExtension(path).ToUpper();
            using (ZipArchive archive = ZipFile.OpenRead(path))
            {
                foreach (ZipArchiveEntry entry in archive.Entries)
                {
                    if(!entry.FullName.StartsWith("KRC/R1")) continue;
                    if(entry.FullName.StartsWith("KRC/R1/UPs")){
                        string[] file_data = entry.Name.Split('.');
                        int key = int.Parse(Regex.Replace(file_data[0], "[^.0-9]", ""));
                        if(!ups.ContainsKey(key)) ups.Add(key, new UP(name));
                        if(file_data[1] == "src") ups[key].loadProgram(entry.Open());
                        if(file_data[1] == "dat") ups[key].loadData(entry.Open());
                    }
                    if(entry.FullName.StartsWith("KRC/R1/Folgen")){
                        string[] file_data = entry.Name.Split('.');
                        if(file_data[1] != "src") continue;
                        string folge_num = Regex.Replace(file_data[0], "[^.0-9]", "");
                        if(folge_num.Length == 0) continue;
                        int key = int.Parse(folge_num);
                        if(!folgens.ContainsKey(key)) folgens.Add(key, new Folgen());
                        folgens[key].loadProgram(entry.Open());
                    }
                }
            }
        }

        public List<Point> getAllPoints(){
            List<Point> result = new List<Point>();
            foreach(var up in ups)
            {
                result.AddRange(up.Value.getPoints());
            }
            return result;
        }
    }
}
