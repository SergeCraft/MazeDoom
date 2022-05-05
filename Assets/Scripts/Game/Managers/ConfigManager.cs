using Assets.Scripts.Game.Config;
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

                PlayerPrefs.SetString("Config", JsonUtility.ToJson(Config));
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
                Config = JsonUtility.FromJson<GameConfig>(PlayerPrefs.GetString("Config"));
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
