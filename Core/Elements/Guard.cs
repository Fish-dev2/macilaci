using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace macilaci.Core.Elements
{
    public class Guard : LevelElement, Rotatable
    {
        private DirectionId directionId;

        public DirectionId DirectionId
        {
            get { return directionId; }
            set 
            {
                directionId = value;
                Image.Source = new BitmapImage(new Uri("Resources/Textures/Guard/" + directionId.ToString("g") + ".png", UriKind.Relative));
            }
        }

        public Guard() : base("Guard/Left.png")
        {
            
        }

    }
}
