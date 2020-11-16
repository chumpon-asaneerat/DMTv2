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

        private void UserControl_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
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

        public void Setup(RevenueEntryManager manager)
        {
            _manager = manager;
            entry = (null != _manager) ? _manager.RevenueEntry : null;
            this.DataContext = entry;

            this.trafficRevenue.Setup(manager);
            this.otherRevenue.Setup(manager);
            this.couponDMT.Setup(manager);
            this.couponRevenue.Setup(manager);
            this.couponUsage.Setup(manager);
            this.emvEntry.Setup(manager);
            this.qrcodeEntry.Setup(manager);

            RefreshItems();
        }

        public bool HasBagNo { get { return !string.IsNullOrWhiteSpace(txtBagNo.Text); } }
        public bool HasBeltNo { get { return !string.IsNullOrWhiteSpace(txtBeltNo.Text); } }
    }
}
