#region Using

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
using System.Windows.Navigation;
using System.Windows.Shapes;

using DMT.Services;

#endregion

namespace DMT.TAxTOD.Pages
{
    /// <summary>
    /// Interaction logic for CouponViewPage.xaml
    /// </summary>
    public partial class CouponViewPage : UserControl
    {
        #region Constructor

        public CouponViewPage()
        {
            InitializeComponent();
        }

        #endregion

        //private TAxTODOperations ops = TODxTAServiceOperations.Instance.Plaza;
        private LocalOperations ops = LocalServiceOperations.Instance.Plaza;
        private TSBCouponManager manager = new TSBCouponManager();

        #region Loaded/Unloaded

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            LoadCoupons();
        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {

        }

        #endregion

        #region Private Methods

        private void LoadCoupons()
        {
            lvCoupons.ItemsSource = null;
            manager.Sync();
            manager.Refresh();
            /*
            var coupons = ops.Coupons.GetTAServerCouponTransactions("311", null, null, null).Value();
            */
            lvCoupons.ItemsSource = manager.Coupons;
        }

        #endregion
    }
}
