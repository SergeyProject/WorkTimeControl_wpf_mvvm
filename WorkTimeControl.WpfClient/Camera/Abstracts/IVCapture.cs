using System.Drawing;
using System.Windows.Media.Imaging;

namespace WorkTimeControl.WpfClient.Camera.Abstracts
{
   public interface IVCapture
    {
        BitmapImage GetBitmapImage { get; set; }        
        Image ImagePath { get; set; }
        void Initialize();
        void StopCapture();
    }
}
