using macilaci.Core.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace macilaci.Core
{
    public enum DirectionId
    {
        Up,
        Right,
        Down,
        Left
    }

    public class Level
    {
        private LevelElement[,] levelElements;

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
        public Level(string levelFile)
        {
            LevelLoader(levelFile);
        }

        private void LevelLoader(string levelFile)
        {
            string[] rows = levelFile.Split('\n');
            string[] firstrow = rows[0].Split('x');
            int xsize = int.Parse(firstrow[0]);
            int ysize = int.Parse(firstrow[1]);
            levelElements = new LevelElement[ysize, xsize];
            for (int i = 1; i < rows.Length; i++)
            {
                string[] currentrow = rows[i].Split(',');
                for (int j = 0; j < currentrow.Length; j++)
                {
                    string currentitem = currentrow[j];
                    if (currentitem.Contains("("))
                    {
                        int itemID = int.Parse(Convert.ToString(currentitem[0]));
                        DirectionId directionId = (DirectionId)int.Parse(Convert.ToString(currentitem[2]));
                        if (itemID == 1)
                        {
                            LevelElements[i, j] = new Player(directionId);
                        }
                        else
                        {
                            LevelElements[i, j] = new Guard(directionId);
                        }
                    }
                    else
                    {
                        int itemID = int.Parse(currentitem);
                        string itemImage = ImageById(itemID);
                        levelElements[i, j] = new LevelElement(itemImage);
                    }
                }
            }
        }

        private string ImageById(int itemID)
        {
            ElementsId elementsId = (ElementsId)itemID;
            string imageDir = "";
            switch (elementsId)
            {
                case ElementsId.Clear:
                    imageDir = "";
                    break;
                case ElementsId.Player:
                    break;
                case ElementsId.Guard:
                    break;
                case ElementsId.Basket:
                    break;
                case ElementsId.Tree:
                    break;
                default:
                    break;
            }
            return imageDir;
        }

        public void LevelSaver(string levelName)
        {
            throw new NotImplementedException();
        }
    }
}
