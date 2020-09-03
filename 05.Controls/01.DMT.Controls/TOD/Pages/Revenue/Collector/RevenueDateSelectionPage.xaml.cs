﻿#region Using

using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

using DMT.Models;
using DMT.Services;
using NLib.Services;
using NLib.Reflection;

#endregion

namespace DMT.TOD.Pages.Revenue
{
    /// <summary>
    /// Interaction logic for RevenueDateSelectionPage.xaml
    /// </summary>
    public partial class RevenueDateSelectionPage : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public RevenueDateSelectionPage()
        {
            InitializeComponent();
        }

        #endregion

        private RevenueEntryManager _manager = new RevenueEntryManager();

        #region Button Handlers

        private void cmdCancel_Click(object sender, RoutedEventArgs e)
        {
            // Main Menu Page
            var page = new Menu.MainMenu();
            PageContentManager.Instance.Current = page;
        }

        private void cmdOk_Click(object sender, RoutedEventArgs e)
        {
            // Revenue Entry Page
            var page = new RevenueEntryPage();
            if (null == _manager || null == _manager.PlazaGroup)
            {
                DMT.Windows.MessageBoxWindow msg = new DMT.Windows.MessageBoxWindow();
                msg.Owner = Application.Current.MainWindow;
                msg.Setup("กรุณาเลือกด่านของรายได้", "DMT - Tour of Duty");
                if (msg.ShowDialog() == true)
                {
                    cbPlazas.Focus();
                    return;
                }
            }

            _manager.LoadRevenueShift();
            if (null != _manager.RevenueShift)
            {
                if (_manager.HasRevenuEntry)
                {
                    DMT.Windows.MessageBoxWindow msg = new DMT.Windows.MessageBoxWindow();
                    msg.Owner = Application.Current.MainWindow;
                    msg.Setup("กะของพนักงานนี้ ถูกป้อนรายได้แล้ว", "DMT - Tour of Duty");
                    if (msg.ShowDialog() == true)
                    {
                        return;
                    }
                }
                if (!_manager.HasIncompletedLanes)
                {
                    DMT.Windows.MessageBoxWindow msg = new DMT.Windows.MessageBoxWindow();
                    msg.Owner = Application.Current.MainWindow;
                    msg.Setup("ไม่พบข้อมูลเลนที่ยังไม่ถูกป้อนรายได้", "DMT - Tour of Duty");
                    if (msg.ShowDialog() == true)
                    {
                        return;
                    }
                }
            }
            else
            {
                if (_manager.IsNewRevenueShift)
                {
                    DMT.Windows.MessageBoxWindow msg = new DMT.Windows.MessageBoxWindow();
                    msg.Owner = Application.Current.MainWindow;
                    msg.Setup("ไม่สามารถนำส่งรายได้ เนื่องจากไม่พบข้อมูลการทำงาน", "DMT - Tour of Duty");
                    if (msg.ShowDialog() == true)
                    {
                        //return;
                    }
                }
                else
                {
                    DMT.Windows.MessageBoxWindow msg = new DMT.Windows.MessageBoxWindow();
                    msg.Owner = Application.Current.MainWindow;
                    msg.Setup("กะนี้ถูกจัดเก็บรายได้แล้ว.", "DMT - Tour of Duty");
                    if (msg.ShowDialog() == true)
                    {
                        //return;
                    }
                }
                return;
            }

            page.Setup(_manager);

            PageContentManager.Instance.Current = page;
        }

        #endregion

        #region Combobox Handlers

        private void cbPlazas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Load related lane data.
            RefreshLanes();
        }

        #endregion

        private void LoadPlazaGroups()
        {
            cbPlazas.ItemsSource = null;
            cbPlazas.ItemsSource = _manager.PlazaGroups;
            if (null != _manager.PlazaGroups && _manager.PlazaGroups.Count > 0)
            {
                cbPlazas.SelectedIndex = 0;
            }
        }

        private void RefreshLanes()
        {
            if (null != _manager && null != _manager.UserShift)
            {
                // get selected plaza group
                _manager.PlazaGroup = cbPlazas.SelectedItem as PlazaGroup;
                txtRevDate.Text = _manager.RevenueDate.ToThaiDateTimeString("dd/MM/yyyy");
                // reload jobs.
                _manager.RefreshJobs();
                if (null == _manager.Attendances || _manager.Attendances.Count <= 0)
                {
                    grid.Setup(null); // no attendance data.
                }
                else
                {
                    grid.Setup(_manager.Attendances);
                }
            }
        }

        public void Setup(User user)
        {
            LoadPlazaGroups();

            _manager.User = user;
            if (null != _manager && null != _manager.User)
            {
                txtEntryDate.Text = _manager.EntryDate.ToThaiDateTimeString("dd/MM/yyyy HH:mm:ss");
                _manager.Refresh();
                RefreshLanes();
            }
        }
    }
}
