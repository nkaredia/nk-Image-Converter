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
using System.Drawing;

namespace nk_Image_Converter.DynamicControls
{
    /// <summary>
    /// Interaction logic for ImageButton.xaml
    /// </summary>
    public partial class ImageButton : UserControl
    {
        private BitmapImage _img = new BitmapImage();
        private Tuple<string, string, string> _file;
        public ImageButton()
        {
            InitializeComponent();
        }

        public void set(Tuple<string,string,string> file)
        {
            this._file = file;
            _img.BeginInit();
            _img.DecodePixelWidth = 135;
            _img.UriSource = new Uri(file.Item1);
            _img.EndInit();
            this.ImageNameTextBlock.Text = file.Item2;
            this.PreviewImage.Source = _img;
        }

        private void ImageHolderButton_Click(object sender, RoutedEventArgs e)
        {
            new DynamicControls.ImageProperties(this._file, this._img).ShowDialog();
        }

        public Tuple<string, string, string> getFile()
        {
            return _file;
        }
    }
}
