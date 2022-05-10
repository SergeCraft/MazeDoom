using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Game.Entities
{
    public abstract class EntityBase
    {
        public GameObject GO;

        public float PosX;
        public float PosY;
        public float PosZ;

        public EntityBase(float posX, float posZ, Transform parent, float posY = 0.25f)
        {
            PosX = posX;
            PosY = posY;
            PosZ = posZ;
        }

    }
}
