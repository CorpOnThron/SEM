using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp45
{
    class Data
    {
        public DateTime Date;
        public float Amount;

        public Data(DateTime DateT, float AmountT)
        {
            Date = DateT;
            Amount = AmountT;
        }

        //public void AddData(DateTime date, float amount)
        //{
        //    Dates.Add(date);
        //    Amount.Add(amount);
        //}

        //public void Sort()
        //{
        //    for(int i = 0; i< Dates.Count; i++)
        //    {
        //        for (int j = i + 1; j < Dates.Count; j++)
        //        {
        //            if (DateTime.Compare(Dates[i], Dates[j]) > 0)
        //            {
        //                DateTime temp = Dates[i];
        //                Dates[i] = Dates[j];
        //                Dates[j] = temp;

        //                float temp2 = Amount[i];
        //                Amount[i] = Amount[j];
        //                Amount[j] = temp2;
        //            }
        //        }
        //    }
        //}

    }
}
