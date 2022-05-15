using Assets.Scripts.Game.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Game.Managers
{
    public class UIManager
    {
        GameController gameController;

        List<GameObject> uiPlayerControlElements;



        public UIManager()
        {
            gameController = GameObject.Find("GameController").GetComponent<GameController>();

            uiPlayerControlElements = GameObject.FindGameObjectsWithTag("UIPalyerControl").ToList();

            GameObject.Find("SwitchTiltModeButton").GetComponent<Button>().onClick
                .AddListener(() => gameController.playerManager.SetPlayerMode(PlayerControllerModes.BallTiltControl));
            GameObject.Find("SwitchJoystickModeButton").GetComponent<Button>().onClick
                .AddListener(() => gameController.playerManager.SetPlayerMode(PlayerControllerModes.BallJoystickControl));
            GameObject.Find("QuitButton").GetComponent<Button>().onClick
                .AddListener(() => gameController.Quit());
        }


        internal void SetMode(PlayerControllerModes mode)
        {
            string modename = mode.ToString();
            foreach (var elem in uiPlayerControlElements.Where(x => x.name != modename)) elem.SetActive(false);
            uiPlayerControlElements.Find(x => x.name == modename).SetActive(true);
        }
    }
}
