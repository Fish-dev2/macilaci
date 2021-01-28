using macilaci.Core.Elements;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace macilaci.Core
{
    public enum DirectionId
    {
        Up,
        Right,
        Down,
        Left
    }

    public class Level : Bindable
    {

        public List<LevelElement> LevelElements { get; } = new List<LevelElement>();
        public Player Player { get => LevelElements.OfType<Player>().First(); }

        private int basketCount = 0;
        public int BasketCount { get => basketCount; set { basketCount = value; OnPropertyChanged(); } }

        private Grid root;
        public Grid Root { get => root; set { root = value; OnPropertyChanged(); } }
        enum ElementsId
        {
            Clear,
            Player,
            Guard,
            Basket = 10,
            Tree = 11,
            Bush = 12
        }

        private string levelName;

        public string LevelName
        {
            get { return levelName; }
            set { levelName = value; }
        }

        public Level(string levelFile)
        {
            LevelName = levelFile;
            LevelLoader();
        }

        private void LevelLoader()
        {
            Root = new Grid();
            Root.Width = SystemParameters.PrimaryScreenWidth;
            Root.Height = SystemParameters.PrimaryScreenHeight - 100;
            string levelFile = "Resources/Levels/" + LevelName;

            string[] rows = File.ReadAllLines(levelFile);
            string[] firstrow = rows[0].Split('x');
            int xsize = int.Parse(firstrow[0]);
            int ysize = int.Parse(firstrow[1]);
            for (int i = 1; i < rows.Length; i++)
            {
                Root.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) });
                Root.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
                string[] currentrow = rows[i].Split(',');
                for (int j = 0; j < currentrow.Length; j++)
                {
                    string currentitem = currentrow[j];
                    LevelElement element = null;
                    if (currentitem.Contains("("))
                    {
                        int itemID = int.Parse(Convert.ToString(currentitem[0]));
                        DirectionId directionId = (DirectionId)int.Parse(Convert.ToString(currentitem[2]));
                        if(itemID == 1)
                        {
                            element = new Player();
                        } else if (itemID == 2)
                        {
                            element = new Guard();
                        }
                        (element as Rotatable).DirectionId = directionId;
                    }
                    else
                    {
                        int itemID = int.Parse(currentitem);
                        element = ObjById(itemID);
                    }

                    if (element != null)
                    {
                        Root.Children.Add(element.Image);
                        Grid.SetRow(element.Image, i - 1);
                        Grid.SetColumn(element.Image, j);

                        element.X = j;
                        element.Y = i - 1;
                        LevelElements.Add(element);
                    }
                }
            }
            BasketCount = LevelElements.OfType<Basket>().Count();
        }

        private LevelElement ObjById(int itemID)
        {
            ElementsId elementsId = (ElementsId)itemID;
            LevelElement toReturn = null;
            switch (elementsId)
            {
                case ElementsId.Clear:
                    toReturn = null;
                    break;
                case ElementsId.Basket:
                    toReturn = new Basket();
                    break;
                case ElementsId.Tree:
                    toReturn = new Tree();
                    break;
                case ElementsId.Bush:
                    toReturn = new Bush();
                    break;
                default:
                    toReturn = null;
                    break;
            }
            return toReturn;
        }

        public void LevelSaver()
        {
            throw new NotImplementedException();
        }

        public void Move(LevelElement element, int toX, int toY)
        {
            element.X = toX;
            element.Y = toY;
            Grid.SetColumn(element.Image, toX);
            Grid.SetRow(element.Image, toY);
        }
    }
}
