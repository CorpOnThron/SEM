using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp45
{
    public class Frequency
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

        public Frequency(Repeat repeat, DateTime startDate, int EveryXOccurance = 1, params int[] daysOfMonth ) // monthly overloaded Type A
        {
            this.Repeat = repeat;
            this.StartDate = startDate;
            this.EveryXOccurance = EveryXOccurance;
            this.FrequencyType = FrequencyType.MonthlyTypeA;
            this.DaysOfMonth = daysOfMonth;
        }

        public Frequency(Repeat repeat, DateTime startDate, int EveryXOccurance = 1, Position position = Position.First, PositionDay dayOfPosition = PositionDay.Day) // monthly overloaded Type B
        {
            this.Repeat = repeat;
            this.StartDate = startDate;
            this.EveryXOccurance = EveryXOccurance;
            this.FrequencyType = FrequencyType.MonthlyTypeB;
            this.Position = position;
            this.DayOfPosition = dayOfPosition;
        }

        public Frequency(Repeat repeat, DateTime startDate, int EveryXOccurance, Month month) // Yearly Overloaded
        {
            this.Repeat = repeat;
            this.StartDate = startDate;
            this.EveryXOccurance = EveryXOccurance;
            this.FrequencyType = FrequencyType.Yearly;
            this.Month = month;
        }

        /*****FUNCTIONS*****/

        private DaysOfWeek GetDayOfWeek(DateTime Date)
        {
            switch (Date.DayOfWeek)
            {
                case System.DayOfWeek.Sunday:
                    return DaysOfWeek.Sunday;
                case System.DayOfWeek.Monday:
                    return DaysOfWeek.Monday;
                case System.DayOfWeek.Tuesday:
                    return DaysOfWeek.Tuesday;
                case System.DayOfWeek.Wednesday:
                    return DaysOfWeek.Wednesday;
                case System.DayOfWeek.Thursday:
                    return DaysOfWeek.Thursday;
                case System.DayOfWeek.Friday:
                    return DaysOfWeek.Friday;
                case System.DayOfWeek.Saturday:
                    return DaysOfWeek.Saturday;
                default:
                    Console.WriteLine("Error: GetDayOfWeek() - Frequency class");
                    return 0;
            }
        }

        private Month GetMonth(DateTime Date)
        {
            switch (Date.Month)
            {
                case 1:
                    return Month.January;
                case 2:
                    return Month.February;
                case 3:
                    return Month.March;
                case 4:
                    return Month.April;
                case 5:
                    return Month.May;
                case 6:
                    return Month.June;
                case 7:
                    return Month.July;
                case 8:
                    return Month.August;
                case 9:
                    return Month.September;
                case 10:
                    return Month.October;
                case 11:
                    return Month.November;
                default:
                    return Month.December;
            }
        }

        public List<DateTime> GetNextDates(DateTime EndDate)
        {
            List<DateTime> Result = new List<DateTime>();
            if (StartDate < EndDate)
            {
                switch (FrequencyType) 
                {
                    case FrequencyType.Standard:
                        DateTime counter = StartDate;
                        switch (Repeat)
                        {
                            case Repeat.Once:
                                Result.Add(counter);
                                break;
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
                    case FrequencyType.Weekly:
                        counter = StartDate;
                        while(counter.ToOADate() <= EndDate.ToOADate())
                        {
                            
                            if (DaysOfWeek.HasFlag(GetDayOfWeek(counter)))
                            {
                                Result.Add(counter);
                            }
                            counter = counter.AddDays(1);

                            if ((counter - StartDate).TotalDays % 7 == 0)
                            {
                                int XOccurences = EveryXOccurance-1;
                                while(XOccurences > 0)
                                {
                                    counter = counter.AddDays(7);
                                    XOccurences--;
                                }                                
                            }

                        }
                        break;
                    case FrequencyType.MonthlyTypeA:
                        counter = StartDate;
                        DateTime LastMonth = StartDate;
                        bool MonthChanged;
                        bool SameDate;
                        bool EndDaysAdded = false;
                        while(counter.ToOADate() <= EndDate.ToOADate())
                        {
                            if(Array.IndexOf(DaysOfMonth, counter.Day) > -1) 
                            {
                                Result.Add(counter);
                            }
                            counter = counter.AddDays(1);
                            MonthChanged = counter.Month == LastMonth.Month ? false : true;
                            if (MonthChanged && !EndDaysAdded)
                            {
                                foreach(int day in DaysOfMonth)
                                {
                                    if (day > DateTime.DaysInMonth(LastMonth.Year, LastMonth.Month))
                                    {
                                        Result.Add(new DateTime(LastMonth.Year, LastMonth.Month, DateTime.DaysInMonth(LastMonth.Year, LastMonth.Month)));
                                    }
                                }
                                EndDaysAdded = true;
                            }
                            SameDate = counter.Day == LastMonth.Day ? true : false;

                            if(MonthChanged && SameDate)
                            {
                                int XOccurences = EveryXOccurance - 1;
                                while(XOccurences > 0)
                                {
                                    counter = counter.AddMonths(1);
                                    XOccurences--;
                                }
                                LastMonth = counter;
                                EndDaysAdded = false;
                            }
                            
                        }
                        break;
                    case FrequencyType.MonthlyTypeB:
                        switch (Position)
                        {
                            case Position.First:
                                switch (DayOfPosition)
                                {
                                    case PositionDay.Day:
                                        counter = StartDate;
                                        while (DateTime.Compare(counter, EndDate) <= 0)
                                        {
                                            Result.Add(new DateTime(counter.Year, counter.Month, 1));
                                            counter = counter.AddMonths(EveryXOccurance);
                                        }
                                        break;
                                    case PositionDay.Monday:
                                        counter = StartDate;
                                        while (DateTime.Compare(counter, EndDate) <= 0)
                                        {
                                            if (GetDayOfWeek(counter) == DaysOfWeek.Monday)
                                            {
                                                if (counter.Day <= 7)
                                                {
                                                    Result.Add(counter);
                                                    counter = counter.AddMonths(EveryXOccurance);
                                                    counter = new DateTime(counter.Year, counter.Month, 1);
                                                }
                                                else
                                                {
                                                    counter = counter.AddDays(1);
                                                }
                                            }
                                            else
                                            {
                                                counter = counter.AddDays(1);
                                            }
                                        }
                                        break;
                                    case PositionDay.Tuesday:
                                        counter = StartDate;
                                        while (DateTime.Compare(counter, EndDate) <= 0)
                                        {
                                            if (GetDayOfWeek(counter) == DaysOfWeek.Tuesday)
                                            {
                                                if (counter.Day <= 7)
                                                {
                                                    Result.Add(counter);
                                                    counter = counter.AddMonths(EveryXOccurance);
                                                    counter = new DateTime(counter.Year, counter.Month, 1);
                                                }
                                                else
                                                {
                                                    counter = counter.AddDays(1);
                                                }
                                            }
                                            else
                                            {
                                                counter = counter.AddDays(1);
                                            }
                                        }
                                        break;
                                    case PositionDay.Wednesday:
                                        counter = StartDate;
                                        while (DateTime.Compare(counter, EndDate) <= 0)
                                        {
                                            if (GetDayOfWeek(counter) == DaysOfWeek.Wednesday)
                                            {
                                                if (counter.Day <= 7)
                                                {
                                                    Result.Add(counter);
                                                    counter = counter.AddMonths(EveryXOccurance);
                                                    counter = new DateTime(counter.Year, counter.Month, 1);
                                                }
                                                else
                                                {
                                                    counter = counter.AddDays(1);
                                                }
                                            }
                                            else
                                            {
                                                counter = counter.AddDays(1);
                                            }
                                        }
                                        break;
                                    case PositionDay.Thursday:
                                        counter = StartDate;
                                        while (DateTime.Compare(counter, EndDate) <= 0)
                                        {
                                            if (GetDayOfWeek(counter) == DaysOfWeek.Thursday)
                                            {
                                                if (counter.Day <= 7)
                                                {
                                                    Result.Add(counter);
                                                    counter = counter.AddMonths(EveryXOccurance);
                                                    counter = new DateTime(counter.Year, counter.Month, 1);
                                                }
                                                else
                                                {
                                                    counter = counter.AddDays(1);
                                                }
                                            }
                                            else
                                            {
                                                counter = counter.AddDays(1);
                                            }
                                        }
                                        break;
                                    case PositionDay.Friday:
                                        counter = StartDate;
                                        while (DateTime.Compare(counter, EndDate) <= 0)
                                        {
                                            if (GetDayOfWeek(counter) == DaysOfWeek.Friday)
                                            {
                                                if (counter.Day <= 7)
                                                {
                                                    Result.Add(counter);
                                                    counter = counter.AddMonths(EveryXOccurance);
                                                    counter = new DateTime(counter.Year, counter.Month, 1);
                                                }
                                                else
                                                {
                                                    counter = counter.AddDays(1);
                                                }
                                            }
                                            else
                                            {
                                                counter = counter.AddDays(1);
                                            }
                                        }
                                        break;
                                    case PositionDay.Saturday:
                                        counter = StartDate;
                                        while (DateTime.Compare(counter, EndDate) <= 0)
                                        {
                                            if (GetDayOfWeek(counter) == DaysOfWeek.Saturday)
                                            {
                                                if (counter.Day <= 7)
                                                {
                                                    Result.Add(counter);
                                                    counter = counter.AddMonths(EveryXOccurance);
                                                    counter = new DateTime(counter.Year, counter.Month, 1);
                                                }
                                                else
                                                {
                                                    counter = counter.AddDays(1);
                                                }
                                            }
                                            else
                                            {
                                                counter = counter.AddDays(1);
                                            }
                                        }
                                        break;
                                    case PositionDay.Sunday:
                                        counter = StartDate;
                                        while (DateTime.Compare(counter, EndDate) <= 0)
                                        {
                                            if (GetDayOfWeek(counter) == DaysOfWeek.Sunday)
                                            {
                                                if (counter.Day <= 7)
                                                {
                                                    Result.Add(counter);
                                                    counter = counter.AddMonths(EveryXOccurance);
                                                    counter = new DateTime(counter.Year, counter.Month, 1);
                                                }
                                                else
                                                {
                                                    counter = counter.AddDays(1);
                                                }
                                            }
                                            else
                                            {
                                                counter = counter.AddDays(1);
                                            }
                                        }
                                        break;
                                }
                                break;
                            case Position.Second:
                                switch (DayOfPosition)
                                {
                                    case PositionDay.Day:
                                        counter = StartDate;
                                        while (counter.ToOADate() < EndDate.ToOADate())
                                        {
                                            Result.Add(new DateTime(counter.Year, counter.Month, 2));
                                            counter = counter.AddMonths(EveryXOccurance);
                                        }
                                        break;
                                    case PositionDay.Monday:
                                        counter = StartDate;
                                        while (DateTime.Compare(counter, EndDate) <= 0)
                                        {
                                            if (GetDayOfWeek(counter) == DaysOfWeek.Monday)
                                            {
                                                if (counter.Day <= 14 && counter.Day > 7)
                                                {
                                                    Result.Add(counter);
                                                    counter = counter.AddMonths(EveryXOccurance);
                                                    counter = new DateTime(counter.Year, counter.Month, 1);
                                                }
                                                else
                                                {
                                                    counter = counter.AddDays(1);
                                                }
                                            }
                                            else
                                            {
                                                counter = counter.AddDays(1);
                                            }
                                        }
                                        break;

                                    case PositionDay.Tuesday:
                                        counter = StartDate;
                                        while (DateTime.Compare(counter, EndDate) <= 0)
                                        {
                                            if (GetDayOfWeek(counter) == DaysOfWeek.Tuesday)
                                            {
                                                if (counter.Day <= 14 && counter.Day > 7)
                                                {
                                                    Result.Add(counter);
                                                    counter = counter.AddMonths(EveryXOccurance);
                                                    counter = new DateTime(counter.Year, counter.Month, 1);
                                                }
                                                else
                                                {
                                                    counter = counter.AddDays(1);
                                                }
                                            }
                                            else
                                            {
                                                counter = counter.AddDays(1);
                                            }
                                        }
                                        break;
                                    case PositionDay.Wednesday:
                                        counter = StartDate;
                                        while (DateTime.Compare(counter, EndDate) <= 0)
                                        {
                                            if (GetDayOfWeek(counter) == DaysOfWeek.Wednesday)
                                            {
                                                if (counter.Day <= 14 && counter.Day > 7)
                                                {
                                                    Result.Add(counter);
                                                    counter = counter.AddMonths(EveryXOccurance);
                                                    counter = new DateTime(counter.Year, counter.Month, 1);
                                                }
                                                else
                                                {
                                                    counter = counter.AddDays(1);
                                                }
                                            }
                                            else
                                            {
                                                counter = counter.AddDays(1);
                                            }
                                        }
                                        break;
                                    case PositionDay.Thursday:
                                        counter = StartDate;
                                        while (DateTime.Compare(counter, EndDate) <= 0)
                                        {
                                            if (GetDayOfWeek(counter) == DaysOfWeek.Thursday)
                                            {
                                                if (counter.Day <= 14 && counter.Day > 7)
                                                {
                                                    Result.Add(counter);
                                                    counter = counter.AddMonths(EveryXOccurance);
                                                    counter = new DateTime(counter.Year, counter.Month, 1);
                                                }
                                                else
                                                {
                                                    counter = counter.AddDays(1);
                                                }
                                            }
                                            else
                                            {
                                                counter = counter.AddDays(1);
                                            }
                                        }
                                        break;
                                    case PositionDay.Friday:
                                        counter = StartDate;
                                        while (DateTime.Compare(counter, EndDate) <= 0)
                                        {
                                            if (GetDayOfWeek(counter) == DaysOfWeek.Friday)
                                            {
                                                if (counter.Day <= 14 && counter.Day > 7)
                                                {
                                                    Result.Add(counter);
                                                    counter = counter.AddMonths(EveryXOccurance);
                                                    counter = new DateTime(counter.Year, counter.Month, 1);
                                                }
                                                else
                                                {
                                                    counter = counter.AddDays(1);
                                                }
                                            }
                                            else
                                            {
                                                counter = counter.AddDays(1);
                                            }
                                        }
                                        break;
                                    case PositionDay.Saturday:
                                        counter = StartDate;
                                        while (DateTime.Compare(counter, EndDate) <= 0)
                                        {
                                            if (GetDayOfWeek(counter) == DaysOfWeek.Saturday)
                                            {
                                                if (counter.Day <= 14 && counter.Day > 7)
                                                {
                                                    Result.Add(counter);
                                                    counter = counter.AddMonths(EveryXOccurance);
                                                    counter = new DateTime(counter.Year, counter.Month, 1);
                                                }
                                                else
                                                {
                                                    counter = counter.AddDays(1);
                                                }
                                            }
                                            else
                                            {
                                                counter = counter.AddDays(1);
                                            }
                                        }
                                        break;
                                    case PositionDay.Sunday:
                                        counter = StartDate;
                                        while (DateTime.Compare(counter, EndDate) <= 0)
                                        {
                                            if (GetDayOfWeek(counter) == DaysOfWeek.Sunday)
                                            {
                                                if (counter.Day <= 14 && counter.Day > 7)
                                                {
                                                    Result.Add(counter);
                                                    counter = counter.AddMonths(EveryXOccurance);
                                                    counter = new DateTime(counter.Year, counter.Month, 1);
                                                }
                                                else
                                                {
                                                    counter = counter.AddDays(1);
                                                }
                                            }
                                            else
                                            {
                                                counter = counter.AddDays(1);
                                            }
                                        }
                                        break;

                                }
                                break;
                            case Position.Third:
                                switch (DayOfPosition)
                                {
                                    case PositionDay.Day:
                                        counter = StartDate;
                                        while (counter.ToOADate() < EndDate.ToOADate())
                                        {
                                            Result.Add(new DateTime(counter.Year, counter.Month, 3));
                                            counter = counter.AddMonths(EveryXOccurance);
                                        }
                                        break;
                                    case PositionDay.Monday:
                                        counter = StartDate;
                                        while (DateTime.Compare(counter, EndDate) <= 0)
                                        {
                                            if (GetDayOfWeek(counter) == DaysOfWeek.Monday)
                                            {
                                                if (counter.Day <= 21 && counter.Day > 14)
                                                {
                                                    Result.Add(counter);
                                                    counter = counter.AddMonths(EveryXOccurance);
                                                    counter = new DateTime(counter.Year, counter.Month, 1);
                                                }
                                                else
                                                {
                                                    counter = counter.AddDays(1);
                                                }
                                            }
                                            else
                                            {
                                                counter = counter.AddDays(1);
                                            }
                                        }
                                        break;
                                    case PositionDay.Tuesday:
                                        counter = StartDate;
                                        while (DateTime.Compare(counter, EndDate) <= 0)
                                        {
                                            if (GetDayOfWeek(counter) == DaysOfWeek.Tuesday)
                                            {
                                                if (counter.Day <= 21 && counter.Day > 14)
                                                {
                                                    Result.Add(counter);
                                                    counter = counter.AddMonths(EveryXOccurance);
                                                    counter = new DateTime(counter.Year, counter.Month, 1);
                                                }
                                                else
                                                {
                                                    counter = counter.AddDays(1);
                                                }
                                            }
                                            else
                                            {
                                                counter = counter.AddDays(1);
                                            }
                                        }
                                        break;
                                    case PositionDay.Wednesday:
                                        counter = StartDate;
                                        while (DateTime.Compare(counter, EndDate) <= 0)
                                        {
                                            if (GetDayOfWeek(counter) == DaysOfWeek.Wednesday)
                                            {
                                                if (counter.Day <= 21 && counter.Day > 14)
                                                {
                                                    Result.Add(counter);
                                                    counter = counter.AddMonths(EveryXOccurance);
                                                    counter = new DateTime(counter.Year, counter.Month, 1);
                                                }
                                                else
                                                {
                                                    counter = counter.AddDays(1);
                                                }
                                            }
                                            else
                                            {
                                                counter = counter.AddDays(1);
                                            }
                                        }
                                        break;
                                    case PositionDay.Thursday:
                                        counter = StartDate;
                                        while (DateTime.Compare(counter, EndDate) <= 0)
                                        {
                                            if (GetDayOfWeek(counter) == DaysOfWeek.Thursday)
                                            {
                                                if (counter.Day <= 21 && counter.Day > 14)
                                                {
                                                    Result.Add(counter);
                                                    counter = counter.AddMonths(EveryXOccurance);
                                                    counter = new DateTime(counter.Year, counter.Month, 1);
                                                }
                                                else
                                                {
                                                    counter = counter.AddDays(1);
                                                }
                                            }
                                            else
                                            {
                                                counter = counter.AddDays(1);
                                            }
                                        }
                                        break;
                                    case PositionDay.Friday:
                                        counter = StartDate;
                                        while (DateTime.Compare(counter, EndDate) <= 0)
                                        {
                                            if (GetDayOfWeek(counter) == DaysOfWeek.Friday)
                                            {
                                                if (counter.Day <= 21 && counter.Day > 14)
                                                {
                                                    Result.Add(counter);
                                                    counter = counter.AddMonths(EveryXOccurance);
                                                    counter = new DateTime(counter.Year, counter.Month, 1);
                                                }
                                                else
                                                {
                                                    counter = counter.AddDays(1);
                                                }
                                            }
                                            else
                                            {
                                                counter = counter.AddDays(1);
                                            }
                                        }
                                        break;
                                    case PositionDay.Saturday:
                                        counter = StartDate;
                                        while (DateTime.Compare(counter, EndDate) <= 0)
                                        {
                                            if (GetDayOfWeek(counter) == DaysOfWeek.Saturday)
                                            {
                                                if (counter.Day <= 21 && counter.Day > 14)
                                                {
                                                    Result.Add(counter);
                                                    counter = counter.AddMonths(EveryXOccurance);
                                                    counter = new DateTime(counter.Year, counter.Month, 1);
                                                }
                                                else
                                                {
                                                    counter = counter.AddDays(1);
                                                }
                                            }
                                            else
                                            {
                                                counter = counter.AddDays(1);
                                            }
                                        }
                                        break;
                                    case PositionDay.Sunday:
                                        counter = StartDate;
                                        while (DateTime.Compare(counter, EndDate) <= 0)
                                        {
                                            if (GetDayOfWeek(counter) == DaysOfWeek.Sunday)
                                            {
                                                if (counter.Day <= 21 && counter.Day > 14)
                                                {
                                                    Result.Add(counter);
                                                    counter = counter.AddMonths(EveryXOccurance);
                                                    counter = new DateTime(counter.Year, counter.Month, 1);
                                                }
                                                else
                                                {
                                                    counter = counter.AddDays(1);
                                                }
                                            }
                                            else
                                            {
                                                counter = counter.AddDays(1);
                                            }
                                        }
                                        break;
                                }
                                break;
                            case Position.Fourth:
                                switch (DayOfPosition)
                                {
                                    case PositionDay.Day:
                                        counter = StartDate;
                                        while (counter.ToOADate() < EndDate.ToOADate())
                                        {
                                            Result.Add(new DateTime(counter.Year, counter.Month, 4));
                                            counter = counter.AddMonths(EveryXOccurance);
                                        }
                                        break;
                                    case PositionDay.Monday:
                                        counter = StartDate;
                                        while (DateTime.Compare(counter, EndDate) <= 0)
                                        {
                                            if (GetDayOfWeek(counter) == DaysOfWeek.Monday)
                                            {
                                                if (counter.Day <= 28 && counter.Day > 21)
                                                {
                                                    Result.Add(counter);
                                                    counter = counter.AddMonths(EveryXOccurance);
                                                    counter = new DateTime(counter.Year, counter.Month, 1);
                                                }
                                                else
                                                {
                                                    counter = counter.AddDays(1);
                                                }
                                            }
                                            else
                                            {
                                                counter = counter.AddDays(1);
                                            }
                                        }
                                        break;
                                    case PositionDay.Tuesday:
                                        counter = StartDate;
                                        while (DateTime.Compare(counter, EndDate) <= 0)
                                        {
                                            if (GetDayOfWeek(counter) == DaysOfWeek.Tuesday)
                                            {
                                                if (counter.Day <= 28 && counter.Day > 21)
                                                {
                                                    Result.Add(counter);
                                                    counter = counter.AddMonths(EveryXOccurance);
                                                    counter = new DateTime(counter.Year, counter.Month, 1);
                                                }
                                                else
                                                {
                                                    counter = counter.AddDays(1);
                                                }
                                            }
                                            else
                                            {
                                                counter = counter.AddDays(1);
                                            }
                                        }
                                        break;
                                    case PositionDay.Wednesday:
                                        counter = StartDate;
                                        while (DateTime.Compare(counter, EndDate) <= 0)
                                        {
                                            if (GetDayOfWeek(counter) == DaysOfWeek.Wednesday)
                                            {
                                                if (counter.Day <= 28 && counter.Day > 21)
                                                {
                                                    Result.Add(counter);
                                                    counter = counter.AddMonths(EveryXOccurance);
                                                    counter = new DateTime(counter.Year, counter.Month, 1);
                                                }
                                                else
                                                {
                                                    counter = counter.AddDays(1);
                                                }
                                            }
                                            else
                                            {
                                                counter = counter.AddDays(1);
                                            }
                                        }
                                        break;
                                    case PositionDay.Thursday:
                                        counter = StartDate;
                                        while (DateTime.Compare(counter, EndDate) <= 0)
                                        {
                                            if (GetDayOfWeek(counter) == DaysOfWeek.Thursday)
                                            {
                                                if (counter.Day <= 28 && counter.Day > 21)
                                                {
                                                    Result.Add(counter);
                                                    counter = counter.AddMonths(EveryXOccurance);
                                                    counter = new DateTime(counter.Year, counter.Month, 1);
                                                }
                                                else
                                                {
                                                    counter = counter.AddDays(1);
                                                }
                                            }
                                            else
                                            {
                                                counter = counter.AddDays(1);
                                            }
                                        }
                                        break;
                                    case PositionDay.Friday:
                                        counter = StartDate;
                                        while (DateTime.Compare(counter, EndDate) <= 0)
                                        {
                                            if (GetDayOfWeek(counter) == DaysOfWeek.Friday)
                                            {
                                                if (counter.Day <= 28 && counter.Day > 21)
                                                {
                                                    Result.Add(counter);
                                                    counter = counter.AddMonths(EveryXOccurance);
                                                    counter = new DateTime(counter.Year, counter.Month, 1);
                                                }
                                                else
                                                {
                                                    counter = counter.AddDays(1);
                                                }
                                            }
                                            else
                                            {
                                                counter = counter.AddDays(1);
                                            }
                                        }
                                        break;
                                    case PositionDay.Saturday:
                                        counter = StartDate;
                                        while (DateTime.Compare(counter, EndDate) <= 0)
                                        {
                                            if (GetDayOfWeek(counter) == DaysOfWeek.Saturday)
                                            {
                                                if (counter.Day <= 28 && counter.Day > 21)
                                                {
                                                    Result.Add(counter);
                                                    counter = counter.AddMonths(EveryXOccurance);
                                                    counter = new DateTime(counter.Year, counter.Month, 1);
                                                }
                                                else
                                                {
                                                    counter = counter.AddDays(1);
                                                }
                                            }
                                            else
                                            {
                                                counter = counter.AddDays(1);
                                            }
                                        }
                                        break;
                                    case PositionDay.Sunday:
                                        counter = StartDate;
                                        while (DateTime.Compare(counter, EndDate) <= 0)
                                        {
                                            if (GetDayOfWeek(counter) == DaysOfWeek.Sunday)
                                            {
                                                if (counter.Day <= 28 && counter.Day > 21)
                                                {
                                                    Result.Add(counter);
                                                    counter = counter.AddMonths(EveryXOccurance);
                                                    counter = new DateTime(counter.Year, counter.Month, 1);
                                                }
                                                else
                                                {
                                                    counter = counter.AddDays(1);
                                                }
                                            }
                                            else
                                            {
                                                counter = counter.AddDays(1);
                                            }
                                        }
                                        break;

                                }
                                break;
                            case Position.Fifth:
                                switch (DayOfPosition)
                                {
                                    case PositionDay.Day:
                                        counter = StartDate;
                                        while (counter.ToOADate() < EndDate.ToOADate())
                                        {
                                            Result.Add(new DateTime(counter.Year, counter.Month, 5));
                                            counter = counter.AddMonths(EveryXOccurance);
                                        }
                                        break;
                                    case PositionDay.Monday:
                                        counter = StartDate;
                                        while (DateTime.Compare(counter, EndDate) <= 0)
                                        {
                                            if (GetDayOfWeek(counter) == DaysOfWeek.Monday)
                                            {
                                                if (counter.Day <= 31 && counter.Day > 28)
                                                {
                                                    Result.Add(counter);
                                                    counter = counter.AddMonths(EveryXOccurance);
                                                    counter = new DateTime(counter.Year, counter.Month, 1);
                                                }
                                                else
                                                {
                                                    counter = counter.AddDays(1);
                                                }
                                            }
                                            else
                                            {
                                                counter = counter.AddDays(1);
                                            }
                                        }
                                        break;
                                    case PositionDay.Tuesday:
                                        counter = StartDate;
                                        while (DateTime.Compare(counter, EndDate) <= 0)
                                        {
                                            if (GetDayOfWeek(counter) == DaysOfWeek.Tuesday)
                                            {
                                                if (counter.Day <= 31 && counter.Day > 28)
                                                {
                                                    Result.Add(counter);
                                                    counter = counter.AddMonths(EveryXOccurance);
                                                    counter = new DateTime(counter.Year, counter.Month, 1);
                                                }
                                                else
                                                {
                                                    counter = counter.AddDays(1);
                                                }
                                            }
                                            else
                                            {
                                                counter = counter.AddDays(1);
                                            }
                                        }
                                        break;
                                    case PositionDay.Wednesday:
                                        counter = StartDate;
                                        while (DateTime.Compare(counter, EndDate) <= 0)
                                        {
                                            if (GetDayOfWeek(counter) == DaysOfWeek.Wednesday)
                                            {
                                                if (counter.Day <= 31 && counter.Day > 28)
                                                {
                                                    Result.Add(counter);
                                                    counter = counter.AddMonths(EveryXOccurance);
                                                    counter = new DateTime(counter.Year, counter.Month, 1);
                                                }
                                                else
                                                {
                                                    counter = counter.AddDays(1);
                                                }
                                            }
                                            else
                                            {
                                                counter = counter.AddDays(1);
                                            }
                                        }
                                        break;
                                    case PositionDay.Thursday:
                                        counter = StartDate;
                                        while (DateTime.Compare(counter, EndDate) <= 0)
                                        {
                                            if (GetDayOfWeek(counter) == DaysOfWeek.Thursday)
                                            {
                                                if (counter.Day <= 31 && counter.Day > 28)
                                                {
                                                    Result.Add(counter);
                                                    counter = counter.AddMonths(EveryXOccurance);
                                                    counter = new DateTime(counter.Year, counter.Month, 1);
                                                }
                                                else
                                                {
                                                    counter = counter.AddDays(1);
                                                }
                                            }
                                            else
                                            {
                                                counter = counter.AddDays(1);
                                            }
                                        }
                                        break;
                                    case PositionDay.Friday:
                                        counter = StartDate;
                                        while (DateTime.Compare(counter, EndDate) <= 0)
                                        {
                                            if (GetDayOfWeek(counter) == DaysOfWeek.Friday)
                                            {
                                                if (counter.Day <= 31 && counter.Day > 28)
                                                {
                                                    Result.Add(counter);
                                                    counter = counter.AddMonths(EveryXOccurance);
                                                    counter = new DateTime(counter.Year, counter.Month, 1);
                                                }
                                                else
                                                {
                                                    counter = counter.AddDays(1);
                                                }
                                            }
                                            else
                                            {
                                                counter = counter.AddDays(1);
                                            }
                                        }
                                        break;
                                    case PositionDay.Saturday:
                                        counter = StartDate;
                                        while (DateTime.Compare(counter, EndDate) <= 0)
                                        {
                                            if (GetDayOfWeek(counter) == DaysOfWeek.Saturday)
                                            {
                                                if (counter.Day <= 31 && counter.Day > 28)
                                                {
                                                    Result.Add(counter);
                                                    counter = counter.AddMonths(EveryXOccurance);
                                                    counter = new DateTime(counter.Year, counter.Month, 1);
                                                }
                                                else
                                                {
                                                    counter = counter.AddDays(1);
                                                }
                                            }
                                            else
                                            {
                                                counter = counter.AddDays(1);
                                            }
                                        }
                                        break;
                                    case PositionDay.Sunday:
                                        counter = StartDate;
                                        while (DateTime.Compare(counter, EndDate) <= 0)
                                        {
                                            if (GetDayOfWeek(counter) == DaysOfWeek.Sunday)
                                            {
                                                if (counter.Day <= 31 && counter.Day > 28)
                                                {
                                                    Result.Add(counter);
                                                    counter = counter.AddMonths(EveryXOccurance);
                                                    counter = new DateTime(counter.Year, counter.Month, 1);
                                                }
                                                else
                                                {
                                                    counter = counter.AddDays(1);
                                                }
                                            }
                                            else
                                            {
                                                counter = counter.AddDays(1);
                                            }
                                        }
                                        break;

                                }
                                break;
                            case Position.Last:
                                switch (DayOfPosition)
                                {
                                    case PositionDay.Day:
                                        counter = StartDate;
                                        while (counter.ToOADate() < EndDate.ToOADate())
                                        {
                                            Result.Add(new DateTime(counter.Year, counter.Month, DateTime.DaysInMonth(counter.Year, counter.Month)));

                                            counter = counter.AddMonths(EveryXOccurance);
                                        }
                                        break;
                                    case PositionDay.Monday:
                                        counter = StartDate;
                                        while (DateTime.Compare(counter, EndDate) <= 0)
                                        {
                                            if (GetDayOfWeek(counter) == DaysOfWeek.Monday)
                                            {
                                                if (counter.Day <= DateTime.DaysInMonth(counter.Year, counter.Month) && counter.Day > DateTime.DaysInMonth(counter.Year, counter.Month) - 7)
                                                {
                                                    Result.Add(counter);
                                                    counter = counter.AddMonths(EveryXOccurance);
                                                    counter = new DateTime(counter.Year, counter.Month, 1);
                                                }
                                                else
                                                {
                                                    counter = counter.AddDays(1);
                                                }
                                            }
                                            else
                                            {
                                                counter = counter.AddDays(1);
                                            }
                                        }
                                        break;
                                    case PositionDay.Tuesday:
                                        counter = StartDate;
                                        while (DateTime.Compare(counter, EndDate) <= 0)
                                        {
                                            if (GetDayOfWeek(counter) == DaysOfWeek.Tuesday)
                                            {
                                                if (counter.Day <= DateTime.DaysInMonth(counter.Year, counter.Month) && counter.Day > DateTime.DaysInMonth(counter.Year, counter.Month) - 7)
                                                {
                                                    Result.Add(counter);
                                                    counter = counter.AddMonths(EveryXOccurance);
                                                    counter = new DateTime(counter.Year, counter.Month, 1);
                                                }
                                                else
                                                {
                                                    counter = counter.AddDays(1);
                                                }
                                            }
                                            else
                                            {
                                                counter = counter.AddDays(1);
                                            }
                                        }
                                        break;
                                    case PositionDay.Wednesday:
                                        counter = StartDate;
                                        while (DateTime.Compare(counter, EndDate) <= 0)
                                        {
                                            if (GetDayOfWeek(counter) == DaysOfWeek.Wednesday)
                                            {
                                                if (counter.Day <= DateTime.DaysInMonth(counter.Year, counter.Month) && counter.Day > DateTime.DaysInMonth(counter.Year, counter.Month) - 7)
                                                {
                                                    Result.Add(counter);
                                                    counter = counter.AddMonths(EveryXOccurance);
                                                    counter = new DateTime(counter.Year, counter.Month, 1);
                                                }
                                                else
                                                {
                                                    counter = counter.AddDays(1);
                                                }
                                            }
                                            else
                                            {
                                                counter = counter.AddDays(1);
                                            }
                                        }
                                        break;
                                    case PositionDay.Thursday:
                                        counter = StartDate;
                                        while (DateTime.Compare(counter, EndDate) <= 0)
                                        {
                                            if (GetDayOfWeek(counter) == DaysOfWeek.Thursday)
                                            {
                                                if (counter.Day <= DateTime.DaysInMonth(counter.Year, counter.Month) && counter.Day > DateTime.DaysInMonth(counter.Year, counter.Month) - 7)
                                                {
                                                    Result.Add(counter);
                                                    counter = counter.AddMonths(EveryXOccurance);
                                                    counter = new DateTime(counter.Year, counter.Month, 1);
                                                }
                                                else
                                                {
                                                    counter = counter.AddDays(1);
                                                }
                                            }
                                            else
                                            {
                                                counter = counter.AddDays(1);
                                            }
                                        }
                                        break;
                                    case PositionDay.Friday:
                                        counter = StartDate;
                                        while (DateTime.Compare(counter, EndDate) <= 0)
                                        {
                                            if (GetDayOfWeek(counter) == DaysOfWeek.Friday)
                                            {
                                                if (counter.Day <= DateTime.DaysInMonth(counter.Year, counter.Month) && counter.Day > DateTime.DaysInMonth(counter.Year, counter.Month) - 7)
                                                {
                                                    Result.Add(counter);
                                                    counter = counter.AddMonths(EveryXOccurance);
                                                    counter = new DateTime(counter.Year, counter.Month, 1);
                                                }
                                                else
                                                {
                                                    counter = counter.AddDays(1);
                                                }
                                            }
                                            else
                                            {
                                                counter = counter.AddDays(1);
                                            }
                                        }
                                        break;
                                    case PositionDay.Saturday:
                                        counter = StartDate;
                                        while (DateTime.Compare(counter, EndDate) <= 0)
                                        {
                                            if (GetDayOfWeek(counter) == DaysOfWeek.Saturday)
                                            {
                                                if (counter.Day <= DateTime.DaysInMonth(counter.Year, counter.Month) && counter.Day > DateTime.DaysInMonth(counter.Year, counter.Month) - 7)
                                                {
                                                    Result.Add(counter);
                                                    counter = counter.AddMonths(EveryXOccurance);
                                                    counter = new DateTime(counter.Year, counter.Month, 1);
                                                }
                                                else
                                                {
                                                    counter = counter.AddDays(1);
                                                }
                                            }
                                            else
                                            {
                                                counter = counter.AddDays(1);
                                            }
                                        }
                                        break;
                                    case PositionDay.Sunday:
                                        counter = StartDate;
                                        while (DateTime.Compare(counter, EndDate) <= 0)
                                        {
                                            if (GetDayOfWeek(counter) == DaysOfWeek.Sunday)
                                            {
                                                if (counter.Day <= DateTime.DaysInMonth(counter.Year, counter.Month) && counter.Day > DateTime.DaysInMonth(counter.Year, counter.Month) - 7)
                                                {
                                                    Result.Add(counter);
                                                    counter = counter.AddMonths(EveryXOccurance);
                                                    counter = new DateTime(counter.Year, counter.Month, 1);
                                                }
                                                else
                                                {
                                                    counter = counter.AddDays(1);
                                                }
                                            }
                                            else
                                            {
                                                counter = counter.AddDays(1);
                                            }
                                        }
                                        break;
                                }
                                break;
                        }
                        break;
                    case FrequencyType.Yearly:
                        counter = StartDate;
                        int LastYear = counter.Year;
                        bool YearChanged = false;
                        while (DateTime.Compare(counter, EndDate) <= 0)
                        {
                            if (Month.HasFlag(GetMonth(counter)))
                            {
                                Result.Add(new DateTime(counter.Year, counter.Month, counter.Day));
                            }
                            counter = counter.AddMonths(1);
                            YearChanged = counter.Year == LastYear ? false : true;
                            if(YearChanged)
                            {
                                counter = counter.AddYears(EveryXOccurance-1);
                                LastYear = counter.Year;
                                counter = new DateTime(counter.Year, counter.Month, counter.Day);
                                YearChanged = false;
                            }
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
    
    public enum FrequencyType
    {
        Standard,
        Weekly,
        MonthlyTypeA,
        MonthlyTypeB,
        Yearly
    }

    public enum Position
    {
        First,
        Second,
        Third,
        Fourth,
        Fifth,
        Last
    }

    public enum PositionDay
    {
        Day,
        Sunday,
        Monday,
        Tuesday,
        Wednesday,
        Thursday,
        Friday,
        Saturday
    }

    [Flags]
    public enum Month
    {
        January=1,
        February=2,
        March=4,
        April=8,
        May=16,
        June=32,
        July=64,
        August=128,
        September=256,
        October=512,
        November=1024,
        December=2048
    }

    [Flags]
    public enum DaysOfWeek
    {
        Sunday=1,
        Monday=2,
        Tuesday=4,
        Wednesday=8,
        Thursday=16,
        Friday=32,
        Saturday=64
    }

    public enum Repeat
    {
        Once,
        Daily,
        Weekly,
        Monthly,
        Yearly
    }
}
