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
        private Lib.FileFetcher fileFetcher;
        private ImageGridContainer container;
        private List<Tuple<string, string, string>> newfiles;
        private int row, col;
        private double _progress, _progressCounter;

        public ProgressBarWindow(string message, ref ImageGridContainer container,ref Lib.FileFetcher fetcher)
        {
            InitializeComponent();
            InitializeBackgroundWorker();
            this.fileFetcher = fetcher;
            this.newfiles = this.fileFetcher.getNewFiles();
            this.container = container;
            this.row = ((MainWindow)Application.Current.MainWindow).row;
            this.col = ((MainWindow)Application.Current.MainWindow).col;
            this._progress = _progressCounter = 0;
            this.Message.Content = message;
            this.Left = Application.Current.MainWindow.Left + 250;
            this.Top = Application.Current.MainWindow.Top + 200;
        }

        private void InitializeBackgroundWorker()
        {
            backgroundWorker = new BackgroundWorker();
            backgroundWorker.WorkerReportsProgress = true;
            backgroundWorker.ProgressChanged += backgroundWorker_ProgressChanged;
            backgroundWorker.DoWork += backgroundWorker_DoWork;
            backgroundWorker.RunWorkerCompleted += backgroundWorker_RunWorkerCompleted;    
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.backgroundWorker.RunWorkerAsync();
        }

        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            this._progress = 0;
            addRowdefinition();
            _progressCounter = (100 / this.newfiles.Count);
            DynamicControls.ImageButton _ImageButton;

            for (int i = 0; i < this.newfiles.Count; i++)
            {
                this.Dispatcher.Invoke((Action)(() => {
                    _ImageButton = new DynamicControls.ImageButton();
                    _ImageButton.set(this.newfiles.ElementAt(i));
                    if (this.col != 0 && Math.Ceiling((double)(this.col % (double)5)) == 0)
                    {
                        ++((MainWindow)Application.Current.MainWindow).row;
                        ++this.row;
                        this.col = 0;
                        ((MainWindow)Application.Current.MainWindow).col = 0;
                    }
                    Grid.SetRow(_ImageButton, this.row);
                    Grid.SetColumn(_ImageButton, ((MainWindow)Application.Current.MainWindow).col++);
                    this.col++;
                    this.container.ImageGrid.Children.Add(_ImageButton);

                    this._progress += ((_progressCounter + _progress) <= 100) ? _progressCounter : 100;
                }));
                (sender as BackgroundWorker).ReportProgress((int)this._progress, this._progress);
            }
        }

        private void backgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.Progressbar.Value = (double)e.UserState;
        }
        private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            while (true)
                if (this.container.ImageGrid.Children.Count == this.fileFetcher.getFiles().Count)
                    break;
            this.Close();
        }

        private void addRowdefinition()
        {
            this.Dispatcher.Invoke((Action)(() => {
                int totalFiles = this.fileFetcher.getFiles().Count;
                double tempRows = totalFiles / (double)5;
                int totalRows = (int)Math.Ceiling(tempRows);
                this.container.Height = totalRows * 125;
                this.container.ImageGrid.Height = this.container.Height;
                if (this.container.ImageGrid.RowDefinitions.Count < totalRows)
                {
                    for (int i = this.container.ImageGrid.RowDefinitions.Count; i < totalRows; i++)
                    {
                        RowDefinition rd = new RowDefinition();
                        rd.Height = new GridLength(125);
                        this.container.ImageGrid.RowDefinitions.Add(rd);
                    }
                }
            }));
            
        }

        public void showDialog()
        {
            ShowDialog();
        }


 /*       private BackgroundWorker backgroundWorker;
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
                if (fileCount > 5)
                {
                    for (int i = 0; i < fileCount + rowCount / 5; i++)
                    {
                        RowDefinition rd = new RowDefinition();
                        rd.Height = new GridLength(125);
                        this.container.ImageGrid.RowDefinitions.Add(rd);
                    }
                }
                else
                {
                    if (this.container.ImageGrid.RowDefinitions.Count < 1)
                    {
                        RowDefinition rd = new RowDefinition();
                        rd.Height = new GridLength(125);
                        this.container.ImageGrid.RowDefinitions.Add(rd);
                    }
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
        }*/
    }
}
