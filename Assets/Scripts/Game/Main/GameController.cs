using Assets.Scripts.Game.Config;
using Assets.Scripts.Game.Managers;
using Assets.Scripts.Game.Player;
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

    // player types
    public bool useBallDefault = true;
    public bool useBallTiltControlled = false;


    /// <summary>
    /// Events
    /// </summary>

    public event GameStarted GameStarted;
    public event PlayerModeChanged PlayerModeChanged;


    /// <summary>
    /// Managers
    /// </summary>
    public ConfigManager configManager;
    public LevelManager levelManager;
    public PlayerManager playerManager;
    public UIManager uiManager;

    private void Awake()
    {
        try
        {
            configManager = new ConfigManager();
            configManager.LoadConfigFromPlayerPerfs();

            levelManager = new LevelManager();

            uiManager = new UIManager();

            playerManager = new PlayerManager();
            playerManager.ModeChanged += uiManager.SetMode;
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

    public void Quit()
    {
        Application.Quit();
    }

}

public delegate void GameStarted();
public delegate void PlayerModeChanged(PlayerControllerModes mode);
public delegate void GameConfigChanged(GameConfig config);