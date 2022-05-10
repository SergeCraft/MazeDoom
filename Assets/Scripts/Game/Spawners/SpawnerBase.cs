using Assets.Scripts.Game.Entities;
using Assets.Scripts.Game.Levels;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SpawnerBase : MonoBehaviour
{
    public List<GameObject> mazes = new List<GameObject>();

    public abstract GameObject SpawnMaze(Mesh mazeMesh);
    public abstract List<GameObject> SpawnEntities(List<EntityBase> entities, GameObject maze);
    public abstract GameObject SpawnPlayer(Level maze);
}
