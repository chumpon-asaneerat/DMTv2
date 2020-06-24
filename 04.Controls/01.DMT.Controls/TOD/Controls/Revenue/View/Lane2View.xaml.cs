#region Using

using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

#endregion

namespace DMT.TOD.Controls.Revenue.View
{
    /// <summary>
    /// Interaction logic for Lane2View.xaml
    /// </summary>
    public partial class Lane2View : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public Lane2View()
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

        /*
        public void Setup(List<Models.Lane> lanes)
        {
            listView.ItemsSource = lanes;
        }
        */
    }
}
