﻿using Assets.Scripts.Game.EntityGenerators;
using Assets.Scripts.Game.EntityGenerators.SinglePortalAtCorner;
using Assets.Scripts.Game.EntityGenerators.SinglePortalAtRandom;
using Assets.Scripts.Game.Levels;
using Assets.Scripts.Game.MazeGenerators;
using Assets.Scripts.Game.MazeGenerators.DFS;
using Assets.Scripts.Game.MazeGenerators.Hardcode;
using Assets.Scripts.Game.MazeGenerators.SergeCraft;
using Assets.Scripts.Game.MeshGenerators;
using Assets.Scripts.Game.MeshGenerators.ModelConstructor;
using Assets.Scripts.Game.MeshGenerators.MultiObjectModelConstructor;
using Assets.Scripts.Game.Spawners.MultiObjectSpawner;
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
            if (gameController.useMultiobjectSpawner) Spawners.Add(new MultiObjectSpawner());

            MeshGenerators = new List<MeshGeneratorBase>();
            if (gameController.useModelConstructor) MeshGenerators.Add(new ModelConstructorGenerator());
            if (gameController.useCityModelConstructor) MeshGenerators.Add(new CityModelConstructorGenerator());
            if (gameController.useMultiobjectModelConstructor) MeshGenerators.Add(new CityMultiObjectModelConstructorGenerator());

            EntityGenerators = new List<EntityGeneratorBase>();
            if (gameController.usePortal) EntityGenerators.Add(new SinglePortalAtRandomEntityGenerator());

            Levels = new List<Level>();

            gameController.GameStarted += OnGameStarted;
        }

        internal void NextLevel()
        {
            Levels[0].Dispose();
            Levels.RemoveAt(0);
            GenerateNewLevel();
            Spawners[UnityEngine.Random.Range(0, Spawners.Count)].SpawnPlayer(Levels[0]);
            
        }

        private void OnGameStarted()
        {
            GenerateNewLevel();
            Spawners[UnityEngine.Random.Range(0, Spawners.Count)].SpawnPlayer(Levels[0]);
        }

        private void GenerateNewLevel()
        {
            var rdm = new System.Random();
            var descr = MazeGenerators[rdm.Next(0, MazeGenerators.Count)].GenerateMaze(rdm.Next(10, 10), rdm.Next(10, 10));
            List<Mesh> wallsMesh = new List<Mesh>();
            List<Mesh> floorMesh = new List<Mesh>();
            (wallsMesh, floorMesh) = MeshGenerators[rdm.Next(0, MeshGenerators.Count)].GenerateMesh(descr);
            var level = new Level();
            var spawner = Spawners[rdm.Next(0, Spawners.Count)];
            level.Entities = EntityGenerators[rdm.Next(0, EntityGenerators.Count)].GenerateEntitiesForMaze(descr);
            level.MazeDiescription = descr;
            (level.MazeWalls, level.MazeFloor) = spawner.SpawnMaze(wallsMesh, floorMesh, level);
            spawner.SpawnEntities(level.Entities, level.MazeWalls);

            Levels.Add(level);
        }
    }
}
