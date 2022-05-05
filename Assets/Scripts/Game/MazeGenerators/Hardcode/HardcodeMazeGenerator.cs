using Assets.Scripts.Game.Maze;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Game.MazeGenerators.Hardcode
{
    public class HardcodeMazeGenerator : MazeGeneratorBase
    {

        public override MazeDescription GenerateMaze(int columns, int rows)
        {
            return new MazeDescription(
                new List<CellDescription>
                {
                new CellDescription (0, 0, false, false, false, false),
                new CellDescription (0, 1, false, false, false, true),
                new CellDescription (0, 2, true, false, false, false),
                new CellDescription (0, 3, false, true, false, false),
                new CellDescription (0, 4, false, false, true, false),
                new CellDescription (0, 5, true, false, false, true),
                new CellDescription (0, 6, false, false, true, true),
                new CellDescription (0, 7, false, true, true, false),
                new CellDescription (0, 8, true, true, false, false),
                new CellDescription (0, 9, true, true, true, false),
                new CellDescription (0, 10, true, true, false, true),
                new CellDescription (0, 11, true, false, true, true),
                new CellDescription (0, 12, false, true, true, true),
                new CellDescription (0, 13, false, true, false, true),
                new CellDescription (0, 14, true, false, true, false),
                new CellDescription (0, 15, true, true, true, true)
                },
                1, 16);
        }
    }
}
