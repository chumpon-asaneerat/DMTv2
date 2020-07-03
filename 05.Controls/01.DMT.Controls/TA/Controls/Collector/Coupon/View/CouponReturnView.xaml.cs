using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace DMT.TA.Controls.Collector.Coupon
{
    /// <summary>
    /// Interaction logic for CouponReturnView.xaml
    /// </summary>
    public partial class CouponReturnView : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public CouponReturnView()
        {
            InitializeComponent();
        }

        #endregion

        private void cmdReturn_Click(object sender, RoutedEventArgs e)
        {

        }

        /*
        public void Setup(List<Models.Coupon> coupons)
        {
            listView.ItemsSource = coupons;
        }
        */
    }
}
