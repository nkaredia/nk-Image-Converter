using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nk_Image_Converter.Lib
{
    public class ConvertImage
    {
        //private const string specialPath = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures) + "\\nkImageCoverter";
        private string savePath;

        public ConvertImage(string path)
        {
            savePath = path;
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        public void convert(DynamicControls.ImageGridContainer container,ImageFormat type)
        {
            int count = container.ImageGrid.Children.Count;
            Bitmap image;
            Tuple<string,string,string> file;
            for (int i = 0; i < count; i++)
            {
                var _controlImage = container.ImageGrid.Children[i] as DynamicControls.ImageButton;
                if((bool)_controlImage.ImageEnableDisableCheckBox.IsChecked)
                {
                    file = _controlImage.getFile();
                    image = new Bitmap(file.Item1);
                    image.Save(savePath + "\\" + rename(file.Item2, "." + type.ToString()) + "." + type.ToString(), type);
                }
            }
        }

        public void resize(DynamicControls.ImageGridContainer container,int width, int height)
        {
            int count = container.ImageGrid.Children.Count;
            Bitmap image;
            DynamicControls.ImageButton _controlImage;
            Tuple<string, string, string> file;
            for (int i = 0; i < count; i++)
            {
                _controlImage = container.ImageGrid.Children[i] as DynamicControls.ImageButton;
                if ((bool)_controlImage.ImageEnableDisableCheckBox.IsChecked)
                {
                    file = _controlImage.getFile();
                    image = new Bitmap(new Bitmap(file.Item1),new Size(width,height));
                    image.Save(savePath + "\\" + rename(file.Item2, file.Item3) + file.Item3);
                }
            }
        }



        public string rename(string file, string ext)
        {
            int count = 1;
            string newFile = file;

            while (File.Exists(savePath + "\\" + newFile + ext))
            {
                newFile = string.Format("{0}({1})", newFile, count++);
            }
            return newFile;
        }


  /*      private string specialPath = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures) + "\\nkImageCoverter";
        private string savePath;

        public string rename(string file, string ext)
        {
            int count = 1;
            string newFile = file;

            while (File.Exists(savePath + "\\" + newFile + ext))
            {
                newFile = string.Format("{0}({1})", newFile, count++);
            }
            return newFile;
        }



        public ConvertImage(string path)
        {
            this.savePath = path;
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        public void convertImage(Tuple<string, string, string> file, ImageFormat type)
        {

        }


        public void convertImageTo(Tuple<string,string,string> file,ImageFormat type)
        {
            new Bitmap(file.Item1).Save(savePath + "\\" + rename(file.Item2) + file.Item3, type);
            switch (ext)
            {
                case ".png":
                    image.Save(specialPath + "\\" + rename(filename, ext) + ext, ImageFormat.Png);
                    break;
                case ".jpg":
                    image.Save(specialPath + "\\" + rename(filename, ext) + ext, ImageFormat.Jpeg);
                    break;
                case ".bmp":
                    image.Save(specialPath + "\\" + rename(filename, ext) + ext, ImageFormat.Bmp);
                    break;
                case ".gif":
                    image.Save(specialPath + "\\" + rename(filename, ext) + ext, ImageFormat.Gif);
                    break;
                case ".ico":
                    image.Save(specialPath + "\\" + rename(filename, ext) + ext, ImageFormat.Icon);
                    break;
                case ".tif":
                    image.Save(specialPath + "\\" + rename(filename, ext) + ext, ImageFormat.Tiff);
                    break;
                case ".wmf":
                    image.Save(specialPath + "\\" + rename(filename, ext) + ext, ImageFormat.Wmf);
                    break;
                case ".emf":
                    image.Save(specialPath + "\\" + rename(filename, ext) + ext, ImageFormat.Emf);
                    break;
                default:
                    image.Save(specialPath + "\\" + rename(filename, ext) + ext, ImageFormat.Png);
                    break;
            }

        }

        public void resize(int width, int height, String Image, String filename, String ext)
        {
            Bitmap image;
            image = new Bitmap(Image);
            Bitmap resize;
            resize = new Bitmap(image, new Size(width, height));
            switch (ext)
            {
                case ".jpg":
                    resize.Save(specialPath + "\\" + rename(filename, ext) + ext, System.Drawing.Imaging.ImageFormat.Jpeg);
                    break;
                case ".png":
                    resize.Save(specialPath + "\\" + rename(filename, ext) + ext, System.Drawing.Imaging.ImageFormat.Png);
                    break;
                case ".bmp":
                    resize.Save(specialPath + "\\" + rename(filename, ext) + ext, System.Drawing.Imaging.ImageFormat.Bmp);
                    break;
                case ".gif":
                    resize.Save(specialPath + "\\" + rename(filename, ext) + ext, System.Drawing.Imaging.ImageFormat.Gif);
                    break;
                case ".ico":
                    resize.Save(specialPath + "\\" + rename(filename, ext) + ext, System.Drawing.Imaging.ImageFormat.Icon);
                    break;
                case ".wmf":
                    resize.Save(specialPath + "\\" + rename(filename, ext) + ext, System.Drawing.Imaging.ImageFormat.Wmf);
                    break;
                case ".emf":
                    resize.Save(specialPath + "\\" + rename(filename, ext) + ext, System.Drawing.Imaging.ImageFormat.Emf);
                    break;
                case ".tif":
                    resize.Save(specialPath + "\\" + rename(filename, ext) + ext, System.Drawing.Imaging.ImageFormat.Tiff);
                    break;
                default:
                    resize.Save(specialPath + "\\" + rename(filename, ext) + ext, System.Drawing.Imaging.ImageFormat.Jpeg);
                    break;
            }

        }

        public String getPath()
        {
            return specialPath;
        }*/
    }
}
