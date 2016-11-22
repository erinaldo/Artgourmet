using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Artebit.Restaurante.Caixa.Controles
{
    public class ImageButton : Button
    {
        public ImageSource Image
        {
            get { return (ImageSource)GetValue(ImageProperty); }
            set { SetValue(ImageProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Image.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ImageProperty =
            DependencyProperty.Register("Image", typeof(ImageSource), typeof(ImageButton), new UIPropertyMetadata(null));

        public string NomeJanela
        {
            get { return (string)GetValue(NomeJanelaProperty); }
            set { SetValue(NomeJanelaProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Image.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty NomeJanelaProperty =
            DependencyProperty.Register("NomeJanela", typeof(String), typeof(ImageButton), new UIPropertyMetadata(null));
    }
}
