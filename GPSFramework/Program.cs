using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPSFramework
{
    class Program
    {
        //initiates to COM3

        ///$GPRMC,135825.000,A,4141.9643,N,08739.5435,W,58.20,13.03,170120,,,A*75
        ///
        static void Main(string[] args)
        {
            System.IO.Ports.SerialPort gpsChip = new System.IO.Ports.SerialPort("COM3");
            if (gpsChip.IsOpen == false) //if not open, open the port
                gpsChip.Open();
            //do your work here

            bool running = true;

            while (running)
            {
                var line = gpsChip.ReadLine();
              
                if (line.Substring(0, 6) == "$GPRMC")
                {
                    var gPRMC = CreateGPRMC(line);

                    //Console.WriteLine("Date is {0}", gPRMC.Date);
                    //Console.WriteLine(gPRMC.IsValidData);
                    Console.WriteLine("Latitute: {0}{1} ; Longitude {2}{3}", gPRMC.LongitudeStamp, gPRMC.LongitudeDirection, gPRMC.LatitudeStamp, gPRMC.LongitudeDirection);
                    Console.WriteLine("Speed: {0} Knots", gPRMC.Knots);
                }
            }

            gpsChip.Close();

            Console.ReadLine();
        }

        //millisecond is being logged from the sensor in case we want it in the future.
        public static DateTime ConvertToDateTime(string timeStamp, string dateStamp)
        {
            var year = Convert.ToInt32("20" + dateStamp.Substring(4, 2));
            var month = Convert.ToInt32(dateStamp.Substring(2, 2));
            var day = Convert.ToInt32(dateStamp.Substring(0, 2));
            var hour = Convert.ToInt32(timeStamp.Substring(0, 2));
            var minute = Convert.ToInt32(timeStamp.Substring(2,2));
            var second = Convert.ToInt32(timeStamp.Substring(4, 2));
            var millisecond = Convert.ToInt32(timeStamp.Substring(7, 3));
            
            var dateTime = new DateTime(year, month, day, hour, minute, second, millisecond);

            return dateTime;
        }

        public static bool IsValidData(string statusCode)
        {
            if(statusCode == "A")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static GPRMC CreateGPRMC(string line)
        {
            string[] gpsData = line.Split(',');

            GPRMC gPRMC = new GPRMC()
            {
                Acroynm = gpsData[0],
                StatusCode = gpsData[2],
                LatitudeStamp = gpsData[3],
                LatitudeDirection = gpsData[4],
                LongitudeStamp = gpsData[5],
                LongitudeDirection = gpsData[6],
                Knots = gpsData[7],
                TrackingAngle = gpsData[8],
                CheckSum = gpsData[10],
                Date = ConvertToDateTime(gpsData[1], gpsData[9]),
                IsValidData = IsValidData(gpsData[2])
            };

            return gPRMC;
        }
    }
}
