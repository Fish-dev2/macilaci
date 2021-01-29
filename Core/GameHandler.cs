using macilaci.Core.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace macilaci.Core
{
    public class GameHandler : Bindable
    {

        private readonly List<Key> holding = new List<Key>();
        private readonly List<Key> disabledControls = new List<Key>();

        public Timer Timer { get; } = new Timer() { Interval = 1 };
        public Timer MoveTimer { get; } = new Timer() { Interval = 1000 };

        private bool paused = false, pauseLocked = false, canMove = true, seenByGuards = false;
        private string pauseTitle;

        public bool IsPaused { get => paused; set { paused = value; OnPropertyChanged(); } }
        public bool IsPauseLocked { get => pauseLocked; set { pauseLocked = value; OnPropertyChanged(); } }
        public string PauseTitle { get => pauseTitle; set { pauseTitle = value; OnPropertyChanged(); } }

        private Level currentLevel;
        public Level CurrentLevel { get => currentLevel; set { currentLevel = value; OnPropertyChanged(); } }

        private int playTime = 0, basketCount = 0;
        public int PlayTime { get => playTime; set { playTime = value; OnPropertyChanged(); } }
        public int BasketCount { get => basketCount; set { basketCount = value; OnPropertyChanged(); } }

        public GameHandler()
        {
            Timer.Elapsed += OnTick;
            MoveTimer.Elapsed += OnMove;
        }

        private void OnMove(object sender, ElapsedEventArgs e)
        {
            PlayTime++;
            // Move guards
            foreach (Guard guard in CurrentLevel.LevelElements.OfType<Guard>().ToList())
            {
                CurrentLevel.Root.Dispatcher.Invoke(() =>
                {
                    int x = Grid.GetColumn(guard.Image);
                    int y = Grid.GetRow(guard.Image);

                    int xOffset = 0;
                    int yOffset = 0;
                    switch (guard.DirectionId)
                    {
                        case DirectionId.Up:
                            yOffset = -1;
                            break;
                        case DirectionId.Down:
                            yOffset = 1;
                            break;
                        case DirectionId.Left:
                            xOffset = -1;
                            break;
                        case DirectionId.Right:
                            xOffset = 1;
                            break;
                    }
                    int toX = x + xOffset, toY = y + yOffset;
                    if (toX > -1 && toX < CurrentLevel.Root.ColumnDefinitions.Count && toY > -1 && toY < CurrentLevel.Root.RowDefinitions.Count && !CurrentLevel.LevelElements.OfType<LevelElement>().Any(element => element.Collideable && element.X == toX && element.Y == toY))
                    {
                        CurrentLevel.Move(guard, toX, toY);
                    }
                    else
                    {
                        guard.DirectionId = guard.DirectionId == DirectionId.Up ?
                            DirectionId.Down : guard.DirectionId == DirectionId.Down ?
                            DirectionId.Up : guard.DirectionId == DirectionId.Left ?
                            DirectionId.Right : guard.DirectionId == DirectionId.Right ?
                            DirectionId.Left : 0;
                    }
                });
            }
        }

        private void OnTick(object sender, ElapsedEventArgs e)
        {
            // Check guards position relative to player
            for(int x = -1; x < 2; x++)
            {
                for(int y = -1; y < 2; y++)
                {
                    if(!(x == 0 && y == 0) && CurrentLevel.LevelElements.OfType<Guard>().Any(guard => guard.X == CurrentLevel.Player.X + x && guard.Y == CurrentLevel.Player.Y + y)) 
                    {
                        seenByGuards = true;
                        GameOver();
                    }
                }
            }

            // Check basket position relative to player
            IEnumerable<Basket> baskets = CurrentLevel.LevelElements.OfType<Basket>();
            if (baskets.Any(basket => basket.X == CurrentLevel.Player.X && basket.Y == CurrentLevel.Player.Y))
            {
                Basket basket = baskets.First(b => b.X == CurrentLevel.Player.X && b.Y == CurrentLevel.Player.Y);
                CurrentLevel.LevelElements.Remove(basket);
                CurrentLevel.Root.Dispatcher.Invoke(() =>
                {
                    CurrentLevel.Root.Children.Remove(basket.Image);    
                });

                BasketCount++;
                if (BasketCount == CurrentLevel.BasketCount) GameOver();
            }
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
            MoveTimer.Enabled = !pause;
            if (IsPaused)
            {
                if (BasketCount == CurrentLevel.BasketCount)
                {
                    PauseTitle = "Összegyűjtötted az összes kosarat!";
                }
                else if (seenByGuards)
                {
                    PauseTitle = "Az őrök megláttak, vesztettél!";
                }
                else
                {
                    PauseTitle = "Játék megállítva";
                }
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
            } else if(!IsPaused && canMove)
            {
                DirectionId? direction = null;
                switch(e.Key)
                {
                    case Key.Up:
                        direction = DirectionId.Up;
                        break;
                    case Key.Right:
                        direction = DirectionId.Right;
                        break;
                    case Key.Down:
                        direction = DirectionId.Down;
                        break;
                    case Key.Left:
                        direction = DirectionId.Left;
                        break;
                }
                if(direction != null)
                {
                    HandleMove((DirectionId)direction);
                }
            }
        }

        public async void HandleMove(DirectionId direction)
        {
            int xOffset = 0, yOffset = 0;
            if (direction == DirectionId.Up)
            {
                yOffset = -1;
                CurrentLevel.Player.DirectionId = DirectionId.Up;
            }
            else if (direction == DirectionId.Down)
            {
                yOffset = 1;
                CurrentLevel.Player.DirectionId = DirectionId.Down;
            }
            else if (direction == DirectionId.Left)
            {
                xOffset = -1;
                CurrentLevel.Player.DirectionId = DirectionId.Left;
            }
            else if (direction == DirectionId.Right)
            {
                xOffset = 1;
                CurrentLevel.Player.DirectionId = DirectionId.Right;
            }

            int toX = CurrentLevel.Player.X + xOffset, toY = CurrentLevel.Player.Y + yOffset;
            if (toX > -1 && toX < CurrentLevel.Root.ColumnDefinitions.Count && toY > -1 && toY < CurrentLevel.Root.RowDefinitions.Count && !CurrentLevel.LevelElements.OfType<LevelElement>().Any(element => element.Collideable && element.X == toX && element.Y == toY))
            {
                CurrentLevel.Move(CurrentLevel.Player, CurrentLevel.Player.X + xOffset, CurrentLevel.Player.Y + yOffset);
                canMove = false;
                await Task.Delay(100);
                canMove = true;
            }
        }
    }
}
