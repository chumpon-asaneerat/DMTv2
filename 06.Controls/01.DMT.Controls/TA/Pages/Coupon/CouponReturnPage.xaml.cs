using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

using NLib;
using NLib.Services;
using System.Collections;


namespace DMT.TA.Pages.Coupon
{
    /// <summary>
    /// Interaction logic for CouponReturnPage.xaml
    /// </summary>
    public partial class CouponReturnPage : UserControl
    {
        public CouponReturnPage()
        {
            InitializeComponent();
        }

        #region Button
        private void cmdCancel_Click(object sender, RoutedEventArgs e)
        {
            // Main Menu Page
            var page = new Menu.MainMenu();
            PageContentManager.Instance.Current = page;
        }

        private void cmdOk_Click(object sender, RoutedEventArgs e)
        {
            // Main Menu Page
            var page = new Menu.MainMenu();
            PageContentManager.Instance.Current = page;
        }

        private void btnNext35_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void btnBack35_Click(object sender, RoutedEventArgs e)
        {
           
        }

        private void btnNext80_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void btnBack80_Click(object sender, RoutedEventArgs e)
        {
            
        }
        #endregion

        #region GetDataGridRows

        public IEnumerable<DataGridRow> GetDataGridRows(DataGrid grid)
        {
            var itemsSource = grid.ItemsSource as IEnumerable;
            if (null == itemsSource) yield return null;
            foreach (var item in itemsSource)
            {
                var row = grid.ItemContainerGenerator.ContainerFromItem(item) as DataGridRow;
                if (null != row) yield return row;
            }
        }

        #endregion

        #region Datagrid
        private void listView_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            try
            {
                if (listView.ItemsSource != null)
                {
                    var row_list = GetDataGridRows(listView);
                    foreach (DataGridRow single_row in row_list)
                    {
                        if (single_row.IsSelected == true)
                        {

                        }
                    }
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void listViewUse_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            try
            {
                if (listViewUse.ItemsSource != null)
                {
                    var row_list = GetDataGridRows(listViewUse);
                    foreach (DataGridRow single_row in row_list)
                    {
                        if (single_row.IsSelected == true)
                        {

                        }
                    }
                }
                else
                {

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void listView80_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            try
            {
                if (listView80.ItemsSource != null)
                {
                    var row_list = GetDataGridRows(listView80);
                    foreach (DataGridRow single_row in row_list)
                    {
                        if (single_row.IsSelected == true)
                        {

                        }
                    }
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void listViewUse80_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            try
            {
                if (listViewUse80.ItemsSource != null)
                {
                    var row_list = GetDataGridRows(listViewUse80);
                    foreach (DataGridRow single_row in row_list)
                    {
                        if (single_row.IsSelected == true)
                        {

                        }
                    }
                }
                else
                {

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion


    }
}
