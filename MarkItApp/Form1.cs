using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MarkItApp
{
    public partial class Form1 : Form
    {
        public readonly List<Stock> shares = null;
        public Form1()
        {
            InitializeComponent();

            shares = new List<Stock>();
            shares.Add(new Stock(100, 10m, new DateTime(2005, 1, 1)));
            shares.Add(new Stock(40, 12m, new DateTime(2005, 2, 2)));
            shares.Add(new Stock(50, 11m, new DateTime(2005, 3, 3)));

            cbCostMethod.DataSource = Enum.GetValues(typeof(CostBasis));

            txtSellDate.Text = "4/4/2005";
        }

        void validateAndEnableCalculation()
        {
            int sharesToSell;
            decimal sellPrice;
            DateTime dt;
            btnCalculate.Enabled = Int32.TryParse(txtShares.Text, out sharesToSell)
                                                && Decimal.TryParse(txtPrice.Text, out sellPrice)
                                                && DateTime.TryParse(txtSellDate.Text, out dt);
                
        }
        private void btnCalculate_Click(object sender, EventArgs e)
        {
            var cbf = CostBasisFactory.GetCostBasisCalculator(
                                            (CostBasis)Enum.Parse(typeof(CostBasis), cbCostMethod.Text, true),
                                            shares, 
                                            Convert.ToInt32(txtShares.Text), 
                                            Convert.ToDecimal(txtPrice.Text));

            var saleDetail = cbf.CalculateDetailsOfSale();

            txtResults.Text = string.Format("Cost price of sold shares:{0:N2}" +
                                        "\r\nGain loss on sale:{1:N2}" +
                                        "\r\nNumber of remaining shares:{2}" +
                                        "\r\nCost price of remaining shares:{3:N2}", 
                                        saleDetail.CostPriceOfSoldShares, saleDetail.GainOrLoss, 
                                        saleDetail.SharesRemaining,saleDetail.CostPriceOfRemainingshares);
        }

        private void txtShares_TextChanged(object sender, EventArgs e)
        {
            validateAndEnableCalculation();
        }

        private void txtPrice_TextChanged(object sender, EventArgs e)
        {
            validateAndEnableCalculation();
        }

        private void txtSellDate_TextChanged(object sender, EventArgs e)
        {
            validateAndEnableCalculation();
        }
    }
}
