using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Media.Imaging;

namespace WorkTimeControl.WpfClient.Camera
{
    public static class ImageConvert
    {
        // Convert Bitmap to Byte[]
        public static byte[] ConvertToByte(Bitmap bmp)
        {
            MemoryStream memoryStream = new MemoryStream();
            // Конвертируем в массив байтов с сжатием Jpeg
            if (bmp != null)
            {
                bmp.Save(memoryStream, ImageFormat.Jpeg);
                return memoryStream.ToArray();
            }
            else  // Если камера выключена, делаем черный квадрат с надписью
            {
                bmp = new Bitmap(64, 48);
                Graphics g = Graphics.FromImage(bmp);
                g.DrawString("No Image", new Font("Tahoma", 10), Brushes.White, 1, 15);
                bmp.Save(memoryStream, ImageFormat.Jpeg);
                return memoryStream.ToArray();
            }
        }


        // Convert Byte[] to Image
        public static Image ByteToImage(byte[] buffer)
        {
            using (var ms = new MemoryStream(buffer))
            {
                return Image.FromStream(ms);
            }
        }

        // Convert Image to BitmapImage
        public static BitmapImage Convert(Image img)
        {
            using (var memory = new MemoryStream())
            {
                img.Save(memory, ImageFormat.Jpeg);
                memory.Position = 0;

                var bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = memory;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();

                return bitmapImage;
            }
        }


        // Convert Byte[] to BitmapImage
        public static BitmapImage ConvertByteToBitmapImage(byte[] bytes)
        {
            using (MemoryStream ms = new MemoryStream(bytes))
            {
                try
                {
                    BitmapImage bitmapImage = new BitmapImage();
                    bitmapImage.BeginInit();
                    bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                    bitmapImage.StreamSource = ms;
                    bitmapImage.EndInit();
                    return bitmapImage;
                }
                catch { }
                return null;
            }
        }
    }
}
