#region Using

using System;
using System.Collections.Generic;
using System.Windows;

using DMT.Models;
using DMT.Services;

#endregion

namespace DMT.TOD.Windows.Job
{
    /// <summary>
    /// Interaction logic for BOJWindow.xaml
    /// </summary>
    public partial class BOJWindow : Window
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public BOJWindow()
        {
            InitializeComponent();
        }

        #endregion

        private PlazaOperations ops = DMTServiceOperations.Instance.Plaza;
        private User _user = null;

        #region Loaded

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            txtDate.Text = DateTime.Now.ToThaiDateString();
            txtTime.Text = DateTime.Now.ToThaiTimeString();
        }

        #endregion

        private void cmdOK_Click(object sender, RoutedEventArgs e)
        {
            if (cbShift.SelectedIndex == -1)
            {
                cbShift.Focus();
                return;
            }
            DialogResult = true;
        }

        private void cmdCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        public void Setup(User user)
        {
            _user = user;
            if (null != _user)
            {
                DateTime dt = DateTime.Now;
                cbShift.ItemsSource = ops.TSB.GetShifts();

                txtDate.Text = dt.ToThaiDateString();
                txtTime.Text = dt.ToThaiTimeString();
                txtID.Text = _user.UserId;
                txtName.Text = _user.FullNameTH;
            }
        }
    }
}
