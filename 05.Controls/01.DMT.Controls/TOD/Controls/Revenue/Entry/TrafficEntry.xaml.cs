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
    /// Interaction logic for TrafficEntry.xaml
    /// </summary>
    public partial class TrafficEntry : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public TrafficEntry()
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

        private void txt1Baht_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter  || e.Key == System.Windows.Input.Key.Return)
            {
                txt2Baht.SelectAll();
                txt2Baht.Focus();
                e.Handled = true;
            }
        }

        private void txt2Baht_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter || e.Key == System.Windows.Input.Key.Return)
            {
                txt5Baht.SelectAll();
                txt5Baht.Focus();
                e.Handled = true;
            }
        }

        private void txt5Baht_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter || e.Key == System.Windows.Input.Key.Return)
            {
                txt10Baht.SelectAll();
                txt10Baht.Focus();
                e.Handled = true;
            }
        }

        private void txt10Baht_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter || e.Key == System.Windows.Input.Key.Return)
            {
                txt20Baht.SelectAll();
                txt20Baht.Focus();
                e.Handled = true;
            }
        }

        private void txt20Baht_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter || e.Key == System.Windows.Input.Key.Return)
            {
                txt50Baht.SelectAll();
                txt50Baht.Focus();
                e.Handled = true;
            }
        }

        private void txt50Baht_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter || e.Key == System.Windows.Input.Key.Return)
            {
                txt100Baht.SelectAll();
                txt100Baht.Focus();
                e.Handled = true;
            }
        }

        private void txt100Baht_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter || e.Key == System.Windows.Input.Key.Return)
            {
                txt500Baht.SelectAll();
                txt500Baht.Focus();
                e.Handled = true;
            }
        }

        private void txt500Baht_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter || e.Key == System.Windows.Input.Key.Return)
            {
                txt1000Baht.SelectAll();
                txt1000Baht.Focus();
                e.Handled = true;
            }
        }

        private void txt1000Baht_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {

            if (e.Key == System.Windows.Input.Key.Enter || e.Key == System.Windows.Input.Key.Return)
            {
                txtRemarkBaht.SelectAll();
                txtRemarkBaht.Focus();
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
