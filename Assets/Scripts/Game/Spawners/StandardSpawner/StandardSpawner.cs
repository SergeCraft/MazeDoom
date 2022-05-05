using Assets.Scripts.Game.Entities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardSpawner : SpawnerBase
{

    public override GameObject SpawnMaze(Mesh mazeMesh)
    {
        string name = $"Maze{mazes.Count + 1}";


        //GameObject maze = new GameObject(name);
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
}
