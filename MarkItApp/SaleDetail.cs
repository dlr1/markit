using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkItApp
{
    public class SaleDetail
    {
        public decimal CostPriceOfSoldShares { get; set; }
        public decimal GainOrLoss { get; set; }
        public int SharesRemaining { get; set; }
        public decimal CostPriceOfRemainingshares { get; set; }
    }
}
