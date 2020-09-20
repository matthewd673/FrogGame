using System;
using System.Collections.Generic;
using System.Text;

namespace FrogGame
{
    public class Pickup : Entity
    {

        public enum PickupType
        {
            Coin,
            HoloFrog,
        }

        public PickupType pType;

        public Pickup(PickupType pType, float x, float y) : base(Sprites.coin, x, y, 4, 4)
        {

            this.pType = pType;

            switch(pType)
            {
                case PickupType.Coin:
                    sprite = Sprites.coin;
                    break;
                case PickupType.HoloFrog:
                    sprite = Sprites.holoFrog;
                    width = 8;
                    height = 8;
                    break;
            }
        }

    }
}
