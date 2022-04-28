using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace MVVM_SocialContractProject.Commands
{
    public class ImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            dynamic dataObject = value;
            if (dataObject != null)
            {
                string path = dataObject;
                if (System.IO.File.Exists(path))
                    return new Uri(dataObject, UriKind.RelativeOrAbsolute);
            }

            return new Uri(@"D:\Anime & Pics\Stuff I want to draw\6b6e25ef0e45ed6c8ae8b0b078b3a00c35676ec6.png@518w.png", UriKind.RelativeOrAbsolute);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
