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

        public BitmapImage Image { get; }

        public LevelElement(string image)
        {
            Image = new BitmapImage(new Uri("Resources/" + image));
        }

    }
}
