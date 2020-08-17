#region Using

using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

#endregion

namespace DMT.TA.Controls.Plaza.Entry
{
    /// <summary>
    /// Interaction logic for PlazaFundEntry.xaml
    /// </summary>
    public partial class PlazaCreditEntry : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public PlazaCreditEntry()
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

        #region Textbox KeyDown
        private void txtBHT1_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter || e.Key == System.Windows.Input.Key.Return)
            {
                txtBHT2.SelectAll();
                txtBHT2.Focus();
                e.Handled = true;
            }
        }

        private void txtBHT2_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter || e.Key == System.Windows.Input.Key.Return)
            {
                txtBHT5.SelectAll();
                txtBHT5.Focus();
                e.Handled = true;
            }
        }

        private void txtBHT5_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter || e.Key == System.Windows.Input.Key.Return)
            {
                txtBHT10.SelectAll();
                txtBHT10.Focus();
                e.Handled = true;
            }
        }

        private void txtBHT10_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter || e.Key == System.Windows.Input.Key.Return)
            {
                txtBHT20.SelectAll();
                txtBHT20.Focus();
                e.Handled = true;
            }
        }

        private void txtBHT20_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter || e.Key == System.Windows.Input.Key.Return)
            {
                txtBHT50.SelectAll();
                txtBHT50.Focus();
                e.Handled = true;
            }
        }

        private void txtBHT50_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter || e.Key == System.Windows.Input.Key.Return)
            {
                txtBHT100.SelectAll();
                txtBHT100.Focus();
                e.Handled = true;
            }
        }

        private void txtBHT100_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter || e.Key == System.Windows.Input.Key.Return)
            {
                txtBHT500.SelectAll();
                txtBHT500.Focus();
                e.Handled = true;
            }
        }

        private void txtBHT500_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter || e.Key == System.Windows.Input.Key.Return)
            {
                txtBHT1000.SelectAll();
                txtBHT1000.Focus();
                e.Handled = true;
            }
        }

        private void txtBHT1000_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter || e.Key == System.Windows.Input.Key.Return)
            {
                txtBHT1.SelectAll();
                txtBHT1.Focus();
                e.Handled = true;
            }
        }

        #endregion
    }
}
