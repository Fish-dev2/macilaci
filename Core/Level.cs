using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace macilaci.Core
{
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
        enum DirectionId
        {
            Up,
            Right,
            Down,
            Left
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
                        //add new item based on ID and direction
                    }
                    else
                    {
                        int itemID = int.Parse(currentitem);
                        //add new item based on ID
                    }
                }

            }
        }
        public void LevelSaver(string levelName)
        {
            throw new NotImplementedException();
        }
    }
}
