using System;
using System.Collections.Generic;
using System.Text;

namespace FrogGame
{
    public class Popup : Entity
    {

        public enum PopupType
        {
            PlusOne,
            MinusOne,
            NewFrog,
            NewBadFrog,
        }

        public PopupType type;

        public int maxLife = 100;

        public Popup(PopupType type, float x, float y) : base(Sprites.pixel, x, y, 4, 4)
        {
            switch (type)
            {
                case PopupType.PlusOne:
                    sprite = Sprites.popupPlusOne;
                    break;
                case PopupType.MinusOne:
                    sprite = Sprites.popupMinusOne;
                    break;
                case PopupType.NewFrog:
                    sprite = Sprites.popupNewFrog;
                    break;
                case PopupType.NewBadFrog:
                    sprite = Sprites.popupNewBadFrog;
                    break;
            }
        }

        public override void Update()
        {

            maxLife--;

            if (maxLife > 0)
            {
                if(maxLife % 10 == 0)
                    y--;
            }
            else
                forRemoval = true;

            base.Update();
        }

    }
}
