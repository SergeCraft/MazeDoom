using Assets.Scripts.Game.EntityGenerators;
using Assets.Scripts.Game.EntityGenerators.SinglePortalAtCorner;
using Assets.Scripts.Game.EntityGenerators.SinglePortalAtRandom;
using Assets.Scripts.Game.Levels;
using Assets.Scripts.Game.MazeGenerators;
using Assets.Scripts.Game.MazeGenerators.DFS;
using Assets.Scripts.Game.MazeGenerators.Hardcode;
using Assets.Scripts.Game.MazeGenerators.SergeCraft;
using Assets.Scripts.Game.MeshGenerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Game.Managers
{
    public class LevelManager
    {
        private GameController gameController;


        public List<MazeGeneratorBase> MazeGenerators;
        public List<MeshGeneratorBase> MeshGenerators;
        public List<SpawnerBase> Spawners;
        public List<EntityGeneratorBase> EntityGenerators;
        public List<Level> Levels;


        public LevelManager()
        {
            this.gameController = GameObject.Find("GameController").GetComponent<GameController>();

            MazeGenerators = new List<MazeGeneratorBase>();
            if (gameController.useHardcodeMazeGenerator) MazeGenerators.Add(new HardcodeMazeGenerator());
            if (gameController.useDFSMazeGenerator) MazeGenerators.Add(new DepthFirstSearchMazeGenerator());
            if (gameController.useSergeCraftMazeGenerator) MazeGenerators.Add(new SergeCraftMazeGenerator());

            Spawners = new List<SpawnerBase>();
            if (gameController.useStandardSpawner) Spawners.Add(new StandardSpawner());

            MeshGenerators = new List<MeshGeneratorBase>();
            if (gameController.useModelConstructor) MeshGenerators.Add(new ModelConstructorGenerator());

            EntityGenerators = new List<EntityGeneratorBase>();
            //if (gameController.usePortal) EntityGenerators.Add(new SinglePortalAtCornerEntityGenerator());
            if (gameController.usePortal) EntityGenerators.Add(new SinglePortalAtRandomEntityGenerator());

            Levels = new List<Level>();

            gameController.GameStarted += OnGameStarted;
        }

        internal void NextLevel()
        {
            Levels[0].Dispose();
            Levels.RemoveAt(0);
            GenerateNewLevel();
            Spawners[UnityEngine.Random.Range(0, Spawners.Count)].SpawnPlayer(Levels[0].Maze);
            
        }

        private void OnGameStarted()
        {
            GenerateNewLevel();
            Spawners[UnityEngine.Random.Range(0, Spawners.Count)].SpawnPlayer(Levels[0].Maze);
        }

        private void GenerateNewLevel()
        {
            var rdm = new System.Random();
            var descr = MazeGenerators[rdm.Next(0, MazeGenerators.Count)].GenerateMaze(rdm.Next(10, 26), rdm.Next(10, 26));
            var mesh = MeshGenerators[rdm.Next(0, MeshGenerators.Count)].GenerateMesh(descr);
            var level = new Level();
            var spawner = Spawners[rdm.Next(0, Spawners.Count)];
            level.Maze = spawner.SpawnMaze(mesh);
            level.Entities = EntityGenerators[rdm.Next(0, EntityGenerators.Count)].GenerateEntitiesForMaze(descr);
            spawner.SpawnEntities(level.Entities, level.Maze);

            Levels.Add(level);
        }
    }
}
