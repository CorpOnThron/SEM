using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp45
{
    class WishItem
    {
        string Name;
        float Amount;
        Priority Prior;

        public WishItem(string name, float amount, Priority prior = Priority.None) {

            Name = name;
            Amount = amount;
            Prior = prior;

        }



    }
}
