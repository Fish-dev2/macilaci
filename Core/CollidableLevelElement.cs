using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace macilaci.Core
{
    public class CollidableLevelElement : LevelElement, ICollidable
    {

        public Point Position { get; set; }

        public CollidableLevelElement(string image) : base(image)
        {

        }

        public bool IsCollidingWith(ICollidable collidable)
        {
            return Position.X == collidable.Position.X && Position.Y == collidable.Position.Y;
        }

        public bool IsCollidingWith(ICollidable collidable, Point offset)
        {
            return Position.X + offset.X == collidable.Position.X && Position.Y + offset.Y == collidable.Position.Y;
        }
    }
}
