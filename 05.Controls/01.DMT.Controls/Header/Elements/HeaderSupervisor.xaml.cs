#region Using

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

//using NLib.Services;
using DMT.Models;
using DMT.Services;

#endregion

namespace DMT.Controls.Header
{
    /// <summary>
    /// Interaction logic for HeaderSupervisor.xaml
    /// </summary>
    public partial class HeaderSupervisor : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public HeaderSupervisor()
        {
            InitializeComponent();
        }

        #endregion

        private LocalOperations ops = LocalServiceOperations.Instance.Plaza;

        #region Loaded/Unloaded

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateUI();
            LocalServiceOperations.Instance.OnChangeShift += Instance_OnChangeShift;
            LocalServiceOperations.Instance.OnActiveTSBChanged += Instance_OnActiveTSBChanged;
        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            LocalServiceOperations.Instance.OnActiveTSBChanged -= Instance_OnActiveTSBChanged;
            LocalServiceOperations.Instance.OnChangeShift -= Instance_OnChangeShift;
        }

        #endregion

        private void UpdateUI()
        {
            var ret = ops.Shifts.GetCurrent();
            var shift = (null != ret && !ret.errors.hasError) ? ret.data : null;
            if (null == shift)
            {
                txtSupervisorId.Text = "รหัสหัวหน้าด่าน : ";
                txtSupervisorName.Text = "หัวหน้าด่าน : ";
            }
            else
            {
                txtSupervisorId.Text = "รหัสหัวหน้าด่าน : " + shift.UserId;
                txtSupervisorName.Text = "หัวหน้าด่าน : " + shift.FullNameTH;
            }
        }

        private void Instance_OnChangeShift(object sender, EventArgs e)
        {
            UpdateUI();
        }

        private void Instance_OnActiveTSBChanged(object sender, EventArgs e)
        {
            UpdateUI();
        }
    }
}
