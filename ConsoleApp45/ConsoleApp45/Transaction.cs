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

    class ProjectedTransaction : Transaction
    {
        string Category;

        ProjectedTransaction(string name, string category, float amount, Frequency frequency, DateTime startDate, DateTime endDate): base(name, amount, frequency, startDate, endDate)
        {
            
        }

    }
}
