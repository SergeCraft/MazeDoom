using Assets.Scripts.Game.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Text.Json;
using System.Threading.Tasks;

namespace Assets.Scripts.Game.Config
{
    public class GameConfig
    {
        public PlayerControllerModes PlayerMode = PlayerControllerModes.BallJoystickControl;

        public int MazeCompletedCount = 0;
    }
}
