using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPSFramework
{
    ///$GPRMC,135825.000,A,4141.9643,N,08739.5435,W,58.20,13.03,170120,,,A*75
    class GPRMC
    {
        //first portion of properties is from csv data that GPS Module spits out.
        public string Acroynm { get; set; }
        public string TimeStamp { get; set; }
        public string StatusCode { get; set; }
        public string LatitudeStamp { get; set; }
        public string LatitudeDirection { get; set; }
        public string LongitudeStamp { get; set; }
        public string LongitudeDirection { get; set; }
        public string Knots { get; set; }
        public string TrackingAngle { get; set; }
        public string DateStamp { get; set; }
        public string CheckSum { get; set; }

        //second portion is after doing some cleanup.
        public DateTime Date { get; set; }
        public bool IsValidData { get; set; }
    }
}
