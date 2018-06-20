using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp45
{
    class User
    {
        Money Wallet;
        public DateTime BudgetTargetDate { get; set; } // This date will work as the end date by default for all expenses
        List<IncomeDetail> IncomeSources;
        List<Transaction> ProjectedTransactions;
        List<Wish> WishLists;// done?
        List<Transaction> ActualTransactions;
        
        //*****FUNCTIONS*****
        /*
         Add Income
         Add Transaction
         Get transcartion today
         get transaction in X days
         get next pay day
         get high priority transactions
         get balance on (date)
         minimumbalanceGoal (this date, to this amount)
         add category
         remove category
         */
    }
    



    class Transaction
    {
        public static List<string> Category;
        public string Name;
        public double Amount;
        //*****CONSTRUCTORS*****
        public Transaction(string name = "NoName",double amount = 0)//i have no idea how to pass list to constructor
        {

        }
        //*****FUNCTIONS*****
        /*
         get all the cateogries
         get category at X
         get category with the name X
         */
    }

    class ProjectedTransaction: Transaction
    {

        DateTime StartDate;
        DateTime EndDate;
        Priority Priority;
        public Frequency Frequency;
    }

    class IncomeDetail: Transaction
    {
        DateTime StartDate;
        DateTime EndDate;
        public Frequency Frequency;
    }

    class ActualTransaction: Transaction
    {
        DateTime Date;
        Priority Priority;
    }


    class Wish
    {
        string Name;
        double Amount;
        Priority Priority;

        //*****CONSTRUCTOR*****

        public Wish(string name = "NoNameWhish", double amount = 0, Priority priority = Priority.None) // for all values, pretty sure we could not make wish without cost and name, so we don't need default value(BUT WHATEVER!)
        {
            this.Name = name;
            this.Amount = amount;
            this.Priority = priority;
        }
    }

    class Money
    {
        public static double MinimumBalance { get; set; }
        public static double CurrentBalance { get; set; }
        List<Loan> Loans;
    }
    
    class Loan
    {
        public double Amount;
        string Name;
        Loan(string name, double amount)
        {
            this.Amount = amount;
            this.Name = name;
        }

        //*****FUNCTIONS*****
        /*
         Acquire Loan(string fromName, amount) // affects the current balance by increasing it and adding it to the loan list
         Give loan(string toName, amount) // affects the current balancce by decreasing it and adding it to the loan list
         Pay loan(Loan)  // affects the current balance by decreasing it and removing it from the Loan list
         Repeal loan(Loan) // affects the current balance by increasing it and removing it form the loan list 
         */
    }

    enum Priority
    {
        High,
        Medium,
        Low,
        None
    }
}
