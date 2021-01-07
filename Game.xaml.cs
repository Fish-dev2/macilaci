using macilaci.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace macilaci
{
    /// <summary>
    /// Interaction logic for Game.xaml
    /// </summary>
    public partial class Game : Window
    {

        public static GameHandler Handler { get; private set; }

        public Game()
        {
            InitializeComponent();

            Handler = new GameHandler();
            this.DataContext = Handler;
            PreviewKeyDown += Handler.OnKeyDown;
            Handler.Timer.Start();
        }

        private void OnLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.Source is TextBlock)
            {
                TextBlock label = e.Source as TextBlock;

                if (label == Endgame)
                {
                    Close();
                }
            }
        }
    }
}
