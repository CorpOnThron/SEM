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
            Frequency Every2Days = new Frequency(Repeat.Monthly, DateTime.Now.AddMonths(-1).AddDays(16), 1);
            List<DateTime> Result = Every2Days.GetNextDates(DateTime.Now.AddDays(60));
            Console.WriteLine($"For every 2 days starting from {DateTime.Now.AddDays(16)} and ending on (and including) {DateTime.Now.AddDays(60)} the dates are");
            foreach (DateTime x in Result)
            {
                Console.WriteLine(x.ToString());
            }
        }
    }
}
