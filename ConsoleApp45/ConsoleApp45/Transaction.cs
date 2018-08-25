using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp45
{
    ///<summary>Transaction Class: The parent class to Data, ProjectedTransaction, ActualTransaction, Income. It is used to store and initialize Name, Amount and StartDate of all kinds of transactions.</summary>
    public class Transaction
    {
        public string Name;
        public float Amount;
        ///<summary>NOTE: For ProjectedTransactions and Income class it works as StartDate. However, for ActualTransaction and Data class it works as just a Date</summary>
        public DateTime StartDate;


        ///<summary>CONSTRUCTOR</summary>
        public Transaction(string name, float amount, DateTime startDate)
        {
            Name = name;
            Amount = amount;
            StartDate = startDate;
        }
    }

    ///<summary>Data Class: This class inherits Transaction Class. It is used for holding the data which could be used for calculation purposes. It is also used as a list of transactions that will occur including the projected transactions and the income.</summary>
    public class Data : Transaction
    {
        public Priority Priority;

        ///<summary>CONSTRUCTOR</summary>
        public Data(DateTime date, float amount, string name = "", Priority priority = Priority.None) : base(name, amount, date)
        {
            Priority = priority;
        }

        ///<summary>+Operator adds two Data objects. The amount goes by adding the total of LHS and RHS amount, whereas the date goes by the older one.</summary>
        public static Data operator +(Data obj1, Data obj2)
        {
            Data Result = new Data((obj1.StartDate > obj2.StartDate ? obj1.StartDate:obj2.StartDate),(obj1.Amount + obj1.Amount));
            return Result;
        }
    }

    ///<summary>ProjectedTransaction Class: This class inherits Transaction Class. It is used by the user to plan his/her expenses, or add what they feel they will spend in future.</summary>
    public class ProjectedTransaction : Transaction
    {
        public string Category;
        public Priority Priority;
        public Frequency Frequency;
        public DateTime EndDate;



        ///<summary>CONSTRUCTOR</summary>
        public ProjectedTransaction(string name, string category, float amount, Priority priority, DateTime startDate, DateTime endDate):base(name, amount, startDate)
        {
            Category = category;
            Priority = priority;
            EndDate = endDate;
        }

        ///<summary>Sets the frequency to the object</summary>
        public void SetFrequency(Frequency frequency)
        {
            this.Frequency = frequency;
        }

    }

    ///<summary>ActualTransaction Class: This class inherits Transaction Class. It is used for holding the transactions that have been actually done by the user. It can either be converted from projected transaction directly or be created directly by the user on the spot.</summary>
    public class ActualTransaction : Transaction
    {
        string Category;
        Priority Priority;
        DateTime Date;

        ///<summary>CONSTRUCTORS - 3 Types</summary>
        public ActualTransaction(string name, string category, float amount, Priority priority, DateTime date): base(name, amount, date)
        {
            this.Category = category;
            Priority = priority;
        }

        ///<summary>CONSTRUCTORS - 3 Types</summary>
        public ActualTransaction(ProjectedTransaction projectedTransaction, DateTime date) : base(projectedTransaction.Name, projectedTransaction.Amount, date)
        {
            this.Category = projectedTransaction.Category;
            this.Priority = projectedTransaction.Priority;
        }

        ///<summary>CONSTRUCTORS - 3 Types</summary>
        public ActualTransaction(Income income, DateTime date) : base(income.Name, -(income.Amount), date)
        {
            this.Category = "Income";
            this.Priority = Priority.High;
        }


        ///<summary>To show the details of the object</summary>
        public override string ToString()
        {
            string action = Amount > 0 ? "Withdrawn" : "Deposited";
            return $"{Name}\t\t({Category})\t\t{Date.ToShortDateString()}\t\t${Amount}\t\t{action}";
        }
    }

    ///<summary>Income Class: This class inherits Transaction Class. It is used to store the income source that the user specifies.</summary>
    public class Income : Transaction
    {
        public DateTime EndDate;
        public Frequency Frequency;

        ///<summary>CONSTRUCTOR</summary>
        public Income(string name, float amount, DateTime startDate, DateTime endDate): base(name, amount, startDate)
        {
            EndDate = endDate;
        }

        ///<summary>Sets the frequency</summary>
        public void SetFrequency(Frequency frequency)
        {
            this.Frequency = frequency;
        }

        ///<summary>Converts the income object to ProjectedTransaction</summary>
        public ProjectedTransaction ToProjectedTransaction()
        {
            return new ProjectedTransaction(Name, "Income", Amount, Priority.High, StartDate, EndDate);
        }
    }

    ///<summary>Priority Enumeration: This enumeration is used to set priority to the transactions that the user specifies.</summary>
    public enum Priority
    {
        None,
        Low,
        Medium,
        High
    }
}
