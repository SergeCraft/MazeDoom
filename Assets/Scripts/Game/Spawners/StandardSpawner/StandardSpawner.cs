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

    public override GameObject SpawnMaze(Mesh mazeMesh)
    {
        string name = $"Maze{mazes.Count + 1}";

        GameObject mazePrefab = Resources.Load<GameObject>("Prefabs/Maze");
        var maze = Instantiate(mazePrefab);
        maze.name = name;
        maze.transform.SetParent(GameObject.Find("Mazes").transform);
        maze.GetComponent<MeshFilter>().mesh = mazeMesh;
        maze.GetComponent<MeshCollider>().sharedMesh = mazeMesh;
        maze.GetComponent<MeshRenderer>().material.color = UnityEngine.Random.ColorHSV();

        mazes.Add(maze);
        return maze;
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

        var size = level.Maze.GetComponent<MeshFilter>().sharedMesh.bounds.size;
        player.GetComponent<Rigidbody>().Sleep();

        player.transform.position = new Vector3(spawnCell.ColumnPosition, 0.6f, -spawnCell.RowPosition);

        return player;
    }
}
