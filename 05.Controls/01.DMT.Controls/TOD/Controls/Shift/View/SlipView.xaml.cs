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

#endregion

namespace DMT.TOD.Controls.Shift.View
{
    /// <summary>
    /// Interaction logic for SlipView.xaml
    /// </summary>
    public partial class SlipView : UserControl
    {
        #region Constructor

        public SlipView()
        {
            InitializeComponent();
        }

        #endregion

        public void Setup(List<Models.RevenueEntry> revenues)
        {
            listView.ItemsSource = null;
            listView.ItemsSource = revenues;
        }

        public Models.RevenueEntry SelectedEntry
        {
            get
            {
                return listView.SelectedItem as Models.RevenueEntry;
            }
        }
    }
}
