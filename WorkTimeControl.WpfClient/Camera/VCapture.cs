using Emgu;
using Emgu.CV;
using Emgu.CV.Util;
using Emgu.CV.Structure;
using Emgu.Util;
using System;
using System.Drawing;
using System.Windows;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Media.Imaging;
using CommunityToolkit.Mvvm.ComponentModel;
using WorkTimeControl.WpfClient.Camera.Abstracts;

namespace WorkTimeControl.WpfClient.Camera
{
    public static class VCapture  
    {
        private static VideoCapture videoCapture = null;

        public static void Initialize()
        {
            videoCapture = new VideoCapture(0);
            videoCapture.ImageGrabbed += VideoCapture_ImageGrabbed;
            videoCapture.Start();
        }

        private static void VideoCapture_ImageGrabbed(object? sender, EventArgs e)
        {
            Mat frame = videoCapture.QueryFrame(); // запрос нового кадра                                                   
            Image getImages = frame.ToImage<Bgr, byte>().Flip(Emgu.CV.CvEnum.FlipType.Horizontal).ToBitmap();
            GetBitmapImage.Dispatcher.Invoke(new Action(() => { ImageConvert.Convert(getImages); }));
            ImagePath = getImages;
        }



        //private static Image _ImagePath;
        public static Image ImagePath { get; set; }
        //{
        //    get { return _ImagePath; }
        //    set
        //    {
        //        _ImagePath = value;
        //    }
        //}

        //private BitmapImage _GetBitmapImage = new BitmapImage();
        public static BitmapImage GetBitmapImage = new BitmapImage();
        //{
        //    get { return _GetBitmapImage; }
        //    set
        //    {
        //        _GetBitmapImage = value;
        //        OnPropertyChanged(nameof(GetBitmapImage));
        //    }
        //}


        public static void StopCapture()
        {
            videoCapture.Stop();
            videoCapture.Dispose();
        }


        // Save image
        private static void SaveImage()
        {
            ImagePath.Save("screen.jpg", ImageFormat.Jpeg);
            MessageBox.Show("Image save!");
        }

        // Screenshot
        public static Image MakeScreenShot(Image<Bgr, byte> _image)
        {
            return _image.AsBitmap();
        }
    }
}
