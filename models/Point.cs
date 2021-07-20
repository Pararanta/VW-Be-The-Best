using System;

namespace VW.Models
{
    class Point
    {
        public float x;
        public float y;
        public float z;
        public string index1;
        public string index2;
        public string name;
        public string robot_name;
        public int robot_type;

        public Point( ProgramPoint _programPoint, DataPoint _dataPoint, string _robot_name, int _robot_type ){
            x = _dataPoint.x;
            y = _dataPoint.y;
            z = _dataPoint.z;
            index1 = _programPoint.index1;
            index2 = _programPoint.index2;
            name = _programPoint.name;
            robot_name = _robot_name;
            robot_type = _robot_type;
        }

        public override string ToString(){
            return name + " (" + index1 + "-" + index2 + ") (" + x + ", " + y + ", " + z + ")";
        }
    }
}