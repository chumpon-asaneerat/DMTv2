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
using System.Collections;
using System.Linq;

#endregion


namespace DMT.TA.Windows.Plaza
{
    /// <summary>
    /// Interaction logic for PlazaInternalCreditExchangeWindow.xaml
    /// </summary>
    public partial class PlazaInternalCreditExchangeWindow : Window
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public PlazaInternalCreditExchangeWindow()
        {
            InitializeComponent();
        }

        #endregion

        #region Internal Variables

        private TSBReplaceCreditManager _manager = new TSBReplaceCreditManager();
        private LocalOperations ops = LocalServiceOperations.Instance.Plaza;

        #endregion

        #region Button Handlers

        private void cmdOK_Click(object sender, RoutedEventArgs e)
        {
            if (null != _manager)
            {
                if (!_manager.IsEquals)
                {
                    // TODO: Replace Message Box.
                    MessageBox.Show("จำนวนเงินขอแลก ไม่เท่ากัน กรุณาตรวจสอบข้อมูล");
                    return;
                }
                _manager.Save();
            }
            this.DialogResult = true;
        }

        private void cmdCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        #endregion

        public void Setup()
        {
            // Set TSB.
            _manager.TSB = ops.TSB.GetCurrent().Value();
            // Set Current Supervisor
            _manager.Supervisor = DMT.Controls.TAApp.User.Current;
            // Set description (Replace out)
            _manager.ReplaceOut.Description = "เงินขอแลกออก";
            this.plazaEntry.DataContext = _manager.ReplaceOut;
            // Set description (Replace in)
            _manager.ReplaceIn.Description = "เงินขอแลกเข้า";
            this.exchangeEntry.DataContext = _manager.ReplaceIn;
        }
    }
}
