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
using System.Windows.Shapes;

using System.Collections;



namespace DMT.TA.Windows.Coupon
{
    /// <summary>
    /// Interaction logic for CouponEditWindow.xaml
    /// </summary>
    public partial class CouponEditWindow : Window
    {
        public CouponEditWindow()
        {
            InitializeComponent();
        }

        #region Button Handlers

        private void cmdSaveExchange_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void cmdCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        #endregion

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
                MessageBox.Show(ex.Message.ToString(), "DMT - Toll Admin", MessageBoxButton.OK, MessageBoxImage.Error);
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
                MessageBox.Show(ex.Message.ToString(), "DMT - Toll Admin", MessageBoxButton.OK, MessageBoxImage.Error);
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
                MessageBox.Show(ex.Message.ToString(), "DMT - Toll Admin", MessageBoxButton.OK, MessageBoxImage.Error);
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
                MessageBox.Show(ex.Message.ToString(), "DMT - Toll Admin", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    }
}
