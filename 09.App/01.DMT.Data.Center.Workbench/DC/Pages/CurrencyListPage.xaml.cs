﻿#region Using

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
using System.Windows.Navigation;
using System.Windows.Shapes;

using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

using DMT.Services;

#endregion

namespace DMT.DC.Pages
{
    /// <summary>
    /// Interaction logic for CurrencyListPage.xaml
    /// </summary>
    public partial class CurrencyListPage : UserControl
    {
        #region Constructor

        public CurrencyListPage()
        {
            InitializeComponent();
        }

        #endregion

        private SCWOperations ops = SCWServiceOperations.Instance.Plaza;

        #region Loaded/Unloaded

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            SCWServiceOperations.Instance.UserName = "DMTUSER";
            SCWServiceOperations.Instance.Password = "DMTPASS";
            LoadCurrencyList();
        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {

        }

        #endregion

        private void LoadCurrencyList()
        {
            pgrid.SelectedObject = ops.Masters.GetCurrencyList(31);
        }
    }
}
