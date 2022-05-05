using Assets.Scripts.Game.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Game.Levels
{
    public class Level
    {
        public List<EntityBase> Entities { get; set; }

        public GameObject Maze { get; set; }

        internal void Dispose()
        {
            GameObject.Destroy(Maze);
        }
    }
}
