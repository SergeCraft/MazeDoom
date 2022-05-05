using Assets.Scripts.Game.Maze;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Game.MazeGenerators
{
    public abstract class MazeGeneratorBase 
    {
        public abstract MazeDescription GenerateMaze(int columns, int rows);


        protected static void CreateDefaultMazeField(int columns, int rows, MazeDescription maze)
        {
            for (int i = 0; i < columns; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    maze.CellDescriptions.Add(new CellDescription(i, j, true, true, true, true));
                }
            }
        }
    }
}
