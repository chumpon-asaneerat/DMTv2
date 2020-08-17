#region Using

using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

#endregion

namespace DMT.TA.Controls.Plaza.Entry
{
    /// <summary>
    /// Interaction logic for FundDetailEntry.xaml
    /// </summary>
    public partial class FundDetailEntry : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public FundDetailEntry()
        {
            InitializeComponent();
        }

        #endregion

        private void txtExchangeBHT_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter || e.Key == System.Windows.Input.Key.Return)
            {
                txtBorrowBHT.SelectAll();
                txtBorrowBHT.Focus();
                e.Handled = true;
            }
        }

        private void txtBorrowBHT_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter || e.Key == System.Windows.Input.Key.Return)
            {
                txtAdditionalBHT.SelectAll();
                txtAdditionalBHT.Focus();
                e.Handled = true;
            }
        }

        private void txtAdditionalBHT_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter || e.Key == System.Windows.Input.Key.Return)
            {
                txtExchangeBHT.SelectAll();
                txtExchangeBHT.Focus();
                e.Handled = true;
            }
        }
    }
}
