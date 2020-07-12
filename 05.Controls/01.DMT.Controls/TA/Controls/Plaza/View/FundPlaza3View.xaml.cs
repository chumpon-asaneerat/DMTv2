using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

using NLib;
using NLib.Services;

namespace DMT.TA.Controls.Plaza.View
{
    /// <summary>
    /// Interaction logic for FundPlaza3View.xaml
    /// </summary>
    public partial class FundPlaza3View : UserControl
    {
        public FundPlaza3View()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            PageContentManager.Instance.OnTick += new EventHandler(Instance_OnTick);
        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            PageContentManager.Instance.OnTick -= new EventHandler(Instance_OnTick);
        }

        private void cmdEdit_Click(object sender, RoutedEventArgs e)
        {
            Button b = sender as Button;

        }


        void Instance_OnTick(object sender, EventArgs e)
        {

        }


        public void Setup()
        {

        }
    }
}
