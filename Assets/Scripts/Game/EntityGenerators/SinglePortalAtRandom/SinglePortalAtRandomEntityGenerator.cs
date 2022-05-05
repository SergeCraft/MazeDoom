using Assets.Scripts.Game.Entities;
using Assets.Scripts.Game.Entities.Portal;
using Assets.Scripts.Game.Maze;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Game.EntityGenerators.SinglePortalAtRandom
{
    public class SinglePortalAtRandomEntityGenerator : EntityGeneratorBase
    {
        public override List<EntityBase> GenerateEntitiesForMaze(MazeDescription maze)
        {

            List<EntityBase> entities = new List<EntityBase>()
            { new PortalEntity(
                (int) UnityEngine.Random.Range(0, maze.ColumnCount - 1),
                (int) UnityEngine.Random.Range(0, -maze.RowCount + 1),
                0.6f,
                GameObject.Find("Mazes").transform)};

            return entities;
        }
    }
}
