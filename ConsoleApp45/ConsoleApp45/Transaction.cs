using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp45
{
    public class Transaction
    {
        string Name;
        float Amount;
        DateTime StartDate;
        DateTime EndDate;
        Frequency Frequency;

        Transaction(string name, float amount, Frequency frequency, DateTime startDate, DateTime endDate)
        {
            Name = name;
            Amount = amount;
            Frequency = frequency;
            StartDate = startDate;
            EndDate = endDate;
        }
    }

    public class ProjectedTransaction : Transaction
    {
        string Category;
        Priority Priority;

        ProjectedTransaction(string name, string category, float amount, Priority priority, Frequency frequency, DateTime startDate, DateTime endDate): base(name, amount, frequency, startDate, endDate)
        {
            this.Category = category;
            Priority = priority;
        }

    }

    public class ProjectedTransaction : Transaction
    {
        string Category;
        Priority Priority;

        ProjectedTransaction(string name, string category, float amount, Priority priority, Frequency frequency, DateTime startDate, DateTime endDate): base(name, amount, frequency, startDate, endDate)
        {
            this.Category = category;
            Priority = priority;
        }

    }

    public class Income : Transaction
    {

        ProjectedTransaction(string name, float amount, Frequency frequency, DateTime startDate, DateTime endDate): base(name, amount, frequency, startDate, endDate)
        {
            this.Category = category;
        }

    }

    enum Priority
    {
        None,
        Low,
        Medium,
        High
    }
}
