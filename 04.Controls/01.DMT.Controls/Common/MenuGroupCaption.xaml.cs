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

#endregion

namespace DMT.Controls
{
    /// <summary>
    /// Interaction logic for MenuGroupCaption.xaml
    /// </summary>
    public partial class MenuGroupCaption : UserControl
    {
        public MenuGroupCaption()
        {
            InitializeComponent();
        }

        #region Public Properties

        /// <summary>
        /// Gets or sets caption text.
        /// </summary>
        public string Caption
        {
            get { return (string)GetValue(CaptionProperty); }
            set { SetValue(CaptionProperty, value); }
        }

        #endregion

        #region Public Static Properties

        /// <summary>
        /// sing a DependencyProperty as the backing store for Caption Property.
        /// </summary>
        public static readonly DependencyProperty CaptionProperty = DependencyProperty.Register("Caption", typeof(string), typeof(MenuGroupCaption), new UIPropertyMetadata(""));

        #endregion
    }
}
