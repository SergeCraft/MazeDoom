using Assets.Scripts.Game.Maze;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Game.MazeGenerators.DFS
{
    public class DepthFirstSearchMazeGenerator : MazeGeneratorBase
    {

        public override MazeDescription GenerateMaze(int columns, int rows)
        {
            MazeDescription maze = new MazeDescription(new List<CellDescription>(), columns, rows);
            CreateDefaultMazeField(columns, rows, maze);

            Random random = new Random();
            var currentCell = maze.CellDescriptions.Find(x => x.ColumnPosition == 0 && x.RowPosition == 0);
            List<CellDescription> visitedCells = new List<CellDescription>();

            while (visitedCells.Count < maze.CellDescriptions.Count - 1)
            {
                List<CellDescription> virginNeighbors = maze.CellDescriptions.Where(x =>
                    (x.ColumnPosition == currentCell.ColumnPosition + 1 && x.RowPosition == currentCell.RowPosition && 
                    x.Type == CellTypes.All) ||
                    (x.ColumnPosition == currentCell.ColumnPosition - 1 && x.RowPosition == currentCell.RowPosition && 
                    x.Type == CellTypes.All) ||
                    (x.ColumnPosition == currentCell.ColumnPosition && x.RowPosition == currentCell.RowPosition + 1 && 
                    x.Type == CellTypes.All) ||
                    (x.ColumnPosition == currentCell.ColumnPosition && x.RowPosition == currentCell.RowPosition - 1 && 
                    x.Type == CellTypes.All)).ToList();

                CellDescription nextCell = null;

                if (virginNeighbors.Count > 0)
                {
                    nextCell = virginNeighbors[random.Next(0, virginNeighbors.Count)];

                    switch (
                        nextCell.ColumnPosition - currentCell.ColumnPosition,
                        nextCell.RowPosition - currentCell.RowPosition)
                    {
                        case (1, 0): // to right from current
                            currentCell.SwitchBorders(right: true);
                            nextCell.SwitchBorders(left: true);
                            break;

                        case (-1, 0): // to left
                            currentCell.SwitchBorders(left: true);
                            nextCell.SwitchBorders(right: true);
                            break;

                        case (0, 1): // to bottom
                            currentCell.SwitchBorders(bot: true);
                            nextCell.SwitchBorders(top: true);
                            break;

                        case (0, -1): // to top
                            currentCell.SwitchBorders(top: true);
                            nextCell.SwitchBorders(bot: true);
                            break;

                        default:
                            throw new Exception("DFS generator failure: undefined direction");
                    }

                    visitedCells.Add(currentCell);
                    currentCell = nextCell;
                }
                else
                {
                    var potentialNewBranchStart = visitedCells.Where(x =>
                    maze.CellDescriptions.Where(y =>
                        (y.ColumnPosition == x.ColumnPosition & y.RowPosition == x.RowPosition - 1 & y.Type == CellTypes.All) ||
                        (y.ColumnPosition == x.ColumnPosition & y.RowPosition == x.RowPosition + 1 & y.Type == CellTypes.All) ||
                        (y.ColumnPosition == x.ColumnPosition - 1 & y.RowPosition == x.RowPosition & y.Type == CellTypes.All) ||
                        (y.ColumnPosition == x.ColumnPosition + 1& y.RowPosition == x.RowPosition & y.Type == CellTypes.All))
                            .Count() > 0).OrderBy(g => Guid.NewGuid()).ToList();
                    currentCell = potentialNewBranchStart.First();
                }
                
            }

            return maze;
        }

    }
}
