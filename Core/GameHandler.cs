using macilaci.Core.Elements;
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

        private readonly List<Key> holding = new List<Key>();
        private readonly List<Key> disabledControls = new List<Key>();

        public Timer Timer { get; } = new Timer() { Interval = 1 };

        private bool paused = false;
        private string pauseTitle;

        public bool IsPaused { get => paused; set { paused = value; OnPropertyChanged(); } }
        public bool IsPauseLocked { get; set; } = false;
        public string PauseTitle { get => pauseTitle; set { pauseTitle = value; OnPropertyChanged(); } }

        private Level currentLevel;
        public Level CurrentLevel { get => currentLevel; set { currentLevel = value; OnPropertyChanged(); } }
        //public Player Player { get; set; } = new Player(DirectionId.Right);

        public GameHandler()
        {
            Timer.Elapsed += OnTick;

            CurrentLevel = new Level("TesztPalya.csv");
        }

        private void OnTick(object sender, ElapsedEventArgs e)
        {
            // Check guards position relative to player
            /*for(int x = -1; x < 2; x++)
            {
                for(int y = -1; y < 2; y++)
                {
                    if(!(x == 0 && y == 0) && CurrentLevel.LevelElements[Player.X + x, Player.Y + y] is Guard) 
                    {
                        GameOver();
                    }
                }
            }*/

            // Move guards
            for(int i = 0; i < CurrentLevel.LevelElements.GetLength(1); i++)
            {
                for(int j = 0; j < CurrentLevel.LevelElements.GetLength(0); j++)
                {
                    if(CurrentLevel.LevelElements[i, j] is Guard)
                    {
                        Guard guard = CurrentLevel.LevelElements[i, j] as Guard;

                        int xOffset = 0;
                        int yOffset = 0;
                        switch(guard.DirectionId)
                        {
                            case DirectionId.Up:
                                xOffset = -1;
                                break;
                            case DirectionId.Down:
                                xOffset = 1;
                                break;
                            case DirectionId.Left:
                                yOffset = -1;
                                break;
                            case DirectionId.Right:
                                yOffset = 1;
                                break;
                        }
                        if(CurrentLevel.LevelElements[i + xOffset, j + yOffset] is Tree)
                        {
                            guard.DirectionId = guard.DirectionId == DirectionId.Up ?
                                DirectionId.Down : guard.DirectionId == DirectionId.Down ?
                                DirectionId.Up : guard.DirectionId == DirectionId.Left ?
                                DirectionId.Right : guard.DirectionId == DirectionId.Right ?
                                DirectionId.Left : 0;
                        } else
                        {
                            CurrentLevel.LevelElements[i, j] = null;
                            CurrentLevel.LevelElements[i + xOffset, j + yOffset] = guard;
                        }
                    }
                }
            }

            // Check basket position relative to player 
            /*if(CurrentLevel.LevelElements[Player.X, Player.Y] is Basket)
            {
                //collect basket

            }*/
        }

        public void DisableControl(Key control, bool disable)
        {
            if (disable)
            {
                if (!disabledControls.Contains(control))
                {
                    disabledControls.Add(control);
                }
            }
            else
            {
                disabledControls.Remove(control);
            }
        }

        public bool HoldingKey(Key key)
        {
            return holding.Contains(key) && !disabledControls.Contains(key);
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

        public void GameOver()
        {
            IsPauseLocked = true;
            SetPause(true);
        }

        public void Restart()
        {
            IsPauseLocked = false;
            SetPause(false);
        }

        internal void OnKeyUp(object sender, KeyEventArgs e)
        {
            holding.Remove(e.Key);
        }

        public void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (!holding.Contains(e.Key)) holding.Add(e.Key);
            if (e.Key == Key.Escape && !IsPauseLocked)
            {
                SetPause(!IsPaused);
            } else
            {
                int xOffset = 0, yOffset = 0;
                if (e.Key == Key.Up)
                {
                    yOffset = -1;
                }
                else if (e.Key == Key.Down)
                {
                    yOffset = 1;
                }
                else if (e.Key == Key.Left)
                {
                    xOffset = -1;
                }
                else if (e.Key == Key.Right)
                {
                    xOffset = 1;
                }

                /*LevelElement element = CurrentLevel.LevelElements[Player.X + xOffset, Player.Y + yOffset];
                if (element is Tree)
                {
                    e.Handled = true;
                }*/
            }
        }
    }
}
