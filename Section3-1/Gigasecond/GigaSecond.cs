using System;

namespace Gigasecond
{
    class GigaSecond
    {
        static void Main(string[] args)
        {
            var culture = new System.Globalization.CultureInfo("en-US");
            Console.WriteLine("Please Specify a Time in Format " + (culture.DateTimeFormat.FullDateTimePattern).ToString());
            string timestring = Console.ReadLine();
            DateTime TheTime = DateTime.Parse(timestring);
            var TheLaterTime = TheTime.AddSeconds(1000000000);
            Console.WriteLine(TheLaterTime);
            Console.Read();

        
        }
    }
}
