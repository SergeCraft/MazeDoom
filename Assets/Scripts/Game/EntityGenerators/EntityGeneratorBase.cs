using Assets.Scripts.Game.Entities;
using Assets.Scripts.Game.Maze;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Game.EntityGenerators
{
    public abstract class EntityGeneratorBase
    {
        public abstract List<EntityBase> GenerateEntitiesForMaze(MazeDescription maze);
    }
}
