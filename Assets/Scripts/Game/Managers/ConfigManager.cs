using Assets.Scripts.Game.Config;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Game.Managers
{
    public class ConfigManager
    {
        public GameConfig Config { get; set; }

        public ConfigManager()
        {
        }

        public bool SaveConfigToPlayerPerfs()
        {
            try
            {
                PlayerPrefs.SetString("Config", JsonConvert.SerializeObject(Config));
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool LoadConfigFromPlayerPerfs()
        {
            try
            {
                var cfg = JsonConvert.DeserializeObject<GameConfig>(PlayerPrefs.GetString("Config"));

                if (cfg == null)
                {
                    Debug.Log("Config load failre. Using default config");
                    Config = GetDefaultConfig();
                }
                else
                {
                    Config = cfg;
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        private GameConfig GetDefaultConfig()
        {
            return new GameConfig()
            {
                PlayerMode = Player.PlayerControllerModes.BallJoystickControl
            };
        }
    }
}
