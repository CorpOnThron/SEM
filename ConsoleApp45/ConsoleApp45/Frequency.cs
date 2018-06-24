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
                    return 0;
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
                            SameDate = counter.Day == LastMonth.Day ? false : true;

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
                                        while (counter.ToOADate() < EndDate.ToOADate())
                                        {
                                            Result.Add(new DateTime(counter.Year, counter.Month, 1));
                                            counter = counter.AddMonths(EveryXOccurance);
                                        }
                                        break;
                                    case PositionDay.Monday:
                                        counter = StartDate;
                                        while (counter.ToOADate() < EndDate.ToOADate())
                                        {
                                            if (GetDayOfWeek(counter) == DaysOfWeek.Monday)
                                                if (counter.Day <= 7)
                                                {
                                                    Result.Add(counter);
                                                    counter = new DateTime(counter.Year, counter.Month + 1, 1);
                                                }
                                            counter.AddDays(1);
                                        }
                                        break;
                                    case PositionDay.Tuesday:
                                        counter = StartDate;
                                        while (counter.ToOADate() < EndDate.ToOADate())
                                        {
                                            if (GetDayOfWeek(counter) == DaysOfWeek.Tuesday)
                                                if (counter.Day <= 7)
                                                {
                                                    Result.Add(counter);
                                                    counter = new DateTime(counter.Year, counter.Month + 1, 1);
                                                }
                                            counter.AddDays(1);
                                        }
                                        break;
                                    case PositionDay.Wednesday:
                                        counter = StartDate;
                                        while (counter.ToOADate() < EndDate.ToOADate())
                                        {
                                            if (GetDayOfWeek(counter) == DaysOfWeek.Wednesday)
                                                if (counter.Day <= 7)
                                                {
                                                    Result.Add(counter);
                                                    counter = new DateTime(counter.Year, counter.Month + 1, 1);
                                                }
                                            counter.AddDays(1);
                                        }
                                        break;
                                    case PositionDay.Thursday:
                                        counter = StartDate;
                                        while (counter.ToOADate() < EndDate.ToOADate())
                                        {
                                            if (GetDayOfWeek(counter) == DaysOfWeek.Thursday)
                                                if (counter.Day <= 7)
                                                {
                                                    Result.Add(counter);
                                                    counter = new DateTime(counter.Year, counter.Month + 1, 1);
                                                }
                                            counter.AddDays(1);
                                        }
                                        break;
                                    case PositionDay.Friday:
                                        counter = StartDate;
                                        while (counter.ToOADate() < EndDate.ToOADate())
                                        {
                                            if (GetDayOfWeek(counter) == DaysOfWeek.Friday)
                                                if (counter.Day <= 7)
                                                {
                                                    Result.Add(counter);
                                                    counter = new DateTime(counter.Year, counter.Month + 1, 1);
                                                }
                                            counter.AddDays(1);
                                        }
                                        break;
                                    case PositionDay.Saturday:
                                        counter = StartDate;
                                        while (counter.ToOADate() < EndDate.ToOADate())
                                        {
                                            if (GetDayOfWeek(counter) == DaysOfWeek.Saturday)
                                                if (counter.Day <= 7)
                                                {
                                                    Result.Add(counter);
                                                    counter = new DateTime(counter.Year, counter.Month + 1, 1);
                                                }
                                            counter.AddDays(1);
                                        }
                                        break;
                                    case PositionDay.Sunday:
                                        counter = StartDate;
                                        while (counter.ToOADate() < EndDate.ToOADate())
                                        {
                                            if (GetDayOfWeek(counter) == DaysOfWeek.Sunday)
                                                if (counter.Day <= 7)
                                                {
                                                    Result.Add(counter);
                                                    counter = new DateTime(counter.Year, counter.Month + 1, 1);
                                                }
                                            counter.AddDays(1);
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
                                        while (counter.ToOADate() < EndDate.ToOADate())
                                        {
                                            if (GetDayOfWeek(counter) == DaysOfWeek.Monday)
                                                if (counter.Day <= 14 && counter.Day >7)
                                                {
                                                    Result.Add(counter);
                                                    counter = new DateTime(counter.Year, counter.Month + 1, 1);
                                                }
                                            counter.AddDays(1);
                                        }
                                        break;
                                    case PositionDay.Tuesday:
                                        counter = StartDate;
                                        while (counter.ToOADate() < EndDate.ToOADate())
                                        {
                                            if (GetDayOfWeek(counter) == DaysOfWeek.Tuesday)
                                                if (counter.Day <= 14 && counter.Day > 7)
                                                {
                                                    Result.Add(counter);
                                                    counter = new DateTime(counter.Year, counter.Month + 1, 1);
                                                }
                                            counter.AddDays(1);
                                        }
                                        break;
                                    case PositionDay.Wednesday:
                                        counter = StartDate;
                                        while (counter.ToOADate() < EndDate.ToOADate())
                                        {
                                            if (GetDayOfWeek(counter) == DaysOfWeek.Wednesday)
                                                if (counter.Day <= 14 && counter.Day > 7)
                                                {
                                                    Result.Add(counter);
                                                    counter = new DateTime(counter.Year, counter.Month + 1, 1);
                                                }
                                            counter.AddDays(1);
                                        }
                                        break;
                                    case PositionDay.Thursday:
                                        counter = StartDate;
                                        while (counter.ToOADate() < EndDate.ToOADate())
                                        {
                                            if (GetDayOfWeek(counter) == DaysOfWeek.Thursday)
                                                if (counter.Day <= 14 && counter.Day > 7)
                                                {
                                                    Result.Add(counter);
                                                    counter = new DateTime(counter.Year, counter.Month + 1, 1);
                                                }
                                            counter.AddDays(1);
                                        }
                                        break;
                                    case PositionDay.Friday:
                                        counter = StartDate;
                                        while (counter.ToOADate() < EndDate.ToOADate())
                                        {
                                            if (GetDayOfWeek(counter) == DaysOfWeek.Friday)
                                                if (counter.Day <= 14 && counter.Day > 7)
                                                {
                                                    Result.Add(counter);
                                                    counter = new DateTime(counter.Year, counter.Month + 1, 1);
                                                }
                                            counter.AddDays(1);
                                        }
                                        break;
                                    case PositionDay.Saturday:
                                        counter = StartDate;
                                        while (counter.ToOADate() < EndDate.ToOADate())
                                        {
                                            if (GetDayOfWeek(counter) == DaysOfWeek.Saturday)
                                                if (counter.Day <= 14 && counter.Day > 7)
                                                {
                                                    Result.Add(counter);
                                                    counter = new DateTime(counter.Year, counter.Month + 1, 1);
                                                }
                                            counter.AddDays(1);
                                        }
                                        break;
                                    case PositionDay.Sunday:
                                        counter = StartDate;
                                        while (counter.ToOADate() < EndDate.ToOADate())
                                        {
                                            if (GetDayOfWeek(counter) == DaysOfWeek.Sunday)
                                                if (counter.Day <= 14 && counter.Day > 7)
                                                {
                                                    Result.Add(counter);
                                                    counter = new DateTime(counter.Year, counter.Month + 1, 1);
                                                }
                                            counter.AddDays(1);
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
                                        while (counter.ToOADate() < EndDate.ToOADate())
                                        {
                                            if (GetDayOfWeek(counter) == DaysOfWeek.Monday)
                                                if (counter.Day <= 21 && counter.Day > 14)
                                                {
                                                    Result.Add(counter);
                                                    counter = new DateTime(counter.Year, counter.Month + 1, 1);
                                                }
                                            counter.AddDays(1);
                                        }
                                        break;
                                    case PositionDay.Tuesday:
                                        counter = StartDate;
                                        while (counter.ToOADate() < EndDate.ToOADate())
                                        {
                                            if (GetDayOfWeek(counter) == DaysOfWeek.Tuesday)
                                                if (counter.Day <= 21 && counter.Day > 14)
                                                {
                                                    Result.Add(counter);
                                                    counter = new DateTime(counter.Year, counter.Month + 1, 1);
                                                }
                                            counter.AddDays(1);
                                        }
                                        break;
                                    case PositionDay.Wednesday:
                                        counter = StartDate;
                                        while (counter.ToOADate() < EndDate.ToOADate())
                                        {
                                            if (GetDayOfWeek(counter) == DaysOfWeek.Wednesday)
                                                if (counter.Day <= 21 && counter.Day > 14)
                                                {
                                                    Result.Add(counter);
                                                    counter = new DateTime(counter.Year, counter.Month + 1, 1);
                                                }
                                            counter.AddDays(1);
                                        }
                                        break;
                                    case PositionDay.Thursday:
                                        counter = StartDate;
                                        while (counter.ToOADate() < EndDate.ToOADate())
                                        {
                                            if (GetDayOfWeek(counter) == DaysOfWeek.Thursday)
                                                if (counter.Day <= 21 && counter.Day > 14)
                                                {
                                                    Result.Add(counter);
                                                    counter = new DateTime(counter.Year, counter.Month + 1, 1);
                                                }
                                            counter.AddDays(1);
                                        }
                                        break;
                                    case PositionDay.Friday:
                                        counter = StartDate;
                                        while (counter.ToOADate() < EndDate.ToOADate())
                                        {
                                            if (GetDayOfWeek(counter) == DaysOfWeek.Friday)
                                                if (counter.Day <= 21 && counter.Day > 14)
                                                {
                                                    Result.Add(counter);
                                                    counter = new DateTime(counter.Year, counter.Month + 1, 1);
                                                }
                                            counter.AddDays(1);
                                        }
                                        break;
                                    case PositionDay.Saturday:
                                        counter = StartDate;
                                        while (counter.ToOADate() < EndDate.ToOADate())
                                        {
                                            if (GetDayOfWeek(counter) == DaysOfWeek.Saturday)
                                                if (counter.Day <= 21 && counter.Day > 14)
                                                {
                                                    Result.Add(counter);
                                                    counter = new DateTime(counter.Year, counter.Month + 1, 1);
                                                }
                                            counter.AddDays(1);
                                        }
                                        break;
                                    case PositionDay.Sunday:
                                        counter = StartDate;
                                        while (counter.ToOADate() < EndDate.ToOADate())
                                        {
                                            if (GetDayOfWeek(counter) == DaysOfWeek.Sunday)
                                                if (counter.Day <= 21 && counter.Day > 14)
                                                {
                                                    Result.Add(counter);
                                                    counter = new DateTime(counter.Year, counter.Month + 1, 1);
                                                }
                                            counter.AddDays(1);
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
                                        while (counter.ToOADate() < EndDate.ToOADate())
                                        {
                                            if (GetDayOfWeek(counter) == DaysOfWeek.Monday)
                                                if (counter.Day <= 28 && counter.Day > 21)
                                                {
                                                    Result.Add(counter);
                                                    counter = new DateTime(counter.Year, counter.Month + 1, 1);
                                                }
                                            counter.AddDays(1);
                                        }
                                        break;
                                    case PositionDay.Tuesday:
                                        counter = StartDate;
                                        while (counter.ToOADate() < EndDate.ToOADate())
                                        {
                                            if (GetDayOfWeek(counter) == DaysOfWeek.Tuesday)
                                                if (counter.Day <= 28 && counter.Day > 21)
                                                {
                                                    Result.Add(counter);
                                                    counter = new DateTime(counter.Year, counter.Month + 1, 1);
                                                }
                                            counter.AddDays(1);
                                        }
                                        break;
                                    case PositionDay.Wednesday:
                                        counter = StartDate;
                                        while (counter.ToOADate() < EndDate.ToOADate())
                                        {
                                            if (GetDayOfWeek(counter) == DaysOfWeek.Wednesday)
                                                if (counter.Day <= 28 && counter.Day > 21)
                                                {
                                                    Result.Add(counter);
                                                    counter = new DateTime(counter.Year, counter.Month + 1, 1);
                                                }
                                            counter.AddDays(1);
                                        }
                                        break;
                                    case PositionDay.Thursday:
                                        counter = StartDate;
                                        while (counter.ToOADate() < EndDate.ToOADate())
                                        {
                                            if (GetDayOfWeek(counter) == DaysOfWeek.Thursday)
                                                if (counter.Day <= 28 && counter.Day > 21)
                                                {
                                                    Result.Add(counter);
                                                    counter = new DateTime(counter.Year, counter.Month + 1, 1);
                                                }
                                            counter.AddDays(1);
                                        }
                                        break;
                                    case PositionDay.Friday:
                                        counter = StartDate;
                                        while (counter.ToOADate() < EndDate.ToOADate())
                                        {
                                            if (GetDayOfWeek(counter) == DaysOfWeek.Friday)
                                                if (counter.Day <= 28 && counter.Day > 21)
                                                {
                                                    Result.Add(counter);
                                                    counter = new DateTime(counter.Year, counter.Month + 1, 1);
                                                }
                                            counter.AddDays(1);
                                        }
                                        break;
                                    case PositionDay.Saturday:
                                        counter = StartDate;
                                        while (counter.ToOADate() < EndDate.ToOADate())
                                        {
                                            if (GetDayOfWeek(counter) == DaysOfWeek.Saturday)
                                                if (counter.Day <= 28 && counter.Day > 21)
                                                {
                                                    Result.Add(counter);
                                                    counter = new DateTime(counter.Year, counter.Month + 1, 1);
                                                }
                                            counter.AddDays(1);
                                        }
                                        break;
                                    case PositionDay.Sunday:
                                        counter = StartDate;
                                        while (counter.ToOADate() < EndDate.ToOADate())
                                        {
                                            if (GetDayOfWeek(counter) == DaysOfWeek.Sunday)
                                                if (counter.Day <= 28 && counter.Day > 21)
                                                {
                                                    Result.Add(counter);
                                                    counter = new DateTime(counter.Year, counter.Month + 1, 1);
                                                }
                                            counter.AddDays(1);
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
                                        while (counter.ToOADate() < EndDate.ToOADate())
                                        {
                                            if (GetDayOfWeek(counter) == DaysOfWeek.Monday)
                                                if (counter.Day <= 31 && counter.Day > 28)
                                                {
                                                    Result.Add(counter);
                                                    counter = new DateTime(counter.Year, counter.Month + 1, 1);
                                                }
                                            counter.AddDays(1);
                                        }
                                        break;
                                    case PositionDay.Tuesday:
                                        counter = StartDate;
                                        while (counter.ToOADate() < EndDate.ToOADate())
                                        {
                                            if (GetDayOfWeek(counter) == DaysOfWeek.Tuesday)
                                                if (counter.Day <= 31 && counter.Day > 28)
                                                {
                                                    Result.Add(counter);
                                                    counter = new DateTime(counter.Year, counter.Month + 1, 1);
                                                }
                                            counter.AddDays(1);
                                        }
                                        break;
                                    case PositionDay.Wednesday:
                                        counter = StartDate;
                                        while (counter.ToOADate() < EndDate.ToOADate())
                                        {
                                            if (GetDayOfWeek(counter) == DaysOfWeek.Wednesday)
                                                if (counter.Day <= 31 && counter.Day > 28)
                                                {
                                                    Result.Add(counter);
                                                    counter = new DateTime(counter.Year, counter.Month + 1, 1);
                                                }
                                            counter.AddDays(1);
                                        }
                                        break;
                                    case PositionDay.Thursday:
                                        counter = StartDate;
                                        while (counter.ToOADate() < EndDate.ToOADate())
                                        {
                                            if (GetDayOfWeek(counter) == DaysOfWeek.Thursday)
                                                if (counter.Day <= 31 && counter.Day > 28)
                                                {
                                                    Result.Add(counter);
                                                    counter = new DateTime(counter.Year, counter.Month + 1, 1);
                                                }
                                            counter.AddDays(1);
                                        }
                                        break;
                                    case PositionDay.Friday:
                                        counter = StartDate;
                                        while (counter.ToOADate() < EndDate.ToOADate())
                                        {
                                            if (GetDayOfWeek(counter) == DaysOfWeek.Friday)
                                                if (counter.Day <= 31 && counter.Day > 28)
                                                {
                                                    Result.Add(counter);
                                                    counter = new DateTime(counter.Year, counter.Month + 1, 1);
                                                }
                                            counter.AddDays(1);
                                        }
                                        break;
                                    case PositionDay.Saturday:
                                        counter = StartDate;
                                        while (counter.ToOADate() < EndDate.ToOADate())
                                        {
                                            if (GetDayOfWeek(counter) == DaysOfWeek.Saturday)
                                                if (counter.Day <= 31 && counter.Day > 28)
                                                {
                                                    Result.Add(counter);
                                                    counter = new DateTime(counter.Year, counter.Month + 1, 1);
                                                }
                                            counter.AddDays(1);
                                        }
                                        break;
                                    case PositionDay.Sunday:
                                        counter = StartDate;
                                        while (counter.ToOADate() < EndDate.ToOADate())
                                        {
                                            if (GetDayOfWeek(counter) == DaysOfWeek.Sunday)
                                                if (counter.Day <= 31 && counter.Day > 28)
                                                {
                                                    Result.Add(counter);
                                                    counter = new DateTime(counter.Year, counter.Month + 1, 1);
                                                }
                                            counter.AddDays(1);
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
                                        while (counter.ToOADate() < EndDate.ToOADate())
                                        {
                                            if (GetDayOfWeek(counter) == DaysOfWeek.Monday)
                                                if (counter.Day <= DateTime.DaysInMonth(counter.Year, counter.Month) && counter.Day > (DateTime.DaysInMonth(counter.Year, counter.Month) - 7))
                                                {
                                                    Result.Add(counter);
                                                    counter = new DateTime(counter.Year, counter.Month + 1, 1);
                                                }
                                            counter.AddDays(1);
                                        }
                                        break;
                                    case PositionDay.Tuesday:
                                        counter = StartDate;
                                        while (counter.ToOADate() < EndDate.ToOADate())
                                        {
                                            if (GetDayOfWeek(counter) == DaysOfWeek.Tuesday)
                                                if (counter.Day <= DateTime.DaysInMonth(counter.Year, counter.Month) && counter.Day > (DateTime.DaysInMonth(counter.Year, counter.Month) - 7))
                                                {
                                                    Result.Add(counter);
                                                    counter = new DateTime(counter.Year, counter.Month + 1, 1);
                                                }
                                            counter.AddDays(1);
                                        }
                                        break;
                                    case PositionDay.Wednesday:
                                        counter = StartDate;
                                        while (counter.ToOADate() < EndDate.ToOADate())
                                        {
                                            if (GetDayOfWeek(counter) == DaysOfWeek.Wednesday)
                                                if (counter.Day <= DateTime.DaysInMonth(counter.Year, counter.Month) && counter.Day > (DateTime.DaysInMonth(counter.Year, counter.Month) - 7))
                                                {
                                                    Result.Add(counter);
                                                    counter = new DateTime(counter.Year, counter.Month + 1, 1);
                                                }
                                            counter.AddDays(1);
                                        }
                                        break;
                                    case PositionDay.Thursday:
                                        counter = StartDate;
                                        while (counter.ToOADate() < EndDate.ToOADate())
                                        {
                                            if (GetDayOfWeek(counter) == DaysOfWeek.Thursday)
                                                if (counter.Day <= DateTime.DaysInMonth(counter.Year, counter.Month) && counter.Day > (DateTime.DaysInMonth(counter.Year, counter.Month) - 7))
                                                {
                                                    Result.Add(counter);
                                                    counter = new DateTime(counter.Year, counter.Month + 1, 1);
                                                }
                                            counter.AddDays(1);
                                        }
                                        break;
                                    case PositionDay.Friday:
                                        counter = StartDate;
                                        while (counter.ToOADate() < EndDate.ToOADate())
                                        {
                                            if (GetDayOfWeek(counter) == DaysOfWeek.Friday)
                                                if (counter.Day <= DateTime.DaysInMonth(counter.Year, counter.Month) && counter.Day > (DateTime.DaysInMonth(counter.Year, counter.Month) - 7))
                                                {
                                                    Result.Add(counter);
                                                    counter = new DateTime(counter.Year, counter.Month + 1, 1);
                                                }
                                            counter.AddDays(1);
                                        }
                                        break;
                                    case PositionDay.Saturday:
                                        counter = StartDate;
                                        while (counter.ToOADate() < EndDate.ToOADate())
                                        {
                                            if (GetDayOfWeek(counter) == DaysOfWeek.Saturday)
                                                if (counter.Day <= DateTime.DaysInMonth(counter.Year, counter.Month) && counter.Day > (DateTime.DaysInMonth(counter.Year, counter.Month) - 7))
                                                {
                                                    Result.Add(counter);
                                                    counter = new DateTime(counter.Year, counter.Month + 1, 1);
                                                }
                                            counter.AddDays(1);
                                        }
                                        break;
                                    case PositionDay.Sunday:
                                        counter = StartDate;
                                        while (counter.ToOADate() < EndDate.ToOADate())
                                        {
                                            if (GetDayOfWeek(counter) == DaysOfWeek.Sunday)
                                                if (counter.Day <= DateTime.DaysInMonth(counter.Year, counter.Month) && counter.Day > (DateTime.DaysInMonth(counter.Year, counter.Month) - 7))
                                                {
                                                    Result.Add(counter);
                                                    counter = new DateTime(counter.Year, counter.Month + 1, 1);
                                                }
                                            counter.AddDays(1);
                                        }
                                        break;
                                }
                                break;
                        }
                        break;
                    case FrequencyType.Yearly:
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
        Sunday=1,
        Monday=2,
        Tuesday=4,
        Wednesday=8,
        Thursday=16,
        Friday=32,
        Saturday=64
    }

    enum Repeat
    {
        Daily,
        Weekly,
        Monthly,
        Yearly
    }
}
