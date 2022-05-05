using Assets.Scripts.Game.Managers;
using Assets.Scripts.Game.Maze;
using Assets.Scripts.Game.MazeGenerators;
using Assets.Scripts.Game.MazeGenerators.DFS;
using Assets.Scripts.Game.MazeGenerators.Hardcode;
using Assets.Scripts.Game.MeshGenerators;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{    
    /// <summary>
    /// Inspector visible settings
    /// </summary>
    
    // modes
    public bool debugMode = false;

    // maze generators
    public bool useHardcodeMazeGenerator = false;
    public bool useDFSMazeGenerator = false;
    public bool useSergeCraftMazeGenerator = true;

    // mesh generators
    public bool useModelConstructor = true;

    // spawners
    public bool useStandardSpawner = true;

    // entities
    public bool usePortal = true;


    /// <summary>
    /// Events
    /// </summary>

    public event GameStarted GameStarted;


    /// <summary>
    /// Managers
    /// </summary>
    private ConfigManager configManager;
    private LevelManager levelManager;

    private void Awake()
    {
        try
        {
            configManager = new ConfigManager();
            configManager.LoadConfigFromPlayerPerfs();

            levelManager = new LevelManager();
        }
        catch (Exception ex)
        {
            Debug.Log($"GameController break on Awake: {ex.Message}\n{ex.InnerException}\n{ex.StackTrace}");
        }
    }


    void Start()
    {
        GameStarted?.Invoke();
    }

    public void NextLevel()
    {
        levelManager.NextLevel();
    }

}

public delegate void GameStarted();