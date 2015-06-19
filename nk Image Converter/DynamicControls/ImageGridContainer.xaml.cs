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

namespace nk_Image_Converter.DynamicControls
{
    /// <summary>
    /// Interaction logic for ImageGridContainer.xaml
    /// </summary>
    public partial class ImageGridContainer : UserControl
    {
        private List<ImageButton> _PreviewImages;
        public ImageGridContainer()
        {
            InitializeComponent();
            this._PreviewImages = new List<ImageButton>();
        }

        public void set()
        {
            RowDefinition rd = new RowDefinition();
            rd.Height = new GridLength(125);
            this.ImageGrid.RowDefinitions.Add(rd);
        }

    }
}
