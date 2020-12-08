using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace macilaci.Core
{
    public class GameTimer : Timer
    {
        public GameTimer()
        {
            Interval = 1;
            Elapsed += OnTick;
        }

        private void OnTick(object sender, ElapsedEventArgs e)
        {
            // TODO: functions per tick
        }

    }
}
