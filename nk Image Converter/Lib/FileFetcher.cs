using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace nk_Image_Converter.Lib
{
    public class FileFetcher
    {
        private OpenFileDialog _fileDialog;
        private FolderBrowserDialog _folderDialog;
        private List<Tuple<string, string, string>> files;
        private List<Tuple<string, string, string>> newfiles;

        public FileFetcher()
        {
            files = new List<Tuple<string, string, string>>();
            _folderDialog = new FolderBrowserDialog();
            _fileDialog = new OpenFileDialog();
            newfiles = new List<Tuple<string, string, string>>();
        }

        public string fetch(string type,System.Windows.Controls.TextBox box)
        {
            string ret = "";
            if (type == "file")
            {
                _fileDialog.Filter = "png files|*.png|jpg files|*.jpg|bmp files|*.bmp|gif files|*.gif" +
                    "|wmf files|*.wmf|emf files|*.emf|tiff files|*.tif;*.tiff|icon files|*.ico" +
                    "|All Graphic Types|*.png;*.jpg;*.bmp;*.gif;*.wmf;*.emf;*.tif;*.tiff;*.ico";
                _fileDialog.FilterIndex = 1;
                _fileDialog.Multiselect = true;
                _fileDialog.Title = "Select Multiple Files";
                newfiles = new List<Tuple<string, string, string>>();
                if (_fileDialog.ShowDialog() == DialogResult.OK)
                {
                    ret = "*\\*";
                    foreach (string file in _fileDialog.FileNames)
                    {
                        extractImageFiles(file);
                    }
                }
            }
            else if (type == "folder")
            {
                _folderDialog.Description = "Select Folder";
                if (_folderDialog.ShowDialog() == DialogResult.OK)
                {
                    ret = box.Text != "" || box.Text != null ? _folderDialog.SelectedPath : "*\\*";
                    foreach (string file in Directory.GetFiles(_folderDialog.SelectedPath))
                    {
                        //string f = file.ToLower();
                        extractImageFiles(file);
                    }
                }
            }
            return ret;
        }

        /*
            Extract Image files if user has selected folder from folder dialog
            and stores in an array
        */
        private void extractImageFiles(string file)
        {
            string filename = "", extension = "";
            if (file.EndsWith(".png") || file.EndsWith(".PNG") || file.EndsWith(".jpg") || file.EndsWith(".JPG") ||
                file.EndsWith(".bmp") || file.EndsWith(".BMP") || file.EndsWith(".gif") || file.EndsWith(".GIF") ||
                file.EndsWith(".wmf") || file.EndsWith(".WMF") || file.EndsWith(".tif") || file.EndsWith(".TIF") ||
                file.EndsWith(".ico") || file.EndsWith(".ICO") || file.EndsWith(".emf") || file.EndsWith(".EMF") ||
                file.EndsWith(".tiff") || file.EndsWith(".TIFF") ||file.EndsWith(".jpeg") || file.EndsWith(".JPEG"))
            {



                extractFilenameAndExtension(file, out filename, out extension);

                /*if (this.files.Count < 1)
                {
                    files.Add(new Tuple<string, string, string>(file, filename, extension));
                    newfiles.Add(new Tuple<string, string, string>(file, filename, extension));
                }*/
                if (!this.files.Exists(f => f.Item1 == file))
                {
                    files.Add(new Tuple<string, string, string>(file, filename, extension));
                    newfiles.Add(new Tuple<string, string, string>(file, filename, extension));
                }


                
            }
        }


        /*
            This method extracts filename and extension from a complete path
            and adds the values to list (2nd and 3rd parameter)
        */
        private void extractFilenameAndExtension(string filePath, out string filename, out string extension)
        {
            string concatName = null;
            for (int i = 0; i < ((filePath.Split('\\'))[(filePath.Split('\\')).Length - 1]).Split('.').Length - 1; i++)
            {
                concatName += ((filePath.Split('\\'))[(filePath.Split('\\')).Length - 1]).Split('.')[i];
                if (i < ((filePath.Split('\\'))[(filePath.Split('\\')).Length - 1]).Split('.').Length - 2)
                    concatName += ".";
            }
            filename = concatName;

            extension = "." + ((filePath.Split('\\'))[(filePath.Split('\\')).Length - 1].Split('.'))[((filePath.Split('\\'))[(filePath.Split('\\')).Length - 1].Split('.')).Length - 1];
        }

        public bool isEmpty()
        {
            return (this.files.Count == 0) ? true : false;
        }

        public bool isNewEmpty()
        {
            return (this.newfiles == null || this.newfiles.Count == 0) ? true : false;
        }

        public List<Tuple<string, string, string>> getFiles()
        {
            return this.files;
        }

        public List<Tuple<string, string, string>> getNewFiles()
        {
            return this.newfiles;
        }
    }
}
