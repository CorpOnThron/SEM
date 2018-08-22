using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp45
{
    class User
    {
        public float MinimumBalance;
        float CurrentBalance;
        public DateTime BudgetEndDate;
        List<ProjectedTransaction> ProjectedTransaction;
        List<ActualTransaction> ActualTransaction;
        List<Income> Income;
        public List<string> Category;
        List<Frequency> Frequencies;
        public List<Data> ListData;
        public List<WishItem> WishList;

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
            WishList = new List<WishItem>();
        }

        public float GetTotalOn(DateTime date)
        {
            float temp = 0;
            foreach(Data obj in ListData)
            {
                if(obj.StartDate == date)
                {
                    temp += obj.Amount;
                }
            }
            return temp;
        }

        public void AddWishItem(WishItem wish) {
            WishList.Add(wish);
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

        public void AddActualTransaction(ProjectedTransaction projectedTransaction, float amount)
        {
            ProjectedTransaction tempPT = projectedTransaction;
            tempPT.Amount = amount;
            ActualTransaction.Add(new ActualTransaction(tempPT, DateTime.Now));
        }

        public float GetBalanceOnDate(DateTime date)
        {
            float CurrentBalanceDupe = CurrentBalance;
            foreach(Data obj in ListData)
            {
                if(obj.StartDate <= date)
                {
                    CurrentBalanceDupe -= obj.Amount;
                }
            }

            return CurrentBalanceDupe;
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
                    Data TempData = new Data(obj2, obj.Amount, obj.Name, obj.Priority);
                    ListData.Add(TempData);
                }
            }
            foreach (Income obj in Income)
            {
                foreach(DateTime obj2 in obj.Frequency.GetNextDates(obj.EndDate))
                {
                    Data TempData = new Data(obj2, -(obj.Amount), obj.Name, Priority.High);
                    ListData.Add(TempData);
                }
            }
            //ListData.OrderBy(x => x.Date).ToList();
            ListData.Sort((x, y) => x.StartDate.CompareTo(y.StartDate));
        }

        public DateTime GetNextPayDay()
        {
            foreach(Data obj in ListData)
            {
                if(obj.Amount < 0)
                {
                    return obj.StartDate;
                }
            }
            return new DateTime();
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
                    Console.WriteLine($"{obj.StartDate} is the date when user hits minimum balance: {Math.Round((obj.StartDate - DateTime.Now).TotalDays)} Days left");
                    hitsMinBalance = true;
                    break;
                }
                
            }
            if (!hitsMinBalance)
            {
                Console.WriteLine($"User never hits minimum balance, he is a rich bitch!");
            }
        }

        public Data CalculateMinimumBalance()
        {
            
            DateTime StartDate = DateTime.Now.Date;
            DateTime TempDate = new DateTime(1,1,1);
            float TempBalance = GetBalanceOnDate(StartDate);
            Data MinBalanceObj = new Data(new DateTime(1, 1, 1), TempBalance,"");
            float MinBalance = TempBalance;
            Console.WriteLine("\n\n");
            foreach (Data obj in ListData)
            {
                if(StartDate <= obj.StartDate && TempDate!=obj.StartDate)
                {
                    TempDate = obj.StartDate;
                    TempBalance -= GetTotalOn(obj.StartDate);
                    if(MinBalance > TempBalance)
                    {
                        MinBalanceObj.Amount = TempBalance;
                        MinBalanceObj.StartDate = obj.StartDate;
                    }
                    if(TempBalance <= MinimumBalance)
                    {
                        Console.WriteLine($"User hits minimum balance : {TempBalance} on {obj.StartDate.ToShortDateString()}");
                    }
                    
                }
            }

            return MinBalanceObj;
        }

        public float CalculateMinimumBalance(float AddToCurrentBalance)
        {

            DateTime StartDate = DateTime.Now.Date;
            DateTime TempDate = new DateTime(1, 1, 1);
            float TempBalance = GetBalanceOnDate(StartDate) + AddToCurrentBalance;
            bool HitsMinimumBalance = false;
            float MinBalance = TempBalance;
            foreach (Data obj in ListData)
            {
                if (StartDate <= obj.StartDate && TempDate != obj.StartDate)
                {
                    
                    TempDate = obj.StartDate;
                    TempBalance -= GetTotalOn(obj.StartDate);
                    if (MinBalance > TempBalance)
                    {
                        MinBalance = TempBalance;
                    }
                    if (TempBalance <= MinimumBalance)
                    {
                        Console.WriteLine($"User hits minimum balance : {TempBalance} on {obj.StartDate.ToShortDateString()}");
                        HitsMinimumBalance = true;
                    }
                    
                }
            }


            return MinBalance;
        }

        //public float CalculateDaysToEscape()
        //{
        //    float DupeCurrent = CurrentBalance;
        //    DateTime LastDate = DateTime.Now ;
        //    bool hitsMinBalance = false;
        //    DateTime HitDateTemp = DateTime.Now;
        //    foreach (Data obj in ListData)
        //    {
        //            if(LastDate != obj.StartDate)
        //            {
        //            DupeCurrent -= GetTotalOn(obj.StartDate);
        //            LastDate = obj.StartDate;
        //            }
                    
        //            if (DupeCurrent <= MinimumBalance)
        //            {
        //                HitDateTemp = obj.StartDate;
        //                hitsMinBalance = true;
        //                break;
        //            }
                    
        //    }
        //    if (!hitsMinBalance)
        //    {
        //        Console.WriteLine($"User never hits minimum balance, he is a rich bitch!");
        //        return 0;
        //    }
        //    else
        //    {
        //        float DupeCurrentTemp = DupeCurrent;

        //        foreach (Data obj in ListData)
        //        {
        //            if (obj.StartDate > HitDateTemp && DupeCurrent <= MinimumBalance)
        //            {
        //                DupeCurrent -= obj.Amount;
        //                if (DupeCurrent < DupeCurrentTemp)
        //                {
        //                    DupeCurrentTemp = DupeCurrent;
        //                }
        //            }
        //            else if (DupeCurrent > MinimumBalance)
        //            {
        //                Console.WriteLine($"{obj.StartDate}");
        //                break;
        //            }
        //        }
        //        return DupeCurrentTemp;
        //    }
        //}


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

        public List<ProjectedTransaction> GetTransactionsOn(DateTime date, int RangeOfDays)
        {
            List<ProjectedTransaction> Result = new List<ProjectedTransaction>();

            foreach (ProjectedTransaction obj in ProjectedTransaction)
            {
                foreach (DateTime obj2 in obj.Frequency.GetNextDates(obj.EndDate))
                {
                    if (DateTime.Compare(date, obj2) >= 0 && DateTime.Compare(date, obj2.AddDays(RangeOfDays)) < 0)
                    {
                        Result.Add(obj);
                    }
                }
            }
            foreach (Income obj in Income)
            {
                foreach (DateTime obj2 in obj.Frequency.GetNextDates(obj.EndDate))
                {
                    if (DateTime.Compare(date, obj2) >= 0 && DateTime.Compare(date, obj2.AddDays(RangeOfDays)) < 0)
                    {
                        Result.Add(obj.ToProjectedTransaction());
                    }
                }
            }
            return Result;
        }

        public List<ProjectedTransaction> GetTransactionsOn(DateTime date, int RangeOfDays, Priority priority)
        {
            List<ProjectedTransaction> Result = new List<ProjectedTransaction>();

            foreach (ProjectedTransaction obj in ProjectedTransaction)
            {
                foreach (DateTime obj2 in obj.Frequency.GetNextDates(obj.EndDate))
                {
                    if (DateTime.Compare(date, obj2) >= 0 && DateTime.Compare(date, obj2.AddDays(RangeOfDays)) < 0 && obj.Priority == priority)
                    {
                        Result.Add(obj);
                    }
                }
            }
            foreach (Income obj in Income)
            {
                foreach (DateTime obj2 in obj.Frequency.GetNextDates(obj.EndDate))
                {
                    if (DateTime.Compare(date, obj2) >= 0 && DateTime.Compare(date, obj2.AddDays(RangeOfDays)) < 0)
                    {
                        Result.Add(obj.ToProjectedTransaction());
                    }
                }
            }
            return Result;
        }

        public List<ProjectedTransaction> GetTransactionsOn(DateTime date, Priority priority)
        {
            List<ProjectedTransaction> Result = new List<ProjectedTransaction>();

            foreach (ProjectedTransaction obj in ProjectedTransaction)
            {
                foreach (DateTime obj2 in obj.Frequency.GetNextDates(obj.EndDate))
                {
                    if (DateTime.Compare(obj2, date) == 0 && priority == obj.Priority)
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
        //This function must display a list of transactions to delete to reach minimum balance.
        //Require help funcation to sync with real ListData<>
        public List<Data> PriorityListSort(Data obj)
        {
            List<Data> PriorityList = new List<Data>();
            foreach (Data obj1 in ListData) {
                if (obj1.StartDate <= obj.StartDate)
                {
                    PriorityList.Add(obj1);
                }
            }
            PriorityList.Sort((x, y) => x.Priority.CompareTo(y.Priority));

            return PriorityList;
        }

        public int CalculatePossibility(Data obj)
        {
            float sum = 0;

            foreach (Data obj1 in ListData)
            {
                
                if (obj1.StartDate >= DateTime.Now && obj1.StartDate <= obj.StartDate && obj1.Amount < 0)
                {
                    float AmountTemp = (obj1.Amount * (-1));
                    sum += AmountTemp; 
                }
            }

            if (sum > obj.Amount)
            {
                return 1;
            }
            else {


                foreach (Data obj1 in ListData)
                {
                    if (obj1.StartDate >= DateTime.Now && obj1.StartDate <= obj.StartDate && obj1.Priority == Priority.None)
                    {
                        float AmountTemp = obj1.Amount;
                        sum += AmountTemp;
                    }
                }
                if (sum > obj.Amount)
                {
                    return 2;
                }
                else {

                    foreach (Data obj1 in ListData)
                    {
                        if (obj1.StartDate >= DateTime.Now && obj1.StartDate <= obj.StartDate && obj1.Priority == Priority.Low)
                        {
                            float AmountTemp = obj1.Amount;
                            sum += AmountTemp;
                        }
                    }
                    if (sum > obj.Amount)
                    {
                        return 3;
                    }
                    else
                    {
                        foreach (Data obj1 in ListData)
                        {
                            if (obj1.StartDate >= DateTime.Now && obj1.StartDate <= obj.StartDate && obj1.Priority == Priority.Medium)
                            {
                                float AmountTemp = obj1.Amount;
                                sum += AmountTemp;
                            }
                        }
                        if (sum > obj.Amount)
                        {
                            return 4;
                        }
                        else {
                            return 5;
                        }
                    }
                }
            }

        }
    }
}
