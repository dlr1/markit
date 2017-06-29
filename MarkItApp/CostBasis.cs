using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkItApp
{
    public abstract class CostBasisMethod
    {
        protected List<Stock> _shareblocks;

        // number of shares to sell
        protected int _sharesToSell;

        // sell at price
        protected decimal _sellPrice;

        public virtual SaleDetail CalculateDetailsOfSale()
        {
            var saleDetail = new SaleDetail();

            var sharesToSell = _sharesToSell;

            //loop through to see how many shares to sell in the blocks
            for (var i = 0; i < _shareblocks.Count; i++)
            {
                // number of shares remaining in the block, after shares are sold
                int SharesRemainingInBlock = 0;

                if (sharesToSell > 0)
                {
                    // number of shares to sell in the current block
                    if (_shareblocks[i].NumberOfshares >= sharesToSell)
                    {
                        // accumulate cost prices
                        saleDetail.CostPriceOfSoldShares += _shareblocks[i].Price * sharesToSell;

                        // remaining shares in the block
                        SharesRemainingInBlock = _shareblocks[i].NumberOfshares - sharesToSell;
                    }
                    else
                    {
                        // accumulate cost prices
                        saleDetail.CostPriceOfSoldShares += _shareblocks[i].Price * _shareblocks[i].NumberOfshares;
                    }

                    // remaining shares to sell, not available in the current block
                    sharesToSell = sharesToSell - _shareblocks[i].NumberOfshares;
                }
                else
                    SharesRemainingInBlock = _shareblocks[i].NumberOfshares;

                // add cost prices of the remaining shares
                saleDetail.CostPriceOfRemainingshares += SharesRemainingInBlock * _shareblocks[i].Price;
            }

            // number of shares remanining
            saleDetail.SharesRemaining = _shareblocks.Sum(x => x.NumberOfshares) - _sharesToSell;

            // gain or loss
            saleDetail.GainOrLoss = _sellPrice * _sharesToSell - saleDetail.CostPriceOfSoldShares;

            return saleDetail;
        }
    }
    
    public class FifoCostBasis : CostBasisMethod
    {
        public FifoCostBasis(List<Stock> shareBlocks, int numbertoSell, decimal sellPrice)
        {
            // order shareblocks by purchase date
            _shareblocks = shareBlocks.OrderBy(block => block.PurchaseDate).ToList();
            _sharesToSell = numbertoSell;
            _sellPrice = sellPrice;
        }
    }
    public class LifoCostBasis : CostBasisMethod
    {
        public LifoCostBasis(List<Stock> shareBlocks, int numbertoSell, decimal sellPrice)
        {
            // order shareblocks by purchase date descending
            _shareblocks = shareBlocks.OrderByDescending(block => block.PurchaseDate).ToList();
            _sharesToSell = numbertoSell;
            _sellPrice = sellPrice;
        }
    }
    public class HighestCostBasis : CostBasisMethod
    {
        public HighestCostBasis(List<Stock> shareBlocks, int numbertoSell, decimal sellPrice)
        {
            // order shareblocks by price descending
            _shareblocks = shareBlocks.OrderByDescending(block => block.Price).ToList();
            _sharesToSell = numbertoSell;
            _sellPrice = sellPrice;
        }

    }
    public class LowestCostBasis : CostBasisMethod
    {
        public LowestCostBasis(List<Stock> shareBlocks, int numbertoSell, decimal sellPrice)
        {
            // order shareblocks by price
            _shareblocks = shareBlocks.OrderBy(block => block.Price).ToList();
            _sharesToSell = numbertoSell;
            _sellPrice = sellPrice;
        }

    }
    public class WeightedAverage : CostBasisMethod
    {
        public WeightedAverage(List<Stock> shareBlocks, int numbertoSell, decimal sellPrice)
        {
            _shareblocks = shareBlocks;
            _sharesToSell = numbertoSell;
            _sellPrice = sellPrice;
        }

        public override SaleDetail CalculateDetailsOfSale()
        {
            var saleDetail = new SaleDetail();

            // get the average cost
            var avgCost = _shareblocks.Sum(x => x.NumberOfshares * x.Price) / _shareblocks.Sum(x => x.NumberOfshares);

            saleDetail.SharesRemaining = _shareblocks.Sum(x => x.NumberOfshares) - _sharesToSell;
            saleDetail.CostPriceOfSoldShares = avgCost * _sharesToSell;
            saleDetail.GainOrLoss = (_sellPrice * _sharesToSell) - (avgCost * _sharesToSell);
            saleDetail.CostPriceOfRemainingshares = saleDetail.SharesRemaining * avgCost;

            return saleDetail;
        }
    }
}
