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
using Emgu.CV;
using Emgu.CV.Structure;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private VideoCapture _capture; // объект захвата видео
        private bool _isCapturing;
        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += MainWindow_Loaded;
            this.Closed += MainWindow_Closed;
        }

        private void MainWindow_Closed(object? sender, EventArgs e)
        {
           StopCapture();
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        private void StartCapture()
        {
            _capture = new VideoCapture(0); // захват видео с первой доступной камеры
            _capture.ImageGrabbed += _capture_ImageGrabbed;
            _capture.Start();          
        }

        private void _capture_ImageGrabbed(object? sender, EventArgs e)
        {
            Mat frame = _capture.QueryFrame(); // запрос нового кадра
            Image<Bgr, byte> image = frame.ToImage<Bgr, byte>(); // конвертация кадра в изображение Emgu
            cameraImg.Source = ToBitmapSource(image); // отображение изображения
        }

        private void StopCapture()
        {
            _isCapturing = false;
            _capture.Dispose();
        }

        private BitmapSource ToBitmapSource(Image<Bgr, byte> image)
        {
            //return System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
            //    image.ToBitmap().GetHbitmap(),
            //    IntPtr.Zero,
            //    Int32Rect.Empty,
            //   BitmapSizeOptions.FromEmptyOptions());
            using (System.Drawing.Bitmap source = image.AsBitmap())
            {
                IntPtr ptr = source.GetHbitmap(); //obtain the Hbitmap

                BitmapSource bs = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
                    ptr,
                    IntPtr.Zero,
                    Int32Rect.Empty,
                    System.Windows.Media.Imaging.BitmapSizeOptions.FromEmptyOptions());

                //DeleteObject(ptr); //release the HBitmap
                return bs;
            }
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            StartCapture();
        }
    }
}
