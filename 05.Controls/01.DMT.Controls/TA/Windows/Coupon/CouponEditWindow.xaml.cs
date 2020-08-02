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

#endregion

namespace DMT.TA.Windows.Coupon
{
    /// <summary>
    /// Interaction logic for CouponEditWindow.xaml
    /// </summary>
    public partial class CouponEditWindow : Window
    {
        #region Constructor

        public CouponEditWindow()
        {
            InitializeComponent();
        }

        #endregion

        private PlazaOperations ops = DMTServiceOperations.Instance.Plaza;
        private User _user = null;

        #region Button Handlers

        private void cmdSaveExchange_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void cmdCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
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

        public void Setup(User user)
        {
            _user = user;
            if (null != _user)
            {
                txtUser.Content = string.Format("พนง: {0} {1}", 
                    _user.UserId, _user.FullNameTH);
            }
            else
            {
                txtUser.Content = string.Empty;
            }
        }
    }
}
