using Assets.Scripts.Game.Maze;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Game.MazeGenerators.SergeCraft
{
    public class SergeCraftMazeGenerator : MazeGeneratorBase
    {
        public override MazeDescription GenerateMaze(int columns, int rows)
        {
            MazeDescription maze = new MazeDescription(new List<CellDescription>(), columns, rows);
            CreateDefaultMazeField(columns, rows, maze);

            System.Random random = new System.Random();
            var currentCell = maze.CellDescriptions.Find(x => x.ColumnPosition == 0 && x.RowPosition == 0);
            List<CellDescription> visitedCells = new List<CellDescription>();
            List<List<CellDescription>> mazeThreads = new List<List<CellDescription>>();
            mazeThreads.Add( new List<CellDescription>());

            while (visitedCells.Count < maze.CellDescriptions.Count - 1)
            {
                currentCell.ThreadId = mazeThreads.IndexOf(mazeThreads.Last());
                mazeThreads.Last().Add(currentCell);
                visitedCells.Add(currentCell);


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
                            throw new Exception("SergeCraft generator failure: undefined direction");
                    }

                    currentCell = nextCell;
                }
                else
                {
                    var unvisitedCells = maze.CellDescriptions.Except(visitedCells);
                    currentCell = unvisitedCells.OrderBy(x => Guid.NewGuid()).First();
                    mazeThreads.Add(new List<CellDescription>());
                }

            }

            List<string> neighboursUnited = new List<string>();

            visitedCells = new List<CellDescription>();

            for (int i = 0; i < maze.CellDescriptions.Count; i++)
            {
                var cell = maze.CellDescriptions.Except(visitedCells).OrderBy(x => Guid.NewGuid()).First();
                var neighbours = maze.CellDescriptions.Where(x =>
                    (x.ColumnPosition == cell.ColumnPosition + 1 && x.RowPosition == cell.RowPosition) |
                    (x.ColumnPosition == cell.ColumnPosition - 1 && x.RowPosition == cell.RowPosition) |
                    (x.ColumnPosition == cell.ColumnPosition && x.RowPosition == cell.RowPosition + 1) |
                    (x.ColumnPosition == cell.ColumnPosition && x.RowPosition == cell.RowPosition - 1)).ToList();

                var neighbourToUnion = neighbours.Where(x => x.ThreadId != cell.ThreadId).OrderBy(x => Guid.NewGuid()).FirstOrDefault();

                //Debug.Log($"C{cell.ColumnPosition} R{cell.RowPosition} T{cell.ThreadId}: \t" +
                //    $" {String.Join(", ", neighbours.Select(x => x.ThreadId.ToString()))} \t" +
                //    $"C{neighbourToUnion.ColumnPosition} R{neighbourToUnion.RowPosition} T{neighbourToUnion.ThreadId}");



                if (neighbourToUnion != null &&
                    !neighboursUnited.Contains(String.Join("-",
                    new List<int>() { cell.ThreadId, neighbourToUnion.ThreadId }.OrderBy(x => x).Select(x => x.ToString()))))
                {

                    switch (
                        neighbourToUnion.ColumnPosition - cell.ColumnPosition,
                        neighbourToUnion.RowPosition - cell.RowPosition)
                    {
                        case (1, 0): // to right from current
                            cell.SwitchBorders(right: true);
                            neighbourToUnion.SwitchBorders(left: true);
                            break;

                        case (-1, 0): // to left
                            cell.SwitchBorders(left: true);
                            neighbourToUnion.SwitchBorders(right: true);
                            break;

                        case (0, 1): // to bottom
                            cell.SwitchBorders(bot: true);
                            neighbourToUnion.SwitchBorders(top: true);
                            break;

                        case (0, -1): // to top
                            cell.SwitchBorders(top: true);
                            neighbourToUnion.SwitchBorders(bot: true);
                            break;

                        default:
                            throw new Exception("SergeCraft generator failure: undefined direction");
                    };

                    visitedCells.Add(cell);
                    visitedCells.Add(neighbourToUnion);
                    neighboursUnited.Add(String.Join("-",
                    new List<int>() { cell.ThreadId, neighbourToUnion.ThreadId }.OrderBy(x => x).Select(x => x.ToString())));
                }
            }

            return maze;
        }
    }
}
