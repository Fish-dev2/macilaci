using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace macilaci.Core
{
    public class LevelElement
    {

        public int X { get; set; } = 0;
        public int Y { get; set; } = 0;

        public bool Collideable { get; set; } = false;
            
        public Image Image { get; } = new Image();

        public LevelElement(string image)
        {
            Image.Source = new BitmapImage(new Uri("Resources/Textures/" + image, UriKind.Relative));
        }

    }
}
