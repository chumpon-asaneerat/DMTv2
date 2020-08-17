#region Using

using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

#endregion

namespace DMT.TA.Controls.Plaza.Entry
{
    /// <summary>
    /// Interaction logic for LoanMoneyEntry.xaml
    /// </summary>
    public partial class LoanMoneyEntry : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public LoanMoneyEntry()
        {
            InitializeComponent();
        }

        #endregion

        private void txtRevolvingFunds_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter || e.Key == System.Windows.Input.Key.Return)
            {
                txtLoanMoney.SelectAll();
                txtLoanMoney.Focus();
                e.Handled = true;
            }
        }

        private void txtLoanMoney_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter || e.Key == System.Windows.Input.Key.Return)
            {
                txtLoanMoneyCabinet.SelectAll();
                txtLoanMoneyCabinet.Focus();
                e.Handled = true;
            }
        }

        private void txtLoanMoneyCabinet_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter || e.Key == System.Windows.Input.Key.Return)
            {
                txtRevolvingFunds.SelectAll();
                txtRevolvingFunds.Focus();
                e.Handled = true;
            }
        }
    }
}
