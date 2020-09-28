﻿#region Using

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

#endregion

namespace DMT.TA.Pages.Plaza
{
    /// <summary>
    /// Interaction logic for PlazaReceivedReturnPage.xaml
    /// </summary>
    public partial class PlazaReceivedReturnPage : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public PlazaReceivedReturnPage()
        {
            InitializeComponent();
        }

        #endregion

        private LocalOperations ops = LocalServiceOperations.Instance.Plaza;
        private TSB _tsb = null;

        #region Button Handlers

        private void cmdCancel_Click(object sender, RoutedEventArgs e)
        {
            // Main Menu Page
            var page = new Menu.MainMenu();
            PageContentManager.Instance.Current = page;
        }

        private void cmdAppend_Click(object sender, RoutedEventArgs e)
        {
            var win = new Windows.Plaza.PlazaReceivedCreditWindow();
            win.Owner = Application.Current.MainWindow;


            win.Title = "คืนเงินทอน";
            //win.Setup(srcObj, rcvObj, retObj, resObj);
            if (win.ShowDialog() == false)
            {
                return;
            }

            RefreshPlazaInfo();
        }

        #endregion

        public void Setup()
        {
            RefreshPlazaInfo();
        }

        private void RefreshPlazaInfo()
        {
            _tsb = ops.TSB.GetCurrent().Value();
            var tsbCredit = ops.Credits.GetTSBBalance(_tsb).Value();

            this.DataContext = tsbCredit;
            tsbCredit.Description = "เงินยืมทอน";
            tsbCredit.HasRemark = false;
            plaza.IsEnabled = false;
            plaza.DataContext = tsbCredit;

            loanEntry.IsEnabled = false;
            loanEntry.DataContext = tsbCredit;
        }
    }
}
