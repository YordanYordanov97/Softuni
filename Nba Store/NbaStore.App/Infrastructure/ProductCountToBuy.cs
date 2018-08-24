using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NbaStore.App.Infrastructure
{
    public class ProductCountToBuy
    {
        public int Number { get; set; } = 1;

        public void Increase()
        {
            this.Number++;
        }

        public void Decrease()
        {
            this.Number--;
            if (this.Number <= 0)
            {
                this.Number = 1;
            }
        }
    }
}
