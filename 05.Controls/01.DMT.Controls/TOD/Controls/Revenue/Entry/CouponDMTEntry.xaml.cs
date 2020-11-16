#region Using

using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

using DMT.Services;

#endregion

namespace DMT.TOD.Controls.Revenue.Entry
{
    /// <summary>
    /// Interaction logic for CouponDMTEntry.xaml
    /// </summary>
    public partial class CouponDMTEntry : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public CouponDMTEntry()
        {
            InitializeComponent();
        }

        #endregion

        private RevenueEntryManager _manager;
        private Models.RevenueEntry entry;

        #region Loaded/Unloaded

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {

        }

        #endregion

        #region TextBox Keydown

        private void txtFreePassUsageClassA_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter || e.Key == System.Windows.Input.Key.Return)
            {
                txtFreePassUsageOther.SelectAll();
                txtFreePassUsageOther.Focus();
                e.Handled = true;
            }
        }

        #endregion

        public void Setup(RevenueEntryManager manager)
        {
            _manager = manager;
            entry = (null != _manager) ? _manager.RevenueEntry : null;
            this.DataContext = entry;
        }
    }
}
