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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace macilaci
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Grid mainGrid;

        public Grid MainGrid
        {
            get { return mainGrid; }
            set { mainGrid = value; }
        }

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;
            mainGrid = new Grid();
            InitMainMenuButtons();
        }

        private void InitMainMenuButtons()
        {
            mainGrid.ColumnDefinitions.Add(new ColumnDefinition());

            for (int i = 0; i < 7; i++)
            {
                mainGrid.RowDefinitions.Add(new RowDefinition());
            }
            Image MainMenuImage = new Image();
            //MainMenuImage.Source = new BitmapImage(new Uri(""));
            Thickness thickness = new Thickness(200, 30, 200, 30);
            Button NewGame = new Button();
                NewGame.Content = "Új Játék";
                mainGrid.Children.Add(NewGame);
                NewGame.Name = "newGameButton";
            Button Continue = new Button();
                Continue.Content = "Folytatás";
                mainGrid.Children.Add(Continue);
                Continue.Name = "continueButton";
            Button About = new Button();
                About.Content = "Játék leirása";
                mainGrid.Children.Add(About);
                About.Name = "aboutButton";
            Button Quit = new Button();
                Quit.Content = "Kilépés";
                mainGrid.Children.Add(Quit);
                Quit.Name = "quitButton";

            var selection = from x in mainGrid.Children.OfType<Button>()
                            select x;

            int counter = 2;
            foreach (var item in selection)
            {
                item.Margin = thickness;
                item.Click += Button_Click;
                Grid.SetRow(item, counter);
                counter++;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            switch (button.Name)
            {
                case "newGameButton":
                    menuFrame.Visibility = Visibility.Hidden;
                    newgameFrame.Visibility = Visibility.Visible;
                    break;
                case "continueButton":
                    throw new NotImplementedException();
                    //TODO:Error if no saves, OR load in saved level(last played)
                    break;
                case "aboutButton":
                    //TODO:Játékleirás kiirasa
                    break;
                case "quitButton":
                    this.Close();
                    break;
                default:
                    break;
            }
        }
    }
}
