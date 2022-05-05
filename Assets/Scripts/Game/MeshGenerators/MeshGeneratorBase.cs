using Assets.Scripts.Game.Maze;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Game.MeshGenerators
{
    public abstract class MeshGeneratorBase
    {
        public abstract Mesh GenerateMesh(MazeDescription description);
    }
}
