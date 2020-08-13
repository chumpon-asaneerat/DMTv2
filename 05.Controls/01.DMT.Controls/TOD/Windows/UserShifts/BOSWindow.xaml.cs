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

        private LocalOperations ops = LocalServiceOperations.Instance.Plaza;
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
                var creRet = ops.UserShifts.Create(shift, _user);
                UserShift inst = (null != creRet && !creRet.errors.hasError) ? creRet.data : null;
                if (null != inst) shift.AssignTo(inst);

                var busrSht = ops.UserShifts.BeginUserShift(inst);
                bool success = (null != busrSht && !busrSht.errors.hasError);
                if (!success)
                {
                    DMT.Windows.MessageBoxWindow msg = new DMT.Windows.MessageBoxWindow();
                    msg.Owner = Application.Current.MainWindow;
                    msg.Setup("ไม่สามารถเปิดกะใหม่ได้ เนื่องจาก ยังมีกะที่ยังไม่ป้อนรายได้", "DMT - Tour of Duty");
                    if (msg.ShowDialog() == true)
                    {

                    }
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
                var ret = ops.Shifts.GetShifts();
                var shifts = (null != ret && !ret.errors.hasError) ? ret.data : null;
                cbShift.ItemsSource = shifts;

                var tsbRet = ops.TSB.GetCurrent();
                var tsb = (null != tsbRet && !tsbRet.errors.hasError) ? tsbRet.data : null;
                if (null != tsb)
                {
                    txtPlaza.Text = tsb.TSBNameTH;
                }
                txtDate.Text = dt.ToThaiDateString();
                txtTime.Text = dt.ToThaiTimeString();

                txtID.Text = _user.UserId;
                txtName.Text = _user.FullNameTH;
            }
        }
    }
}
