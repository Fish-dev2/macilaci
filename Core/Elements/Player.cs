using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace macilaci.Core.Elements
{
    public class Player : LevelElement
    {
        private DirectionId directionId;
        public int X { get; set; } = 0;
        public int Y { get; set; } = 0;

        public DirectionId DirectionId
        {
            get { return directionId; }
            set { directionId = value; }
        }

        public Player(DirectionId direction) : base("player.png")
        {

        }

    }
}
