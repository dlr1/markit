using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkItApp
{
    public static class CostBasisFactory
    {
        public static CostBasisMethod GetCostBasisCalculator(CostBasis costBasis, List<Stock> shareBlocks, int numbertoSell, decimal sellPrice)
        {
            CostBasisMethod cb = null;
            switch (costBasis)
            {
                case CostBasis.FIFO:
                    cb = new FifoCostBasis(shareBlocks, numbertoSell, sellPrice);
                    break;
                case CostBasis.LIFO:
                    cb = new LifoCostBasis(shareBlocks, numbertoSell, sellPrice);
                    break;
                case CostBasis.Highest:
                    cb = new HighestCostBasis(shareBlocks, numbertoSell, sellPrice);
                    break;
                case CostBasis.Lowest:
                    cb = new LowestCostBasis(shareBlocks, numbertoSell, sellPrice);
                    break;
                case CostBasis.WeightedAverage:
                    cb = new WeightedAverage(shareBlocks, numbertoSell, sellPrice);
                    break;
            }
            return cb;
        }
    }
}
