using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp45
{
    class Test
    {
        public static void Main()
        {
            Frequency freq = new Frequency(Repeat.Daily, DateTime.Now,1);
            List<string> cat = new List<string>();
            cat[0] = "Noname";
            Transaction trans = new Transaction(DateTime.Now,DateTime.Now.AddDays(1), freq, "test1",25,cat);
             
            Console.WriteLine(trans);

            //Frequency Every2DaysAWeek = new Frequency(Repeat.Monthly, DateTime.Now.AddMonths(-1), 1, Position.First, PositionDay.Monday);
           // List<DateTime> Result = Every2DaysAWeek.GetNextDates(DateTime.Now.AddDays(60));
           // Console.WriteLine($"For every 2 days a week starting from {DateTime.Now.AddDays(16)} and ending on (and including) {DateTime.Now.AddDays(60)} the dates are");
            //foreach (DateTime x in Result)
            //{
            //    Console.WriteLine(x.ToString());
            //}
        }
    }
}
