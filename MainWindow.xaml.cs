using macilaci.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;
            helpLabel.Text = "A játék lényege,hogy az összes kosarat összegyűjtsd az adott pályán anélkül, hogy az őrök elkapjanak, a szomszédos és átlós mezőkön.\nMozgás: Nyilak\nKilépés:Esc";
            listbox.Items.Add("Első pálya");
            listbox.Items.Add("Második pálya");
            listbox.Items.Add("Harmadik pálya");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            switch (button.Name)
            {
                case "newGameButton":
                    newGameButton.Visibility = Visibility.Hidden;
                    listbox.Visibility = Visibility.Visible;
                    break;
                case "helpButton":
                    helpLabel.Visibility = Visibility.Visible;
                    helpButton.Visibility = Visibility.Hidden;
                    //TODO:Játékleirás kiirasa
                    break;
                case "quitButton":
                    this.Close();
                    break;
                default:
                    break;
            }
        }

        private void MouseLeaveLabel(object sender, MouseEventArgs e)
        {
            helpLabel.Visibility = Visibility.Hidden;
            helpButton.Visibility = Visibility.Visible;
        }

        private void PalyaSelection(object sender, SelectionChangedEventArgs e)
        {
            if (listbox.SelectedItem != null)
            {
                string választottpálya = listbox.SelectedItem.ToString();
                string pályafájl = "";
                switch (választottpálya)
                {
                    case "Első pálya":
                        pályafájl = "level1.csv";
                        break;
                    case "Második pálya":
                        pályafájl = "level2.csv";
                        break;
                    case "Harmadik pálya":
                        pályafájl = "level3.csv";
                        break;
                    default:
                        break;
                }
                listbox.Visibility = Visibility.Hidden;
                newGameButton.Visibility = Visibility.Visible;
                //pályafájl = "TesztPalya.csv";
                new Game(pályafájl).Show();
            }
            listbox.UnselectAll();
        }

        private void MouseLeaveListbox(object sender, MouseEventArgs e)
        {
            listbox.Visibility = Visibility.Hidden;
            newGameButton.Visibility = Visibility.Visible;
        }
    }
}
