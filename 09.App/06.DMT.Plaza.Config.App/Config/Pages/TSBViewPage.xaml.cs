#region Using

using System;
using System.Collections.Generic;
using System.ComponentModel;
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

using DMT.Models;
using DMT.Services;
using System.Collections.ObjectModel;
using NLib.Reflection;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;
using System.Runtime.InteropServices;

#endregion

namespace DMT.Config.Pages
{
    /// <summary>
    /// Interaction logic for TSBViewPage.xaml
    /// </summary>
    public partial class TSBViewPage : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public TSBViewPage()
        {
            InitializeComponent();
        }

        #endregion

        private LocalOperations ops = LocalServiceOperations.Instance.Plaza;
        private List<TSBItem> items = new List<TSBItem>();

        #region Loaded/Unloaded

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            RefreshTree();
        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
        }

        #endregion

        private void RefreshTree()
        {
            tree.ItemsSource = null;

            items.Clear();
            var tsbs = ops.TSB.GetTSBs().Value();
            if (null == tsbs)
                return;

            tsbs.ForEach(tsb =>
            {
                TSBItem item = tsb.CloneTo<TSBItem>();
                items.Add(item);

                var plazas = ops.TSB.GetTSBPlazas(item).Value();
                if (null != plazas)
                {
                    plazas.ForEach(plaza =>
                    {
                        PlazaItem pItem = plaza.CloneTo<PlazaItem>();
                        item.Plazas.Add(pItem);

                        var lanes = ops.TSB.GetPlazaLanes(plaza).Value();
                        if (null != lanes)
                        {
                            lanes.ForEach(lane =>
                            {
                                var lItem = lane.CloneTo<LaneItem>();
                                pItem.Lanes.Add(lItem);
                            });
                        }
                    });
                }
            });

            tree.ItemsSource = items;
        }

        #region Button Handler

        private void cmdSetActiveTSB_Click(object sender, RoutedEventArgs e)
        {
            // Set Active.
            var item = (sender as Button).DataContext;
            if (null != item && item is TSBItem)
            {
                ops.TSB.SetActive(item as TSB);
                RefreshTree();
            }
        }

        #endregion

        #region TreeView Handler

        private void tree_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            pgrid.SelectedObject = e.NewValue;
        }

        #endregion
    }

    public class TSBItem : TSB
    {
        public TSBItem()
        {
            Plazas = new ObservableCollection<PlazaItem>();
        }
        [ReadOnly(true)]
        public string IsActive 
        {
            get { return (this.Active) ? "[A]" : ""; }
            set { }
        }
        [Browsable(false)]
        public ObservableCollection<PlazaItem> Plazas { get; set; }
    }

    public class PlazaItem : Plaza
    {
        public PlazaItem()
        {
            Lanes = new ObservableCollection<LaneItem>();
        }
        public ObservableCollection<LaneItem> Lanes { get; set; }
    }
    [Browsable(false)]
    public class LaneItem : Lane { }
}
