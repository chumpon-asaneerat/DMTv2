#region Using

using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

#endregion

namespace DMT.TOD.Controls.Revenue.Entry
{
    /// <summary>
    /// Interaction logic for RevenueEntry.xaml
    /// </summary>
    public partial class RevenueEntry : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public RevenueEntry()
        {
            InitializeComponent();
        }

        #endregion

        private Models.RevenueEntry entry;

        #region Loaded/Unloaded

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {

        }

        #endregion

        private void UserControl_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            entry = this.DataContext as Models.RevenueEntry;
            if (null != entry)
            {
                /*
                if (entry.IsHistorical)
                {
                    tabQRCode.Visibility = Visibility.Collapsed;
                    tabEMV.Visibility = Visibility.Collapsed;
                }
                else
                {
                    tabQRCode.Visibility = Visibility.Visible;
                    tabEMV.Visibility = Visibility.Visible;
                }
                */
            }
        }

        private void txtBagNo_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter || e.Key == System.Windows.Input.Key.Return)
            {
                txtBeltNo.SelectAll();
                txtBeltNo.Focus();
                e.Handled = true;
            }
        }

        private void txtBeltNo_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter || e.Key == System.Windows.Input.Key.Return)
            {
                trafficRevenue.Focus();
                trafficRevenue.txt1Baht.SelectAll();
                trafficRevenue.txt1Baht.Focus();
                e.Handled = true;
            }
        }

        public void FocusBagNo()
        {
            txtBagNo.Focus();
            txtBagNo.SelectAll();
        }

        public void FocusBeltNo()
        {
            txtBeltNo.Focus();
            txtBeltNo.SelectAll();
        }

        public void RefreshItems()
        {
            qrcodeEntry.LoadItems();
            emvEntry.LoadItems();
        }

        public bool HasBagNo { get { return !string.IsNullOrWhiteSpace(txtBagNo.Text); } }
        public bool HasBeltNo { get { return !string.IsNullOrWhiteSpace(txtBeltNo.Text); } }
    }
}
