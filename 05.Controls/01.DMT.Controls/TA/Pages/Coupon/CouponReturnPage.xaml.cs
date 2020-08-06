#region Using

using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

using DMT.Models;
using DMT.Services;
using NLib.Services;
using NLib.Reflection;
using NLib.Reports.Rdlc;
using System.Reflection;
using System.ComponentModel;
using System.Windows.Interop;
using NLib;
using System.Windows.Threading;

#endregion

namespace DMT.TA.Pages.Coupon
{
    /// <summary>
    /// Interaction logic for CouponReturnPage.xaml
    /// </summary>
    public partial class CouponReturnPage : UserControl
    {
        #region Constructor

        public CouponReturnPage()
        {
            InitializeComponent();
        }

        #endregion

        #region Button
        private void cmdCancel_Click(object sender, RoutedEventArgs e)
        {
            // Main Menu Page
            var page = new Menu.MainMenu();
            PageContentManager.Instance.Current = page;
        }

        private void cmdOk_Click(object sender, RoutedEventArgs e)
        {
            DMT.Windows.MessageBoxYesNoRed3Window msg = new DMT.Windows.MessageBoxYesNoRed3Window();
            msg.Owner = Application.Current.MainWindow;
            msg.Setup("ยืนยันการขายคูปอง จำนวน ","5", " เล่ม"
                , "คูปอง 35 บาท = ", "2"," เล่ม"
                , "คูปอง 80 บาท = ", "3", " เล่ม"
                , "Toll Admin");
            if (msg.ShowDialog() == true)
            {
                // Main Menu Page
                var page = new Menu.MainMenu();
                PageContentManager.Instance.Current = page;
            }
            
        }

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
        #endregion

    }
}
