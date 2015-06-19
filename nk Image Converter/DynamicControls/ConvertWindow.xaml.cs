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
using System.Windows.Shapes;

namespace nk_Image_Converter.DynamicControls
{
    /// <summary>
    /// Interaction logic for ConvertWindow.xaml
    /// </summary>
    public partial class ConvertWindow : Window
    {
        private Lib.FileFetcher fetcher;
        private List<Tuple<string, string, string>> files;
        private BackgroundWorker worker;
        private string type;
        public ConvertWindow(string type)
        {
            InitializeComponent();
            this.fetcher = ((MainWindow)Application.Current.MainWindow).fetcher;
            this.files = this.fetcher.getFiles();
            this.type = type;
            worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;
            worker.DoWork += worker_DoWork;
            worker.ProgressChanged += worker_ProgressChanged;
            worker.RunWorkerCompleted += worker_RunWorkerCompleted;
            worker.WorkerSupportsCancellation = true;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            worker.RunWorkerAsync();
        }

        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            foreach (var file in files)
            {
                if (worker.CancellationPending)
                {
                    e.Cancel = true;
                    break;
                }
                else
                {
                    if (type == "convert")
                    {

                    }
                    else if (type == "resize")
                    {

                    }
                    else
                    {
                        break;
                    }
                }
            }
        }

        private void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
        }

        private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
        }

    }
}
