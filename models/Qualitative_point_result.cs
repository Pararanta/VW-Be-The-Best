using System;

namespace VW.Models
{
    class Qualitative_point_result
    {
        public Deviation_massage deviation_massage;
        public Point point;
        public Qualitative_point_result(Deviation_massage _deviation_massage, Point _point){
            deviation_massage = _deviation_massage;
            point = _point;
        }
    }
}