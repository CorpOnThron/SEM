using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp45
{
    class User
    {
        float MinimumBalance;
        float CurrentBalance;
        DateTime BudgetEndDate;
        List<ProjectedTransaction> ProjectedTransaction;
        List<ActualTransaction> ActualTransaction;
        List<Income> Income;
        Data Calculation;
        List<string> Category;
        List<Frequency> Frequencies;

        public User(float minimumBalance, float currentBalance, DateTime budgetEndDate)
        {
            MinimumBalance = minimumBalance;
            CurrentBalance = currentBalance;
            BudgetEndDate = budgetEndDate;
        }

        public void AddIncome(string name, float amount, Frequency frequency, DateTime startDate, DateTime endDate)
        {
            Income.Add(new Income(name, amount, startDate, endDate));
        }

        public void AddProjectedTransaction(string name, string category, float amount, Priority priority, Frequency frequency, DateTime startDate, DateTime endDate)
        {
            ProjectedTransaction.Add(new ProjectedTransaction(name, category, amount, priority, startDate, endDate));
        }

        public void AddActualTransaction(ProjectedTransaction projectedTransaction)
        {
            ActualTransaction.Add(new ActualTransaction(projectedTransaction));
        }
        

        public void UpdateMinimumBalance(float newBalance)
        {
            MinimumBalance = newBalance;
        }

        public void UpdateCurrentBalance (float newBalance)
        {
            CurrentBalance = newBalance;
        }

        public void UpdateBudgetDate(DateTime newDate)
        {
            BudgetEndDate = newDate;
        }
    }
}
