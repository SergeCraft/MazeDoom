using Assets.Scripts.Game.Entities;
using Assets.Scripts.Game.Levels;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SpawnerBase 
{
    public List<GameObject> mazes = new List<GameObject>();

    public abstract (GameObject, GameObject) SpawnMaze(Mesh wallsMesh, Mesh floorMesh);
    public abstract List<GameObject> SpawnEntities(List<EntityBase> entities, GameObject maze);
    public abstract GameObject SpawnPlayer(Level maze);
}
