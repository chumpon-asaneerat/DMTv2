#region Using

using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

#endregion

namespace DMT.TOD.Controls.Revenue.View
{
    /// <summary>
    /// Interaction logic for SelectLaneView.xaml
    /// </summary>
    public partial class SelectLaneView : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public SelectLaneView()
        {
            InitializeComponent();
        }

        #endregion

        #region Loaded/Unloaded

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {

        }

        #endregion

        public void Setup(List<Models.LaneAttendance> lanes)
        {
            listView.ItemsSource = lanes;
        }
    }
}
