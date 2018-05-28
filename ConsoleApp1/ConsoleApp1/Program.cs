using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Transaction
    {
        Priority priority;
        string category;
        string name;
        double amount;
        Frequency frequency;
        List<DateTime> dates;
        
        public Transaction(double amount, Priority priority = Priority.None, Frequency frequency = Frequency.Once)
        {
            this.amount = amount;
            this.priority = priority;
            this.frequency = frequency;
            dates.Add(DateTime.Now);
            // we need to implement AutoNames
            //switch statement to be implemented to define dates
           
        }

    }

    class User
    {
        double Balance;
        List<Transaction> transaction;
        //schedule if needed
        double minimumBalance;
        List<Income> income; //need to think about it
        public User (double Balance = 0, double minimumBalance = 0)
        {

        }

        public DateTime hitMinimumBalance()
        {
            //calculate the date when you hit the minimumBalance
            return DateTime.Now;
        }

        public double avoidHittingMinumumBalance()
        {
            //calculate how much user needs to save this month to avoid hitting minumum balance
            return 0;
        }

        public void pathToAvoid()
        {
            //method suggests the ways to avoid hitting the minimum balance
            //think about it later
        }
    }

    class Income
    {
        string NameIncome;
        Frequency IncomeFrequency;
        List<DateTime> IncomeDates;
        double Amount;

        public Income(string name, double amount, Frequency frequency = Frequency.Once)
        {

            this.NameIncome = name;
            this.Amount = amount;
            this.IncomeFrequency= frequency;
            IncomeDates.Add(DateTime.Now);
            // we need to implement AutoNames
            //switch statement to be implemented to define dates

        }
    }

    enum Frequency
    {
        Daily,
        Weekly,
        Monthly,
        Yearly,
        Once
    };
    enum Priority
    {
        Unavoidable,
        High,
        Medium,
        Low,
        None
    };
}
