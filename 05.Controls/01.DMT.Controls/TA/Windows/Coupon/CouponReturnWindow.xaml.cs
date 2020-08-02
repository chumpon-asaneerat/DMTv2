using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

using System.Collections;



namespace DMT.TA.Windows.Coupon
{
    /// <summary>
    /// Interaction logic for CouponReturnWindow.xaml
    /// </summary>
    public partial class CouponReturnWindow : Window
    {
        public CouponReturnWindow()
        {
            InitializeComponent();
        }

        #region Button Handlers

        private void cmdSaveExchange_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void cmdCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        #endregion

        private void btnNext35_Click(object sender, RoutedEventArgs e)
        {
           
        }

        private void btnBack35_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void btnNext80_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void btnBack80_Click(object sender, RoutedEventArgs e)
        {
            
        }

    }
}
