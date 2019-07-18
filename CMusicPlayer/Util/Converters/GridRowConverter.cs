using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows.Data;
using CMusicPlayer.Media.Models;

namespace CMusicPlayer.Util.Converters
{
    internal class GridRowConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
//            Debug.WriteLine("Values");
//            values.ForEach(x => Debug.WriteLine(x));
            var track = (ITrack) values[0];
            var tracks = (Collection<ITrack>) values[1];
            var row = (int) values[2];
            if (row < 0) return false;
            return ReferenceEquals(tracks[row], track);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}