using System;
using System.Collections.Generic;
using System.Linq;

namespace MarkItApp
{
    public enum CostBasis{
        FIFO,
        LIFO,
        Lowest,
        Highest,
        WeightedAverage
    }
        
    public class Stock
    {
        public int NumberOfshares { get; set; }
        public decimal Price { get; set; }
        public DateTime PurchaseDate { get; set; }
        public Stock(int numberOfshares, decimal price, DateTime purchaseDate)
        {
            NumberOfshares = numberOfshares;
            Price = price;
            PurchaseDate = purchaseDate;
        }
    }
   
}
