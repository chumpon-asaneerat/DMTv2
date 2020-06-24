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

namespace DMT.TOD.Controls.Revenue.View
{
    /// <summary>
    /// Interaction logic for EMVQRCodeView.xaml
    /// </summary>
    public partial class EMVQRCodeView : UserControl
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public EMVQRCodeView()
        {
            InitializeComponent();
        }

        #endregion

        /*
        public void Setup(List<Models.EMVQRCode> lanes)
        {
            listView.ItemsSource = lanes;
        }
        */
    }
}
