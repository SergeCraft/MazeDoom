using Assets.Scripts.Game.Entities;
using Assets.Scripts.Game.Entities.Portal;
using Assets.Scripts.Game.Levels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Game.Spawners.MultiObjectSpawner
{
    internal class MultiObjectSpawner : SpawnerBase
    {
        public GameObject meshAsset;
        public List<Material> materialsAsset;

        public MultiObjectSpawner()
        {
            meshAsset = Resources.Load<GameObject>("Prefabs/MeshAssets/CityMazeCells");
            materialsAsset = Resources.LoadAll<Material>("Materials/").ToList();
        }

        public override List<GameObject> SpawnEntities(List<EntityBase> entities, GameObject maze)
        {

            List<GameObject> entityObjects = new List<GameObject>();

            foreach (var entity in entities)
            {
                entityObjects.Add(GameObject.Instantiate<GameObject>(
                entity.GO,
                new Vector3(entity.PosX, entity.PosY, entity.PosZ),
                Quaternion.identity,
                maze.transform));
            }

            return entityObjects;
        }

        public override (GameObject, GameObject) SpawnMaze(List<Mesh> wallsMesh, List<Mesh> floorMesh, Level level)
        {
            string name = $"Maze{mazes.Count + 1}";

            GameObject mazePrefab = Resources.Load<GameObject>("Prefabs/CellComponents/MazeWalls");
            var mazeWalls = GameObject.Instantiate(mazePrefab);
            mazeWalls.name = name;
            mazeWalls.transform.SetParent(GameObject.Find("Mazes").transform);
            foreach (var cell in level.MazeDiescription.CellDescriptions)
            {
                var cellGO = GameObject.Instantiate(meshAsset.transform.GetChild((int)cell.Type - 1)).gameObject;
                cellGO.name = $"Cell_C{cell.ColumnPosition}_R{cell.RowPosition}";
                cellGO.transform.SetParent(mazeWalls.transform);
                cellGO.transform.localScale = Vector3.one;
                cellGO.transform.localRotation = Quaternion.Euler(Vector3.up);
                cellGO.transform.localPosition = new Vector3((int)cell.ColumnPosition, 0, -(int)cell.RowPosition);
                cellGO.AddComponent<MeshCollider>();
                    var matName = $"Material.Cell01.{((int)cell.Type).ToString("D2")}";
                    cellGO.GetComponent<MeshRenderer>().material = 
                        materialsAsset.FirstOrDefault(m => m.name == matName);
            }

            GameObject mazeFloorPrefab = Resources.Load<GameObject>("Prefabs/CellComponents/MazeFloor");
            var mazeFloor = GameObject.Instantiate(mazePrefab);
            mazeFloor.name = name + ".Floor";
            mazeFloor.transform.SetParent(mazeWalls.transform);
            mazeFloor.GetComponent<MeshRenderer>().material.color = UnityEngine.Random.ColorHSV();
            mazeFloor.GetComponent<MeshFilter>().sharedMesh = floorMesh[0];
            mazeFloor.GetComponent<MeshCollider>().sharedMesh = floorMesh[0];

            mazes.Add(mazeWalls);
            return (mazeWalls, mazeFloor);
        }

        public override GameObject SpawnPlayer(Level level)
        {

            var player = GameObject.Find("Player");
            var portal = level.Entities.Find(x => x is PortalEntity);
            var deadZoneSize = 5;

            var deadZone = level.MazeDiescription.CellDescriptions.Where(x =>
                (x.ColumnPosition - portal.PosX <= deadZoneSize & x.ColumnPosition - portal.PosX >= -deadZoneSize) &&
                (x.RowPosition + portal.PosZ <= deadZoneSize & x.RowPosition + portal.PosZ >= -deadZoneSize)).ToList();
            var spawnCell = level.MazeDiescription.CellDescriptions.Except(deadZone).OrderBy(x => Guid.NewGuid()).First();

            //var size = level.MazeWalls.GetComponent<MeshFilter>().sharedMesh.bounds.size;
            player.GetComponent<Rigidbody>().Sleep();

            player.transform.position = new Vector3(spawnCell.ColumnPosition, 0.6f, -spawnCell.RowPosition);

            return player;
        }
    }
}
