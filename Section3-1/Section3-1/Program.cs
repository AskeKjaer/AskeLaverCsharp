using System;

namespace Section3_1
{
    class Program
    {
        private static int YearValidator(string input) 
        {
            int Year = 0;
            {
                if ((int.TryParse(input, out Year)))
                {
                    Year = int.Parse(input);
                    if (Year >= 0 && Year < 10000)
                    {
                        return Year;
                    }
                    else
                    {
                        Console.WriteLine("Please enter a valid year. Using 2020.");
                        return 2020;
                    }
                }
                else
                {
                    Console.WriteLine("Please enter a valid year. Using 2020.");
                    return 2020;
                }
            }
        }
        static void Main(string[] args)
        {
            int CurrentYear = 0;
            string CurrentYearInput;
            bool IsYear = false;
            Console.WriteLine("What year is it?");
            CurrentYearInput = Console.ReadLine();
            CurrentYear = YearValidator(CurrentYearInput);
            if (CurrentYear % 4 == 0 && (CurrentYear % 100 != 0 || CurrentYear % 400 == 0))
            {
                Console.WriteLine($"{CurrentYear} is a Leap Year!");
            }
            else
            {
                Console.WriteLine($"{CurrentYear} is not a Leap Year!");
            }
            Console.WriteLine("Press Any Key to Exit");
            Console.Read();
        }
    }
}
