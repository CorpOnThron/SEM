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

        ///<summary>CONSTRUCTOR</summary>
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
        ///<summary>
        ///Returns the total amount that user will spend/earn on the date specified. Positive value suggests that the amount is spent. Negative amount tells that the user earned on that date.
        ///</summary>
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


        ///<summary>Adds the wish to the user's wishlist</summary>
        public void AddWishItem(WishItem wish) {
            WishList.Add(wish);
        }

        ///<summary>Adds income source to the user's income</summary>
        public void AddIncome(Income income)
        {
            Income.Add(income);
        }

        ///<summary>Adds a transaction plan to the user's projection of transactions</summary>
        public void AddProjectedTransaction(ProjectedTransaction projectedTransaction)
        {
            ProjectedTransaction.Add(projectedTransaction);
        }

        ///<summary>Converts the projected transaction to the user's list of actual transactions. The function must be called when the date on the projected transaction has passed the today's date.</summary>
        public void AddActualTransaction(ProjectedTransaction projectedTransaction)
        {
            ActualTransaction.Add(new ActualTransaction(projectedTransaction, DateTime.Now));
        }

        ///<summary>Converts the projected transaction to the user's list of actual transactions with an altered amount spent by the user. The function must be called when the date on the projected transaction has passed the today's date.</summary>
        public void AddActualTransaction(ProjectedTransaction projectedTransaction, float amount)
        {
            ProjectedTransaction tempPT = projectedTransaction;
            tempPT.Amount = amount;
            ActualTransaction.Add(new ActualTransaction(tempPT, DateTime.Now));
        }

        /// <summary>
        /// Returns the user's current balance on the date specified.
        /// </summary>
        /// <param name="date"></param>
        /// <returns> Returns the user's current balance on the date specified.</returns>
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

        /// <summary>
        /// Adds the income object and today's date to the list of actual transactions.
        /// </summary>
        /// <param name="income"></param>
        public void AddActualTransaction(Income income)
        {
            ActualTransaction.Add(new ActualTransaction(income,DateTime.Now));
        }

        /// <summary>
        /// Used to adjust the minimum balance that the user plans to have in his/her budget period.
        /// </summary>
        /// <param name="newBalance"></param>
        public void UpdateMinimumBalance(float newBalance)
        {
            MinimumBalance = newBalance;
        }

        /// <summary>
        /// Used to adjust the user's current blance.
        /// </summary>
        /// <param name="newBalance"></param>
        public void UpdateCurrentBalance (float newBalance)
        {
            CurrentBalance = newBalance;
        }

        /// <summary>
        /// To change the date till which the user wants the application to handle the budget for him/her.
        /// </summary>
        /// <param name="newDate"></param>
        public void UpdateBudgetDate(DateTime newDate)
        {
            BudgetEndDate = newDate;
        }

        /// <summary>
        /// Adds all the occurences of ProjectedTransactions and Income to List of Data. It also sorts every occurence by date in ascending order.
        /// </summary>
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

        /// <summary>
        /// Returns the date when the user will next get his/her pay. Will always be in future only IF THE DATA CLASS DOESN'T HAVE ANY TRANSACTIONS THAT ARE BEFORE TODAY.
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Returns the date when the user will next get his/her pay after the date specified. Will always be in future only IF THE DATA CLASS DOESN'T HAVE ANY TRANSACTIONS THAT ARE BEFORE TODAY and THE DATE SPECIFIED IS NOT IN PAST.
        /// </summary>
        /// <returns></returns>
        public DateTime GetNextPayDay(DateTime Date)
        {
            foreach (Data obj in ListData)
            {
                if (obj.Amount < 0 && Date >= DateTime.Now.Date)
                {
                    return obj.StartDate;
                }
            }
            return new DateTime();
        }

        /// <summary>
        /// Returns a list of all transactions that have occured till now.
        /// </summary>
        /// <returns></returns>
        public List<ActualTransaction> DisplayActualTransactions()
        {
            foreach(ActualTransaction obj in ActualTransaction)
            {
                Console.WriteLine(obj);
            }
            return ActualTransaction;
        }

        /// <summary>
        /// Displays on console if the user ever hits the minimum balance. If he/she does, then it will display the date on which he hits the balance, and the balance. Along with the days left to hit the minimum balance.
        /// </summary>
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


        /// <summary>
        /// Returns the date and amount that will represent the all-time minimum balance that the user will hit during his/her budget period.
        /// </summary>
        /// <returns></returns>
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
            while(CalculateMinimumBalance(MinBalanceObj.Amount) < MinimumBalance)
            {
                MinBalanceObj.Amount += CalculateMinimumBalance(MinBalanceObj.Amount);
            }

            return MinBalanceObj;
        }


        /// <summary>
        /// Returns the date and amount that will represent the all-time minimum balance that the user will hit during his/her budget period after adding the AddToCurrentBalance to the current balance of the user.
        /// </summary>
        /// <param name="AddToCurrentBalance">Adjustments made to the user's current balance to check the new minimum balance</param>
        /// <returns></returns>
        public float CalculateMinimumBalance(float AddToCurrentBalance)
        {

            DateTime StartDate = DateTime.Now.Date;
            DateTime TempDate = new DateTime(1, 1, 1);
            float TempBalance = GetBalanceOnDate(StartDate) + AddToCurrentBalance;
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

        /// <summary>
        /// Adds a category to user's category list.
        /// </summary>
        /// <param name="category"></param>
        public void AddCategory(string category)
        {
            Category.Add(category);
        }

        /// <summary>
        /// Gets a list of transactions on the specified date.
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Gets a list of transactions starting from the specified date. And for the next few days starting from the start date.
        /// </summary>
        /// <param name="date"></param>
        /// <param name="RangeOfDays"></param>
        /// <returns></returns>
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


        /// <summary>
        /// Gets a list of transactions starting from the specified date. And for the next few days starting from the start date. Which meet a certain priority.
        /// </summary>
        /// <param name="date"></param>
        /// <param name="RangeOfDays"></param>
        /// <param name="priority"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Gets a list of transactions starting from the specified date and having a certain priority.
        /// </summary>
        /// <param name="date"></param>
        /// <param name="priority"></param>
        /// <returns></returns>
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

        /// <summary>
        /// returns 1: Income is enough for the user to meet his/her budget.<para />
        /// returns 2: Income is required along with avoiding None priority transactions to meet the budget.<para />
        /// returns 3: Income is required along with avoiding None and low priority transactions to meet the budget.<para />
        /// returns 4: Income is required along with making only High priority transactions to meet the budget.<para />
        /// returns 5: Impossible budget.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
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

        /// <summary>
        /// returns the amount the user needs to save for current month.
        /// </summary>
        /// <param name="DataList"></param>
        /// <returns></returns>
        public float CalculateSavingsCurrentMonth(Data DataList)
        {
            Data result = DataList;

            int TempDate = (int)(result.StartDate - DateTime.Now).TotalDays;
            int TempDate2 = (int)(new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month)) - DateTime.Now).TotalDays;
            float SavingsRequired = result.Amount / TempDate;

            return TempDate2 * SavingsRequired;
        }
        /// <summary>
        /// function that return time appropriate to buy a wish list item
        /// </summary>
        /// <param name="UserTemp"></param>
        /// <returns></returns>
        public DateTime TimeToGetWish(User UserTemp)// takes user that we will search time to buy wish list for
        {
            Data MinDrop = UserTemp.CalculateMinimumBalance();//find the max drop
            DateTime DateTemp = MinDrop.StartDate;//first day to start coubt from
            DateTime DateTempFinal = new DateTime();//assigning it to avoid mistake
            UserTemp.WishList.Sort((x, y) => x.Prior.CompareTo(y.Prior));//we sort list of wishes to make it detect wich wish must be done first

            for (; DateTemp < UserTemp.BudgetEndDate; DateTemp.AddDays(1))//for loop. Hope i made it right
            {
                    float BalanceTemp = GetBalanceOnDate(DateTemp);//we search a balance in each day after max drop
                    if ((BalanceTemp - UserTemp.WishList[0].Amount) > UserTemp.MinimumBalance)//check if today balance - wish item > minimum balance of user
                    {
                        DateTempFinal = DateTemp;
                        Console.WriteLine($"{DateTemp} is right day to buy {UserTemp.WishList[0]}");// here is a time!
                    }
            }


            return DateTempFinal;
        }
    }
}
