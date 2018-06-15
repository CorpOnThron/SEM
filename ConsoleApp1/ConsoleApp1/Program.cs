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
        Month month;
    int x;

// Constructors

public Frequency(Freq Freq, DateTime startDate, int x, DaysOfWeek da)    //This constructor sets the Freq variable of the class, and the startDate  CHECK OUT CHANGES!!!
    {
            this.freq = Freq;
            this.startDate = startDate;
            this.x = x;
            this.daysOfWeek = da;
    }
        public Frequency(Freq Freq, DateTime startDate, DaysOfWeek da)    //This constructor sets the Freq variable of the class, and the startDate  CHECK OUT CHANGES!!!
        {
            this.freq = Freq;
            this.startDate = startDate;
            this.x = 1;
            this.daysOfWeek = da;
        }

        public Frequency(Freq Freq, DateTime startDate, int x)    //This constructor sets the Freq variable of the class, and the startDate  CHECK OUT CHANGES!!!
        {
            this.freq = Freq;
            this.startDate = startDate;
            this.x = x;
            this.daysOfWeek = startDate.DayOfWeek;
        }

        public Frequency(Freq Freq, DateTime startDate)    //This constructor sets the Freq variable of the class, and the startDate  CHECK OUT CHANGES!!!
        {
            this.freq = Freq;
            this.startDate = startDate;
            this.x = 1;
            this.daysOfWeek = startDate.DayOfWeek;
        }


        public Frequency(Freq Freq, DateTime startDate, int x)    //This constructor sets the Freq variable of the class, and the startDate  CHECK OUT CHANGES!!!
        {
            this.freq = Freq;
            this.startDate = DateTime.Now;
            this.x = x;
            this.daysOfWeek = startDate.DayOfWeek;
        }
        public Frequency(Freq Freq, int x)    //This constructor sets the Freq variable of the class, and the startDate  CHECK OUT CHANGES!!!
        {
            this.freq = Freq;
            this.startDate = DateTime.Now;
            this.x = x;
            this.daysOfWeek = startDate.DayOfWeek;
        }
        //daysOfWeek.HasFlag()
        public List<DateTime> GetNextDate(Frequency freqObj, DateTime finish)// return x number of dates, which satisfy frequency and starts from start date(by default today)
        {
            List<DateTime> shedule = new List<DateTime>();

            switch (freqObj)
            {
                case Daily:
                    DateTime date1 = freqObj.startDate;
                    int i = 1;
                    shedule[0] = freqObj.startDate;
                    while (shedule[i] < finish)
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

                    while (shedule[i] < finish)
                    {
                        date1.AddDays(1);
                        if (freqObj.daysOfWeek.HasFlag(date1.DayOfWeek) == true)
                        {
                            shedule[i] = date1.AddDays(da);
                            i++;
                        }
                    }
                    break;
                case Monthly:
                    DateTime date1 = freqObj.startDate;
                    int i = 0;
                    shedule[0] = freqObj.startDate;
                    while (shedule[i] < finish)
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
                    while (shedule[i] < finish)
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
