using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp45
{
    class Frequency
    {
        Repeat Repeat;
        DateTime StartDate;
        int EveryXOccurance;
        DaysOfWeek DaysOfWeek;
        int[] DaysOfMonth;
        Position Position;
        PositionDay DayOfPosition;
        Month Month;
        FrequencyType FrequencyType;


        /*****CONSTRUCTORS*****/

        public Frequency(Repeat repeat, DateTime startDate, int EveryXOccurance=1) // for all four
        {
            this.Repeat = repeat;
            this.StartDate = startDate;
            this.EveryXOccurance = EveryXOccurance;
            this.FrequencyType = FrequencyType.Standard;
        }

        public Frequency(Repeat repeat, DateTime startDate, int EveryXOccurance = 1, DaysOfWeek daysOfWeek = DaysOfWeek.Sunday) // weekly overloaded
        {
            this.Repeat = repeat;
            this.StartDate = startDate;
            this.EveryXOccurance = EveryXOccurance;
            this.FrequencyType = FrequencyType.Weekly;
            this.DaysOfWeek = daysOfWeek;
        }

        public Frequency(Repeat repeat, DateTime startDate, int EveryXOccurance = 1, params int[] daysOfMonth ) // monthly overloaded
        {
            this.Repeat = repeat;
            this.StartDate = startDate;
            this.EveryXOccurance = EveryXOccurance;
            this.FrequencyType = FrequencyType.MonthlyTypeA;
            this.DaysOfMonth = daysOfMonth;
        }

        public Frequency(Repeat repeat, DateTime startDate, int EveryXOccurance, Position position, PositionDay dayOfPosition) // monthly overloaded
        {
            this.Repeat = repeat;
            this.StartDate = startDate;
            this.EveryXOccurance = EveryXOccurance;
            this.FrequencyType = FrequencyType.MonthlyTypeB;
            this.Position = position;
            this.DayOfPosition = dayOfPosition;
        }

        public Frequency(Repeat repeat, DateTime startDate, int EveryXOccurance, Month month, Position position, PositionDay dayOfPosition) // Yearly Overloaded
        {
            this.Repeat = repeat;
            this.StartDate = startDate;
            this.EveryXOccurance = EveryXOccurance;
            this.FrequencyType = FrequencyType.Yearly;
            this.Month = month;
            this.Position = position;
            this.DayOfPosition = dayOfPosition;
        }

        /*****FUNCTIONS*****/

        public List<DateTime> GetNextDates(DateTime EndDate)
        {
            Console.WriteLine("I have been called");
            List<DateTime> Result = new List<DateTime>();
            if (StartDate < EndDate)
            {
                switch (FrequencyType)
                {
                    case FrequencyType.Standard:
                        DateTime counter = StartDate;
                        switch (Repeat)
                        {
                            case Repeat.Daily:
                                while (counter.ToOADate() < EndDate.ToOADate())
                                { 
                                    Result.Add(counter);
                                    counter = counter.AddDays(EveryXOccurance);
                                }
                                break;
                            case Repeat.Weekly:
                                while (counter.ToOADate() <= EndDate.ToOADate())
                                {
                                    Result.Add(counter);
                                    counter = counter.AddDays(7*EveryXOccurance);
                                }
                                break;
                            case Repeat.Monthly:
                                while(counter.ToOADate() <= EndDate.ToOADate())
                                {
                                    Result.Add(counter);
                                    counter = counter.AddMonths(EveryXOccurance);
                                }
                                break;
                            case Repeat.Yearly:
                                while(counter.ToOADate() <= EndDate.ToOADate())
                                {
                                    Result.Add(counter);
                                    counter = counter.AddYears(EveryXOccurance);
                                }
                                break;
                        }
                        break;
                }
                return Result;
            }
            else
            {
                // ERROR: EndDate is earlier than StartDate
                Console.WriteLine("EndDate is earlier than StartDate");
                return Result;
            }
        }
    }
    
    enum FrequencyType
    {
        Standard,
        Weekly,
        MonthlyTypeA,
        MonthlyTypeB,
        Yearly
    }

    enum Position
    {
        First,
        Second,
        Third,
        Fourth,
        Fifth,
        Last
    }

    enum PositionDay
    {
        Day,
        Weekday,
        WeekendDay,
        Sunday,
        Monday,
        Tuesday,
        Wednesday,
        Thursday,
        Friday,
        Saturday
    }

    [Flags]
    enum Month
    {
        January,
        February,
        March,
        April,
        May,
        June,
        July,
        August,
        September,
        October,
        November,
        December
    }

    [Flags]
    enum DaysOfWeek
    {
        Sunday,
        Monday,
        Tuesday,
        Wednesday,
        Thursday,
        Friday,
        Saturday
    }

    enum Repeat
    {
        Daily,
        Weekly,
        Monthly,
        Yearly
    }
}
