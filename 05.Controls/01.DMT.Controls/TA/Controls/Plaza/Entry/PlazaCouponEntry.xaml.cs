#region Using

using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

#endregion

namespace DMT.TA.Controls.Plaza.Entry
{
    /// <summary>
    /// Interaction logic for PlazaCouponEntry.xaml
    /// </summary>
    public partial class PlazaCouponEntry : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public PlazaCouponEntry()
        {
            InitializeComponent();
        }

        #endregion

        #region Loaded/Unloaded

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {

        }

        #endregion

        private void txtCouponBHT35_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter || e.Key == System.Windows.Input.Key.Return)
            {
                txtCouponTotal.SelectAll();
                txtCouponTotal.Focus();
                e.Handled = true;
            }
        }

        private void txtCouponBHT80_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter || e.Key == System.Windows.Input.Key.Return)
            {
                txtCouponBHT35.SelectAll();
                txtCouponBHT35.Focus();
                e.Handled = true;
            }
        }

        private void txtCouponTotal_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter || e.Key == System.Windows.Input.Key.Return)
            {
                txtCouponBHT35.SelectAll();
                txtCouponBHT35.Focus();
                e.Handled = true;
            }
        }
    }
}
