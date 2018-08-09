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
            Frequency fq1 = new Frequency(Repeat.Daily, DateTime.Now, 2); // every 2 days from start date
            Frequency fq2 = new Frequency(Repeat.Weekly, DateTime.Now, 2); // every 2 weeks from start date

            User OlekDev = new User(50, 100, DateTime.Now.AddYears(2));
            OlekDev.Category.Add("TTC");
            OlekDev.Category.Add("Rent");

            ProjectedTransaction prt1 = new ProjectedTransaction("TTC",OlekDev.Category[1], 50, Priority.High, DateTime.Now, OlekDev.BudgetEndDate );
            ProjectedTransaction prt2 = new ProjectedTransaction("Rent", OlekDev.Category[0], 150, Priority.Medium, DateTime.Now, OlekDev.BudgetEndDate);


           
            Income inc1 = new Income("Subway", 500, DateTime.Now, DateTime.Now.AddYears(2));

            inc1.SetFrequency(fq2);

            prt1.SetFrequency(fq1);
            prt2.SetFrequency(fq2);

            OlekDev.AddProjectedTransaction(prt2);
            OlekDev.AddProjectedTransaction(prt1);
            //OlekDev.AddIncome(inc1);

            OlekDev.AddActualTransaction(prt1);
            OlekDev.AddActualTransaction(prt2);

            //OlekDev.AddActualTransaction(inc1);


            OlekDev.AddData();


            foreach (Data obj in OlekDev.ListData)
                Console.WriteLine($"{obj.Date.ToLongDateString()}-{obj.Amount}");

            //OlekDev.CalculateDaysLeft();


            float tempShit = OlekDev.CalculateDaysToEscape();
            Console.WriteLine($"Min balance will be {tempShit}");
            
            
            //OlekDev.DisplayActualTransactions();

            //Frequency fq3 = new Frequency(Repeat.Monthly, DateTime.Now, 2); // every 2 months from start date
            //Frequency fq4 = new Frequency(Repeat.Yearly, new DateTime(2016, 2, 29), 2); // every 2 years from start date

            //Frequency fq5 = new Frequency(Repeat.Weekly, DateTime.Now, 2, DaysOfWeek.Monday); // every second Monday from start date

            //Frequency fq9 = new Frequency(Repeat.Monthly, DateTime.Now , 2, Position.Last, PositionDay.Wednesday); // every last wednesday from start date
            //Frequency fq10 = new Frequency(Repeat.Monthly, new DateTime(2018,08,2) , 2, 1,15,31); // every 1st, 15th, 31st of the month from start date

            //Frequency fq11 = new Frequency(Repeat.Yearly, new DateTime(2016,2,29) , 2 , Month.January | Month.February | Month.October);

            //List<DateTime> result1 = fq1.GetNextDates(DateTime.Now.AddDays(10));
            //List<DateTime> result2 = fq2.GetNextDates(DateTime.Now.AddDays(10*7));
            //List<DateTime> result3 = fq3.GetNextDates(DateTime.Now.AddDays(10*30));
            //List<DateTime> result4 = fq4.GetNextDates(DateTime.Now.AddDays(10*365));

            //List<DateTime> result5 = fq5.GetNextDates(DateTime.Now.AddDays(10*7));

            //List<DateTime> result9 = fq9.GetNextDates(DateTime.Now.AddDays(10*30));
            //List<DateTime> result10 = fq10.GetNextDates(DateTime.Now.AddDays(10*30));

            //List<DateTime> result11 = fq11.GetNextDates(DateTime.Now.AddDays(10*365));

            //foreach (DateTime dt in result1)
            //    Console.WriteLine($"{dt.ToLongDateString()} : every 2 days from start date");

            //Console.WriteLine("\n\n");
            //foreach (DateTime dt in result2)
            //    Console.WriteLine($"{dt.ToLongDateString()} : every 2 weeks from start date");

            //Console.WriteLine("\n\n");
            //foreach (DateTime dt in result3)
            //    Console.WriteLine($"{dt.ToLongDateString()} : every 2 months from start date");

            //Console.WriteLine("\n\n");
            //foreach (DateTime dt in result4)
            //    Console.WriteLine($"{dt.ToLongDateString()} : every 2 years from start date");

            //Console.WriteLine("\n\n");
            //foreach (DateTime dt in result5)
            //    Console.WriteLine($"{dt.ToLongDateString()} : every 2 weeks from start date on mondays");

            //Console.WriteLine("\n\n(if starts on 2nd august, it will display first three dates that is.. 15th and 31st of august and 1st of september)\n");
            //foreach (DateTime dt in result10)
            //    Console.WriteLine($"{dt.ToLongDateString()} : every 2 months on 1st 15th and 31st starting from start date");

            //Console.WriteLine("\n\n");
            //foreach (DateTime dt in result9)

            //Console.WriteLine("\n\n");
            //foreach (DateTime dt in result11)
            //    Console.WriteLine($"{dt.ToLongDateString()} : every Jan, feb, oct every 2 years");



        }
    }
}
