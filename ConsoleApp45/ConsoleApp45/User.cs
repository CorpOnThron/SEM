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
        public DateTime BudgetEndDate;
        List<ProjectedTransaction> ProjectedTransaction;
        List<ActualTransaction> ActualTransaction;
        List<Income> Income;
        Data Calculation;
        public List<string> Category;
        List<Frequency> Frequencies;
        public List<Data> ListData;

        public User(float minimumBalance, float currentBalance, DateTime budgetEndDate)
        {
            MinimumBalance = minimumBalance;
            CurrentBalance = currentBalance;
            BudgetEndDate = budgetEndDate;
            ListData = new List<Data>();
            Category = new List<string>();
            ProjectedTransaction = new List<ProjectedTransaction>();
        }

        public void AddIncome(string name, float amount, Frequency frequency, DateTime startDate, DateTime endDate)
        {
            Income.Add(new Income(name, amount, startDate, endDate));
        }

        public void AddProjectedTransaction(ProjectedTransaction projectedTransaction)
        {
            ProjectedTransaction.Add(projectedTransaction);
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

        public void AddData() {
            foreach (ProjectedTransaction obj in ProjectedTransaction) {
                foreach(DateTime obj2 in obj.Frequency.GetNextDates(obj.EndDate))
                {
                    Data TempData = new Data(obj2, obj.Amount);
                    ListData.Add(TempData);
                }
            }
            ListData.OrderBy(x => x.Date).ToList();

        }

       
    }
}
