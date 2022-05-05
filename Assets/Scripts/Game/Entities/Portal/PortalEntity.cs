using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Game.Entities.Portal
{
    public class PortalEntity : EntityBase
    {
        public PortalEntity(float posX, float posZ, float posY, Transform parent) : base (posX, posZ, parent, posY)
        {
            GO = Resources.Load<GameObject>("Prefabs/Entities/Portal");
        }
    }
}
