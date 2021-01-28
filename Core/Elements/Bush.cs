using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace macilaci.Core.Elements
{
    public class Bush : LevelElement
    {

        public Bush() : base("bush.png")
        {
            Collideable = true;
        }

    }
}
