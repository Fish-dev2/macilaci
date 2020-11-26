using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace macilaci
{
    public class GameHandler : Bindable
    {

        public Game Game { get; }

        public GameTimer Timer { get; }
        public bool Paused { get; private set; } = false;

        public List<Key> Holding { get; } = new List<Key>();

        public GameHandler(Game game)
        {
            Game = game;

            Game.KeyDown += OnKeyDown;
            Game.KeyUp += OnKeyUp;

            Timer = new GameTimer();
        }

        public void TogglePause()
        {
            Paused = !Paused;
            Timer.Enabled = !Paused;
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (!Holding.Contains(e.Key)) Holding.Add(e.Key);
        }

        private void OnKeyUp(object sender, KeyEventArgs e)
        {
            Holding.Remove(e.Key);
        }
    }
}
