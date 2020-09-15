using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FrogGame
{
    public abstract class Entity
    {

        public float x;
        public float y;
        public int width;
        public int height;
        public Texture2D sprite;

        public Collider col;

        public bool forRemoval = false;

        public Entity(Texture2D sprite, float x, float y, int width, int height)
        {
            this.sprite = sprite;
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;

            col = new Collider(0, 0, width, height);
        }

        public virtual void Update()
        {
            
        }

        public virtual void Render(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite, Renderer.GetRenderRect(x, y, width, height), Color.White);
        }

    }
}
