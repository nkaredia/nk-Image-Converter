using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace nk_Image_Converter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private Resize resize = new Resize();
        private DynamicControls.ProgressBarWindow _progressBarWindow;
        public DynamicControls.ImageGridContainer _imageContainer;
        public List<DynamicControls.ImageButton> _imageButton;
        public Lib.FileFetcher fetcher;
        public int row = 0, col = 0;
        private ImageFormat type;
        private Lib.ConvertImage convert;

        public MainWindow()
        {
            InitializeComponent();
            RadioButtonConstructor();
            NumericUpDownConstructor();
            this.SavePathTextBox.Text = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures) + "\\nkImageCoverter\\";
            fetcher = new Lib.FileFetcher();
            _imageButton = new List<DynamicControls.ImageButton>();
            _imageContainer = new DynamicControls.ImageGridContainer();
            RowDefinition rd = new RowDefinition();
            rd.Height = new GridLength(125);
            this._imageContainer.ImageGrid.RowDefinitions.Add(rd);
            this.ImageContainerSV.Content = _imageContainer;
            this.AllSelectionGrid.Visibility = System.Windows.Visibility.Hidden;
            this.convert = new Lib.ConvertImage(this.SavePathTextBox.Text);
            type = ImageFormat.Png;
            for (int i = 0; i < 8; i++)
            {
                (RadioButtonStackPanel.Children[i] as RadioButton).Checked += MainWindow_RadioButton_Checked;
            }
        }

        private void MainWindow_RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            var rb = sender as RadioButton;
            switch (rb.Content.ToString())
            {
                case "PNG":
                    type = ImageFormat.Png;
                    break;
                case "JPG":
                    type = ImageFormat.Jpeg;
                    break;
                case "GIF":
                    type = ImageFormat.Gif;
                    break;
                case "BMP":
                    type = ImageFormat.Bmp;
                    break;
                case "ICO":
                    type = ImageFormat.Icon;
                    break;
                case "TIF/TIFF":
                    type = ImageFormat.Tiff;
                    break;
                case "WMF":
                    type = ImageFormat.Wmf;
                    break;
                case "EMF":
                    type = ImageFormat.Emf;
                    break;
            }
            
        }

        private void RadioButtonConstructor()
        {
            FileRadioButton.IsChecked = true;
            RBPng.IsChecked = true;
        }

        private void NumericUpDownConstructor()
        {
            NumericUpDownWidth.Text = Convert.ToString(resize.Width);
            NumericUpDownHeight.Text = Convert.ToString(resize.Height);
        }

        #region File_Folder_RadioButtons
        private void FileRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            FolderRadioButton.IsChecked = false;
        }

        private void FolderRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            FileRadioButton.IsChecked = false;
        }
        #endregion

        #region Numeric_Up_Down
        private void NumericUpButtonW_Click(object sender, RoutedEventArgs e)
        {
            NumericUpDownWidth.Text = (int.Parse(NumericUpDownWidth.Text) < Resize.MAX) ? Convert.ToString((int.Parse(NumericUpDownWidth.Text)) + 1) : NumericUpDownWidth.Text;
            resize.Width = int.Parse(NumericUpDownWidth.Text);
        }



        private void NumericDownButtonW_Click(object sender, RoutedEventArgs e)
        {
            NumericUpDownWidth.Text = (int.Parse(NumericUpDownWidth.Text) > Resize.MIN) ? Convert.ToString((int.Parse(NumericUpDownWidth.Text)) - 1) : NumericUpDownWidth.Text;
            resize.Width = int.Parse(NumericUpDownWidth.Text);
        }




        private void NumericUpDownWidth_LostFocus(object sender, RoutedEventArgs e)
        {
            if (NumericUpDownWidth.Text == "") NumericUpDownWidth.Text = "64";
            else if (int.Parse(NumericUpDownWidth.Text) < Resize.MIN) NumericUpDownWidth.Text = Resize.MIN.ToString();
            else if (int.Parse(NumericUpDownWidth.Text) > Resize.MAX) NumericUpDownWidth.Text = Resize.MAX.ToString();
            resize.Width = int.Parse(NumericUpDownWidth.Text);
        }

        private void NumericUpDownWidth_TextChanged(object sender, TextChangedEventArgs e)
        {
            int i;
            if (NumericUpDownWidth.Text != "")
            {
                if (!int.TryParse(NumericUpDownWidth.Text, out i))
                {
                    string s = NumericUpDownWidth.Text;
                    removeAlphabet(ref s);
                    NumericUpDownWidth.Text = s;
                }
                resize.Width = int.Parse(NumericUpDownWidth.Text);
            }

        }



        private void NumericUpButtonH_Click(object sender, RoutedEventArgs e)
        {
            NumericUpDownHeight.Text = (int.Parse(NumericUpDownHeight.Text) < Resize.MAX) ? Convert.ToString((int.Parse(NumericUpDownHeight.Text)) + 1) : NumericUpDownHeight.Text;
            resize.Height = int.Parse(NumericUpDownHeight.Text);
        }

        private void NumericDownButtonH_Click(object sender, RoutedEventArgs e)
        {
            NumericUpDownHeight.Text = (int.Parse(NumericUpDownHeight.Text) > Resize.MIN) ? Convert.ToString((int.Parse(NumericUpDownHeight.Text)) - 1) : NumericUpDownHeight.Text;
            resize.Height = int.Parse(NumericUpDownHeight.Text);
        }


        private void NumericUpDownHeight_TextChanged(object sender, TextChangedEventArgs e)
        {
            int i;
            if (NumericUpDownHeight.Text != "")
            {
                if (!int.TryParse(NumericUpDownHeight.Text, out i))
                {
                    string s = NumericUpDownHeight.Text;
                    removeAlphabet(ref s);
                    NumericUpDownHeight.Text = s;
                }
                resize.Height = int.Parse(NumericUpDownHeight.Text);
            }
        }

        private void NumericUpDownHeight_LostFocus(object sender, RoutedEventArgs e)
        {
            if (NumericUpDownHeight.Text == "") NumericUpDownHeight.Text = "64";
            else if (int.Parse(NumericUpDownHeight.Text) < Resize.MIN) NumericUpDownHeight.Text = Resize.MIN.ToString();
            else if (int.Parse(NumericUpDownHeight.Text) > Resize.MAX) NumericUpDownHeight.Text = Resize.MAX.ToString();
            resize.Height = int.Parse(NumericUpDownHeight.Text);
        }

        private void removeAlphabet(ref string s)
        {
            for (int i = 0; i < s.Length; i++)
                if (Char.IsLetter(s[i]))
                {
                    s = s.Remove(i, 1);
                    break;
                }

        }

        #endregion

        #region FormBehaviour

        private IntPtr hWnd;

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            //this.WindowStyle = WindowStyle.SingleBorderWindow;
            //this.WindowState = WindowState.Minimized;
            SendMessage(hWnd, ApiCodes.WM_SYSCOMMAND, new IntPtr(ApiCodes.SC_MINIMIZE), IntPtr.Zero);
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            Dispatcher.BeginInvoke(DispatcherPriority.ApplicationIdle, new Action(() => WindowStyle = WindowStyle.None));
        }

        private void HeaderGrid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                DragMove();
            }
        }

        internal class ApiCodes
        {
            public const int SC_RESTORE = 0xF120;
            public const int SC_MINIMIZE = 0xF020;
            public const int WM_SYSCOMMAND = 0x0112;
        }


        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int wMsg, IntPtr wParam, IntPtr lParam);

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            hWnd = new WindowInteropHelper(this).Handle;
            HwndSource.FromHwnd(hWnd).AddHook(WindowProc);
        }

        private IntPtr WindowProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            if (msg == ApiCodes.WM_SYSCOMMAND)
            {
                if (wParam.ToInt32() == ApiCodes.SC_MINIMIZE)
                {
                    WindowStyle = WindowStyle.SingleBorderWindow;
                    WindowState = WindowState.Minimized;
                    handled = true;
                }
                else if (wParam.ToInt32() == ApiCodes.SC_RESTORE)
                {
                    WindowState = WindowState.Normal;
                    WindowStyle = WindowStyle.None;
                    handled = true;
                }
            }
            return IntPtr.Zero;
        }

        #endregion

        private void ConvertResizeSelect_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ImageTypeSelectionGrid != null && ResizeImageGrid != null)
            {
                if (ConvertResizeSelect.SelectedIndex == 0)
                {
                    ImageTypeSelectionGrid.Visibility = System.Windows.Visibility.Visible;
                    ResizeImageGrid.Visibility = System.Windows.Visibility.Hidden;
                }
                else if (ConvertResizeSelect.SelectedIndex == 1)
                {
                    ImageTypeSelectionGrid.Visibility = System.Windows.Visibility.Hidden;
                    ResizeImageGrid.Visibility = System.Windows.Visibility.Visible;
                }
            }
        }

        public void AddButton_Click(object sender, RoutedEventArgs e)
        {
            this.PathTextBox.Text = fetcher.fetch(((bool)FileRadioButton.IsChecked) ? "file" : "folder", this.PathTextBox);
            if (!fetcher.isNewEmpty())
            {
                _progressBarWindow = new DynamicControls.ProgressBarWindow("Please Wait...",ref _imageContainer,ref fetcher);
                _progressBarWindow.showDialog();
            }
        }

        private void ConvertButton_Click(object sender, RoutedEventArgs e)
        {
            if (ConvertResizeSelect.SelectedIndex == 0)
            {
                this.convert.convert(this._imageContainer, type);
            }
            else
            {
                this.convert.resize(this._imageContainer, int.Parse(NumericUpDownWidth.Text), int.Parse(NumericUpDownHeight.Text));
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog saveDialog = new System.Windows.Forms.FolderBrowserDialog();
            saveDialog.Description = "Select Folder";
            if (saveDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.SavePathTextBox.Text = saveDialog.SelectedPath;
            }
        }

        private void ImageContainerSV_MouseEnter(object sender, MouseEventArgs e)
        {
            if (this.fetcher != null && !this.fetcher.isEmpty())
            {
                this.AllSelectionGrid.Visibility = System.Windows.Visibility.Visible;
            }
        }

        private void ImageContainerSV_MouseLeave(object sender, MouseEventArgs e)
        {
            this.AllSelectionGrid.Visibility = System.Windows.Visibility.Hidden;
        }

        private void AllSelectionGrid_MouseEnter(object sender, MouseEventArgs e)
        {
            if (this.fetcher != null && !this.fetcher.isEmpty())
            {
                this.AllSelectionGrid.Visibility = System.Windows.Visibility.Visible;
            }
        }

        private void AllSelectionGrid_MouseLeave(object sender, MouseEventArgs e)
        {
            this.AllSelectionGrid.Visibility = System.Windows.Visibility.Hidden;
        }

        private void SelectAllCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (this.fetcher != null &&  !this.fetcher.isEmpty())
            {
                foreach (var child in this._imageContainer.ImageGrid.Children)
                {
                    (child as DynamicControls.ImageButton).ImageEnableDisableCheckBox.IsChecked = true;
                }
            }
        }

        private void UnSelectAllCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (this.fetcher != null && !this.fetcher.isEmpty())
            {
                foreach (var child in this._imageContainer.ImageGrid.Children)
                {
                    (child as DynamicControls.ImageButton).ImageEnableDisableCheckBox.IsChecked = false;
                }
            }
        }

    }
}
