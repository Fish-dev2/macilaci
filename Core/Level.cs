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
        private LevelElement[,] levelElements;

        private Grid root;
        public Grid Root { get => root; set { root = value; OnPropertyChanged(); } }

        public LevelElement[,] LevelElements
        {
            get { return levelElements; }
            set { levelElements = value; }
        }
        enum ElementsId
        {
            Clear,
            Player,
            Guard,
            Basket = 10,
            Tree = 11
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
            Root.Height = SystemParameters.PrimaryScreenHeight;
            string levelFile = "Resources/Levels/" + LevelName;

            string[] rows = File.ReadAllLines(levelFile);
            string[] firstrow = rows[0].Split('x');
            int xsize = int.Parse(firstrow[0]);
            int ysize = int.Parse(firstrow[1]);
            levelElements = new LevelElement[ysize, xsize];
            for (int i = 1; i < rows.Length; i++)
            {
                Root.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(Root.Height / rows.Length - 1, GridUnitType.Pixel) });
                string[] currentrow = rows[i].Split(',');
                for (int j = 0; j < currentrow.Length; j++)
                {
                    Root.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(Root.Width / currentrow.Length, GridUnitType.Pixel) });
                    string currentitem = currentrow[j];
                    LevelElement element = null;
                    if (currentitem.Contains("("))
                    {
                        int itemID = int.Parse(Convert.ToString(currentitem[0]));
                        DirectionId directionId = (DirectionId)int.Parse(Convert.ToString(currentitem[2]));
                        if (itemID == 1)
                        {
                            element = new Player(directionId);
                        }
                        else
                        {
                            element = new Guard(directionId);
                        }
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
                    }
                    levelElements[i - 1, j] = element;
                }
            }
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
    }
}
