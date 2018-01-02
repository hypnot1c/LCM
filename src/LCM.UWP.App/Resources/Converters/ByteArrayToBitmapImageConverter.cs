using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media.Imaging;

namespace LCM.UWP.App.Resources.Converters
{
  public class ByteArrayToBitmapImageConverter : IValueConverter
  {
    public object Convert(object value, Type targetType, object parameter, string language)
    {
      if (value == null) return value;

      var arr = new byte[0];
      if (value is byte[])
      {
        arr = value as byte[];
      }

      var image = new BitmapImage();
      using (var stream = new InMemoryRandomAccessStream())
      {
        var r = stream.WriteAsync(arr.AsBuffer()).AsTask().Result;
        stream.Seek(0);
        image.SetSource(stream);
      }
      return image;
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
      if (value is BitmapImage)
      {
        var source = value as BitmapImage;
        var file = StorageFile.GetFileFromApplicationUriAsync(source.UriSource).AsTask().Result;
        using (var inputStream = file.OpenSequentialReadAsync().AsTask().Result)
        {
          var readStream = inputStream.AsStreamForRead();

          var byteArray = new byte[readStream.Length];
          var r = readStream.ReadAsync(byteArray, 0, byteArray.Length).Result;
          return byteArray;
        }
      }
      else
      {
        return new byte[0];
      }
    }
  }
}
