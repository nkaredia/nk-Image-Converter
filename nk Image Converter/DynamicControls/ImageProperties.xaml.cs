using System;
using System.Collections.Generic;
using System.Drawing;
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

namespace nk_Image_Converter.DynamicControls
{
    /// <summary>
    /// Interaction logic for ImageProperties.xaml
    /// </summary>
    public partial class ImageProperties : Window
    {
        public ImageProperties(Tuple<string, string, string> file,ImageSource source)
        {
            InitializeComponent();
            this.Left = Application.Current.MainWindow.Left + 350;
            this.Top = Application.Current.MainWindow.Top + 50;
            this._Image.Source = source;
            this.Path.Text = file.Item1;
            this.Path.ToolTip = file.Item1;
            this.Name.Text = file.Item2;
            this.Name.ToolTip = file.Item2;
            this.Type.Text = file.Item3;
            this.Width.Text = new Bitmap(file.Item1).Width.ToString();
            this.Height.Text = new Bitmap(file.Item1).Height.ToString();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}
