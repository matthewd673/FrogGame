using System;
using System.Collections.Generic;
using System.Text;

namespace FrogGame
{
    public class Camera
    {

        public float x;
        public float y;
        public int width;
        public int height;
        public int scale;

        float shakeStrength = 0;
        float shakeDuration = 0;
        float shakeDurCt = 0;

        public Camera(float x, float y, int width, int height, int scale)
        {
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
            this.scale = scale;
        }

        public void SetShake(float strength, float duration)
        {
            shakeStrength = strength;
            shakeDuration = duration * 60;
            shakeDurCt = 0;
            x = 0;
            y = 0;
        }

        void Shake()
        {
            x = shakeStrength * GameMath.RandomFloat();
            y = shakeStrength * GameMath.RandomFloat();
        }

        public void Update()
        {
            //handle camera shake
            if (shakeDurCt < shakeDuration)
            {
                shakeDurCt++;
                Shake();
            }
            if (shakeDurCt >= shakeDuration)
            {
                SetShake(0, 0);
            }
        }

    }
}
