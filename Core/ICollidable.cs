using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace macilaci.Core
{
    public interface ICollidable
    {

        Point Position { get; }
        bool IsCollidingWith(ICollidable collidable);
        bool IsCollidingWith(ICollidable collidable, Point offset);

    }
}
