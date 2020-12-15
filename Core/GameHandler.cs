using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Input;

namespace macilaci.Core
{
    public class GameHandler : Bindable
    {

        public Timer Timer { get; } = new Timer() { Interval = 1 };

        private bool paused = false;
        private string pauseTitle;

        public bool IsPaused { get => paused; set { paused = value; OnPropertyChanged(); } }
        public bool IsPauseLocked { get; set; } = false;
        public string PauseTitle { get => pauseTitle; set { pauseTitle = value; OnPropertyChanged(); } }

        public Level CurrentLevel { get; private set; }

        public GameHandler()
        {
            Timer.Elapsed += OnTick;
        }

        private void OnTick(object sender, ElapsedEventArgs e)
        {
            // TODO: functions per tick
        }

        public void LoadLevel(string levelFile)
        {
            CurrentLevel = new Level(levelFile);
        }

        public void SetPause(bool pause)
        {
            IsPaused = pause;
            Timer.Enabled = !pause;
            if (IsPaused)
            {
                PauseTitle = "Játék megállítása";
            }
        }

        public void EndGame()
        {
            IsPauseLocked = true;
            SetPause(true);
        }

        public void Restart()
        {
            IsPauseLocked = false;
            SetPause(false);
        }

        public void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape && !IsPauseLocked)
            {
                SetPause(!IsPaused);
            }
            //Move
        }
    }
}
