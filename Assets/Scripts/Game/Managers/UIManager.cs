using Assets.Scripts.Game.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Game.Managers
{
    public class UIManager
    {
        GameController gameController;

        List<GameObject> uiPlayerControlElements;
        List<GameObject> uiMainElements;



        public UIManager()
        {
            gameController = GameObject.Find("GameController").GetComponent<GameController>();

            uiMainElements = GameObject.FindGameObjectsWithTag("UIMain").ToList();
            uiPlayerControlElements = GameObject.FindGameObjectsWithTag("UIPalyerControl").ToList();

            //subscribing to UI events
            GameObject.Find("SwitchTiltModeButton").GetComponent<Button>().onClick
                .AddListener(() => gameController.playerManager.SetPlayerMode(PlayerControllerModes.BallTiltControl));
            GameObject.Find("SwitchJoystickModeButton").GetComponent<Button>().onClick
                .AddListener(() => gameController.playerManager.SetPlayerMode(PlayerControllerModes.BallJoystickControl));
            GameObject.Find("QuitButton").GetComponent<Button>().onClick
                .AddListener(() => gameController.Quit());
            GameObject.Find("SetNormalButton").GetComponent<Button>().onClick
                .AddListener(() => gameController.playerManager.BallTiltControlSetAnchors());
        }


        internal void SetMode(PlayerControllerModes mode)
        {
            string modename = mode.ToString();
            foreach (var elem in uiPlayerControlElements.Where(x => x.name != modename)) elem.SetActive(false);
            uiPlayerControlElements.Find(x => x.name == modename).SetActive(true);
        }

        public void UpdateMazesCompletedCount(int count)
        {
            uiMainElements.Where(x => x.name == "MazeCompletedCountText").First()
                .GetComponent<TMP_Text>().text = $"Mazes solved: {count}";
        }
    }
}
