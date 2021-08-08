using System;

namespace VW.Models
{
    public class ProgramPoint
    {
        public string index1;
        public string index2;
        public string name;
        public string original_name;
        public int data_point_index;

        public ProgramPoint(string _name, string _original_name, int _data_point_index, string _index1, string _index2){
            name = _name;
            original_name = _original_name;
            data_point_index = _data_point_index;
            index1 = _index1;
            index2 = _index2;
        }
    }
}