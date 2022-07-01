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
        public List<GameObject> cellsAsset = new List<GameObject>();
        public List<GameObject> floorsAsset = new List<GameObject>();
        public List<Material> materialsAsset;

        public MultiObjectSpawner()
        {
            var meshAssetGO = Resources.Load<GameObject>("Prefabs/MeshAssets/CityMazeCells");
            foreach (Transform child in meshAssetGO.transform) if (child.gameObject.name.StartsWith("Cell")) cellsAsset.Add(child.gameObject);
            foreach (Transform child in meshAssetGO.transform) if (child.gameObject.name.StartsWith("Floor")) floorsAsset.Add(child.gameObject);
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
                var cellGOPrefab = cellsAsset[(int)cell.Type - 1];
                var prefabName = cellGOPrefab.name;
                prefabName =  prefabName switch
                {
                    "Cell01.001" => "Cell01.002",
                    "Cell01.002" => "Cell01.002",
                    "Cell01.003" => "Cell01.002",
                    "Cell01.004" => "Cell01.002",
                    "Cell01.005" => "Cell01.002",
                    "Cell01.006" => "Cell01.006",
                    "Cell01.007" => "Cell01.006",
                    "Cell01.008" => "Cell01.006",
                    "Cell01.009" => "Cell01.006",
                    "Cell01.010" => "Cell01.010",
                    "Cell01.011" => "Cell01.010",
                    "Cell01.012" => "Cell01.010",
                    "Cell01.013" => "Cell01.010",
                    "Cell01.014" => "Cell01.014",
                    "Cell01.015" => "Cell01.014",
                    _ => "HelperTexture"
                };
                var cellGO = GameObject.Instantiate(cellGOPrefab).gameObject;
                cellGO.name = $"Cell_C{cell.ColumnPosition}_R{cell.RowPosition}";
                cellGO.transform.SetParent(mazeWalls.transform);
                cellGO.transform.localScale = Vector3.one;
                cellGO.transform.localRotation = Quaternion.Euler(Vector3.zero);
                cellGO.transform.localPosition = new Vector3((int)cell.ColumnPosition, 0, -(int)cell.RowPosition);
                cellGO.AddComponent<MeshCollider>();
                //cellGO.GetComponent<MeshFilter>().sharedMesh.RecalculateNormals();
                var matName = $"Material.{prefabName}";
                cellGO.GetComponent<MeshRenderer>().material = 
                    materialsAsset.FirstOrDefault(m => m.name == matName);

                var floorGOPrefab = floorsAsset.FirstOrDefault(f => f.name == cellGOPrefab.name.Replace("Cell", "Floor"));
                var floorMatName = $"Material.{floorGOPrefab.name}";
                var floorGO = GameObject.Instantiate(floorGOPrefab).gameObject;
                floorGO.transform.SetParent(cellGO.transform);
                floorGO.name = $"Floor_C{cell.ColumnPosition}_R{cell.RowPosition}";
                floorGO.transform.localPosition = Vector3.zero;
                floorGO.transform.localScale = Vector3.one;
                floorGO.transform.localRotation = Quaternion.Euler(Vector3.zero);
                floorGO.GetComponent<MeshRenderer>().material =
                    materialsAsset.FirstOrDefault(m => m.name == floorMatName);

            }

            GameObject mazeFloorPrefab = Resources.Load<GameObject>("Prefabs/CellComponents/MazeFloor");
            var mazeFloor = GameObject.Instantiate(mazePrefab);
            mazeFloor.name = name + ".Floor";
            mazeFloor.transform.SetParent(mazeWalls.transform);
            mazeFloor.GetComponent<MeshRenderer>().enabled = false;
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
