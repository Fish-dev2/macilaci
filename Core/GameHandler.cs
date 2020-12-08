using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace macilaci.Core
{
    public class GameHandler : Bindable
    {

        public Game Game { get; }

        public GameTimer Timer { get; }
        public bool Paused { get; private set; } = false;

        public Level Level { get; }

        public GameHandler(Game game)
        {
            Game = game;

            Game.KeyDown += OnKeyDown;

            Timer = new GameTimer();
        }

        public void LoadLevel(string levelFile)
        {
            Level level = new Level(levelFile);
        }

        public void TogglePause()
        {
            Paused = !Paused;
            Timer.Enabled = !Paused;
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            //Move
        }
    }
}
