using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp45
{
    public class Transaction
    {
        public string Name;
        public float Amount;
        public DateTime StartDate; 
        // for ProjectedTransactions and Income it works as startDate. However, for ActualTransaction it works as just a Date

        public Transaction(string name, float amount, DateTime startDate)
        {
            Name = name;
            Amount = amount;
            StartDate = startDate;
        }
    }

    public class ProjectedTransaction : Transaction
    {
        public string Category;
        public Priority Priority;
        public Frequency Frequency;
        public DateTime EndDate;

        public ProjectedTransaction(string name, string category, float amount, Priority priority, DateTime startDate, DateTime endDate):base(name, amount, startDate)
        {
            Category = category;
            Priority = priority;
            EndDate = endDate;
        }

        public void SetFrequency(Frequency frequency)
        {
            this.Frequency = frequency;
        }

    }

    public class ActualTransaction : Transaction
    {
        string Category;
        Priority Priority;
        DateTime Date;

        public ActualTransaction(string name, string category, float amount, Priority priority, DateTime date): base(name, amount, date)
        {
            this.Category = category;
            Priority = priority;
        }

        public ActualTransaction(ProjectedTransaction projectedTransaction) : base(projectedTransaction.Name, projectedTransaction.Amount, projectedTransaction.StartDate)
        {
            this.Category = projectedTransaction.Category;
            this.Priority = projectedTransaction.Priority;
        }

    }

    public class Income : Transaction
    {
        DateTime EndDate;
        Frequency Frequency;
        public Income(string name, float amount, DateTime startDate, DateTime endDate): base(name, amount, startDate)
        {
            EndDate = endDate;
        }

        public void SetFrequency(Frequency frequency)
        {
            this.Frequency = frequency;
        }
    }

    public enum Priority
    {
        None,
        Low,
        Medium,
        High
    }
}
