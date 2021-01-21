using macilaci.Core.Elements;
using System;
using System.Collections.Generic;
using System.IO;
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
            string levelFile = "Resources/Levels/" + LevelName;

            string[] rows = File.ReadAllLines(levelFile);
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
                        levelElements[i, j] = ObjById(itemID);
                    }
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
                case ElementsId.Player:
                    //ez nem lehet
                    break;
                case ElementsId.Guard:
                    //ez se
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
