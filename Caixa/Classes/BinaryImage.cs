using System;
using System.Globalization;
using System.IO;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace Artebit.Restaurante.Caixa.Classes
{
    internal class BinaryImage : IValueConverter
    {
        //Implements IValueConverter
        //Private Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.Convert
        //    If value IsNot Nothing AndAlso TypeOf value Is Byte() Then
        //        Dim ByteArray As Byte() = TryCast(value, Byte())
        //        Dim bmp As New BitmapImage()
        //        bmp.BeginInit()
        //        bmp.StreamSource = New MemoryStream(ByteArray)
        //        bmp.EndInit()
        //        Return bmp
        //    End If
        //    Return Nothing
        //End Function
        //Private Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements IValueConverter.ConvertBack
        //    Throw New Exception("The method or operation is not implemented.")
        //End Function

        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Byte[])
            {
                var byteArray = (byte[]) value;

                var bmp = new BitmapImage();
                bmp.BeginInit();
                bmp.StreamSource = new MemoryStream(byteArray);
                bmp.EndInit();
                return bmp;
            }
            else
            {
                return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}