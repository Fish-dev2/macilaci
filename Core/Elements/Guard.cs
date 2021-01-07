﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace macilaci.Core.Elements
{
    public class Guard : LevelElement
    {
        private DirectionId directionId;

        public DirectionId DirectionId
        {
            get { return directionId; }
            set { directionId = value; }
        }

        public Guard(DirectionId direction) : base("guard.png")
        {

        }

    }
}
