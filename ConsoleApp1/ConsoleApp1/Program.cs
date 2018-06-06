using System;
using System.Collections.Generic;

namespace ConsoleApp4
{
    //Enumerators
    enum Freq
    {
        Daily,
    Weekly,
    BiWeekly,
    Monthly,
    Yearly
    };

    enum Position
    {
        First,
    Second,
    Third,
    Fourth,
    Fifth
    };

enum DayTypes
{
    Sunday,
    Monday,
    Tuesday,
    Wednesday,
    Thursday,
    Friday,
    Saturday,
    Day,
    Weekday,
    WeekendDay
};
    [Flags]
enum DaysOfWeek
{
    Sunday = 0,
    Monday = 1,
    Tuesday = 2,
    Wednesday = 4,
    Thursday = 8,
    Friday = 16,
    Saturday = 32
};
    [Flags]
enum Month
{
    January = 0,
    February = 1,
    March = 2,
    April = 4,
    May = 8,
    June = 16,
    July = 32,
    August = 64,
    September = 128,
    October = 256,
    November = 512,
    December = 1024
};


    class Frequency
{
        // variables
        Freq freq;
    DateTime startDate;
    DaysOfWeek daysOfWeek;
    int x;
    int[] DaysOfMonth;

// Constructors

Frequency(Freq Freq, DateTime startDate = DateTime.Now)    //This constructor sets the Freq variable of the class, and the startDate  CHECK OUT CHANGES!!!
    {
            this.freq = Freq;
            this.startDate = startDate;
    }
Frequency(int x = 1)    //This constructor serts the Freq Variable of the class, and the enum DaysOfWeek. X lets the user skip the period by X amount
    {
            freq = Freq.Daily; 
            this.x = x;
    }
Frequency(int x = 1,  Enum DaysOfWeek)
        {
            freq = Freq.Weekly;
            this.x = x;
        }
Frequency(int X = 1, Params int DaysOfMonth)   //This constructor sets the Freq Variable of the class, and the int array DaysOfMonth. X lets the user skip the period by X amount
    {
            freq = Freq.Monthly;
        }
Frequency(int X = 1, Month Month, Position Position, DayTypes DayTypes)
    {
            freq = Freq.Yearly;
    }

        public List<DateTime> GetNextDate(Frequency freqObj, int ah = 10)// return x number of dates, which satisfy frequency and starts from start date(by default today)
        {
            List<DateTime> shedule = new List<DateTime>();

            switch (freqObj)
            {
                case Daily:
                    DateTime date1 = freqObj.startDate;
                    int i = 1;
                    shedule[0] = freqObj.startDate;
                    while (i < ah)
                    {
                        double da = Convert.ToDouble(freqObj.x);
                        shedule[i] = date1.AddDays(da);
                        i++;
                    }
                    break;
                case Weekly:
                    DateTime date1 = freqObj.startDate;
                    int i = 0;
                    shedule[0] = freqObj.startDate;
                    while (i < ah)
                    {
                        double da = Convert.ToDouble(freqObj.x) * 7;
                        shedule[i] = date1.AddDays(da);
                        i++;
                    }
                    break;
                case BiWeekly:// what is this?
                    break;
                case Monthly:
                    DateTime date1 = freqObj.startDate;
                    int i = 0;
                    shedule[0] = freqObj.startDate;
                    while (i < ah)
                    {
                        double da = Convert.ToDouble(freqObj.x);
                        shedule[i] = date1.AddMonths(da);
                        i++;
                    }
                    break;
                case Yearly:
                    DateTime date1 = freqObj.startDate;
                    int i = 0;
                    shedule[0] = freqObj.startDate;
                    while (i < ah)
                    {
                        double da = Convert.ToDouble(freqObj.x);
                        shedule[i] = date1.AddYears(da);
                        i++;
                    }
                    break;
                default:
                    break;
            }

            return shedule;
        }
    }
}
