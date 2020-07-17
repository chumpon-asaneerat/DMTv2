#region Using

using System;
using System.Collections.Generic;
using System.Windows;

using DMT.Models;
using DMT.Services;
using NLib.Reflection;


#endregion

namespace DMT.TOD.Windows.UserShifts
{
    /// <summary>
    /// Interaction logic for BOSWindow.xaml
    /// </summary>
    public partial class BOSWindow : Window
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public BOSWindow()
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
            Shift shift = cbShift.SelectedItem as Shift;
            if (null != shift)
            {
                UserShift inst = ops.UserShifts.Create(shift, _user);
                if (null != inst) shift.AssignTo(inst);
                bool success = ops.UserShifts.BeginUserShift(inst);
                if (!success)
                {
                    // Some job is open required to enter revenue entry first.
                    MessageBox.Show(this, "ไม่สามารถเปิดกะใหม่ได้ เนื่องจาก ยังมีกะที่ยังไม่ป้อนรายได้", 
                        "DMT - Tour of Duty");
                }
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
                cbShift.ItemsSource = ops.Shifts.GetShifts();
                var tsb = ops.TSB.GetCurrent();
                txtPlaza.Text = tsb.TSBNameTH;
                txtDate.Text = dt.ToThaiDateString();
                txtTime.Text = dt.ToThaiTimeString();
                txtID.Text = _user.UserId;
                txtName.Text = _user.FullNameTH;
            }
        }
    }
}
