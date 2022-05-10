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
        private GameObject playerGO;
        private GameController gameController;

        public PlayerManager()
        {

            gameController = GameObject.Find("GameController").GetComponent<GameController>();
            playerGO = GameObject.Find("Player");
        }
    }
}
