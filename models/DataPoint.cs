using System;

namespace VW.Models
{
    public class DataPoint
    {
        public float x;
        public float y;
        public float z;

        public DataPoint(float _x, float _y, float _z){
            x = _x;
            y = _y;
            z = _z;
        }
    }
}