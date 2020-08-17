#region Using

using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

#endregion

namespace DMT.TA.Controls.Plaza.Entry
{
    /// <summary>
    /// Interaction logic for LoanMoney2Entry.xaml
    /// </summary>
    public partial class LoanMoney2Entry : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public LoanMoney2Entry()
        {
            InitializeComponent();
        }

        #endregion

        private void txtBHTTotal_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter || e.Key == System.Windows.Input.Key.Return)
            {
                txtAdditionalBHTTotal.SelectAll();
                txtAdditionalBHTTotal.Focus();
                e.Handled = true;
            }
        }

        private void txtAdditionalBHTTotal_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter || e.Key == System.Windows.Input.Key.Return)
            {
                txtUserBHTTotal.SelectAll();
                txtUserBHTTotal.Focus();
                e.Handled = true;
            }
        }

        private void txtUserBHTTotal_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter || e.Key == System.Windows.Input.Key.Return)
            {
                txtBHTTotal.SelectAll();
                txtBHTTotal.Focus();
                e.Handled = true;
            }
        }
    }
}
