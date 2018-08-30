using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp45
{
    class WishItem
    {
        public string Name;
        public float Amount;
        public Priority Prior;

        public WishItem(string name, float amount, Priority prior = Priority.None) {

            Name = name;
            Amount = amount;
            Prior = prior;

        }



    }
}
