﻿using System;
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
            Income = new List<Income>();
            ActualTransaction = new List<ActualTransaction>();
        }

        public void AddIncome(Income income)
        {
            Income.Add(income);
        }

        public void AddProjectedTransaction(ProjectedTransaction projectedTransaction)
        {
            ProjectedTransaction.Add(projectedTransaction);
        }

        public void AddActualTransaction(ProjectedTransaction projectedTransaction)
        {
            ActualTransaction.Add(new ActualTransaction(projectedTransaction, DateTime.Now));
        }

        public void AddActualTransaction(Income income)
        {
            ActualTransaction.Add(new ActualTransaction(income,DateTime.Now));
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
            foreach (Income obj in Income)
            {
                foreach(DateTime obj2 in obj.Frequency.GetNextDates(obj.EndDate))
                {
                    Data TempData = new Data(obj2, -(obj.Amount));
                    ListData.Add(TempData);
                }
            }
            //ListData.OrderBy(x => x.Date).ToList();
            ListData.Sort((x, y) => x.Date.CompareTo(y.Date));
        }

        public List<ActualTransaction> DisplayActualTransactions()
        {
            foreach(ActualTransaction obj in ActualTransaction)
            {
                Console.WriteLine(obj);
            }
            return ActualTransaction;
        }

        public void CalculateDaysLeft()
        {
            float DupeCurrent = CurrentBalance;
            bool hitsMinBalance = false;
            foreach(Data obj in ListData)
            {
                DupeCurrent -= obj.Amount;
                if(DupeCurrent <= MinimumBalance)
                {
                    Console.WriteLine($"{obj.Date} is the date when user hits minimum balance: {Math.Round((obj.Date - DateTime.Now).TotalDays)} Days left");
                    hitsMinBalance = true;
                    break;
                }
            }
            if (!hitsMinBalance)
            {
                Console.WriteLine($"User never hits minimum balance, he is a rich bitch!");
            }
        }
       
        public void AddCategory(string category)
        {
            Category.Add(category);
        }

        public List<ProjectedTransaction> GetTransactionsOn(DateTime date)
        {
            List<ProjectedTransaction> Result = new List<ProjectedTransaction>();

            foreach(ProjectedTransaction obj in ProjectedTransaction)
            {
                foreach(DateTime obj2 in obj.Frequency.GetNextDates(obj.EndDate))
                {
                    if(DateTime.Compare(obj2, date) == 0)
                    {
                        Result.Add(obj);
                    }
                }
            }
            foreach (Income obj in Income)
            {
                foreach (DateTime obj2 in obj.Frequency.GetNextDates(obj.EndDate))
                {
                    if (DateTime.Compare(obj2, date) == 0)
                    {
                        Result.Add(obj.ToProjectedTransaction());
                    }
                }
            }
            return Result;
        }
    }
}
