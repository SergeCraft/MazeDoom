using Assets.Scripts.Game.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Game.Managers
{
    public class PlayerManager
    {
        private GameController gameController;
        private GameObject playerGO;
        private PlayerControllerModes mode;
         
        public PlayerModeChanged ModeChanged;

        public PlayerManager()
        {

            gameController = GameObject.Find("GameController").GetComponent<GameController>();
            playerGO = GameObject.Find("Player");

            mode = gameController.configManager.Config.PlayerMode;

            gameController.GameStarted += SetupPlayer;
        }

        private void SetupPlayer()
        {
            // TODO refactoring
            switch (mode)
            {
                case PlayerControllerModes.BallJoystickControl:
                    playerGO.GetComponent<BallTiltController>().enabled = false;
                    playerGO.GetComponent<BallJoystickController>().enabled = true;
                    break;

                case PlayerControllerModes.BallTiltControl:
                    playerGO.GetComponent<BallJoystickController>().enabled = false;
                    playerGO.GetComponent<BallTiltController>().enabled = true;
                    break;
            };

            // TODO refactoring
            gameController.configManager.Config.PlayerMode = mode;
            gameController.configManager.SaveConfigToPlayerPerfs();

            ModeChanged?.Invoke(mode);
        }

        public void SetPlayerMode(PlayerControllerModes newMode)
        {
            mode = newMode;
            SetupPlayer();
        }
    }
}
