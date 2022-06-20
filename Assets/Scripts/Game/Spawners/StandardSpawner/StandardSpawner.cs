using Assets.Scripts.Game.Entities;
using Assets.Scripts.Game.Entities.Portal;
using Assets.Scripts.Game.Levels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StandardSpawner : SpawnerBase
{

    public override (GameObject, GameObject) SpawnMaze(List<Mesh> wallsMesh, List<Mesh> floorMesh, Level level)
    {
        string name = $"Maze{mazes.Count + 1}";

        GameObject mazePrefab = Resources.Load<GameObject>("Prefabs/CellComponents/MazeWalls");
        var mazeWalls = GameObject.Instantiate(mazePrefab);
        mazeWalls.name = name;
        mazeWalls.transform.SetParent(GameObject.Find("Mazes").transform);
        mazeWalls.GetComponent<MeshFilter>().mesh = wallsMesh[0];
        mazeWalls.GetComponent<MeshCollider>().sharedMesh = wallsMesh[0];
        mazeWalls.GetComponent<MeshRenderer>().material.color = UnityEngine.Random.ColorHSV();

        GameObject mazeFloorPrefab = Resources.Load<GameObject>("Prefabs/CellComponents/MazeFloor");
        var mazeFloor = GameObject.Instantiate(mazePrefab);
        mazeFloor.name = name + ".Floor";
        mazeFloor.transform.SetParent(mazeWalls.transform);
        mazeFloor.GetComponent<MeshRenderer>().material.color = UnityEngine.Random.ColorHSV();
        mazeFloor.GetComponent<MeshFilter>().sharedMesh = floorMesh[0];
        mazeFloor.GetComponent<MeshCollider>().sharedMesh = floorMesh[0];
        //mazeFloor.transform.localScale = new Vector3(wallsMesh.bounds.size.x, 1.0f, wallsMesh.bounds.size.z);
        //mazeFloor.transform.position = new Vector3(wallsMesh.bounds.center.x, -1.05f, wallsMesh.bounds.center.z);

        mazes.Add(mazeWalls);
        return (mazeWalls, mazeFloor);
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

    public override GameObject SpawnPlayer(Level level)
    {
        var player = GameObject.Find("Player");
        var portal = level.Entities.Find(x => x is PortalEntity);
        var deadZoneSize = 5;

        var deadZone = level.MazeDiescription.CellDescriptions.Where(x => 
            (x.ColumnPosition - portal.PosX <= deadZoneSize & x.ColumnPosition - portal.PosX >= -deadZoneSize) &&
            (x.RowPosition + portal.PosZ <= deadZoneSize & x.RowPosition + portal.PosZ >= -deadZoneSize)).ToList();
        var spawnCell = level.MazeDiescription.CellDescriptions.Except(deadZone).OrderBy(x => Guid.NewGuid()).First();

        var size = level.MazeWalls.GetComponent<MeshFilter>().sharedMesh.bounds.size;
        player.GetComponent<Rigidbody>().Sleep();

        player.transform.position = new Vector3(spawnCell.ColumnPosition, 0.6f, -spawnCell.RowPosition);

        return player;
    }
}
