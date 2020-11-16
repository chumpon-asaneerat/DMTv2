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
    /// Interaction logic for CouponRevenueEntry.xaml
    /// </summary>
    public partial class CouponRevenueEntry : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public CouponRevenueEntry()
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

        #region TextBox KeyDown

        private void txt35Baht_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter || e.Key == System.Windows.Input.Key.Return)
            {
                txt80Baht.SelectAll();
                txt80Baht.Focus();
                e.Handled = true;
            }
        }

        private void txt35Book_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter || e.Key == System.Windows.Input.Key.Return)
            {

            }
        }

        private void txt80Baht_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter || e.Key == System.Windows.Input.Key.Return)
            {

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
