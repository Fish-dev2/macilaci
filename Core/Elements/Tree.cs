using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace macilaci.Core.Elements
{
    public class Tree : LevelElement
    {

        public Tree() : base("tree.png")
        {
            Collideable = true;
        }

    }
}
