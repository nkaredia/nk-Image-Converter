using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
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
    public partial class ProgressBarWindow : Window
    {
        private BackgroundWorker backgroundWorker;
        private Lib.FileFetcher fetcher;
        private List<Tuple<string, string, string>> newfiles;
        private double _progress = 0;
        private double _progressCounter = 0;
        private ImageGridContainer container;
        int row, col;

        public ProgressBarWindow(string message,ref ImageGridContainer container,ref int row,ref int col)
        {
            InitializeComponent();
            this.container = container;
            this.row = row;
            this.col = col;
            InitializeBackgroundWorker();
            this.Message.Content = message;
            this.Left = Application.Current.MainWindow.Left + 250;
            this.Top = Application.Current.MainWindow.Top + 200;
            fetcher = ((MainWindow)Application.Current.MainWindow).fetcher;
            newfiles = this.fetcher.getNewFiles();
        }

        

        private void InitializeBackgroundWorker()
        {
            backgroundWorker = new BackgroundWorker();
            backgroundWorker.WorkerReportsProgress = true;
            backgroundWorker.ProgressChanged += backgroundWorker_ProgressChanged;
            backgroundWorker.DoWork += backgroundWorker_DoWork;
            backgroundWorker.RunWorkerCompleted += backgroundWorker_RunWorkerCompleted;
            
            
        }


        private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            while (true)
                if (this.container.ImageGrid.Children.Count == this.fetcher.getFiles().Count)
                    break;
            this.Close();
        }

        private void backgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.Progressbar.Value = (double)e.UserState;
        }

        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            this._progress = 0;
            addRowstoGrid();
            _progressCounter = (100 / this.newfiles.Count);

            for (int i = 0; i < this.newfiles.Count; i++)
            {
                this.Dispatcher.Invoke((Action)(() =>
                {
                    DynamicControls.ImageButton b = new DynamicControls.ImageButton();
                    b.set(this.newfiles.ElementAt(i));
                    this.container.Height = (this.row + 1) * 125;
                    this.container.ImageGrid.Height = this.container.Height;
                    Grid.SetRow(b, this.row);
                    Grid.SetColumn(b, col++);
                    if ((this.col != 0 && this.col % 5 == 0) || this.col > 5)
                    {
                        this.col = 0;
                        ++this.row;
                    }
                    this.container.ImageGrid.Children.Add(b);

                    this._progress += ((_progressCounter + _progress) <= 100) ? _progressCounter : 100;
                }));
                    
                (sender as BackgroundWorker).ReportProgress((int)this._progress, this._progress);
            }

        }

        private void addRowstoGrid()
        {
            this.Dispatcher.Invoke((Action)(() =>
            {
                int rowCount = this.container.ImageGrid.RowDefinitions.Count;
                int fileCount = this.fetcher.getFiles().Count;
                for (int i = 0; i < fileCount + rowCount / 5; i++)
                {
                    RowDefinition rd = new RowDefinition();
                    rd.Height = new GridLength(125);
                    this.container.ImageGrid.RowDefinitions.Add(rd);
                }
            }));
            
        }


        public void showDialog()
        {
            ShowDialog();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            backgroundWorker.RunWorkerAsync();
        }

       /* private double _progressCounter = 0;
        private double _progress = 0;
        private BackgroundWorker backgroundWorker;
        private Lib.FileFetcher fetcher;
        private List<Tuple<string, string, string>> files;

        public ProgressBarWindow(string message)
        {
            InitializeComponent();
            backgroundWorker = new BackgroundWorker();
            this.Message.Content = message;
            this.Left = Application.Current.MainWindow.Left + 250;
            this.Top = Application.Current.MainWindow.Top + 200;
            backgroundWorker.WorkerReportsProgress = true;
            backgroundWorker.ProgressChanged += backgroundWorker_ProgressChanged;
            backgroundWorker.DoWork += backgroundWorker_DoWork;
            backgroundWorker.RunWorkerCompleted += backgroundWorker_RunWorkerCompleted;
            this.Progressbar.Value = 0;
            this.fetcher = ((MainWindow)Application.Current.MainWindow).fetcher;
            this.files = ((MainWindow)Application.Current.MainWindow).fetcher.getNewFiles();
                        

        }

        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            this.Dispatcher.Invoke((Action)(() =>
            {
                this._progress = 0;

                addRowstoGrid();
                _progressCounter = (100 / this.files.Count);
            }));


            for (int i = 0; i < this.files.Count; i++)
            {
                this.Dispatcher.Invoke((Action)(() =>
                {
                    DynamicControls.ImageButton b = new DynamicControls.ImageButton();
                    b.set(this.files.ElementAt(i));
                    ((MainWindow)Application.Current.MainWindow)._imageContainer.Height = ((((MainWindow)Application.Current.MainWindow).row + 1) * 125);
                    ((MainWindow)Application.Current.MainWindow)._imageContainer.ImageGrid.Height = ((MainWindow)Application.Current.MainWindow)._imageContainer.Height;
                    Grid.SetRow(b, ((MainWindow)Application.Current.MainWindow).row);
                    Grid.SetColumn(b, ((MainWindow)Application.Current.MainWindow).col++);
                    if ((((MainWindow)Application.Current.MainWindow).col != 0 && (((MainWindow)Application.Current.MainWindow).col) % 5 == 0) || (((MainWindow)Application.Current.MainWindow).col) > 5)
                    {
                        ((MainWindow)Application.Current.MainWindow).col = 0;
                        ++((MainWindow)Application.Current.MainWindow).row;
                    }
                    ((MainWindow)Application.Current.MainWindow)._imageContainer.ImageGrid.Children.Add(b);

                    this._progress += ((_progressCounter + _progress) <= 100) ? _progressCounter : 100;
                }));
                (sender as BackgroundWorker).ReportProgress((int)this._progress, this._progress);
            }

        }

        private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            while (true)
                if (((MainWindow)Application.Current.MainWindow)._imageContainer.ImageGrid.Children.Count == this.fetcher.getFiles().Count)
                    break;
            this.Close();
        }

        private void backgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.Progressbar.Value = (double)e.UserState;
        }
        public void showDialog()
        {
            ShowDialog();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            backgroundWorker.RunWorkerAsync();
        }

        private void addRowstoGrid()
        {
            int rowCount = ((MainWindow)Application.Current.MainWindow)._imageContainer.ImageGrid.RowDefinitions.Count;
            for (int i = 0; i < this.fetcher.getFiles().Count + rowCount / 5; i++)
            {
                RowDefinition rd = new RowDefinition();
                rd.Height = new GridLength(125);
                ((MainWindow)Application.Current.MainWindow)._imageContainer.ImageGrid.RowDefinitions.Add(rd);
            }
        }*/
    }
}
