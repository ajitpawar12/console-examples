using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeZoneCheck
{
    class Program
    {
        static void Main(string[] args)
        {

            try {


                #region Check date time format and convert
                //check date time format and convert.
                string inputString = "2019/12/13";
                DateTime dDate;
                DateTime? finaldate = null;
                //var dt = Convert.ToDateTime(inputString);

                if (DateTime.TryParse(inputString, out dDate))
                {
                    //String.Format("{0:d/MM/yyyy}", dDate);
                    //String.Format("{dd/MM/yyyy}", dDate);
                    finaldate = dDate;
                }
                else
                {
                    Console.WriteLine("Invalid"); // <-- Control flow goes here
                    finaldate = DateTime.ParseExact(inputString, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

                }
                Console.WriteLine(dDate);
                #endregion

                #region Add Days to Datetime
                //Add Days to Datetime
                string datestring = "30-11-2018 1:40:00";

                DateTime datetimestring = Convert.ToDateTime(DateTime.Now.Date);

                var dttoday = DateTime.Today;

                DateTime start = DateTime.Today.AddDays(3).AddTicks(-1);

                Console.WriteLine("Sample datetime string= " + datetimestring + "\n");

                var today = datetimestring.AddDays(4);

                Console.WriteLine("Third day from samaple date = " + today + "\n");
                #endregion

                #region Checking Datetime Array to find unwanted data
                //checking datetimestring array

                var datestringArray = new List<string> { "29-11-2018 00:00", "2-12-2018 00:00", "3-12-2018 00:00", "4-12-2018 00:00" };

                for (var d = 0; d < datestringArray.Count; d++)
                {
                    var datetimearr = Convert.ToDateTime(datestringArray[d]);

                    Console.WriteLine($"No {d} =" + datetimearr + "\n");

                }

                for (var d = 0; d < datestringArray.Count; d++)
                {
                    var datetimearr = Convert.ToDateTime(datestringArray[d]);

                    if (datetimearr > datetimestring.AddDays(3))
                    {
                        Console.WriteLine("Nonuploaded date from above list, when consider sample date's trigger= " + datetimearr + "\n");
                    }

                }
                #endregion

                #region Finding different time zones
                //---current time zone
                TimeZone zone = TimeZone.CurrentTimeZone;
                string standard = zone.StandardName;
                string daylight = zone.DaylightName;
                Console.WriteLine("Current timezone.StandardName = " + standard + "\n");
                Console.WriteLine("Current timezone.DaylightName = " + daylight + "\n");

                //local and universal timezone
                DateTime local = zone.ToLocalTime(DateTime.Now);
                DateTime universal = zone.ToUniversalTime(DateTime.Now);
                Console.WriteLine("Local time zone datetime = " + local + "\n");
                Console.WriteLine("Universal time zone datetime = " + universal + "\n");

                // central america standard time
                var AmTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Central America Standard Time");
                DateTime AmTime = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.Local, AmTimeZone);
                Console.WriteLine("Central america time zone = " + AmTimeZone + "\n");
                Console.WriteLine("Current date time of Central america time zone =  " + AmTime + "\n");
                Console.WriteLine("Current date time of Central america time zone =  " + AmTime.ToString("dd/MM/yyyy hh:mm:ss.fff", CultureInfo.InvariantCulture) + "\n");


                #endregion

                #region Convert Datetime to Central datetime zones datetime

                //convert current time zone to central america time zone
                DateTime timeUtc = DateTime.UtcNow;
                try
                {
                    Console.WriteLine(timeUtc.Date);
                    TimeZoneInfo cstZone = TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time");
                    DateTime cstTime = TimeZoneInfo.ConvertTimeFromUtc(timeUtc, cstZone);
                    Console.WriteLine("The date and time are {0} {1}.",
                                      cstTime,
                                      cstZone.IsDaylightSavingTime(cstTime) ?
                                              cstZone.DaylightName : cstZone.StandardName);
                }
                catch (TimeZoneNotFoundException)
                {
                    Console.WriteLine("The registry does not define the Central Standard Time zone.");
                }
                catch (InvalidTimeZoneException)
                {
                    Console.WriteLine("Registry data on the Central Standard Time zone has been corrupted.");
                }
                #endregion

                #region Split string convert to timspan and add to datetime
                DateTime first = new DateTime(1900, 01, 01);
                string str = "88:30";
                string[] strArr = str.Split(':');
                TimeSpan hours = TimeSpan.FromHours(Convert.ToInt32(strArr[0]));
                TimeSpan minutes = TimeSpan.FromMinutes(Convert.ToInt32(strArr[1]));

                TimeSpan final = hours.Add(minutes);

                Console.WriteLine(hours + "-" + minutes);

                Console.WriteLine(final.ToString());
                DateTime finaldate1 = first + final;

                DateTime displaydate = finaldate1.AddDays(-1);

                Console.WriteLine(displaydate); 
                #endregion
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
            Console.ReadKey();

        }
    }
}
